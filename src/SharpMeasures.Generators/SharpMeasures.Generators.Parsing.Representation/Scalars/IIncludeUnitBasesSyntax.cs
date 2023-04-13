namespace SharpMeasures.Generators.Parsing.Scalars;

using Microsoft.CodeAnalysis;

using System.Collections.Generic;

/// <summary>Represents syntactical information about a parsed <see cref="IncludeUnitBasesAttribute"/>.</summary>
public interface IIncludeUnitBasesSyntax
{
    /// <summary>The <see cref="Location"/> of the argument for the names of the included unit instances.</summary>
    public abstract Location IncludedUnitBasesCollection { get; }

    /// <summary>The <see cref="Location"/> of each individual element in the argument for the names of the included unit instances.</summary>
    public abstract IReadOnlyList<Location> IncludedUnitBasesElements { get; }

    /// <summary>The <see cref="Location"/> of the argument for how multiple filters are stacked. May be <see cref="Location.None"/> if no argument was provided.</summary>
    public abstract Location StackingMode { get; }
}
