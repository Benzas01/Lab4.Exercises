using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.Expando;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
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
    public static void FindLongestFragment(string line, int linenumber, ref int curbestchainl, List<int> linenumbers, string alphabet, string punctuation, ref bool ischain, ref int curchainl, ref string Chainfrag, ref List<char> puncmarks, ref string bestchain,ref char lastletter, ref List<int> currentChainLineNumbers)
    {
        // Skip empty lines but maintain the chain
        if (string.IsNullOrWhiteSpace(line))
        {
            return;
        }

        // Split by spaces while keeping punctuation
        string[] words = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

        foreach (var word in words)
        {
            // Retain punctuation-only "words" in the chain but do not count them as breaking it
            if (word.All(ch => punctuation.Contains(ch)))
            {
                if (ischain)
                {
                    Chainfrag += word + " "; // Include punctuation in the chain
                    if (!currentChainLineNumbers.Contains(linenumber))
                    {
                        currentChainLineNumbers.Add(linenumber);
                    }
                }
                continue;
            }

            // Extract the first and last letters
            char firstLetter = word.FirstOrDefault(ch => char.IsLetter(ch));
            char lastLetterOfWord = word.LastOrDefault(ch => char.IsLetter(ch));

            // If there's no active chain, start a new one
            if (!ischain)
            {
                ischain = true;
                curchainl = 1; // Start with one word
                Chainfrag = word + " "; // Add the first word to the chain
                lastletter = lastLetterOfWord; // Update the last letter
                currentChainLineNumbers.Clear();
                currentChainLineNumbers.Add(linenumber); // Record the line number
            }
            else
            {
                // Check if the chain can continue
                if (char.ToLower(lastletter) == char.ToLower(firstLetter))
                {
                    curchainl++; // Increment the chain word count
                    Chainfrag += word + " "; // Append the word to the chain

                    // Add the line number if not already added
                    if (!currentChainLineNumbers.Contains(linenumber))
                    {
                        currentChainLineNumbers.Add(linenumber);
                    }

                    // Update the last letter for the next check
                    lastletter = lastLetterOfWord;
                }
                else
                {
                    // Chain breaks, check if it's the longest chain so far
                    if (curchainl > curbestchainl)
                    {
                        curbestchainl = curchainl; // Update best chain length
                        bestchain = Chainfrag.Trim(); // Save the best chain
                        linenumbers.Clear();
                        linenumbers.AddRange(currentChainLineNumbers); // Save line numbers
                    }

                    // Start a new chain
                    ischain = true;
                    curchainl = 1;
                    Chainfrag = word + " ";
                    lastletter = lastLetterOfWord;
                    currentChainLineNumbers.Clear();
                    currentChainLineNumbers.Add(linenumber);
                }
            }
        }

        // Finalize the chain after processing all words in the line
        if (curchainl > curbestchainl)
        {
            curbestchainl = curchainl;
            bestchain = Chainfrag.Trim();
            linenumbers.Clear();
            linenumbers.AddRange(currentChainLineNumbers);
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
        // Add padding to handle edge cases
        line = " " + line + "  ";
        char[] num = numbers.ToCharArray();
        int cursor = 0;
        bool contains = false;
        bool negative = false;
        while (cursor < line.Length)
        {
            // Find the first occurrence of any number starting from the cursor
            int firstnumloc = line.IndexOfAny(num, cursor);
            if (firstnumloc == -1)
            {
                // No more numbers found; return
                return contains;
            }
            if (line[firstnumloc - 1] == '-')
            {
                negative = true;
            }
            // Check if the number is part of a word; if so, skip it
            else if (firstnumloc == 0 || !punctuation.Contains(line[firstnumloc - 1]))
            {
                cursor = firstnumloc + 1;
                negative = false;
                continue;
            }

            // Find the last contiguous numeric character
            int lastnumloc = firstnumloc;
            while (lastnumloc < line.Length && numbers.Contains(line[lastnumloc]))
            {
                lastnumloc++;
            }

            // Check if the character after the number sequence is punctuation
            if (lastnumloc < line.Length && punctuation.Contains(line[lastnumloc]))
            {
                contains = true;

                // Extract the number word
                string numword = line.Substring(firstnumloc, lastnumloc - firstnumloc);
                // Update sum and number word count
                if (negative == true)
                {
                    numsum += Int32.Parse(numword) * -1;
                }
                else
                {
                    numsum += Int32.Parse(numword);
                }
                numamount++;
                negative = false;
                // Move the cursor past the number word
                cursor = lastnumloc;
            }
            else
            {
                // If no punctuation follows, skip to the end of the current sequence
                cursor = lastnumloc;
            }
        }

        return contains;
    }
    /// <summary>
    /// Finds the maximum line spacings
    /// </summary>
    /// <param name="line">line</param>
    /// <param name="punctuation">punctuation</param>
    /// <returns></returns>
    public static List<int> Linespacings(string line, string punctuation, ref List<string> RefWords)
    {
        StringBuilder stringBuilder = new StringBuilder();
        string newLine;
        RemoveSamePunctuation(line, punctuation, out stringBuilder);
        if (RemoveSamePunctuation(line, punctuation, out stringBuilder) == false)
        {
            newLine = line;
        }
        else
        {
            newLine = stringBuilder.ToString();
        }
        string[] indword = newLine.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        List<int> LineSpacing = new List<int>(indword.Length);
        if (indword.Length > 0)
        {
            LineSpacing.Insert(0, 1);
            RefWords.Insert(0, indword[0]);
            for (int i = 1; i < indword.Length; i++)
            {
                LineSpacing.Insert(i, newLine.IndexOf(indword[i]));
                RefWords.Add(indword[i]);
            }
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
        for (int i = 0; i < 40; i++)
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
    public static string SpacingLine(string line, string punctuation, List<int> secwordstart, List<int> SpaceIndexes)
    {
        StringBuilder newLine = new StringBuilder();
        // Corrected Split
        string[] Allwords = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

        for (int i = 0; i < Allwords.Length; i++)
        {
            // Calculate the starting position for this word
            int startIndex = secwordstart[i];

            // Add spaces for alignment if the current index is less than the required start position
            if (newLine.Length < startIndex)
            {
                int spacesToAdd = startIndex - newLine.Length;
                newLine.Append(' ', spacesToAdd);
            }

            // Add the word itself
            newLine.Append(Allwords[i]);

            // Add additional spaces if the word is shorter than the longest word at this position
            int wordLength = Allwords[i].Length;
            if (i < SpaceIndexes.Count && wordLength < SpaceIndexes[i])
            {
                int extraSpaces = SpaceIndexes[i] - wordLength;
                newLine.Append(' ', extraSpaces);
            }

            // Add a single space for separation, except after the last word
            if (i < Allwords.Length - 1)
            {
                newLine.Append(' ');
            }
        }

        return newLine.ToString();
    }

    /// <summary>
    /// Finds the punctuation amount in the longest string that we have
    /// </summary>
    /// <param name="line">Longest line</param>
    /// <param name="punctuation">All punctuation</param>
    /// <returns>Amount of punctuation</returns>
    public static int PuncAms(string line, string punctuation)
    {
        char[] puncs = punctuation.ToCharArray();
        int puncamount = 0;
        for (int i = 0; i < line.Length - 1; i++)
        {
            if (punctuation.Contains(line[i]) == true)
            {
                puncamount++;
            }
        }
        return puncamount;
    }
}