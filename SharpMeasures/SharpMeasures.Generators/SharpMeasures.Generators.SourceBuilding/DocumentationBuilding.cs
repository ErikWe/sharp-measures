namespace SharpMeasures.Generators.SourceBuilding;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Documentation;

using System.Text;
using System.Text.RegularExpressions;

public static class DocumentationBuilding
{
    private static Regex NewLineRegex { get; } = new(@"(?:\r\n|\r|\n|^)", RegexOptions.Singleline | RegexOptions.Compiled);

    public static void AppendDocumentation(SourceProductionContext context, StringBuilder source, DocumentationFile documentation,
        Indentation indentation, string tag)
    {
        if (documentation.ResolveTagAndReportDiagnostics(context, tag) is string { Length: > 0 } tagContent)
        {
            string indentedTagContent = NewLineRegex.Replace(tagContent, $"$0{indentation}");

            source.AppendLine(indentedTagContent);
        }
    }
}
