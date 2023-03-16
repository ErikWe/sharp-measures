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
        get => valueElementsField;
        init => valueElementsField = value.AsReadOnlyEquatable();
    }

    public MinimalLocation? ExpressionCollection { get; init; }
    public IReadOnlyList<MinimalLocation> ExpressionElements
    {
        get => expressionElementsField;
        init => expressionElementsField = value.AsReadOnlyEquatable();
    }

    public bool ExplicitlySetValue => ValueCollection is not null;
    public bool ExplicitlySetExpressions => ExpressionCollection is not null;

    private readonly IReadOnlyList<MinimalLocation> valueElementsField = ReadOnlyEquatableList<MinimalLocation>.Empty;
    private readonly IReadOnlyList<MinimalLocation> expressionElementsField = ReadOnlyEquatableList<MinimalLocation>.Empty;

    protected override VectorConstantLocations Locations => this;

    private VectorConstantLocations() { }
}
