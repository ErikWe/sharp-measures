namespace SharpMeasures.Generators.Quantities.Parsing.ProcessedQuantity;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Attributes.Parsing;

using System.Collections.Generic;

public sealed record class ProcessedQuantityDefinition : AAttributeDefinition<IProcessedQuantityLocations>, IProcessedQuantity
{
    public string Name { get; }
    public NamedType? Result { get; }
    public string Expression { get; }

    public bool ImplementAsProperty { get; }
    public bool ImplementStatically { get; }

    public bool ResultsInCurrentType { get; }

    public IReadOnlyList<NamedType> ParameterTypes { get; }
    public IReadOnlyList<string> ParameterNames { get; }

    public ProcessedQuantityDefinition(string name, NamedType? result, string expression, bool implementAsProperty, bool implementStatically, bool resultsInCurrentType, IReadOnlyList<NamedType> parameterTypes, IReadOnlyList<string> parameterNames, IProcessedQuantityLocations locations) : base(locations)
    {
        Name = name;
        Result = result;
        Expression = expression;

        ImplementAsProperty = implementAsProperty;
        ImplementStatically = implementStatically;

        ResultsInCurrentType = resultsInCurrentType;

        ParameterTypes = parameterTypes.AsReadOnlyEquatable();
        ParameterNames = parameterNames.AsReadOnlyEquatable();
    }
}
