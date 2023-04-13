namespace SharpMeasures.Generators.Parsing.Units.UnitAttribute;

using Microsoft.CodeAnalysis;

/// <inheritdoc cref="IUnitSyntax"/>
internal sealed record class UnitSyntax : IUnitSyntax
{
    private Location Scalar { get; }
    private Location BiasTerm { get; }

    /// <summary>Instantiates a <see cref="UnitSyntax"/>, representing syntactial information about a parsed <see cref="UnitAttribute{TScalar}"/>.</summary>
    /// <param name="scalar"><inheritdoc cref="IUnitSyntax.Scalar" path="/summary"/></param>
    /// <param name="biasTerm"><inheritdoc cref="IUnitSyntax.BiasTerm" path="/summary"/></param>
    public UnitSyntax(Location scalar, Location biasTerm)
    {
        Scalar = scalar;
        BiasTerm = biasTerm;
    }

    Location IUnitSyntax.Scalar => Scalar;
    Location IUnitSyntax.BiasTerm => BiasTerm;
}
