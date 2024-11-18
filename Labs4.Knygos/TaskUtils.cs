using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.Expando;
using System.Text;
public static class TaskUtils
{
    /// <summary>
    /// Finds the longest fragment where the last letter of a word and the first letter of the next word are the sane
    /// </summary>
    /// <param name="line">Input line</param>
    /// <param name="linenumber">Input line number</param>
    /// <param name="curbestchainl">Currently the best chain</param>
    /// <param name="linenumbers">Line numbers for the best chain</param>
    /// <param name="alphabet">Whole alphabet</param>
    /// <param name="punctuation">All punctuation</param>
    /// <param name="ischain">bool for if the chain rolls over to next line</param>
    /// <param name="curchainl">Current chain length</param>
    /// <param name="Chainfrag">Current chain fragment</param>
    /// <param name="puncmarks">All punctuation that exists in the current longest chain</param>
    public static void FindLongestFragment(string line, int linenumber, ref int curbestchainl, List<int> linenumbers, string alphabet, string punctuation, ref bool ischain, ref int curchainl, ref string Chainfrag,ref List<char> puncmarks)
    {
        //Initial variables
        int cursor = 1;
        string NewLine = " " + line + " ";
        char[] Alphabet = alphabet.ToCharArray();
        char[] Punctuation = punctuation.ToCharArray();
        //If we are on a new chain, set current chain length to zero
        if (ischain == false)
        {
            curchainl = 0;
        }
        //I use a while and a cursor to navigate the line itself
        while (cursor < NewLine.Length)
        {
            //We find the location of the final letter of the first word
            int firstlet = NewLine.IndexOfAny(Punctuation, cursor);
            //If such a letter is found
            if (firstlet != -1)
            {
                //We use a variable for punctuation ammounts
                int puncam = 1;
                firstlet--;
                //We have a second letter, which we add to punctuation if it doesnt contain
                int secondlet = firstlet + 1;
                if (secondlet < line.Length && puncmarks.Contains(line[secondlet-1]) == false && punctuation.Contains(line[secondlet-1]) == true)
                {
                    puncmarks.Add(line[secondlet-1]);
                }
                //Go through line continuisly, until the line either ends or we find the end of punctation
                //Continuisly adding new punctuationa
                while (secondlet < NewLine.Length && punctuation.Contains(NewLine[secondlet]) == true)
                {
                    if (secondlet < line.Length && puncmarks.Contains(line[secondlet]) == false && punctuation.Contains(line[secondlet]) == true)
                    {
                        puncmarks.Add(line[secondlet]);
                    }
                    secondlet++;
                    puncam++;
                   
                }
                //Checking if last letter in word equals first letter in other word
                if (secondlet < NewLine.Length && char.ToLower(NewLine[firstlet]) == char.ToLower(NewLine[secondlet]))
                {
                    //If new chain, add only one word to count at all
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
                        //Adds double
                        if (ischain == true)
                        {
                            curchainl ++;
                            Chainfrag += NewLine.Substring(cursor, (NewLine.IndexOfAny(Punctuation, firstlet) - cursor));
                            Chainfrag += " ";
                            if (linenumbers.Contains(linenumber) == false)
                            {
                                linenumbers.Add(linenumber);
                            }
                        }
                        else
                        {
                            ischain = true;
                            curchainl ++;
                            Chainfrag += NewLine.Substring(cursor, (NewLine.IndexOfAny(Punctuation, firstlet)) - cursor);
                            Chainfrag += " ";
                            if (linenumbers.Contains(linenumber) == false)
                            {
                                linenumbers.Add(linenumber);
                            }
                        }
                    }


                }
                //This is for chain breaking
                else if (secondlet < NewLine.Length && char.ToLower(NewLine[firstlet]) != char.ToLower(NewLine[secondlet]))
                {
                    //Overwrites best chain if is best chain
                    //Sets all variables to zero
                    ischain = false;
                    if (curchainl > curbestchainl)
                    {
                        curbestchainl = curchainl;
                        linenumbers.Clear();
                        linenumbers.TrimExcess();
                        linenumbers.Add(linenumber);
                    }
                    Chainfrag = string.Empty;
                    puncmarks.Clear();
                    curchainl = 0;
                }
                //If that was the last word, just set it to the end
                else if (secondlet == NewLine.Length - 1)
                {
                    cursor = secondlet - 1;
                }
                cursor = secondlet;
            }
        }
    }
    /// <summary>
    /// Removes the same punctuation in a row from the line
    /// </summary>
    /// <param name="line">input line</param>
    /// <param name="punctuation">All punctuation</param>
    /// <param name="newline">output line</param>
    /// <returns>If the line has been changed</returns>
    public static bool RemoveSamePunctuation(string line, string punctuation, out StringBuilder newline)
    {
        //initial variables
        char[] allpunc = punctuation.ToCharArray();
        int cursor = 0;
        int writepoint = 0;
        bool ischanged = false;
        newline = new StringBuilder();
        //Using a cursor int to navigate whole string agian
        while (cursor < line.Length)
        {
            //We find first punctuation mark
            int firstpunc = line.IndexOfAny(allpunc, cursor);
            if (firstpunc != -1)
            {
                //We check if next punctuation is same
                char ch = line[firstpunc];
                if (firstpunc != line.Length - 1 && line[firstpunc] == line[firstpunc + 1] && punctuation.Contains(line[firstpunc]) == true)
                {
                    //if yes, we search the final point of punctuation
                    ischanged = true;
                    newline.Append(line.Substring(writepoint, (firstpunc - writepoint) + 1));
                    while (firstpunc < line.Length && line[firstpunc] == ch)
                    {
                        firstpunc++;
                    }
                    //Then write everytinh
                    cursor = firstpunc;
                    writepoint = firstpunc;
                }
                //If we reach end finish
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
                //If no punctuation found, add whole line and finish function
                newline.Append(line.Substring(writepoint));
                return ischanged;
            }
        }
        return ischanged;
    }
    /// <summary>
    /// Finds the amount of words comprised of numbers in line and adds them together
    /// </summary>
    /// <param name="line">input line</param>
    /// <param name="numsum">whole file sum of number words</param>
    /// <param name="numbers">all numbers</param>
    /// <param name="punctuation">all punctuation</param>
    /// <param name="numamount">Amount of number words in file</param>
    /// <returns></returns>
    public static bool NumberWordsinLine(string line, ref int numsum, string numbers, string punctuation, ref int numamount)
    {
        //Initial variables
        line = " " + line + "  ";
        char[] num = numbers.ToCharArray();
        int cursor = 0;
        bool contains = false;
        while (cursor < line.Length)
        {
            //Using same cursor method again
            //We find the first location of a number
            int firstnumloc = line.IndexOfAny(num, cursor);
            if (firstnumloc == -1)
            {
                //if there are no numbers, we just return false
                return contains;
            }
            else if (punctuation.Contains(line[firstnumloc - 1]) == false)
            {
                //if the number is a part of a word, we just ignore it
                cursor = firstnumloc + 1;
            }
            else
            {
                //We start of by finding the final number element
                //By using a while loop
                int lastnumloc = firstnumloc;
                while (numbers.Contains(line[lastnumloc]) == true)
                {
                    lastnumloc++;
                }
                //If the character after the final number is punctuation, we can say that that is a number word
                if (punctuation.Contains(line[lastnumloc + 1]) == true)
                {
                    contains = true;
                    //We make a string of the word
                    string numword = line.Substring(firstnumloc, lastnumloc - firstnumloc);
                    //Give it to a function to calculate the sum and add it to program wide sum
                    numsum += TaskUtils.NumberWordSum(numword);
                    cursor = lastnumloc + 1;
                    //Add 1 to number word amount
                    numamount++;
                }
            }
        }
        return contains;
    }
    /// <summary>
    /// Gets the sum of a numberword
    /// </summary>
    /// <param name="numword">numberword</param>
    /// <returns>sum of numbers in word</returns>
    private static int NumberWordSum(string numword)
    {
        int sum = 0;
        //We just go through the whole word and add the number value to a sum, then return it
        for (int i = 0; i < numword.Length; i++)
        {
            sum += Convert.ToInt32(char.GetNumericValue(numword[i]));
        }
        return sum;
    }
    /// <summary>
    /// Finds the maximum line spacings
    /// </summary>
    /// <param name="line">line</param>
    /// <param name="punctuation">punctuation</param>
    /// <returns></returns>
    public static List<int> Linespacings(string line, string punctuation)
    {
        //we make an empty list up to 80 word capacity
        List<int> LineSpacing = new List<int>();
        EmptyList(ref LineSpacing);
        StringBuilder stringBuilder = new StringBuilder();
        //We remove same punctuation, as this is for hard output, so we dont need extra punctuation (hehe)
        RemoveSamePunctuation(line, punctuation, out stringBuilder);
        string newLine = stringBuilder.ToString();
        //We split the line into words, presuming that all words have atleast 1 space
        string[] indword = newLine.Split(' ');
        //set first element to 1, as that is specified in instructions
        LineSpacing[0] = 1;
        for (int i = 1; i < indword.Length; i++)
        {
            //We caluclate the linespacing, using indexof
            LineSpacing[i] = newLine.IndexOf(indword[i]);
        }
        return LineSpacing;
    }
    /// <summary>
    /// Takes a list and expands its capacity to 80 and fills it with -1
    /// </summary>
    /// <param name="list">List to empty</param>
    public static void EmptyList(ref List<int> list)
    {
        //Takes a list and expands its capacity to 80 and fills it with -1
        for (int i = 0; i < 80; i++)
        {
            list.Insert(i, -1);
        }
    }
    /// <summary>
    /// Adds the required line spacings, according to task at hand
    /// </summary>
    /// <param name="line">line</param>
    /// <param name="punctuation">all punctuation</param>
    /// <param name="secwordstart">the locations to add</param>
    /// <returns></returns>
    public static string SpacingLine(string line, string punctuation, List<int> secwordstart)
    {
        StringBuilder newLine = new StringBuilder();
        //We again split the words by space
        string[] Allwords = line.Split(' ');
        for (int i = 0; i < Allwords.Length; i++)
        {

            if (i == 0)
            {
                newLine.Append(" " + Allwords[i] + " ");
            }
            else if (line.IndexOf(Allwords[i]) < secwordstart[i])
            {
                //Calculates spaces to add and adds them
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
