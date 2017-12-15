using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using redesigned_chatbot;

namespace REPL
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                var input = Console.ReadLine();
                var output = string.Join(", ", Parser.ParseKorean(input));
                Console.WriteLine($"({output})");
            }
        }
    }
}
