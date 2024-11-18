using System;

namespace Labs4._4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string CFd = "Duomenys.txt";
            string punctuation = "\\s,.;:!?()\\-";
            Console.WriteLine("Sutampančių žodžių: {0, 3:d}", TaskUtils.Process(CFd,
            punctuation));
        }
    }
}
