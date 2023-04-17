namespace SharpMeasures.Generators.Parsing.Quantities;

using Microsoft.CodeAnalysis;

using System.Collections.Generic;

/// <summary>Represents syntactical information about a parsed <see cref="IncludeUnitsAttribute"/>.</summary>
public interface IIncludeUnitsSyntax : IAttributeSyntax
{
    /// <summary>The <see cref="Location"/> of the argument for the names of the included unit instances.</summary>
    public abstract Location IncludedUnitsCollection { get; }

    /// <summary>The <see cref="Location"/> of each individual element in the argument for the names of the included unit instances.</summary>
    public abstract IReadOnlyList<Location> IncludedUnitsElements { get; }

    /// <summary>The <see cref="Location"/> of the argument for how multiple filters are stacked. May be <see cref="Location.None"/> if no argument was provided.</summary>
    public abstract Location StackingMode { get; }
}
