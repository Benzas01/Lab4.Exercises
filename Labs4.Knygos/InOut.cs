using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

public static class Inout
{
    public static string ReadLine()
    {
        Console.WriteLine("Input a line: ");
        return Console.ReadLine();
    }
    public static void ProcessEasy(string Input, string OutputEasy, string Alphabet, string punctuation, string numbers, ref List<int> secwordstar)
    {
        using (StreamReader Reader = new StreamReader(Input))
        {
            List<char> Puncmarks = new List<char>();
            int numsum = 0, numamount = 0;
            int linenumber = 0;
            string line;
            int curbestchainl = 0, curchainl = 0;
            bool ischain = false;
            string curchain = string.Empty, bestchain = string.Empty;
            List<int> Linenumbers = new List<int>();
            while ((line = Reader.ReadLine()) != null)
            {
                linenumber++;
                TaskUtils.FindLongestFragment(line, linenumber, ref curbestchainl, Linenumbers, Alphabet, punctuation, ref ischain, ref curchainl, ref bestchain,ref Puncmarks);
                TaskUtils.NumberWordsinLine(line, ref numsum, numbers, punctuation, ref numamount);
                List<int> linewordspacings = TaskUtils.Linespacings(line, punctuation);
                for (int i = 1; i < linewordspacings.Count; i++)
                {
                    if (secwordstar[i] < linewordspacings[i])
                    {
                        secwordstar[i] = linewordspacings[i];
                    }
                }
            }
            if (curchainl > curbestchainl)
            {
                curbestchainl = curchainl;
            }
            using (StreamWriter Writer = new StreamWriter(OutputEasy))
            {
                Writer.WriteLine("Ilgiausias teksto fragmentas ir jo ilgis: ");
                Writer.WriteLine(bestchain);
                Writer.WriteLine(curbestchainl);
                Writer.WriteLine("\n" + "Eiluciu, pro kurias tesiasi ilgiausias fragmentas, skaiciai: ");
                for(int i = 0;i < Linenumbers.Count;i++)
                {
                    Writer.WriteLine(Linenumbers[i]);
                }
                Writer.WriteLine("\n" + "Visi skyrikliai, kurie yra fragmente: ");
                foreach(char a in Puncmarks)
                {
                    Writer.WriteLine("'" + a + "'");
                }
                Writer.WriteLine("\n" + "Zodziu sudarytu is numeriu skaicius ir suma: ");
                Writer.WriteLine(numsum);
                Writer.WriteLine(numamount);
                Writer.Close();
            }
        }

    }
    public static void ProcessHard(string Input, string OutputHard, string Alphabet, string numbers, string punctuation, List<int> secwordstar)
    {
        using (StreamReader Reader = new StreamReader(Input))
        using (StreamWriter Writer = new StreamWriter(OutputHard))
        {
            string line;
            StringBuilder newLine = new StringBuilder();
            while ((line = Reader.ReadLine()) != null)
            {
                TaskUtils.RemoveSamePunctuation(line, punctuation, out newLine);
                string line2 = newLine.ToString();
                string Printline = TaskUtils.SpacingLine(line2, punctuation, secwordstar);
                Writer.WriteLine(Printline);
            }

        }
    }
}