namespace SharpMeasures.Generators.SourceBuilding;

using System.Text;
using System.Text.RegularExpressions;

public static class DocumentationBuilding
{
    private static Regex NewLineRegex { get; } = new(@"(?:\r\n|\r|\n|^)", RegexOptions.Singleline | RegexOptions.Compiled);

    public static void AppendDocumentation(StringBuilder source, Indentation indentation, string text)
    {
        string indentedTagContent = NewLineRegex.Replace(text, $"$0{indentation}");

        source.AppendLine(indentedTagContent);
    }
}
