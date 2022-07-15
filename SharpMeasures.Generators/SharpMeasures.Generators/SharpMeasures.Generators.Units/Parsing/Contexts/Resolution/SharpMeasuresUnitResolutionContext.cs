namespace SharpMeasures.Generators.Units.Parsing.Contexts.Resolution;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Units.Parsing.SharpMeasuresUnit;
using SharpMeasures.Generators.Unresolved.Scalars;

internal record class SharpMeasuresUnitResolutionContext : SimpleProcessingContext, ISharpMeasuresUnitResolutionContext
{
    public IUnresolvedScalarPopulation ScalarPopulation { get; }

    public SharpMeasuresUnitResolutionContext(DefinedType type, IUnresolvedScalarPopulation scalarPopulation) : base(type)
    {
        ScalarPopulation = scalarPopulation;
    }
}
