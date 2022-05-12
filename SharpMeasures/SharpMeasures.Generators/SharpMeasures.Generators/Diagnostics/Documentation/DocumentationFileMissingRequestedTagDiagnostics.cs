namespace SharpMeasures.Generators.Diagnostics.Documentation;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

using System.IO;

internal static class DocumentationFileMissingRequestedTagDiagnostics
{
    public static Diagnostic Create(AdditionalText file, string requestedTag)
    {
        if (file.GetText() is not SourceText text)
        {
            return Diagnostic.Create(DiagnosticRules.DocumentationFileMissingRequestedTag, null, requestedTag, file.Path);
        }
        else
        {
            LinePositionSpan span = new(new LinePosition(0, 0), new LinePosition(text.Lines.Count - 1, text.Lines[text.Lines.Count - 1].End));
            Location location = Location.Create(file.Path, TextSpan.FromBounds(0, file.ToString().Length - 1), span);

            return Diagnostic.Create(DiagnosticRules.DocumentationFileMissingRequestedTag, location, requestedTag, Path.GetFileName(file.Path));
        }
    }

    public static Diagnostic Create(Location location, string fileName, string requestedTag)
    {
        return Diagnostic.Create(DiagnosticRules.DocumentationFileMissingRequestedTag, location, requestedTag, $"{fileName}.doc.txt");
    }
}
