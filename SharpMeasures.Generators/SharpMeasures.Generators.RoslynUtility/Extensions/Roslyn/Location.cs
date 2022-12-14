namespace SharpMeasures.Generators;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

using System;

public static partial class RoslynUtilityExtensions
{
    public static MinimalLocation Minimize(this Location location) => MinimalLocation.FromLocation(location);

    public static Location ExtendToInclude(this Location originalLocation, Location otherLocation)
    {
        var originalLineSpan = originalLocation.GetLineSpan();
        var otherLineSpan = otherLocation.GetLineSpan();

        if (originalLineSpan.Path != otherLineSpan.Path)
        {
            throw new NotSupportedException("Cannot combine locations from different sources.");
        }

        var originalLocationIsFirst = isOriginalLocationFirst();

        FileLinePositionSpan combinedLineSpan = originalLocationIsFirst
            ? new(originalLineSpan.Path, originalLineSpan.StartLinePosition, otherLineSpan.EndLinePosition)
            : new(originalLineSpan.Path, otherLineSpan.StartLinePosition, originalLineSpan.EndLinePosition);

        var combinedTextSpan = originalLocationIsFirst
            ? TextSpan.FromBounds(originalLocation.SourceSpan.Start, otherLocation.SourceSpan.End)
            : TextSpan.FromBounds(otherLocation.SourceSpan.Start, originalLocation.SourceSpan.End);

        return Location.Create(originalLineSpan.Path, combinedTextSpan, combinedLineSpan.Span);

        bool isOriginalLocationFirst()
        {
            if (originalLineSpan.StartLinePosition.Line < otherLineSpan.StartLinePosition.Line)
            {
                return true;
            }

            if (originalLineSpan.StartLinePosition.Line > otherLineSpan.StartLinePosition.Line)
            {
                return false;
            }

            return originalLineSpan.StartLinePosition.Character < otherLineSpan.StartLinePosition.Character;
        }
    }
}
