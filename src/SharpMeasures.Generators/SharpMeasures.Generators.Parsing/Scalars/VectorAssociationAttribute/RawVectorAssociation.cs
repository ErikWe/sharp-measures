namespace SharpMeasures.Generators.Parsing.Scalars.VectorAssociationAttribute;

using Microsoft.CodeAnalysis;

/// <inheritdoc cref="IRawVectorAssociation"/>
internal sealed record class RawVectorAssociation : IRawVectorAssociation
{
    private ITypeSymbol Vector { get; }

    private IVectorAssociationSyntax? Syntax { get; }

    /// <summary>Instantiates a <see cref="RawVectorAssociation"/>, representing a parsed <see cref="VectorAssociationAttribute{TVector}"/>.</summary>
    /// <param name="vector"><inheritdoc cref="IRawVectorAssociation.Vector" path="/summary"/></param>
    /// <param name="syntax"><inheritdoc cref="IRawVectorAssociation.Syntax" path="/summary"/></param>
    public RawVectorAssociation(ITypeSymbol vector, IVectorAssociationSyntax? syntax)
    {
        Vector = vector;

        Syntax = syntax;
    }

    ITypeSymbol IRawVectorAssociation.Vector => Vector;

    IVectorAssociationSyntax? IRawVectorAssociation.Syntax => Syntax;
}
