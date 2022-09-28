namespace SharpMeasures.Generators.Quantities.Parsing.ProcessedQuantity;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Attributes.Parsing;

using System.Collections.Generic;

public sealed record class RawProcessedQuantityDefinition : ARawAttributeDefinition<RawProcessedQuantityDefinition, ProcessedQuantityLocations>
{
    public static RawProcessedQuantityDefinition Empty { get; } = new();

    public string? Name { get; init; }
    public NamedType? Result { get; init; }
    public string? Expression { get; init; }

    public bool ImplementAsProperty { get; init; }
    public bool ImplementStatically { get; init; }

    public IReadOnlyList<NamedType?> ParameterTypes
    {
        get => parameterTypes;
        init => parameterTypes = value.AsReadOnlyEquatable();
    }

    public IReadOnlyList<string?> ParameterNames
    {
        get => parameterNames;
        init => parameterNames = value.AsReadOnlyEquatable();
    }

    private IReadOnlyList<NamedType?> parameterTypes { get; init; } = ReadOnlyEquatableList<NamedType?>.Empty;
    private IReadOnlyList<string?> parameterNames { get; init; } = ReadOnlyEquatableList<string?>.Empty;

    protected override RawProcessedQuantityDefinition Definition => this;

    private RawProcessedQuantityDefinition() : base(ProcessedQuantityLocations.Empty) { }
}
