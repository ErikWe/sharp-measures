namespace SharpMeasures.Generators.Parsing.Quantities;

using Microsoft.CodeAnalysis;

/// <summary>Represents syntactical information about a parsed <see cref="QuantitySumAttribute{TSum}"/>.</summary>
public interface IQuantitySumSyntax
{
    /// <summary>The <see cref="Location"/> of the argument for the quantity that is the sum of two instances of the implementing quantity.</summary>
    public abstract Location Sum { get; }
}
