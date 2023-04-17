namespace SharpMeasures.Generators.Parsing.Quantities;

using Microsoft.CodeAnalysis;

/// <summary>Represents syntactical information about a parsed <see cref="QuantityDifferenceAttribute{TDifference}"/>.</summary>
public interface IQuantityDifferenceSyntax : IAttributeSyntax
{
    /// <summary>The <see cref="Location"/> of the argument for the quantity that is the difference between two instances of the implementing quantity.</summary>
    public abstract Location Difference { get; }
}
