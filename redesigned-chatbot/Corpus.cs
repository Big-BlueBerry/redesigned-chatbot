using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace redesigned_chatbot
{
    public class Corpus
    {
        public Dictionary<int, float> _sentenceIdx;
        public readonly String Word;

        public Corpus(String word)
        {
            Word = word;
            _sentenceIdx = new Dictionary<int, float>();
        }

        public Corpus(String word, int index, float weight) : this(word)
        {
            Update(index, weight);
        }

        public void Update(int index, float weight)
        {
            _sentenceIdx.Add(index, weight);
        }
    }
}
