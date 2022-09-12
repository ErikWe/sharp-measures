namespace SharpMeasures.Generators.Units;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Units.Parsing.Abstractions;

using System.Collections.Generic;
using System.Collections.Immutable;

internal class UnitPopulation : IUnitPopulationWithData
{
    public static UnitPopulation Build(ImmutableArray<IUnitType> units)
    {
        Dictionary<NamedType, IUnitType> unitPopulation = new(units.Length);

        Dictionary<NamedType, IUnitType> duplicatePopulation = new();

        foreach (var unit in units)
        {
            if (unitPopulation.TryAdd(unit.Type.AsNamedType(), unit))
            {
                continue;
            }

            duplicatePopulation.TryAdd(unit.Type.AsNamedType(), unit);
        }

        return new(unitPopulation, duplicatePopulation);
    }

    public IReadOnlyDictionary<NamedType, IUnitType> Units => units;
    public IReadOnlyDictionary<NamedType, IUnitType> DuplicatelyDefinedUnits => duplicatelyDefinedUnits;

    private ReadOnlyEquatableDictionary<NamedType, IUnitType> units { get; }
    private ReadOnlyEquatableDictionary<NamedType, IUnitType> duplicatelyDefinedUnits { get; }

    public UnitPopulation(IReadOnlyDictionary<NamedType, IUnitType> units, IReadOnlyDictionary<NamedType, IUnitType> duplicatelyDefinedUnits)
    {
        this.units = units.AsReadOnlyEquatable();
        this.duplicatelyDefinedUnits = duplicatelyDefinedUnits.AsReadOnlyEquatable();
    }
}
