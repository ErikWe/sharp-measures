namespace SharpMeasures.Generators.Parsing.Quantities;

using System.Collections.Generic;

/// <summary>Represents a parsed <see cref="ExcludeUnitsAttribute"/>.</summary>
public interface IRawExcludeUnits
{
    /// <summary>The names of the units that are excluded.</summary>
    public abstract IReadOnlyList<string?>? ExcludedUnits { get; }

    /// <summary>Determines how multiple filters are stacked.</summary>
    public abstract FilterStackingMode? StackingMode { get; }

    /// <summary>Provides syntactical information about the parsed <see cref="ExcludeUnitsAttribute"/>.</summary>
    public abstract IExcludeUnitsSyntax? Syntax { get; }
}
