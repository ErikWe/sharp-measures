namespace SharpMeasures.Generators.DriverUtility;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

using System.Threading;

public sealed class CustomAdditionalText : AdditionalText
{
    public override string Path { get; }
    private string Source { get; }

    public CustomAdditionalText(string path, string source)
    {
        Path = path;
        Source = source;
    }

    public override SourceText GetText(CancellationToken cancellationToken = new CancellationToken()) => SourceText.From(Source);

    public bool Equals(CustomAdditionalText? other) => other is not null && other.Path == Path && other.Source == Source; 
    public override bool Equals(object? obj) => obj is CustomAdditionalText other && Equals(other);

    public static bool operator ==(CustomAdditionalText? lhs, CustomAdditionalText? rhs) => lhs?.Equals(rhs) ?? rhs is null;
    public static bool operator !=(CustomAdditionalText? lhs, CustomAdditionalText? rhs) => (lhs == rhs) is false;

    public override int GetHashCode() => (Path, Source).GetHashCode();
}
