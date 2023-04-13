namespace SharpMeasures.Generators.Parsing.Vectors;

using Microsoft.CodeAnalysis;

/// <summary>Represents a parsed <see cref="VectorQuantityAttribute{TUnit}"/>.</summary>
public interface IRawVectorQuantity
{
    /// <summary>The <see cref="ITypeSymbol"/> of the unit that describes the quantity.</summary>
    public abstract ITypeSymbol Unit { get; }

    /// <summary>The dimension of the quantity.</summary>
    public abstract int? Dimension { get; }

    /// <summary>Dictates whether the quantity should support addition of two instances.</summary>
    public abstract bool? ImplementSum { get; }

    /// <summary>Dictates whether the quantity should support subtraction of two instances.</summary>
    public abstract bool? ImplementDifference { get; }

    /// <summary>Provides syntactical information about the parsed <see cref="VectorQuantityAttribute{TUnit}"/>.</summary>
    public abstract IVectorQuantitySyntax? Syntax { get; }
}
