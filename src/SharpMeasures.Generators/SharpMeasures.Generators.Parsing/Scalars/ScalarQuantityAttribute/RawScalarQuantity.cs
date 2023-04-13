namespace SharpMeasures.Generators.Parsing.Scalars.ScalarQuantityAttribute;

using Microsoft.CodeAnalysis;

/// <inheritdoc cref="IRawScalarQuantity"/>
internal sealed record class RawScalarQuantity : IRawScalarQuantity
{
    private ITypeSymbol Unit { get; }
    private bool? AllowNegative { get; }
    private bool? UseUnitBias { get; }
    private bool? ImplementSum { get; }
    private bool? ImplementDifference { get; }

    private IScalarQuantitySyntax? Syntax { get; }

    /// <summary>Instantiates a <see cref="RawScalarQuantity"/>, representing a parsed <see cref="ScalarQuantityAttribute{TUnit}"/>.</summary>
    /// <param name="unit"><inheritdoc cref="IRawScalarQuantity.Unit" path="/summary"/></param>
    /// <param name="allowNegative"><inheritdoc cref="IRawScalarQuantity.AllowNegative" path="/summary"/></param>
    /// <param name="useUnitBias"><inheritdoc cref="IRawScalarQuantity.UseUnitBias" path="/summary"/></param>
    /// <param name="implementSum"><inheritdoc cref="IRawScalarQuantity.ImplementSum" path="/summary"/></param>
    /// <param name="implementDifference"><inheritdoc cref="IRawScalarQuantity.ImplementDifference" path="/summary"/></param>
    /// <param name="syntax"><inheritdoc cref="IRawScalarQuantity.Syntax" path="/summary"/></param>
    public RawScalarQuantity(ITypeSymbol unit, bool? allowNegative, bool? useUnitBias, bool? implementSum, bool? implementDifference, IScalarQuantitySyntax? syntax)
    {
        Unit = unit;
        AllowNegative = allowNegative;
        UseUnitBias = useUnitBias;
        ImplementSum = implementSum;
        ImplementDifference = implementDifference;

        Syntax = syntax;
    }

    ITypeSymbol IRawScalarQuantity.Unit => Unit;
    bool? IRawScalarQuantity.AllowNegative => AllowNegative;
    bool? IRawScalarQuantity.UseUnitBias => UseUnitBias;
    bool? IRawScalarQuantity.ImplementSum => ImplementSum;
    bool? IRawScalarQuantity.ImplementDifference => ImplementDifference;

    IScalarQuantitySyntax? IRawScalarQuantity.Syntax => Syntax;
}
