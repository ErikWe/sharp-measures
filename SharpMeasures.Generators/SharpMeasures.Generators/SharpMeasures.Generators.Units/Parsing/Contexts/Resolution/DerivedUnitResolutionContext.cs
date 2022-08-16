namespace SharpMeasures.Generators.Units.Parsing.Contexts.Resolution;

using SharpMeasures.Generators.Units.Parsing.DerivedUnit;
using SharpMeasures.Generators.Raw.Units;
using SharpMeasures.Generators.Raw.Units.UnitInstances;

using System.Collections.Generic;

internal record class DerivedUnitResolutionContext : DependantUnitResolutionContext, IDerivedUnitResolutionContext
{
    public IRawDerivableUnit? UnnamedDerivation { get; }
    public IReadOnlyDictionary<string, IRawDerivableUnit> DerivationsByID { get; }

    public IRawUnitPopulation UnitPopulation { get; }

    public DerivedUnitResolutionContext(DefinedType type, IRawDerivableUnit? unnamedDerivation, IReadOnlyDictionary<string, IRawUnitInstance> unitsByName,
        IReadOnlyDictionary<string, IRawDerivableUnit> derivationsByID, IRawUnitPopulation unitPopulation) : base(type, unitsByName)
    {
        UnnamedDerivation = unnamedDerivation;
        DerivationsByID = derivationsByID;

        UnitPopulation = unitPopulation;
    }
}
