namespace SharpMeasures.Generators.Parsing.Quantities.QuantityDifferenceAttribute;

using Microsoft.CodeAnalysis;

/// <inheritdoc cref="IQuantityDifferenceSyntax"/>
internal sealed record class QuantityDifferenceSyntax : IQuantityDifferenceSyntax
{
    private Location Difference { get; }

    /// <summary>Instantiates a <see cref="QuantityDifferenceSyntax"/>, representing syntactical information about a parsed <see cref="QuantityDifferenceAttribute{TDifference}"/>.</summary>
    /// <param name="difference"><inheritdoc cref="IQuantityDifferenceSyntax.Difference" path="/summary"/></param>
    public QuantityDifferenceSyntax(Location difference)
    {
        Difference = difference;
    }

    Location IQuantityDifferenceSyntax.Difference => Difference;
}
