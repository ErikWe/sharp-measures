namespace SharpMeasures.Generators.Parsing.Scalars;

using System.Collections.Generic;

/// <summary>Represents a parsed <see cref="IncludeUnitBasesAttribute"/>.</summary>
public interface IRawIncludeUnitBases
{
    /// <summary>The names of the units that are included as bases.</summary>
    public abstract IReadOnlyList<string?>? IncludedUnitBases { get; }

    /// <summary>Determines how multiple filters are stacked.</summary>
    public abstract FilterStackingMode? StackingMode { get; }

    /// <summary>Provides syntactical information about the parsed <see cref="IncludeUnitBasesAttribute"/>.</summary>
    public abstract IIncludeUnitBasesSyntax? Syntax { get; }
}
