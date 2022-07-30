namespace SharpMeasures.Generators.Units.Parsing;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Units.Parsing.Abstractions;
using SharpMeasures.Generators.Unresolved.Units;

using System.Collections.Generic;
using System.Collections.Immutable;

internal class UnresolvedUnitPopulation : IUnresolvedUnitPopulationWithData
{
    public static UnresolvedUnitPopulation Build(ImmutableArray<IUnresolvedUnitType> units)
    {
        Dictionary<NamedType, IUnresolvedUnitType> unitPopulation = new(units.Length);

        Dictionary<NamedType, IUnresolvedUnitType> duplicatePopulation = new();

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

    public IReadOnlyDictionary<NamedType, IUnresolvedUnitType> Units => units;
    public IReadOnlyDictionary<NamedType, IUnresolvedUnitType> DuplicatelyDefined => duplicatelyDefined;

    private ReadOnlyEquatableDictionary<NamedType, IUnresolvedUnitType> units { get; }
    private ReadOnlyEquatableDictionary<NamedType, IUnresolvedUnitType> duplicatelyDefined { get; }

    public UnresolvedUnitPopulation(IReadOnlyDictionary<NamedType, IUnresolvedUnitType> units, IReadOnlyDictionary<NamedType, IUnresolvedUnitType> duplicatelyDefined)
    {
        this.units = units.AsReadOnlyEquatable();
        this.duplicatelyDefined = duplicatelyDefined.AsReadOnlyEquatable();
    }
}
