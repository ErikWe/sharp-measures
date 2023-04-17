namespace SharpMeasures.Generators.Parsing.Units;

using Microsoft.CodeAnalysis;

using System.Collections.Generic;

/// <summary>Represents syntactical information about a parsed <see cref="UnitDerivationAttribute"/>.</summary>
public interface IUnitDerivationSyntax
{
    /// <summary>The <see cref="Location"/> of the argument for the ID of the derivation. May be <see cref="Location.None"/> if no argument was provided.</summary>
    public abstract Location DerivationID { get; }

    /// <summary>The <see cref="Location"/> of the argument for the expression of the derivation.</summary>
    public abstract Location Expression { get; }

    /// <summary>The <see cref="Location"/> of the argument for the signature.</summary>
    public abstract Location SignatureCollection { get; }

    /// <summary>The <see cref="Location"/> of each individual element in the argument for the signature.</summary>
    public abstract IReadOnlyList<Location> SignatureElements { get; }
}
