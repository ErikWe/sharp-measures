namespace SharpMeasures.Generators.Parsing.Scalars.UnitlessQuantityAttribute;

using SharpMeasures;

/// <inheritdoc cref="IRawUnitlessQuantity"/>
internal sealed record class RawUnitlessQuantity : IRawUnitlessQuantity
{
    private bool? AllowNegative { get; }
    private bool? ImplementSum { get; }
    private bool? ImplementDifference { get; }

    private IUnitlessQuantitySyntax? Syntax { get; }

    /// <summary>Instantiates a <see cref="RawUnitlessQuantity"/>, representing a parsed <see cref="UnitlessQuantityAttribute"/>.</summary>
    /// <param name="allowNegative"><inheritdoc cref="IRawUnitlessQuantity.AllowNegative" path="/summary"/></param>
    /// <param name="implementSum"><inheritdoc cref="IRawUnitlessQuantity.ImplementSum" path="/summary"/></param>
    /// <param name="implementDifference"><inheritdoc cref="IRawUnitlessQuantity.ImplementDifference" path="/summary"/></param>
    /// <param name="syntax"><inheritdoc cref="IRawUnitlessQuantity.Syntax" path="/summary" /></param>
    public RawUnitlessQuantity(bool? allowNegative, bool? implementSum, bool? implementDifference, IUnitlessQuantitySyntax? syntax)
    {
        AllowNegative = allowNegative;
        ImplementSum = implementSum;
        ImplementDifference = implementDifference;

        Syntax = syntax;
    }

    bool? IRawUnitlessQuantity.AllowNegative => AllowNegative;
    bool? IRawUnitlessQuantity.ImplementSum => ImplementSum;
    bool? IRawUnitlessQuantity.ImplementDifference => ImplementDifference;

    IUnitlessQuantitySyntax? IRawUnitlessQuantity.Syntax => Syntax;
}
