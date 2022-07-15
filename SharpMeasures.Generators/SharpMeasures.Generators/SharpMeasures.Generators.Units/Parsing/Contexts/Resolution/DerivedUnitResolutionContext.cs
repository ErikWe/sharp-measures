namespace SharpMeasures.Generators.Units.Parsing.Contexts.Resolution;

using SharpMeasures.Generators.Units.Parsing.DerivedUnit;
using SharpMeasures.Generators.Unresolved.Units;
using SharpMeasures.Generators.Unresolved.Units.UnitInstances;

using System.Collections.Generic;

internal record class DerivedUnitResolutionContext : DependantUnitResolutionContext, IDerivedUnitResolutionContext
{
    public IReadOnlyDictionary<string, IUnresolvedDerivableUnit> DerivationsByID { get; }

    public IUnresolvedUnitPopulation UnitPopulation { get; }

    public DerivedUnitResolutionContext(DefinedType type, IReadOnlyDictionary<string, IUnresolvedUnitInstance> unitsByName,
        IReadOnlyDictionary<string, IUnresolvedDerivableUnit> derivationsByID, IUnresolvedUnitPopulation unitPopulation) : base(type, unitsByName)
    {
        DerivationsByID = derivationsByID;

        UnitPopulation = unitPopulation;
    }
}
