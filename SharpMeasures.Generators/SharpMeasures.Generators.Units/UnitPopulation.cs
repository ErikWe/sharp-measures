namespace SharpMeasures.Generators.Units;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Units.Parsing;

using System.Collections.Generic;

internal class UnitPopulation : IUnitPopulation
{
    public static (UnitPopulation Population, UnitProcessingData ProcessingData) Build(IReadOnlyList<IUnitType> units)
    {
        Dictionary<NamedType, IUnitType> unitPopulation = new(units.Count);

        Dictionary<NamedType, IUnitType> duplicatePopulation = new();

        foreach (var unit in units)
        {
            if (unitPopulation.TryAdd(unit.Type.AsNamedType(), unit))
            {
                continue;
            }

            duplicatePopulation.TryAdd(unit.Type.AsNamedType(), unit);
        }

        return (new UnitPopulation(unitPopulation), new UnitProcessingData(duplicatePopulation));
    }

    public static UnitPopulation BuildWithoutProcessingData(IReadOnlyList<IUnitType> units)
    {
        Dictionary<NamedType, IUnitType> unitPopulation = new(units.Count);

        foreach (var unit in units)
        {
            unitPopulation.TryAdd(unit.Type.AsNamedType(), unit);
        }

        return new(unitPopulation);
    }

    public IReadOnlyDictionary<NamedType, IUnitType> Units => units;

    private ReadOnlyEquatableDictionary<NamedType, IUnitType> units { get; }

    public UnitPopulation(IReadOnlyDictionary<NamedType, IUnitType> units)
    {
        this.units = units.AsReadOnlyEquatable();
    }
}
