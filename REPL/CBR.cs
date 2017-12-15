using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using redesigned_chatbot;

namespace REPL
{
    public class CBR
    {
        public List<Corpus> _corpuses;
        public List<Sentence> _sentences;


        public Func<string, string[]> ParserDelegate;

        public CBR() : this(Parser.ParseKorean)
        {

        }
        
        public CBR(Func<string, string[]> parser)
        {
            _corpuses = new List<Corpus>();
            _sentences = new List<Sentence>();
            ParserDelegate = parser;
        }

        public void AddConversation(string a, string b)
        {
            var a_corpuses = ParserDelegate(a);
            _sentences.Add(new Sentence(b));

            foreach(string word in a_corpuses)
            {
                int index = _sentences.Count - 1;

                float weight = 1f / a_corpuses.Length;

                if (_corpuses.Any(c => c.Word == word))
                    _corpuses.Where(c => c.Word == word).First().Update(index, _sentences[index]);
                else
                    _corpuses.Add(new Corpus(word, index, _sentences[index]));
            }
        }

        public string GetAnswer(string sentence)
        {
            var parsed = ParserDelegate(sentence);
            
            int maxIndex = -1;
            float maxWeight = -1;
            for(int i = 0; i < _sentences.Count; i++)
            {
                var sum = parsed
                    .Select(s => _corpuses.Where(c => c.Word == s).First())
                    .Where(c => c._sentenceIdx.ContainsKey(i))
                    .Select(c => 1 / (c._sentenceIdx[i].Count * c.SumOfWeights))
                    .Sum();

                if (sum > maxWeight)
                {
                    maxIndex = i;
                    maxWeight = sum;
                }
            }

            return _sentences[maxIndex].Value;
        }
    }
}
