namespace SharpMeasures.Generators.Parsing.Quantities.QuantityDifferenceAttribute;

using Microsoft.CodeAnalysis;

/// <inheritdoc cref="IRawQuantityDifference"/>
internal sealed record class RawQuantityDifference : IRawQuantityDifference
{
    private ITypeSymbol Difference { get; }

    private IQuantityDifferenceSyntax? Syntax { get; }

    /// <summary>Instantiates a <see cref="RawQuantityDifference"/>, representing a parsed <see cref="QuantityDifferenceAttribute{TDifference}"/>.</summary>
    /// <param name="difference"><inheritdoc cref="IRawQuantityDifference.Difference" path="/summary"/></param>
    /// <param name="syntax"><inheritdoc cref="IRawQuantityDifference.Syntax" path="/summary"/></param>
    public RawQuantityDifference(ITypeSymbol difference, IQuantityDifferenceSyntax? syntax)
    {
        Difference = difference;

        Syntax = syntax;
    }

    ITypeSymbol IRawQuantityDifference.Difference => Difference;

    IQuantityDifferenceSyntax? IRawQuantityDifference.Syntax => Syntax;
}
