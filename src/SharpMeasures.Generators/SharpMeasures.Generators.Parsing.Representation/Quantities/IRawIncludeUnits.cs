namespace SharpMeasures.Generators.Parsing.Quantities;

using System.Collections.Generic;

/// <summary>Represents a parsed <see cref="IncludeUnitsAttribute"/>.</summary>
public interface IRawIncludeUnits
{
    /// <summary>The names of the units that are included.</summary>
    public abstract IReadOnlyList<string?>? IncludedUnits { get; }

    /// <summary>Determines how multiple filters are stacked.</summary>
    public abstract FilterStackingMode? StackingMode { get; }

    /// <summary>Provides syntactical information about the parsed <see cref="IncludeUnitsAttribute"/>.</summary>
    public abstract IIncludeUnitsSyntax? Syntax { get; }
}
