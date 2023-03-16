namespace SharpMeasures.Generators.Units.ForeignUnitParsing;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Units;

using System.Collections.Generic;

internal sealed record class ExtendedUnitPopulation : IUnitPopulation
{
    public static ExtendedUnitPopulation Build(IUnitPopulation originalPopulation, ForeignUnitProcessingResult foreignPopulation)
    {
        Dictionary<NamedType, IUnitType> unitPopulation = new(originalPopulation.Units.Count + foreignPopulation.Units.Count);

        foreach (var keyValue in originalPopulation.Units)
        {
            unitPopulation.Add(keyValue.Key, keyValue.Value);
        }

        foreach (var unit in foreignPopulation.Units)
        {
            unitPopulation.TryAdd(unit.Type.AsNamedType(), unit);
        }

        return new(unitPopulation);
    }

    public IReadOnlyDictionary<NamedType, IUnitType> Units { get; }

    public ExtendedUnitPopulation(IReadOnlyDictionary<NamedType, IUnitType> units)
    {
        Units = units.AsReadOnlyEquatable();
    }
}
