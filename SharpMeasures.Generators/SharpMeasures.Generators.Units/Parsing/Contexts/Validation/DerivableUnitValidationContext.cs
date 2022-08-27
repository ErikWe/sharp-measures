namespace SharpMeasures.Generators.Units.Parsing.Contexts.Validation;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Units.Parsing.DerivableUnit;
using SharpMeasures.Generators.Units;

internal record class DerivableUnitValidationContext : SimpleProcessingContext, IDerivableUnitValidationContext
{
    public IUnitPopulation UnitPopulation { get; }

    public DerivableUnitValidationContext(DefinedType type, IUnitPopulation unitPopulation) : base(type)
    {
        UnitPopulation = unitPopulation;
    }
}
