namespace SharpMeasures.Generators.Parsing.Vectors.ScalarAssociationAttribute;

using Microsoft.CodeAnalysis;

/// <inheritdoc cref="IScalarAssociationSyntax"/>
internal sealed record class ScalarAssociationSyntax : IScalarAssociationSyntax
{
    private Location Scalar { get; }

    private Location AsComponents { get; }
    private Location AsMagnitude { get; }

    /// <summary>Instantiates a <see cref="ScalarAssociationSyntax"/>, representing syntactical information about a parsed <see cref="ScalarAssociationAttribute{TScalar}"/>.</summary>
    /// <param name="scalar"><inheritdoc cref="IScalarAssociationSyntax.Scalar" path="/summary"/></param>
    /// <param name="asComponents"><inheritdoc cref="IScalarAssociationSyntax" path="/summary"/></param>
    /// <param name="asMagnitude"><inheritdoc cref="IScalarAssociationSyntax" path="/summary"/></param>
    public ScalarAssociationSyntax(Location scalar, Location asComponents, Location asMagnitude)
    {
        Scalar = scalar;

        AsComponents = asComponents;
        AsMagnitude = asMagnitude;
    }

    Location IScalarAssociationSyntax.Scalar => Scalar;

    Location IScalarAssociationSyntax.AsComponents => AsComponents;
    Location IScalarAssociationSyntax.AsMagnitude => AsMagnitude;
}
