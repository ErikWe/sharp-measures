namespace SharpMeasures.Generators.Scalars.Parsing.Contexts.Validation;

using SharpMeasures.Generators.Scalars.Parsing.ConvertibleScalar;

using System.Collections.Generic;

internal sealed record class ConvertibleScalarFilteringContext : IConvertibleScalarFilteringContext
{
    public DefinedType Type { get; }

    public bool UseUnitBias { get; }
    public IScalarPopulation ScalarPopulation { get; }

    public HashSet<NamedType> InheritedConversions { get; }

    public ConvertibleScalarFilteringContext(DefinedType type, bool useUnitBias, IScalarPopulation scalarPopulation, HashSet<NamedType> inheritedConvertibleScalars)
    {
        Type = type;

        UseUnitBias = useUnitBias;
        ScalarPopulation = scalarPopulation;

        InheritedConversions = inheritedConvertibleScalars;
    }
}
