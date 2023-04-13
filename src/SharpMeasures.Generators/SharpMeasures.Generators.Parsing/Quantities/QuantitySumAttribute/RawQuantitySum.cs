namespace SharpMeasures.Generators.Parsing.Quantities.QuantitySumAttribute;

using Microsoft.CodeAnalysis;

/// <inheritdoc cref="IRawQuantitySum"/>
internal sealed record class RawQuantitySum : IRawQuantitySum
{
    private ITypeSymbol Sum { get; }

    private IQuantitySumSyntax? Syntax { get; }

    /// <summary>Instantiates a <see cref="RawQuantitySum"/>, representing a parsed <see cref="QuantitySumAttribute{TSum}"/>.</summary>
    /// <param name="sum"><inheritdoc cref="IRawQuantitySum.Sum" path="/summary"/></param>
    /// <param name="syntax"><inheritdoc cref="IRawQuantitySum.Syntax" path="/summary"/></param>
    public RawQuantitySum(ITypeSymbol sum, IQuantitySumSyntax? syntax)
    {
        Sum = sum;

        Syntax = syntax;
    }

    ITypeSymbol IRawQuantitySum.Sum => Sum;

    IQuantitySumSyntax? IRawQuantitySum.Syntax => Syntax;
}
