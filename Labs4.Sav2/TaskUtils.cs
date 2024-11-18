public static class TaskUtils
{
    //------------------------------------------------------------
    /** Removes comments and returns an indication about performed activity.
@param line – line having possible comments
@param newLine – line without comments */
    public static bool RemoveComments(string line, out string newLine, ref bool iscomment)
    {
        bool ischanged = false;
        newLine = string.Empty;
        for (int i = 0; i < line.Length; i++)
        {
            if (i < line.Length - 1 && line[i] == '/' && line[i + 1] == '/')
            {
                ischanged = true;
                break;
            }
            else if (i < line.Length - 1 && line[i] == '/' && line[i + 1] == '*')
            {
                iscomment = true;
                ischanged = true;
            }
            else if (i < line.Length - 1 && line[i] == '*' && line[i + 1] == '/' && iscomment == true)
            {
                ischanged = true;
                iscomment = false;
                i += 2;
            }
            else if (iscomment != true)
            {
                newLine += line[i];
            }
        }
        return ischanged;
    }
    //------------------------------------------------------------
}