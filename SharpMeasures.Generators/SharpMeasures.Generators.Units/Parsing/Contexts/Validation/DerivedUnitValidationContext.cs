namespace SharpMeasures.Generators.Units.Parsing.Contexts.Validation;

using SharpMeasures.Generators.Units.Parsing.DerivedUnit;
using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Units.UnitInstances;

using System.Collections.Generic;

internal record class DerivedUnitValidationContext : DependantUnitValidationContext, IDerivedUnitValidationContext
{
    public IDerivableUnit? UnnamedDerivation { get; }
    public IReadOnlyDictionary<string, IDerivableUnit> DerivationsByID { get; }

    public IUnitPopulation UnitPopulation { get; }

    public DerivedUnitValidationContext(DefinedType type, IDerivableUnit? unnamedDerivation, IReadOnlyDictionary<string, IUnitInstance> unitsByName,
        IReadOnlyDictionary<string, IDerivableUnit> derivationsByID, IUnitPopulation unitPopulation) : base(type, unitsByName)
    {
        UnnamedDerivation = unnamedDerivation;
        DerivationsByID = derivationsByID;

        UnitPopulation = unitPopulation;
    }
}
