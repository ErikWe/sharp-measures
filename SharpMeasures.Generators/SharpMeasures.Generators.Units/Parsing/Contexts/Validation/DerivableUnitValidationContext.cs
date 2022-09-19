namespace SharpMeasures.Generators.Units.Parsing.Contexts.Validation;

using SharpMeasures.Generators.Units.Parsing.DerivableUnit;

internal sealed record class DerivableUnitValidationContext : IDerivableUnitValidationContext
{
    public DefinedType Type { get; }

    public IUnitPopulation UnitPopulation { get; }

    public DerivableUnitValidationContext(DefinedType type, IUnitPopulation unitPopulation)
    {
        Type = type;

        UnitPopulation = unitPopulation;
    }
}
