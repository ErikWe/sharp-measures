namespace SharpMeasures.Generators.Parsing.Vectors;

using Microsoft.CodeAnalysis;

/// <summary>Represents a parsed <see cref="SpecializedVectorQuantityAttribute{TOriginal}"/>.</summary>
public interface IRawSpecializedVectorQuantity
{
    /// <summary>The <see cref="ITypeSymbol"/> of the original vector quantity, of which this quantity is a specialized form.</summary>
    public abstract ITypeSymbol Original { get; }

    /// <summary>Dictates whether the quantity should inherit the operations defined by the original quantity.</summary>
    public abstract bool? InheritOperations { get; }

    /// <summary>Dictates whether the quantity should inherit the processes defined by the original quantity.</summary>
    public abstract bool? InheritProcesses { get; }

    /// <summary>Dictates whether the quantity should inherit the properties defined by the original quantity.</summary>
    public abstract bool? InheritProperties { get; }

    /// <summary>Dictates whether the quantity should inherit the constants defined by the original quantity.</summary>
    public abstract bool? InheritConstants { get; }

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

    /// <summary>Provides syntactical information about the parsed <see cref="SpecializedVectorQuantityAttribute{TOriginal}"/>.</summary>
    public abstract ISpecializedVectorQuantitySyntax? Syntax { get; }
}
