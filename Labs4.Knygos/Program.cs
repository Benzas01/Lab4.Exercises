using System.Collections.Generic;
using System.IO;
namespace Labs4.Knygos
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string Input = "Knyga.txt";
            const string OutputEasy = "Rodikliai.txt";
            const string OutputHard = "ManoKnyga.txt";
            const string Punctuation = " .,!?:;()\t'";
            const string Alphabet = "AaĄąBbCcČčDdEeĘęĖėFfGgHhIiĮįYyJjKkLlMmNnOoPpRrSsŠšTtUuŲųŪūVvZzŽž";
            const string Numbers = "1234567890";
            File.Delete(OutputHard);
            File.Delete(OutputEasy);
            List<int> sWordStartLoc = new List<int>(80);
            TaskUtils.EmptyList(ref sWordStartLoc);
            sWordStartLoc[0] = 1;
            Inout.ProcessEasy(Input, OutputEasy, Alphabet, Punctuation, Numbers, ref sWordStartLoc);
            Inout.ProcessHard(Input, OutputHard, Alphabet, Numbers, Punctuation, sWordStartLoc);
        }
    }
}
