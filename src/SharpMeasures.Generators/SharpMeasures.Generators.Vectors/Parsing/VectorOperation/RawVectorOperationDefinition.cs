namespace SharpMeasures.Generators.Vectors.Parsing.VectorOperation;

using SharpMeasures.Generators;
using SharpMeasures.Generators.Attributes.Parsing;

internal sealed record class RawVectorOperationDefinition : ARawAttributeDefinition<RawVectorOperationDefinition, VectorOperationLocations>
{
    public static RawVectorOperationDefinition FromSymbolic(SymbolicVectorOperationDefinition symbolicDefinition) => new RawVectorOperationDefinition(symbolicDefinition.Locations) with
    {
        Result = symbolicDefinition.Result?.AsNamedType(),
        Other = symbolicDefinition.Other?.AsNamedType(),
        OperatorType = symbolicDefinition.OperatorType,
        Position = symbolicDefinition.Position,
        Mirror = symbolicDefinition.Mirror,
        Name = symbolicDefinition.Name,
        MirroredName = symbolicDefinition.MirroredName
    };

    public NamedType? Result { get; init; }
    public NamedType? Other { get; init; }

    public VectorOperatorType OperatorType { get; init; }
    public OperatorPosition Position { get; init; } = OperatorPosition.Left;

    public bool Mirror { get; init; }

    public string? Name { get; init; }
    public string? MirroredName { get; init; }

    protected override RawVectorOperationDefinition Definition => this;

    private RawVectorOperationDefinition(VectorOperationLocations locations) : base(locations) { }
}
