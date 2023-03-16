namespace SharpMeasures.Generators.Vectors.Parsing.VectorConstant;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Quantities.Parsing.QuantityConstant;

using System.Collections.Generic;

internal sealed record class RawVectorConstantDefinition : ARawQuantityConstantDefinition<RawVectorConstantDefinition, VectorConstantLocations>
{
    public static RawVectorConstantDefinition Empty { get; } = new();

    public IReadOnlyList<double>? Value
    {
        get => valueField;
        init => valueField = value?.AsReadOnlyEquatable();
    }

    public IReadOnlyList<string>? Expressions
    {
        get => expressionsField;
        init => expressionsField = value?.AsReadOnlyEquatable();
    }

    private readonly IReadOnlyList<double>? valueField;
    private readonly IReadOnlyList<string>? expressionsField;

    protected override RawVectorConstantDefinition Definition => this;

    private RawVectorConstantDefinition() : base(VectorConstantLocations.Empty) { }
}
