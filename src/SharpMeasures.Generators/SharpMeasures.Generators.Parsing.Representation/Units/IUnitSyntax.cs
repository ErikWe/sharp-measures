namespace SharpMeasures.Generators.Parsing.Units;

using Microsoft.CodeAnalysis;

/// <summary>Represents syntactical information about a parsed <see cref="UnitAttribute{TScalar}"/>.</summary>
public interface IUnitSyntax : IAttributeSyntax
{
    /// <summary>The <see cref="Location"/> of the argument for the scalar quantity that the unit describes.</summary>
    public abstract Location Scalar { get; }

    /// <summary>The <see cref="Location"/> of the argument determining whether the unit includes a bias term. May be <see cref="Location.None"/> if no argument was provided.</summary>
    public abstract Location BiasTerm { get; }
}
