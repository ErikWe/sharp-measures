namespace SharpMeasures.Generators.Parsing.Vectors.ScalarAssociationAttribute;

using Microsoft.CodeAnalysis;

/// <inheritdoc cref="IRawScalarAssociation"/>
internal sealed record class RawScalarAssociation : IRawScalarAssociation
{
    private ITypeSymbol Scalar { get; }

    private bool? AsComponents { get; }
    private bool? AsMagnitude { get; }

    private IScalarAssociationSyntax? Syntax { get; }

    /// <summary>Instantiates a <see cref="RawScalarAssociation"/>, representing a parsed <see cref="ScalarAssociationAttribute{TScalar}"/>.</summary>
    /// <param name="scalar"><inheritdoc cref="IRawScalarAssociation.Scalar" path="/summary"/></param>
    /// <param name="asComponents"><inheritdoc cref="IRawScalarAssociation.AsComponents" path="/summary"/></param>
    /// <param name="asMagnitude"><inheritdoc cref="IRawScalarAssociation.AsMagnitude" path="/summary"/></param>
    /// <param name="syntax"><inheritdoc cref="IRawScalarAssociation.Syntax" path="/summary"/></param>
    public RawScalarAssociation(ITypeSymbol scalar, bool? asComponents, bool? asMagnitude, IScalarAssociationSyntax? syntax)
    {
        Scalar = scalar;

        AsComponents = asComponents;
        AsMagnitude = asMagnitude;

        Syntax = syntax;
    }

    ITypeSymbol IRawScalarAssociation.Scalar => Scalar;

    bool? IRawScalarAssociation.AsComponents => AsComponents;
    bool? IRawScalarAssociation.AsMagnitude => AsMagnitude;

    IScalarAssociationSyntax? IRawScalarAssociation.Syntax => Syntax;
}
