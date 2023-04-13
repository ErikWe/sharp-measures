namespace SharpMeasures.Generators.Parsing.Vectors.VectorQuantityAttribute;

using Microsoft.CodeAnalysis;

using SharpMeasures;

/// <inheritdoc cref="IRawVectorQuantity"/>
internal sealed record class RawVectorQuantity : IRawVectorQuantity
{
    private ITypeSymbol Unit { get; }

    private int? Dimension { get; }

    private bool? ImplementSum { get; }
    private bool? ImplementDifference { get; }

    private IVectorQuantitySyntax? Syntax { get; }

    /// <summary>Instantiates a <see cref="RawVectorQuantity"/>, representing a parsed <see cref="VectorQuantityAttribute{TUnit}"/>.</summary>
    /// <param name="unit"><inheritdoc cref="IRawVectorQuantity.Unit" path="/summary"/></param>
    /// <param name="dimension"><inheritdoc cref="IRawVectorQuantity.Dimension" path="/summary"/></param>
    /// <param name="implementSum"><inheritdoc cref="IRawVectorQuantity.ImplementSum" path="/summary"/></param>
    /// <param name="implementDifference"><inheritdoc cref="IRawVectorQuantity.ImplementDifference" path="/summary"/></param>
    /// <param name="syntax"><inheritdoc cref="IRawVectorQuantity.Syntax" path="/summary"/></param>
    public RawVectorQuantity(ITypeSymbol unit, int? dimension, bool? implementSum, bool? implementDifference, IVectorQuantitySyntax? syntax)
    {
        Unit = unit;

        Dimension = dimension;

        ImplementSum = implementSum;
        ImplementDifference = implementDifference;

        Syntax = syntax;
    }

    ITypeSymbol IRawVectorQuantity.Unit => Unit;

    int? IRawVectorQuantity.Dimension => Dimension;

    bool? IRawVectorQuantity.ImplementSum => ImplementSum;
    bool? IRawVectorQuantity.ImplementDifference => ImplementDifference;

    IVectorQuantitySyntax? IRawVectorQuantity.Syntax => Syntax;
}
