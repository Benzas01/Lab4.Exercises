﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
public static class TaskUtils
{
    public static void FindLongestFragment(string line, int linenumber, ref int curbestchainl, List<int> linenumbers, string alphabet, string punctuation, ref bool ischain, ref int curchainl, ref string Chainfrag,ref List<char> puncmarks)
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
            int firstlet = NewLine.IndexOfAny(Punctuation, cursor);
            if (firstlet != -1)
            {
                int puncam = 1;
                firstlet--;
                int secondlet = firstlet + 1;
                if (secondlet < line.Length && puncmarks.Contains(line[secondlet-1]) == false && punctuation.Contains(line[secondlet-1]) == true)
                {
                    puncmarks.Add(line[secondlet-1]);
                }
                while (secondlet < NewLine.Length && punctuation.Contains(NewLine[secondlet]) == true)
                {
                    if (secondlet < line.Length && puncmarks.Contains(line[secondlet]) == false && punctuation.Contains(line[secondlet]) == true)
                    {
                        puncmarks.Add(line[secondlet]);
                    }
                    secondlet++;
                    puncam++;
                   
                }
                if (secondlet < NewLine.Length && char.ToLower(NewLine[firstlet]) == char.ToLower(NewLine[secondlet]))
                {
                    if (Chainfrag.Length == 0)
                    {
                        if (ischain == true)
                        {
                            curchainl++;
                            Chainfrag += NewLine.Substring(cursor, firstlet - cursor);
                            Chainfrag += " ";
                            if(linenumbers.Contains(linenumber) == false)
                            {
                                linenumbers.Add(linenumber);
                            }
                        }
                        else
                        {
                            ischain = true;
                            curchainl++;
                            Chainfrag += NewLine.Substring(cursor, firstlet - cursor);
                            Chainfrag += " ";
                            if (linenumbers.Contains(linenumber) == false)
                            {
                                linenumbers.Add(linenumber);
                            }
                        }
                    }
                    else
                    {
                        if (ischain == true)
                        {
                            curchainl += 2;
                            Chainfrag += NewLine.Substring(cursor, (NewLine.IndexOfAny(Punctuation, secondlet) - cursor));
                            Chainfrag += " ";
                            if (linenumbers.Contains(linenumber) == false)
                            {
                                linenumbers.Add(linenumber);
                            }
                        }
                        else
                        {
                            ischain = true;
                            curchainl += 2;
                            Chainfrag += NewLine.Substring(cursor, (NewLine.IndexOfAny(Punctuation, secondlet)) - cursor);
                            Chainfrag += " ";
                            if (linenumbers.Contains(linenumber) == false)
                            {
                                linenumbers.Add(linenumber);
                            }
                        }
                    }


                }
                else if (secondlet < NewLine.Length && char.ToLower(NewLine[firstlet]) != char.ToLower(NewLine[secondlet]))
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
                else if (secondlet == NewLine.Length - 1)
                {
                    cursor = secondlet - 1;
                }
                cursor = secondlet;
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
                if (firstpunc != line.Length - 1 && line[firstpunc] == line[firstpunc + 1] && punctuation.Contains(line[firstpunc]) == true)
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
    public static bool NumberWordsinLine(string line, ref int numsum, string numbers, string punctuation, ref int numamount)
    {
        line = " " + line + "  ";
        char[] num = numbers.ToCharArray();
        int cursor = 0;
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
                    numamount++;
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
    public static List<int> Linespacings(string line, string punctuation)
    {
        List<int> LineSpacing = new List<int>();
        EmptyList(ref LineSpacing);
        StringBuilder stringBuilder = new StringBuilder();
        RemoveSamePunctuation(line, punctuation, out stringBuilder);
        string newLine = stringBuilder.ToString();
        string[] indword = newLine.Split(' ');
        LineSpacing[0] = 1;
        for (int i = 1; i < indword.Length; i++)
        {

            LineSpacing[i] = newLine.IndexOf(indword[i]);
        }
        return LineSpacing;
    }
    public static void EmptyList(ref List<int> list)
    {
        for (int i = 0; i < 80; i++)
        {
            list.Insert(i, -1);
        }
    }
    public static string SpacingLine(string line, string punctuation, List<int> secwordstart)
    {
        StringBuilder newLine = new StringBuilder();
        string[] Allwords = line.Split(' ');
        for (int i = 0; i < Allwords.Length; i++)
        {
            if (i == 0)
            {
                newLine.Append(" " + Allwords[i] + " ");
            }
            else if (line.IndexOf(Allwords[i]) < secwordstart[i])
            {
                int index = line.IndexOf(Allwords[i]);
                int spacesToAdd = secwordstart[i] - index;
                newLine.Append(' ', spacesToAdd); 
                newLine.Append(Allwords[i]);
            }
            else if (i != 0)
            {
                newLine.Append(Allwords[i] + " ");
            }
        }
        return newLine.ToString();

    }
}