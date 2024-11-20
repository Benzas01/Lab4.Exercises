using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
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
    public static void ProcessEasy(string Input, string OutputEasy, string Alphabet, string punctuation, string numbers, ref List<int> secwordstar,ref List<int> SpaceIndexes)
    {
        using (StreamReader Reader = new StreamReader(Input))
        {
            List<char> Puncmarks = new List<char>();
            char lastletter = ' ';
            int numsum = 0, numamount = 0;
            int linenumber = 0;
            string line, chain = string.Empty;
            int curbestchainl = 0, curchainl = 0;
            bool ischain = false;
            string curchain = string.Empty, bestchain = string.Empty;
            List<int> currentChainLineNumbers = new List<int>();
            List<int> bestChainLineNumbers = new List<int>();
            List<string> RefWords = new List<string>();

            while ((line = Reader.ReadLine()) != null)
            {
                linenumber++;

                if (string.IsNullOrWhiteSpace(line))
                {
                    continue; // Skip processing for empty lines
                }

                TaskUtils.FindLongestFragment(line,linenumber,ref curbestchainl,bestChainLineNumbers,Alphabet,punctuation,ref ischain,ref curchainl,ref chain,ref Puncmarks,ref bestchain,ref lastletter,ref currentChainLineNumbers);

                TaskUtils.NumberWordsinLine(line, ref numsum, numbers, punctuation, ref numamount);

                List<int> linewordspacings = TaskUtils.Linespacings(line, punctuation, ref RefWords);

                RefWords.Clear();
            }

            if (curchainl > curbestchainl)
            {
                curbestchainl = curchainl;
                bestchain = chain;
            }

            int Puncamount = TaskUtils.PuncAms(bestchain, punctuation);

            using (StreamWriter Writer = new StreamWriter(OutputEasy))
            {
                Writer.WriteLine("Ilgiausias teksto fragmentas ir jo ilgis: ");
                Writer.WriteLine(bestchain);
                Writer.WriteLine(curbestchainl);

                Writer.WriteLine("\nSkyrikliu fragmente skaicius: ");
                Writer.WriteLine(Puncamount);

                Writer.WriteLine("\nEilutes numeriai: ");
                foreach (int a in bestChainLineNumbers)
                {
                    Writer.Write(a + " ");
                }

                Writer.WriteLine("\n\nZodziu sudarytu is numeriu skaicius ir suma: ");
                Writer.WriteLine(numsum);
                Writer.WriteLine(numamount);
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
    public static void ProcessHard(string Input, string OutputHard, string Alphabet, string numbers, string punctuation, List<int> secwordstar,List<int> SpaceIndexes)
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
                string Printline = TaskUtils.SpacingLine(line2, punctuation, secwordstar,SpaceIndexes);
                //And print it
                Writer.WriteLine(Printline);
            }

        }
    }
}