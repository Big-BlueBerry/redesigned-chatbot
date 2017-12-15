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
        public List<String> _sentences;
        
        public CBR()
        {
            _corpuses = new List<Corpus>();
            _sentences = new List<string>();
        }

        public void AddConversation(string a, string b)
            => AddConversation(a, b, Parser.ParseKorean);

        public void AddConversation(string a, string b, Func<string, string[]> parser)
        {
            _sentences.Add(a);
            _sentences.Add(b);

            var a_corpuses = parser(a);
            var b_corpuses = parser(b);

            foreach(string word in a_corpuses)
            {
                int index = _sentences.IndexOf(a);
                float weight = 1f / a_corpuses.Length;

                if (_corpuses.Any(c => c.Word == word))
                    _corpuses.Where(c => c.Word == word).First().Update(index, weight);
                else
                    _corpuses.Add(new Corpus(word, index, weight));
            }
        }
    }
}
