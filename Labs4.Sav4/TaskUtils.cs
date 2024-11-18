using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
public class TaskUtils
{
    //------------------------------------------------------------
    /** Adds surname to the given name.
    @param line – string of data
    @param fout – name of result file
    @param punctuation – punctuation marks to separate words
    @param name – given word to find
    @param surname – given word to add */
    public static void Process(string fin, string fout, string punctuation, List<string> names)
    {
        string[] lines = File.ReadAllLines(fin, Encoding.UTF8);
        using (var writer = File.CreateText(fout))
        {
            foreach (string line in lines)
            {
                StringBuilder resultLine = new StringBuilder(line);
                foreach (string name in names)
                {
                    StringBuilder tempResult = new StringBuilder();
                    RemoveWord(resultLine.ToString(), punctuation, name, tempResult);
                    resultLine = tempResult;
                }
                writer.WriteLine(resultLine);
            }
        }
    }
    //------------------------------------------------------------
    //Salina zodi, kuris yra kito zodzio dalis
    private static void RemoveWord(string line, string punctuation, string delw, StringBuilder newLine)
    {
        int cursor = 0;

        while (cursor < line.Length)
        {
            int ind = line.IndexOf(delw, cursor);

            if (ind != -1)
            {
                if (punctuation.Contains(line[ind - 1]) == true && ind != -1 && punctuation.Contains(line[ind + delw.Length]) == true)
                {
                    newLine.Append(line.Substring(cursor, ind - cursor));
                    int puncdelete = ind + delw.Length;
                    while (puncdelete < line.Length && punctuation.Contains(line[puncdelete]))
                    {
                        puncdelete++;
                    }
                    cursor = puncdelete;
                }
                else
                {
                    newLine.Append(line.Substring(cursor, (ind + delw.Length) - cursor));
                    cursor = ind + delw.Length;

                }

            }
            else
            {
                newLine.Append(line.Substring(cursor));
                break;
            }
        }
    }
}
