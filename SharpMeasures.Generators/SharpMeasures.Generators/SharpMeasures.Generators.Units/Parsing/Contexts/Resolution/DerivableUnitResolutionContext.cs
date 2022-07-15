namespace SharpMeasures.Generators.Units.Parsing.Contexts.Resolution;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Units.Parsing.DerivableUnit;
using SharpMeasures.Generators.Unresolved.Units;

internal record class DerivableUnitResolutionContext : SimpleProcessingContext, IDerivableUnitResolutionContext
{
    public IUnresolvedUnitPopulation UnitPopulation { get; }

    public DerivableUnitResolutionContext(DefinedType type, IUnresolvedUnitPopulation unitPopulation) : base(type)
    {
        UnitPopulation = unitPopulation;
    }
}
