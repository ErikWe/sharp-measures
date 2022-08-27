namespace SharpMeasures.Generators.Scalars.Parsing.Contexts.Validation;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Scalars.Parsing.ConvertibleScalar;

using System.Collections.Generic;

internal record class ConvertibleScalarFilteringContext : SimpleProcessingContext, IConvertibleScalarFilteringContext
{
    public bool UseUnitBias { get; }
    public IScalarPopulation ScalarPopulation { get; }

    public HashSet<NamedType> InheritedConversions { get; }

    public ConvertibleScalarFilteringContext(DefinedType type, bool useUnitBias, IScalarPopulation scalarPopulation, HashSet<NamedType> inheritedConvertibleScalars) : base(type)
    {
        UseUnitBias = useUnitBias;
        ScalarPopulation = scalarPopulation;

        InheritedConversions = inheritedConvertibleScalars;
    }
}
