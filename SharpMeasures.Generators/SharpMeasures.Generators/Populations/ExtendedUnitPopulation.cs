namespace SharpMeasures.Generators.Populations;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Units;

using System.Collections.Generic;

internal class ExtendedUnitPopulation : IUnitPopulation
{
    public static ExtendedUnitPopulation Build(IUnitPopulation originalPopulation, IReadOnlyList<IUnitType> additionalUnits)
    {
        Dictionary<NamedType, IUnitType> unitPopulation = new(originalPopulation.Units.Count + additionalUnits.Count);

        foreach (var keyValue in originalPopulation.Units)
        {
            unitPopulation.Add(keyValue.Key, keyValue.Value);
        }

        foreach (var unit in additionalUnits)
        {
            unitPopulation.TryAdd(unit.Type.AsNamedType(), unit);
        }

        return new(unitPopulation);
    }

    public IReadOnlyDictionary<NamedType, IUnitType> Units => units;

    private ReadOnlyEquatableDictionary<NamedType, IUnitType> units { get; }

    public ExtendedUnitPopulation(IReadOnlyDictionary<NamedType, IUnitType> units)
    {
        this.units = units.AsReadOnlyEquatable();
    }
}
