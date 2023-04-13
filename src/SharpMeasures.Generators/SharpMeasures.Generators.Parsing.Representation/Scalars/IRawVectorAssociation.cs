namespace SharpMeasures.Generators.Parsing.Scalars;

using Microsoft.CodeAnalysis;

/// <summary>Represents a parsed <see cref="VectorAssociationAttribute{TVector}"/>.</summary>
public interface IRawVectorAssociation
{
    /// <summary>The vector quantity associated with the scalar quantity.</summary>
    public abstract ITypeSymbol Vector { get; }

    /// <summary>Provides syntactical information about the parsed <see cref="VectorAssociationAttribute{TVector}"/>.</summary>
    public abstract IVectorAssociationSyntax? Syntax { get; }
}
