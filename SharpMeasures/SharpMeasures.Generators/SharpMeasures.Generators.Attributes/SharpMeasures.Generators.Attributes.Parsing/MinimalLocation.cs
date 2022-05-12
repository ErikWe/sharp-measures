namespace SharpMeasures.Generators;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

using System;
using System.Collections.Generic;

public readonly record struct MinimalLocation(string Path, TextSpan TextSpan, LinePositionSpan LineSpan)
{
    public static MinimalLocation FromLocation(Location location)
    {
        if (location is null)
        {
            throw new ArgumentNullException(nameof(location));
        }

        var fileSpan = location.GetLineSpan();
        string path = fileSpan.Path;
        var textSpan = location.SourceSpan;

        return new(path, textSpan, fileSpan.Span);
    }

    public static IReadOnlyCollection<MinimalLocation> FromLocations(IReadOnlyCollection<Location> locations)
    {
        if (locations is null)
        {
            throw new ArgumentNullException(nameof(locations));
        }

        MinimalLocation[] minimalLocations = new MinimalLocation[locations.Count];

        int index = 0;
        foreach (Location location in locations)
        {
            minimalLocations[index++] = FromLocation(location);
        }

        return minimalLocations;
    }

    public Location ToLocation() => Location.Create(Path, TextSpan, LineSpan);
}
