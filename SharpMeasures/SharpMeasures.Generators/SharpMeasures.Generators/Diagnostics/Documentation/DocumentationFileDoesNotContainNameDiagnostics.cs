namespace SharpMeasures.Generators.Diagnostics.Documentation;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

internal static class DocumentationFileDoesNotContainNameDiagnostics
{
    public static Diagnostic Create(AdditionalText file, SourceText text)
    {
        LinePositionSpan span = new(new LinePosition(0, 0), new LinePosition(text.Lines.Count - 1, text.Lines[text.Lines.Count - 1].End));
        Location location = Location.Create(file.Path, TextSpan.FromBounds(0, file.ToString().Length - 1), span);

        return Diagnostic.Create(DiagnosticRules.DocumentationFileDoesNotContainName, location, file.Path);
    }
}
