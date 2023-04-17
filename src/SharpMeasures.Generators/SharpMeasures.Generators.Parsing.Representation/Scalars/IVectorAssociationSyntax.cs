namespace SharpMeasures.Generators.Parsing.Scalars;

using Microsoft.CodeAnalysis;

/// <summary>Represents syntactical information about a parsed <see cref="VectorAssociationAttribute{TVector}"/>.</summary>
public interface IVectorAssociationSyntax : IAttributeSyntax
{
    /// <summary>The <see cref="Location"/> of the argument for the vector quantity associated with the scalar quantity.</summary>
    public abstract Location Vector { get; }
}
