namespace SharpMeasures.Generators.Vectors.Parsing.VectorConstant;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Quantities.Parsing.QuantityConstant;

using System.Collections.Generic;

internal sealed record class VectorConstantLocations : AQuantityConstantLocations<VectorConstantLocations>, IVectorConstantLocations
{
    public static VectorConstantLocations Empty { get; } = new();

    public MinimalLocation? ValueCollection { get; init; }
    public IReadOnlyList<MinimalLocation> ValueElements
    {
        get => valueElements;
        init => valueElements = value.AsReadOnlyEquatable();
    }

    public MinimalLocation? ExpressionCollection { get; init; }
    public IReadOnlyList<MinimalLocation> ExpressionElements
    {
        get => expressionElements;
        init => expressionElements = value.AsReadOnlyEquatable();
    }

    public bool ExplicitlySetValue => ValueCollection is not null;
    public bool ExplicitlySetExpressions => ExpressionCollection is not null;

    private IReadOnlyList<MinimalLocation> valueElements { get; init; } = ReadOnlyEquatableList<MinimalLocation>.Empty;
    private IReadOnlyList<MinimalLocation> expressionElements { get; init; } = ReadOnlyEquatableList<MinimalLocation>.Empty;

    protected override VectorConstantLocations Locations => this;

    private VectorConstantLocations() { }
}
