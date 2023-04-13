namespace SharpMeasures.Generators.Parsing.Vectors;

using Microsoft.CodeAnalysis;

/// <summary>Represents a parsed <see cref="SpecializedVectorGroupAttribute{TOriginal}"/>.</summary>
public interface IRawSpecializedVectorGroup
{
    /// <summary>The <see cref="ITypeSymbol"/> of the original group, of which this group is a specialized form.</summary>
    public abstract ITypeSymbol Original { get; }

    /// <summary>Dictates whether the quantity should inherit the operations defined by the original quantity.</summary>
    public abstract bool? InheritOperations { get; }

    /// <summary>Dictates whether the quantity should inherit the conversions defined by the original quantity.</summary>
    public abstract bool? InheritConversions { get; }

    /// <summary>Determines how the conversion operator from the original quantity to this quantity is implemented.</summary>
    public abstract ConversionOperatorBehaviour? ForwardsCastOperatorBehaviour { get; }

    /// <summary>Determines how the conversion operator from this quantity to the original quantity is implemented.</summary>
    public abstract ConversionOperatorBehaviour? BackwardsCastOperatorBehaviour { get; }

    /// <summary>Dictates whether the quantity should support addition of two instances.</summary>
    public abstract bool? ImplementSum { get; }

    /// <summary>Dictates whether the quantity should support subtraction of two instances.</summary>
    public abstract bool? ImplementDifference { get; }

    /// <summary>Provides syntactical information about the parsed <see cref="SpecializedVectorGroupAttribute{TOriginal}"/>.</summary>
    public abstract ISpecializedVectorGroupSyntax? Syntax { get; }
}
