namespace SharpMeasures.Generators.Parsing.Scalars;

using Microsoft.CodeAnalysis;

using System.Collections.Generic;

/// <summary>Represents syntactical information about a parsed <see cref="ExcludeUnitBasesAttribute"/>.</summary>
public interface IExcludeUnitBasesSyntax
{
    /// <summary>The <see cref="Location"/> of the argument for the names of the excluded unit instances.</summary>
    public abstract Location ExcludedUnitBasesCollection { get; }

    /// <summary>The <see cref="Location"/> of each individual element in the argument for the names of the excluded unit instances.</summary>
    public abstract IReadOnlyList<Location> ExcludedUnitBasesElements { get; }

    /// <summary>The <see cref="Location"/> of the argument for how multiple filters are stacked. May be <see cref="Location.None"/> if no argument was provided.</summary>
    public abstract Location StackingMode { get; }
}
