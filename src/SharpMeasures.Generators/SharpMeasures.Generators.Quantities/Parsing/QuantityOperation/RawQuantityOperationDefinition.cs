namespace SharpMeasures.Generators.Quantities.Parsing.QuantityOperation;

using SharpMeasures.Generators;
using SharpMeasures.Generators.Attributes.Parsing;

public sealed record class RawQuantityOperationDefinition : ARawAttributeDefinition<RawQuantityOperationDefinition, QuantityOperationLocations>
{
    public static RawQuantityOperationDefinition FromSymbolic(SymbolicQuantityOperationDefinition symbolicDefinition) => new RawQuantityOperationDefinition(symbolicDefinition.Locations) with
    {
        Result = symbolicDefinition.Result?.AsNamedType(),
        Other = symbolicDefinition.Other?.AsNamedType(),
        OperatorType = symbolicDefinition.OperatorType,
        Position = symbolicDefinition.Position,
        Mirror = symbolicDefinition.Mirror,
        Implementation = symbolicDefinition.Implementation,
        MethodName = symbolicDefinition.MethodName,
        MirroredMethodName = symbolicDefinition.MirroredMethodName
    };

    public NamedType? Result { get; init; }
    public NamedType? Other { get; init; }

    public OperatorType OperatorType { get; init; }
    public OperatorPosition Position { get; init; } = OperatorPosition.Left;

    public bool Mirror { get; init; }

    public QuantityOperationImplementation Implementation { get; init; } = QuantityOperationImplementation.OperatorAndMethod;

    public string? MethodName { get; init; }
    public string? MirroredMethodName { get; init; }

    protected override RawQuantityOperationDefinition Definition => this;

    private RawQuantityOperationDefinition(QuantityOperationLocations locations) : base(locations) { }
}
