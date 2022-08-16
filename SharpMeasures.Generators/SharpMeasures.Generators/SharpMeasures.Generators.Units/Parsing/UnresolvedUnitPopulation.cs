namespace SharpMeasures.Generators.Units.Parsing;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Units.Parsing.Abstractions;
using SharpMeasures.Generators.Raw.Units;

using System.Collections.Generic;
using System.Collections.Immutable;

internal class UnresolvedUnitPopulation : IRawUnitPopulationWithData
{
    public static UnresolvedUnitPopulation Build(ImmutableArray<IRawUnitType> units)
    {
        Dictionary<NamedType, IRawUnitType> unitPopulation = new(units.Length);

        Dictionary<NamedType, IRawUnitType> duplicatePopulation = new();

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

    public IReadOnlyDictionary<NamedType, IRawUnitType> Units => units;
    public IReadOnlyDictionary<NamedType, IRawUnitType> DuplicatelyDefinedUnits => duplicatelyDefined;

    private ReadOnlyEquatableDictionary<NamedType, IRawUnitType> units { get; }
    private ReadOnlyEquatableDictionary<NamedType, IRawUnitType> duplicatelyDefined { get; }

    public UnresolvedUnitPopulation(IReadOnlyDictionary<NamedType, IRawUnitType> units, IReadOnlyDictionary<NamedType, IRawUnitType> duplicatelyDefined)
    {
        this.units = units.AsReadOnlyEquatable();
        this.duplicatelyDefined = duplicatelyDefined.AsReadOnlyEquatable();
    }
}
