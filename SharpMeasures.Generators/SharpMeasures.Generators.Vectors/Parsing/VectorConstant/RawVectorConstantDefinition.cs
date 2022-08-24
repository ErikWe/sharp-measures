namespace SharpMeasures.Generators.Vectors.Parsing.VectorConstant;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Quantities.Parsing.QuantityConstant;

using System.Collections.Generic;

internal record class RawVectorConstantDefinition : ARawQuantityConstantDefinition<RawVectorConstantDefinition, VectorConstantLocations>
{
    public static RawVectorConstantDefinition Empty { get; } = new();

    public IReadOnlyList<double> Value
    {
        get => value;
        init => this.value = value.AsReadOnlyEquatable();
    }

    private ReadOnlyEquatableList<double> value { get; init; } = ReadOnlyEquatableList<double>.Empty;

    protected override RawVectorConstantDefinition Definition => this;

    private RawVectorConstantDefinition() : base(VectorConstantLocations.Empty) { }
}
