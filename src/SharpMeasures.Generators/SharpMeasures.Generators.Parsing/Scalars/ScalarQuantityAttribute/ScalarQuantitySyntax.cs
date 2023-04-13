namespace SharpMeasures.Generators.Parsing.Scalars.ScalarQuantityAttribute;

using Microsoft.CodeAnalysis;

/// <inheritdoc cref="IScalarQuantitySyntax"/>
internal sealed record class ScalarQuantitySyntax : IScalarQuantitySyntax
{
    private Location Unit { get; }
    private Location AllowNegative { get; }
    private Location UseUnitBias { get; }
    private Location ImplementSum { get; }
    private Location ImplementDifference { get; }

    /// <summary>Instantiates a <see cref="ScalarQuantitySyntax"/>, representing syntactical information about a parsed <see cref="ScalarQuantityAttribute{TUnit}"/>.</summary>
    /// <param name="unit"><inheritdoc cref="IScalarQuantitySyntax.Unit" path="/summary"/></param>
    /// <param name="allowNegative"><inheritdoc cref="IScalarQuantitySyntax.AllowNegative" path="/summary"/></param>
    /// <param name="useUnitBias"><inheritdoc cref="IScalarQuantitySyntax.UseUnitBias" path="/summary"/></param>
    /// <param name="implementSum"><inheritdoc cref="IScalarQuantitySyntax.ImplementSum" path="/summary"/></param>
    /// <param name="implementDifference"><inheritdoc cref="IScalarQuantitySyntax.ImplementDifference" path="/summary"/></param>
    public ScalarQuantitySyntax(Location unit, Location allowNegative, Location useUnitBias, Location implementSum, Location implementDifference)
    {
        Unit = unit;
        AllowNegative = allowNegative;
        UseUnitBias = useUnitBias;
        ImplementSum = implementSum;
        ImplementDifference = implementDifference;
    }

    Location IScalarQuantitySyntax.Unit => Unit;
    Location IScalarQuantitySyntax.AllowNegative => AllowNegative;
    Location IScalarQuantitySyntax.UseUnitBias => UseUnitBias;
    Location IScalarQuantitySyntax.ImplementSum => ImplementSum;
    Location IScalarQuantitySyntax.ImplementDifference => ImplementDifference;
}
