namespace SharpMeasures.Generators.Parsing.Scalars.UnitlessQuantityAttribute;

using Microsoft.CodeAnalysis;

using SharpMeasures;

/// <inheritdoc cref="IUnitlessQuantitySyntax"/>
internal sealed record class UnitlessQuantitySyntax : IUnitlessQuantitySyntax
{
    private Location AllowNegative { get; }
    private Location ImplementSum { get; }
    private Location ImplementDifference { get; }

    /// <summary>Instantiates a <see cref="UnitlessQuantitySyntax"/>, representing syntactical information about a parsed <see cref="UnitlessQuantityAttribute"/>.</summary>
    /// <param name="allowNegative"><inheritdoc cref="IUnitlessQuantitySyntax.AllowNegative" path="/summary"/></param>
    /// <param name="implementSum"><inheritdoc cref="IUnitlessQuantitySyntax.ImplementSum" path="/summary"/></param>
    /// <param name="implementDifference"><inheritdoc cref="IUnitlessQuantitySyntax.ImplementDifference" path="/summary"/></param>
    public UnitlessQuantitySyntax(Location allowNegative, Location implementSum, Location implementDifference)
    {
        AllowNegative = allowNegative;
        ImplementSum = implementSum;
        ImplementDifference = implementDifference;
    }

    Location IUnitlessQuantitySyntax.AllowNegative => AllowNegative;
    Location IUnitlessQuantitySyntax.ImplementSum => ImplementSum;
    Location IUnitlessQuantitySyntax.ImplementDifference => ImplementDifference;
}
