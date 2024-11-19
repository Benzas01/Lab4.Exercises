using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

public static class Inout
{
    /// <summary>
    /// Reading and outputting all required things for the easy version of the task
    /// </summary>
    /// <param name="Input">Input file name</param>
    /// <param name="OutputEasy">Output file name</param>
    /// <param name="Alphabet">The whole alphabet</param>
    /// <param name="punctuation">All punctuation</param>
    /// <param name="numbers">All numbers</param>
    /// <param name="secwordstar">List for storing starting locations for other words</param>
    public static void ProcessEasy(string Input, string OutputEasy, string Alphabet, string punctuation, string numbers, ref List<int> secwordstar)
    {
        //Reading function
        using (StreamReader Reader = new StreamReader(Input))
        {
            //Initial variables
            List<char> Puncmarks = new List<char>();
            int numsum = 0, numamount = 0;
            int linenumber = 0;
            string line, chain = string.Empty;
            int curbestchainl = 0, curchainl = 0;
            bool ischain = false;
            string curchain = string.Empty, bestchain = string.Empty;
            List<int> Linenumbers = new List<int>();
            //Reading of the file
            while ((line = Reader.ReadLine()) != null)
            {
                //Adds to linenumber
                linenumber++;
                //Finds the longest text fragment
                TaskUtils.FindLongestFragment(line, linenumber, ref curbestchainl, Linenumbers, Alphabet, punctuation, ref ischain, ref curchainl, ref chain,ref Puncmarks,ref bestchain);
                //Finds and adds all the number words
                TaskUtils.NumberWordsinLine(line, ref numsum, numbers, punctuation, ref numamount);
                //Gets the spacings of this line
                List<int> linewordspacings = TaskUtils.Linespacings(line, punctuation);
                //The loop goes through and checks if they're bigger
                for (int i = 1; i < linewordspacings.Count; i++)
                {
                    if (secwordstar[i] < linewordspacings[i])
                    {
                            secwordstar[i] = linewordspacings[i];
                    }
                }
            }
            //Checking for chain length after exit
            if (curchainl > curbestchainl)
            {
                curbestchainl = curchainl;
                bestchain = chain;
            }
            int Puncamount = TaskUtils.PuncAms(bestchain, punctuation);
            //Writing to Rodikliai.txt
            using (StreamWriter Writer = new StreamWriter(OutputEasy))
            {
                //Chain fragments
                Writer.WriteLine("Ilgiausias teksto fragmentas ir jo ilgis: ");
                Writer.WriteLine(bestchain);
                Writer.WriteLine(curbestchainl);
                //Lines for longest fragment
                Writer.WriteLine("\n" + "Skyrikliu fragmente skaicius: ");
                Writer.WriteLine(Puncamount);
                //All punctuation that exists
                Writer.WriteLine("\n" + "Eilutes numeriai: ");
                foreach(int a in Linenumbers)
                {
                    Writer.Write(a + " ");
                }
                //Number word info
                Writer.WriteLine("\n" + "\n" + "Zodziu sudarytu is numeriu skaicius ir suma: ");
                Writer.WriteLine(numsum);
                Writer.WriteLine(numamount);
                Writer.Close();
            }
        }

    }
    /// <summary>
    /// Reading and outputting required things for harder version of task
    /// </summary>
    /// <param name="Input">Input file name</param>
    /// <param name="OutputHard">Output file name</param>
    /// <param name="Alphabet">All alphabet characters</param>
    /// <param name="numbers">All numbers</param>
    /// <param name="punctuation">All punctuation</param>
    /// <param name="secwordstar">List for storing positions of words</param>
    public static void ProcessHard(string Input, string OutputHard, string Alphabet, string numbers, string punctuation, List<int> secwordstar)
    {
        //Readign and writing at same time
        using (StreamReader Reader = new StreamReader(Input))
        using (StreamWriter Writer = new StreamWriter(OutputHard))
        {
            string line, line2;
            StringBuilder newLine = new StringBuilder();
            while ((line = Reader.ReadLine()) != null)
            {
                //We remove identical punctuation
                if (TaskUtils.RemoveSamePunctuation(line, punctuation, out newLine) == false)
                {
                    line2 = line;
                }
                else
                {
                    line2 = newLine.ToString();
                }
                //Add the required spacing
                string Printline = TaskUtils.SpacingLine(line2, punctuation, secwordstar);
                //And print it
                Writer.WriteLine(Printline);
            }

        }
    }
}