using System.Collections.Generic;
using System.IO;
namespace Labs4.Knygos
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Skaitmenu suma, o ne individualiu skaiciu
            //Neveikia jei kartojasi, yra su kableliu ar minusinis ir 0
            //Fragmentam dave faila, pakeisk 123etc. i raides
            //Pasilieku jo duom. faila. Jis netaip lygiuojamas

            //Starting variables
            const string Input = "Knygos2.txt";
            const string OutputEasy = "Rodikliai.txt";
            const string OutputHard = "ManoKnyga.txt";
            const string Punctuation = " .,!?:;()\t'><";
            const string Alphabet = "AaĄąBbCcČčDdEeĘęĖėFfGgHhIiĮįYyJjKkLlMmNnOoPpRrSsŠšTtUuŲųŪūVvZzŽž";
            const string Numbers = "1234567890";
            //Deletion of output files, as we use append
            File.Delete(OutputHard);
            File.Delete(OutputEasy);
            //Creates and allocates space for empty list
            List<int> sWordStartLoc = new List<int>(80);
            List<int> SpaceIndexes = new List<int>(80);
            TaskUtils.EmptyList(ref sWordStartLoc);
            TaskUtils.EmptyList(ref SpaceIndexes);
            sWordStartLoc[0] = 1;
            //Input and output for both versions of the task at hand
            Inout.ProcessEasy(Input, OutputEasy, Alphabet, Punctuation, Numbers, ref sWordStartLoc,ref SpaceIndexes);
            sWordStartLoc[0] = 1;
            SpaceIndexes[0] = 1;
            Inout.ProcessHard(Input, OutputHard, Alphabet, Numbers, Punctuation, sWordStartLoc,SpaceIndexes);
        }
    }
}
