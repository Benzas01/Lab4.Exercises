using System.IO;
using System.Text;
using System.Text.RegularExpressions;

public class TaskUtils
{
    //------------------------------------------------------------
    /** Reads file and finds the number of words having same the first and
    the last letters.
    @param fin – name of data file
    @param punctuation – punctuation marks to separate words */
    public static int Process(string fin, string punctuation)
    {
        string[] lines = File.ReadAllLines(fin, Encoding.UTF8);
        int equal = 0;
        foreach (string line in lines)
            if (line.Length > 0)
                equal += FirstEqualLast(line, punctuation);
        return equal;
    }
    //------------------------------------------------------------
    //------------------------------------------------------------
    /** Splits line into words and counts the words having same the first and the
    last letters.
    @param line – string of data
    @param punctuation – punctuation marks to separate words */
    private static int FirstEqualLast(string line, string punctuation)
    {
        string[] parts = Regex.Split(line, "[" + punctuation + "]+");
        int equal = 0;
        foreach (string word in parts)
        {
            if (word.Length > 0)
            {
                if (IsPalindrome(word) == true)
                {
                    equal++;
                }
            }
        }
        return equal;
    }
    private static bool IsPalindrome(string word)
    {
        for (int i = 0; i < word.Length / 2; i++)
        {
            if (word[i] != word[(word.Length - 1) - i])
            {
                return false;
            }
        }
        return true;
    }
    //------------------------------------------------------------
}
