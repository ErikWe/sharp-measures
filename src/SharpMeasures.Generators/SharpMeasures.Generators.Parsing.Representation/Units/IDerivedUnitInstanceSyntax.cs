namespace SharpMeasures.Generators.Parsing.Units;

using Microsoft.CodeAnalysis;

using System.Collections.Generic;

/// <summary>Represents syntactical information about a parsed <see cref="DerivedUnitInstanceAttribute"/>.</summary>
public interface IDerivedUnitInstanceSyntax : IUnitInstanceSyntax
{
    /// <summary>The <see cref="Location"/> of the argument for the ID of the derivation. May be <see cref="Location.None"/> if no argument was provided.</summary>
    public abstract Location DerivationID { get; }

    /// <summary>The <see cref="Location"/> of the argument for the names of the unit instances.</summary>
    public abstract Location UnitsCollection { get; }

    /// <summary>The <see cref="Location"/> of each individual element in the argument for the names of the unit instances.</summary>
    public abstract IReadOnlyList<Location> UnitsElements { get; }
}
