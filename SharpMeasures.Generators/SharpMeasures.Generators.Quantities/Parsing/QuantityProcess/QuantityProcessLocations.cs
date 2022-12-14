namespace SharpMeasures.Generators.Quantities.Parsing.QuantityProcess;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Attributes.Parsing;

using System.Collections.Generic;

public sealed record class QuantityProcessLocations : AAttributeLocations<QuantityProcessLocations>, IQuantityProcessLocations
{
    public static QuantityProcessLocations Empty { get; } = new();

    public MinimalLocation? Name { get; init; }
    public MinimalLocation? Result { get; init; }
    public MinimalLocation? Expression { get; init; }

    public MinimalLocation? ImplementAsProperty { get; init; }
    public MinimalLocation? ImplementStatically { get; init; }

    public MinimalLocation? ParameterTypesCollection { get; init; }
    public IReadOnlyList<MinimalLocation> ParameterTypeElements
    {
        get => parameterTypeElementsField;
        init => parameterTypeElementsField = value.AsReadOnlyEquatable();
    }

    public MinimalLocation? ParameterNamesCollection { get; init; }
    public IReadOnlyList<MinimalLocation> ParameterNameElements
    {
        get => parameterNameElementsField;
        init => parameterNameElementsField = value.AsReadOnlyEquatable();
    }

    public bool ExplicitlySetName => Name is not null;
    public bool ExplicitlySetResult => Result is not null;
    public bool ExplicitlySetExpression => Expression is not null;

    public bool ExplicitlySetImplementAsProperty => ImplementAsProperty is not null;
    public bool ExplicitlySetImplementStatically => ImplementStatically is not null;

    public bool ExplicitlySetParameterTypes => ParameterTypesCollection is not null;
    public bool ExplicitlySetParameterNames => ParameterNamesCollection is not null;

    private readonly IReadOnlyList<MinimalLocation> parameterTypeElementsField = ReadOnlyEquatableList<MinimalLocation>.Empty;
    private readonly IReadOnlyList<MinimalLocation> parameterNameElementsField = ReadOnlyEquatableList<MinimalLocation>.Empty;

    protected override QuantityProcessLocations Locations => this;

    private QuantityProcessLocations() { }
}
