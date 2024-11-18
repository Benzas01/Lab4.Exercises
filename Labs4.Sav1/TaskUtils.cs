using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
public static class TaskUtils
{
    public static void FindLongestFragment(string line, int linenumber, ref int curbestchainl, List<int> linenumbers, string alphabet, string punctuation, ref bool ischain, ref int curchainl, ref string Chainfrag)
    {
        int cursor = 1;
        string NewLine = " " + line + " ";
        char[] Alphabet = alphabet.ToCharArray();
        char[] Punctuation = punctuation.ToCharArray();
        if (ischain == false)
        {
            curchainl = 0;
        }
        while (cursor < NewLine.Length)
        {
            int firstlet = line.IndexOfAny(Punctuation, cursor);
            if (firstlet != -1)
            {
                int puncam = 1;
                firstlet--;
                int secondlet = firstlet + 1;
                while (secondlet < line.Length && punctuation.Contains(NewLine[secondlet]) == true)
                {
                    secondlet++;
                    puncam++;
                }
                if (char.ToLower(NewLine[firstlet]) == char.ToLower(NewLine[secondlet]))
                {
                    ischain = true;
                    curchainl++;
                    Chainfrag += NewLine.Substring(cursor, firstlet - cursor);
                }
                else
                {
                    ischain = false;
                    if (curchainl > curbestchainl)
                    {
                        curbestchainl = curchainl;
                        linenumbers.Clear();
                        linenumbers.TrimExcess();
                        linenumbers.Add(linenumber);
                    }
                    curchainl = 0;
                }
                cursor = firstlet;
            }
        }
    }
    public static bool RemoveSamePunctuation(string line, string punctuation, out StringBuilder newline)
    {
        char[] allpunc = punctuation.ToCharArray();
        int cursor = 0;
        int writepoint = 0;
        bool ischanged = false;
        newline = new StringBuilder();
        while (cursor < line.Length)
        {
            int firstpunc = line.IndexOfAny(allpunc, cursor);
            if (firstpunc != -1)
            {
                char ch = line[firstpunc];
                if (line[firstpunc] == line[firstpunc + 1] && punctuation.Contains(line[firstpunc]) == true && firstpunc != line.Length - 1)
                {
                    ischanged = true;
                    newline.Append(line.Substring(writepoint, (firstpunc - writepoint) + 1));
                    while (firstpunc < line.Length && line[firstpunc] == ch)
                    {
                        firstpunc++;
                    }
                    cursor = firstpunc;
                    writepoint = firstpunc;
                }
                else if (firstpunc != line.Length - 1)
                {
                    cursor = firstpunc + 1;
                }
                else
                {
                    break;
                }
            }
            else
            {
                newline.Append(line.Substring(writepoint));
                return ischanged;
            }
        }
        return ischanged;
    }
    public static bool NumberWordsinLine(string line, out int numsum, string numbers, string punctuation)
    {
        line = " " + line + "  ";
        char[] num = numbers.ToCharArray();
        int cursor = 0;
        numsum = 0;
        bool contains = false;
        while (cursor < line.Length)
        {
            int firstnumloc = line.IndexOfAny(num, cursor);
            if (firstnumloc == -1)
            {
                return contains;
            }
            else if (punctuation.Contains(line[firstnumloc - 1]) == false)
            {
                cursor = firstnumloc + 1;
            }
            else
            {
                int lastnumloc = firstnumloc;
                while (numbers.Contains(line[lastnumloc]) == true)
                {
                    lastnumloc++;
                }
                if (punctuation.Contains(line[lastnumloc + 1]) == true)
                {
                    contains = true;
                    string numword = line.Substring(firstnumloc, lastnumloc - firstnumloc);
                    numsum += TaskUtils.NumberWordSum(numword);
                    cursor = lastnumloc + 1;
                }
            }
        }
        return contains;
    }
    private static int NumberWordSum(string numword)
    {
        int sum = 0;
        for (int i = 0; i < numword.Length; i++)
        {
            sum += Convert.ToInt32(char.GetNumericValue(numword[i]));
        }
        return sum;
    }
}
