using System.Collections.Generic;

namespace Labs._4._5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string CFd = "Duomenys.txt";
            const string CFr = "Rezultatai.txt";
            string punctuation = " .,!?:;()\t'";
            List<string> names = new List<string> { "C++", "Free", "Pascal" };
            TaskUtils.Process(CFd, CFr, punctuation, names);
        }
    }
}
