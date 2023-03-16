namespace SharpMeasures.Generators.Quantities.Parsing.QuantityProcess;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Attributes.Parsing;

using System.Collections.Generic;

public sealed record class QuantityProcessDefinition : AAttributeDefinition<IQuantityProcessLocations>, IQuantityProcess
{
    public string Name { get; }
    public NamedType? Result { get; }
    public string Expression { get; }

    public bool ImplementAsProperty { get; }
    public bool ImplementStatically { get; }

    public IReadOnlyList<NamedType> ParameterTypes { get; }
    public IReadOnlyList<string> ParameterNames { get; }

    public QuantityProcessDefinition(string name, NamedType? result, string expression, bool implementAsProperty, bool implementStatically, IReadOnlyList<NamedType> parameterTypes, IReadOnlyList<string> parameterNames, IQuantityProcessLocations locations) : base(locations)
    {
        Name = name;
        Result = result;
        Expression = expression;

        ImplementAsProperty = implementAsProperty;
        ImplementStatically = implementStatically;

        ParameterTypes = parameterTypes.AsReadOnlyEquatable();
        ParameterNames = parameterNames.AsReadOnlyEquatable();
    }
}
