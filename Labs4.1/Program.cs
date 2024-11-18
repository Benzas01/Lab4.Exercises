using System;

namespace Labs4._1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string CFd = "Duomenys.txt";
            const string CFr = "Rezultatai.txt";
            LettersFrequency letters = new LettersFrequency();
            InOut.Repetitions(CFd, letters);
            letters.buildic();
            Char mostcommon = letters.mostcommonletter();
            Console.WriteLine("Dazniausia raide yra: {0}", mostcommon);
            InOut.PrintRepetitions(CFr, letters);

        }
    }
}
