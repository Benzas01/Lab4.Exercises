﻿using System.IO;
using System.Text;

public static class InOut
{
    //------------------------------------------------------------
    /** Reads, removes comments and prints to two files.
@param fin – name of data file
@param fout – name of result file
@param finfo – name of informative file */
    public static void Process(string fin, string fout, string finfo)
    {
        //should delete empty lines if they're in comments
        bool iscommented = false;
        string[] lines = File.ReadAllLines(fin, Encoding.UTF8);
        using (var writerF = File.CreateText(fout))
        {
            using (var writerI = File.CreateText(finfo))
            {
                foreach (string line in lines)
                {

                    if (line.Length > 0)
                    {
                        string newLine = line;
                        if (TaskUtils.RemoveComments(line, out newLine, ref iscommented) == true && newLine.Length > 0)
                            writerI.Write(line);
                        if (newLine.Length > 0)
                            writerF.WriteLine(newLine);
                    }
                    else if (iscommented == false)
                    {
                        writerF.WriteLine(line);
                    }
                }
            }
        }
    }
    //------------------------------------------------------------
}