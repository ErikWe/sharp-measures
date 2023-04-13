namespace SharpMeasures.Generators.Parsing.Scalars;

using System.Collections.Generic;

/// <summary>Represents a parsed <see cref="ExcludeUnitBasesAttribute"/>.</summary>
public interface IRawExcludeUnitBases
{
    /// <summary>The names of the units that are excluded as bases.</summary>
    public abstract IReadOnlyList<string?>? ExcludedUnitBases { get; }

    /// <summary>Determines how multiple filters are stacked.</summary>
    public abstract FilterStackingMode? StackingMode { get; }

    /// <summary>Provides syntactical information about the parsed <see cref="ExcludeUnitBasesAttribute"/>.</summary>
    public abstract IExcludeUnitBasesSyntax? Syntax { get; }
}
