using System.Collections.Generic;

class LettersFrequency
{
    public Dictionary<char, int> frequencyd;
    private const int CMax = 382;
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
            if (frequencyd.ContainsKey(line[i]))
            {
                frequencyd[line[i]]++;
            }
        }
    }
    public char mostcommonletter()
    {
        char mostCommon = 'ž';
        foreach (KeyValuePair<char, int> pairs in frequencyd)
        {
            if (frequencyd[pairs.Key] > frequencyd[mostCommon])
            {
                mostCommon = pairs.Key;
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
        frequencyd.Add('ą', 0);
        frequencyd.Add('Ą', 0);
        frequencyd.Add('č', 0);
        frequencyd.Add('Č', 0);
        frequencyd.Add('ę', 0);
        frequencyd.Add('Ę', 0);
        frequencyd.Add('ė', 0);
        frequencyd.Add('Ė', 0);
        frequencyd.Add('į', 0);
        frequencyd.Add('Į', 0);
        frequencyd.Add('š', 0);
        frequencyd.Add('Š', 0);
        frequencyd.Add('ų', 0);
        frequencyd.Add('Ų', 0);
        frequencyd.Add('ū', 0);
        frequencyd.Add('Ū', 0);
        frequencyd.Add('ž', 0);
        frequencyd.Add('Ž', 0);
    }
}
