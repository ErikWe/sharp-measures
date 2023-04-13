namespace SharpMeasures.Generators.Parsing.Quantities.QuantitySumAttribute;

using Microsoft.CodeAnalysis;

/// <inheritdoc cref="IQuantitySumSyntax"/>
internal sealed record class QuantitySumSyntax : IQuantitySumSyntax
{
    private Location Sum { get; }

    /// <summary>Instantiates a <see cref="QuantitySumSyntax"/>, representing syntactical information about a parsed <see cref="QuantitySumAttribute{TSum}"/>.</summary>
    /// <param name="sum"><inheritdoc cref="IQuantitySumSyntax.Sum" path="/summary"/></param>
    public QuantitySumSyntax(Location sum)
    {
        Sum = sum;
    }

    Location IQuantitySumSyntax.Sum => Sum;
}
