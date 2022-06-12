﻿namespace SharpMeasures.Generators.Scalars.Parsing.ScalarConstant;

using SharpMeasures.Generators.Attributes.Parsing;

internal record class RawScalarConstantDefinition : ARawAttributeDefinition<ScalarConstantLocations>
{
    public static RawScalarConstantDefinition Empty { get; } = new();

    public string? Name { get; init; }
    public string? Unit { get; init; }
    public double Value { get; init; }

    public bool GenerateMultiplesProperty { get; init; }
    public string? MultiplesName { get; init; }

    public ScalarConstantParsingData ParsingData { get; init; } = ScalarConstantParsingData.Empty;

    private RawScalarConstantDefinition() : base(ScalarConstantLocations.Empty) { }
}