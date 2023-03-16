namespace SharpMeasures.Generators.Quantities.Parsing.QuantityOperation;

using SharpMeasures.Generators;
using SharpMeasures.Generators.Attributes.Parsing;

public sealed record class QuantityOperationDefinition : AAttributeDefinition<IQuantityOperationLocations>, IQuantityOperation
{
    public NamedType Result { get; }
    public NamedType Other { get; }

    public OperatorType OperatorType { get; }
    public OperatorPosition Position { get; }

    public bool Mirror { get; }
    public bool MirrorMethod { get; }

    public QuantityOperationImplementation Implementation { get; }

    public string MethodName { get; }
    public string MirroredMethodName { get; }

    public QuantityOperationDefinition(NamedType result, NamedType other, OperatorType operatorType, OperatorPosition position, bool mirror, bool mirrorMethod, QuantityOperationImplementation implementation, string methodName, string mirroredMethodName, QuantityOperationLocations locations) : base(locations)
    {
        Result = result;
        Other = other;

        OperatorType = operatorType;
        Position = position;

        Mirror = mirror;
        MirrorMethod = mirrorMethod;

        Implementation = implementation;

        MethodName = methodName;
        MirroredMethodName = mirroredMethodName;
    }
}
