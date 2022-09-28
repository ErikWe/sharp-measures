namespace SharpMeasures.Generators.Quantities.Parsing.ProcessedQuantity;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Attributes.Parsing;

using System.Collections.Generic;

public sealed record class ProcessedQuantityLocations : AAttributeLocations<ProcessedQuantityLocations>, IProcessedQuantityLocations
{
    public static ProcessedQuantityLocations Empty { get; } = new();

    public MinimalLocation? Name { get; init; }
    public MinimalLocation? Result { get; init; }
    public MinimalLocation? Expression { get; init; }

    public MinimalLocation? ImplementAsProperty { get; init; }
    public MinimalLocation? ImplementStatically { get; init; }

    public MinimalLocation? ParameterTypesCollection { get; init; }
    public IReadOnlyList<MinimalLocation> ParameterTypeElements
    {
        get => parameterTypeElements;
        init => parameterTypeElements = value.AsReadOnlyEquatable();
    }

    public MinimalLocation? ParameterNamesCollection { get; init; }
    public IReadOnlyList<MinimalLocation> ParameterNameElements
    {
        get => parameterNameElements;
        init => parameterNameElements = value.AsReadOnlyEquatable();
    }

    public bool ExplicitlySetName => Name is not null;
    public bool ExplicitlySetResult => Result is not null;
    public bool ExplicitlySetExpression => Expression is not null;

    public bool ExplicitlySetImplementAsProperty => ImplementAsProperty is not null;
    public bool ExplicitlySetImplementStatically => ImplementStatically is not null;

    public bool ExplicitlySetParameterTypes => ParameterTypesCollection is not null;
    public bool ExplicitlySetParameterNames => ParameterNamesCollection is not null;

    private IReadOnlyList<MinimalLocation> parameterTypeElements { get; init; } = ReadOnlyEquatableList<MinimalLocation>.Empty;
    private IReadOnlyList<MinimalLocation> parameterNameElements { get; init; } = ReadOnlyEquatableList<MinimalLocation>.Empty;

    protected override ProcessedQuantityLocations Locations => this;

    private ProcessedQuantityLocations() { }
}
