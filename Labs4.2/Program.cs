using System;

namespace Labs4._2
{
    internal class Program
    {
        //------------------------------------------------------------
        static void Main(string[] args)
        {
            const string CFd = "Duomenys.txt";
            const string CFr = "Rezultatai.txt";
            int No = InOut.LongestLine(CFd);
            int maxlength = InOut.LongestLineLength(CFr);
            InOut.RemoveLine(CFd, CFr, maxlength);
            Console.WriteLine("Ilgiausios eilutės nr. {0, 4:d}", No + 1);
        }
        //------------------------------------------------------------
    }
}
