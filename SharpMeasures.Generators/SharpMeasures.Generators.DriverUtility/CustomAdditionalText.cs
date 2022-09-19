namespace SharpMeasures.Generators.DriverUtility;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

using System.IO;
using System.Threading;

internal sealed class CustomAdditionalText : AdditionalText
{
    public override string Path { get; }
    private string Source { get; }

    public CustomAdditionalText(string path)
    {
        Path = path;
        Source = File.ReadAllText(path);
    }

    public override SourceText GetText(CancellationToken cancellationToken = new CancellationToken()) => SourceText.From(Source);
}
