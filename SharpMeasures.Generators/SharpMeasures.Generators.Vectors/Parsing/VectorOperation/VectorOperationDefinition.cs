namespace SharpMeasures.Generators.Vectors.Parsing.VectorOperation;

using SharpMeasures.Generators;
using SharpMeasures.Generators.Attributes.Parsing;

internal sealed record class VectorOperationDefinition : AAttributeDefinition<IVectorOperationLocations>, IVectorOperation
{
    public NamedType Result { get; }
    public NamedType Other { get; }

    public VectorOperatorType OperatorType { get; }
    public OperatorPosition Position { get; }

    public bool Mirror { get; }

    public string Name { get; }
    public string MirroredName { get; }

    public VectorOperationDefinition(NamedType result, NamedType other, VectorOperatorType operatorType, OperatorPosition position, bool mirror, string name, string mirroredName, VectorOperationLocations locations) : base(locations)
    {
        Result = result;
        Other = other;

        OperatorType = operatorType;
        Position = position;

        Mirror = mirror;

        Name = name;
        MirroredName = mirroredName;
    }
}
