using System.Collections.Generic;

class LettersFrequency
{
    public Dictionary<char, int> frequencyd;
    private const int CMax = 256;
    private int[] Frequency; // Frequency of letters
    public string line { get; set; }
    public LettersFrequency()
    {
        line = "";
        Frequency = new int[CMax];
        frequencyd = new Dictionary<char, int>();
        for (int i = 0; i < CMax; i++)
            Frequency[i] = 0;
    }
    public int Get(char character)
    {
        return frequencyd[character];
    }
    //------------------------------------------------------------
    /** Counts repetition of letters. */
    public void Count()
    {
        for (int i = 0; i < line.Length; i++)
        {
            if (('a' <= line[i] && line[i] <= 'z') ||
            ('A' <= line[i] && line[i] <= 'Z'))
                Frequency[line[i]]++;
        }
    }
    public char mostcommonletter()
    {
        char mostCommon = 'a';
        for (char a = 'a'; a <= 'z'; a++)
        {
            if (frequencyd[a] > frequencyd[mostCommon])
            {
                mostCommon = a;
            }
        }
        return mostCommon;
    }
    public void remove(char character)
    {
        frequencyd[character] = -111111;
    }
    public void buildic()
    {
        for (char a = 'a'; a <= 'z'; a++)
        {
            frequencyd.Add(a, Frequency[a]);
            frequencyd.Add(char.ToUpper(a), Frequency[char.ToUpper(a)]);
        }
    }
}
