using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace redesigned_chatbot
{
    public class Sentence
    {
        public readonly string Value;
        public int Count;

        public Sentence(string value)
        {
            Value = value;
        }
    }
}
