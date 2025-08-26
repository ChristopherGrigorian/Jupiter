using System.Text.RegularExpressions;

public static class DialogueFormatter
{
    // Converts _italics_ into <i>italics</i> for TextMeshPro
    public static string ConvertItalics(string input)
    {
        return Regex.Replace(input, @"_(.+?)_", "<i>$1</i>");
    }
}
