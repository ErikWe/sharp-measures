namespace SharpMeasures.Generators.Parsing.Scalars.VectorAssociationAttribute;

using Microsoft.CodeAnalysis;

/// <inheritdoc cref="IVectorAssociationSyntax"/>
internal sealed record class VectorAssociationSyntax : IVectorAssociationSyntax
{
    private Location Vector { get; }

    /// <summary>Instantiates a <see cref="VectorAssociationSyntax"/>, representing syntactical information about a parsed <see cref="VectorAssociationAttribute{TVector}"/>.</summary>
    /// <param name="vector"><inheritdoc cref="IVectorAssociationSyntax.Vector" path="/summary"/></param>
    public VectorAssociationSyntax(Location vector)
    {
        Vector = vector;
    }

    Location IVectorAssociationSyntax.Vector => Vector;
}
