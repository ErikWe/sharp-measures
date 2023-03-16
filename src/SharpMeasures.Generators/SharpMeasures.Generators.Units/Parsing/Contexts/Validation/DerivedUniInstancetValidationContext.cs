namespace SharpMeasures.Generators.Units.Parsing.Contexts.Validation;

using SharpMeasures.Generators.Units.Parsing.DerivedUnitInstance;

using System.Collections.Generic;

internal sealed record class DerivedUniInstancetValidationContext : IDerivedUnitInstanceValidationContext
{
    public DefinedType Type { get; }

    public IDerivableUnit? UnnamedDerivation { get; }
    public IReadOnlyDictionary<string, IDerivableUnit> DerivationsByID { get; }

    public IUnitPopulation UnitPopulation { get; }

    public DerivedUniInstancetValidationContext(DefinedType type, IDerivableUnit? unnamedDerivation, IReadOnlyDictionary<string, IDerivableUnit> derivationsByID, IUnitPopulation unitPopulation)
    {
        Type = type;

        UnnamedDerivation = unnamedDerivation;
        DerivationsByID = derivationsByID;

        UnitPopulation = unitPopulation;
    }
}
