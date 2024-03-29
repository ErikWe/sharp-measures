﻿namespace SharpMeasures.Generators.Scalars.Parsing.ScalarConstant;

using SharpMeasures.Generators.Quantities.Parsing.QuantityConstant;

internal sealed record class RawScalarConstantDefinition : ARawQuantityConstantDefinition<RawScalarConstantDefinition, ScalarConstantLocations>
{
    public static RawScalarConstantDefinition Empty { get; } = new();

    public double? Value { get; init; }
    public string? Expression { get; init; }

    protected override RawScalarConstantDefinition Definition => this;

    private RawScalarConstantDefinition() : base(ScalarConstantLocations.Empty) { }
}
