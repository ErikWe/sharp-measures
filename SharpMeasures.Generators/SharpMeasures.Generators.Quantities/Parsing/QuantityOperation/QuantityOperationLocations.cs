namespace SharpMeasures.Generators.Quantities.Parsing.QuantityOperation;

using SharpMeasures.Generators.Attributes.Parsing;

public sealed record class QuantityOperationLocations : AAttributeLocations<QuantityOperationLocations>, IQuantityOperationLocations
{
    public static QuantityOperationLocations Empty { get; } = new();

    public MinimalLocation? Result { get; init; }
    public MinimalLocation? Other { get; init; }

    public MinimalLocation? OperatorType { get; init; }
    public MinimalLocation? Position { get; init; }

    public MinimalLocation? Mirror { get; init; }

    public MinimalLocation? Implementation { get; init; }

    public MinimalLocation? MethodName { get; init; }
    public MinimalLocation? MirroredMethodName { get; init; }

    public bool ExplicitlySetResult => Result is not null;
    public bool ExplicitlySetOther => Other is not null;

    public bool ExplicitlySetOperatorType => OperatorType is not null;
    public bool ExplicitlySetPosition => Position is not null;

    public bool ExplicitlySetMirror => Mirror is not null;

    public bool ExplicitlySetImplementation => Implementation is not null;

    public bool ExplicitlySetMethodName => MethodName is not null;
    public bool ExplicitlySetMirroredMethodName => MirroredMethodName is not null;

    protected override QuantityOperationLocations Locations => this;

    private QuantityOperationLocations() { }
}
