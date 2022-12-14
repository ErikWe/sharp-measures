namespace SharpMeasures.Generators;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

public readonly record struct MinimalLocation(string Path, TextSpan TextSpan, LinePositionSpan LineSpan)
{
    public static MinimalLocation None => FromLocation(Location.None);

    internal static MinimalLocation FromLocation(Location? location)
    {
        if (location is null)
        {
            return new();
        }

        var fileSpan = location.GetLineSpan();
        var path = fileSpan.Path;
        var textSpan = location.SourceSpan;

        return new(path, textSpan, fileSpan.Span);
    }

    public Location AsRoslynLocation() => Location.Create(Path, TextSpan, LineSpan);
}
