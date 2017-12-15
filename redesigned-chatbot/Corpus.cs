using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace redesigned_chatbot
{
    public class Corpus
    {
        public Dictionary<int, Sentence> _sentenceIdx;
        public readonly String Word;

        public float SumOfWeights => _sentenceIdx.Values.Select(v => 1f / v.Count).Sum();

        public Corpus(String word)
        {
            Word = word;
            _sentenceIdx = new Dictionary<int, Sentence>();
        }

        public Corpus(String word, int index, Sentence sentence) : this(word)
        {
            Update(index, sentence);
        }

        public void Update(int index, Sentence sentence)
        {
            _sentenceIdx.Add(index, sentence);
            sentence.Count++;
        }
    }
}
