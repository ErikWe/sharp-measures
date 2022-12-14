namespace SharpMeasures.Generators.Quantities.Parsing.QuantityProcess;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Attributes.Parsing;

using System.Collections.Generic;

public sealed record class RawQuantityProcessDefinition : ARawAttributeDefinition<RawQuantityProcessDefinition, QuantityProcessLocations>
{
    public static RawQuantityProcessDefinition Empty { get; } = new();

    public string? Name { get; init; }
    public NamedType? Result { get; init; }
    public string? Expression { get; init; }

    public bool ImplementAsProperty { get; init; }
    public bool ImplementStatically { get; init; }

    public IReadOnlyList<NamedType?> ParameterTypes
    {
        get => parameterTypesField;
        init => parameterTypesField = value.AsReadOnlyEquatable();
    }

    public IReadOnlyList<string?> ParameterNames
    {
        get => parameterNamesField;
        init => parameterNamesField = value.AsReadOnlyEquatable();
    }

    private readonly IReadOnlyList<NamedType?> parameterTypesField = ReadOnlyEquatableList<NamedType?>.Empty;
    private readonly IReadOnlyList<string?> parameterNamesField = ReadOnlyEquatableList<string?>.Empty;

    protected override RawQuantityProcessDefinition Definition => this;

    private RawQuantityProcessDefinition() : base(QuantityProcessLocations.Empty) { }
}
