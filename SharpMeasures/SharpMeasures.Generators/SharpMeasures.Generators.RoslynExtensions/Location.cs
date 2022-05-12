namespace Microsoft.CodeAnalysis;

using Microsoft.CodeAnalysis.Text;

using System;

public static partial class Extensions
{
    public static Location ExtendToInclude(this Location originalLocation, Location otherLocation)
    {
        if (originalLocation is null)
        {
            throw new ArgumentNullException(nameof(originalLocation));
        }

        if (otherLocation is null)
        {
            throw new ArgumentNullException(nameof(otherLocation));
        }

        FileLinePositionSpan originalLineSpan = originalLocation.GetLineSpan();
        FileLinePositionSpan otherLineSpan = otherLocation.GetLineSpan();

        if (originalLineSpan.Path != otherLineSpan.Path)
        {
            throw new NotSupportedException("Cannot combine locations from different sources.");
        }

        bool originalLocationIsFirst = isOriginalLocationFirst();

        FileLinePositionSpan combinedLineSpan = originalLocationIsFirst
            ? new(originalLineSpan.Path, originalLineSpan.StartLinePosition, otherLineSpan.EndLinePosition)
            : new(originalLineSpan.Path, otherLineSpan.StartLinePosition, originalLineSpan.EndLinePosition);

        TextSpan combinedTextSpan = originalLocationIsFirst
            ? new(originalLocation.SourceSpan.Start, otherLocation.SourceSpan.End)
            : new(otherLocation.SourceSpan.Start, originalLocation.SourceSpan.End);

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
