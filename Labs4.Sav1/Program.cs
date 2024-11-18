using System;
using System.Text;

namespace Labs4.Sav1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            const string CFd = "Duomenys.txt";
            const string CFr = "Rezultatai.txt";
            LettersFrequency letters = new LettersFrequency();
            letters.buildic();
            InOut.Repetitions(CFd, letters);
            Char mostcommon = letters.mostcommonletter();
            Console.WriteLine("Dazniausia raide yra: {0}", mostcommon);
            InOut.PrintRepetitions(CFr, letters);

        }
    }
}
