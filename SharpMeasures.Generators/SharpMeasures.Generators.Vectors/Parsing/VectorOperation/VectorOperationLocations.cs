namespace SharpMeasures.Generators.Vectors.Parsing.VectorOperation;

using SharpMeasures.Generators.Attributes.Parsing;

internal sealed record class VectorOperationLocations : AAttributeLocations<VectorOperationLocations>, IVectorOperationLocations
{
    public static VectorOperationLocations Empty { get; } = new();

    public MinimalLocation? Result { get; init; }
    public MinimalLocation? Other { get; init; }

    public MinimalLocation? OperatorType { get; init; }
    public MinimalLocation? Position { get; init; }

    public MinimalLocation? Mirror { get; init; }

    public MinimalLocation? Name { get; init; }
    public MinimalLocation? MirroredName { get; init; }

    public bool ExplicitlySetResult => Result is not null;
    public bool ExplicitlySetOther => Other is not null;

    public bool ExplicitlySetOperatorType => OperatorType is not null;
    public bool ExplicitlySetPosition => Position is not null;

    public bool ExplicitlySetMirror => Mirror is not null;

    public bool ExplicitlySetName => Name is not null;
    public bool ExplicitlySetMirroredName => MirroredName is not null;

    protected override VectorOperationLocations Locations => this;

    private VectorOperationLocations() { }
}
