namespace SharpMeasures.Generators.Parsing.Vectors;

using Microsoft.CodeAnalysis;

/// <summary>Represents a parsed <see cref="ScalarAssociationAttribute{TScalar}"/>.</summary>
public interface IRawScalarAssociation
{
    /// <summary>The scalar quantity associated with the vector quantity.</summary>
    public abstract ITypeSymbol Scalar { get; }

    /// <summary>Dictates whether the scalar quantity should be used to describe each Cartesian component of the vector quantity.</summary>
    public abstract bool? AsComponents { get; }

    /// <summary>Dictates whether the scalar quantity should be used to decribe the magnitude of the vector quantity.</summary>
    public abstract bool? AsMagnitude { get; }

    /// <summary>Provides syntactical information about the parsed <see cref="ScalarAssociationAttribute{TScalar}"/>.</summary>
    public abstract IScalarAssociationSyntax? Syntax { get; }
}
