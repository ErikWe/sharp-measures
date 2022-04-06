namespace ErikWe.SharpMeasures.SourceGenerators.Tests;

using System.IO;
using System.Threading;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

public class CustomAdditionalText : AdditionalText
{
    public override string Path { get; }
    private string Source { get; }

    public CustomAdditionalText(string path)
    {
        Path = path;
        Source = File.ReadAllText(path);
    }

    public override SourceText GetText(CancellationToken cancellationToken = new CancellationToken())
    {
        return SourceText.From(Source);
    }
}