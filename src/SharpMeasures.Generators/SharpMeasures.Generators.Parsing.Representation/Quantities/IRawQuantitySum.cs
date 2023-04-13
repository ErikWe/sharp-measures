namespace SharpMeasures.Generators.Parsing.Quantities;

using Microsoft.CodeAnalysis;

/// <summary>Represents a parsed <see cref="QuantitySumAttribute{TSum}"/>.</summary>
public interface IRawQuantitySum
{
    /// <summary>The quantity that represents the sum of two instances of the implementing quantity.</summary>
    public abstract ITypeSymbol Sum { get; }

    /// <summary>Provides syntactical information about the parsed <see cref="QuantitySumAttribute{TSum}"/>.</summary>
    public abstract IQuantitySumSyntax? Syntax { get; }
}
