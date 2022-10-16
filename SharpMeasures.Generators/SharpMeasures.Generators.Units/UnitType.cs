namespace SharpMeasures.Generators.Units;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Units.Parsing.BiasedUnitInstance;
using SharpMeasures.Generators.Units.Parsing.DerivableUnit;
using SharpMeasures.Generators.Units.Parsing.DerivedUnitInstance;
using SharpMeasures.Generators.Units.Parsing.FixedUnitInstance;
using SharpMeasures.Generators.Units.Parsing.PrefixedUnitInstance;
using SharpMeasures.Generators.Units.Parsing.ScaledUnitInstance;
using SharpMeasures.Generators.Units.Parsing.SharpMeasuresUnit;
using SharpMeasures.Generators.Units.Parsing.UnitInstanceAlias;

using System.Collections.Generic;

internal sealed class UnitType : IUnitType
{
    public DefinedType Type { get; }

    public SharpMeasuresUnitDefinition Definition { get; }

    public IReadOnlyList<DerivableUnitDefinition> UnitDerivations { get; }

    public FixedUnitInstanceDefinition? FixedUnitInstance { get; }
    public IReadOnlyList<UnitInstanceAliasDefinition> UnitInstanceAliases { get; }
    public IReadOnlyList<BiasedUnitInstanceDefinition> BiasedUnitInstances { get; }
    public IReadOnlyList<DerivedUnitInstanceDefinition> DerivedUnitInstances { get; }
    public IReadOnlyList<PrefixedUnitInstanceDefinition> PrefixedUnitInstances { get; }
    public IReadOnlyList<ScaledUnitInstanceDefinition> ScaledUnitInstances { get; }

    public IReadOnlyDictionary<string, IUnitInstance> UnitInstancesByName { get; }
    public IReadOnlyDictionary<string, IUnitInstance> UnitInstancesByPluralForm { get; }
    public IReadOnlyDictionary<string, IDerivableUnit> DerivationsByID { get; }

    ISharpMeasuresObject ISharpMeasuresObjectType.Definition => Definition;
    IUnit IUnitType.Definition => Definition;

    IFixedUnitInstance? IUnitType.FixedUnitInstance => FixedUnitInstance;
    IReadOnlyList<IDerivableUnit> IUnitType.UnitDerivations => UnitDerivations;

    IReadOnlyList<IUnitInstanceAlias> IUnitType.UnitInstanceAliases => UnitInstanceAliases;
    IReadOnlyList<IBiasedUnitInstance> IUnitType.BiasedUnitInstances => BiasedUnitInstances;
    IReadOnlyList<IDerivedUnitInstance> IUnitType.DerivedUnitInstances => DerivedUnitInstances;
    IReadOnlyList<IPrefixedUnitInstance> IUnitType.PrefixedUnitInstances => PrefixedUnitInstances;
    IReadOnlyList<IScaledUnitInstance> IUnitType.ScaledUnitInstances => ScaledUnitInstances;

    public UnitType(DefinedType type, SharpMeasuresUnitDefinition definition, IReadOnlyList<DerivableUnitDefinition> unitDerivations, FixedUnitInstanceDefinition? fixedUnitInstance, IReadOnlyList<UnitInstanceAliasDefinition> unitInstanceAliases,
        IReadOnlyList<DerivedUnitInstanceDefinition> derivedUnitInstances, IReadOnlyList<BiasedUnitInstanceDefinition> biasedUnitInstances, IReadOnlyList<PrefixedUnitInstanceDefinition> prefixedUnitInstances, IReadOnlyList<ScaledUnitInstanceDefinition> scaledUnitInstances)
    {
        Type = type;

        Definition = definition;

        UnitDerivations = unitDerivations.AsReadOnlyEquatable();

        FixedUnitInstance = fixedUnitInstance;
        UnitInstanceAliases = unitInstanceAliases.AsReadOnlyEquatable();
        DerivedUnitInstances = derivedUnitInstances.AsReadOnlyEquatable();
        BiasedUnitInstances = biasedUnitInstances.AsReadOnlyEquatable();
        PrefixedUnitInstances = prefixedUnitInstances.AsReadOnlyEquatable();
        ScaledUnitInstances = scaledUnitInstances.AsReadOnlyEquatable();

        UnitInstancesByName = ConstructUnitInstancesByNameDictionary().AsReadOnlyEquatable();
        UnitInstancesByPluralForm = ConstructUnitInstancesByPluralFormDictionary().AsReadOnlyEquatable();
        DerivationsByID = ConstructDerivationsByIDDictionary().AsReadOnlyEquatable();
    }

    private int UnitInstanceCount() => UnitInstanceAliases.Count + BiasedUnitInstances.Count + DerivedUnitInstances.Count + PrefixedUnitInstances.Count + ScaledUnitInstances.Count + (FixedUnitInstance is null ? 0 : 1);

    private IEnumerable<IUnitInstance> AllUnitInstances()
    {
        foreach (var unitInstanceAlias in UnitInstanceAliases)
        {
            yield return unitInstanceAlias;
        }

        foreach (var biasedUnitInstance in BiasedUnitInstances)
        {
            yield return biasedUnitInstance;
        }

        foreach (var derivedUnitInstance in DerivedUnitInstances)
        {
            yield return derivedUnitInstance;
        }

        foreach (var prefixedUnitInstance in PrefixedUnitInstances)
        {
            yield return prefixedUnitInstance;
        }

        foreach (var scaledUnitInstance in ScaledUnitInstances)
        {
            yield return scaledUnitInstance;
        }

        if (FixedUnitInstance is not null)
        {
            yield return FixedUnitInstance;
        }
    }

    private IReadOnlyDictionary<string, IUnitInstance> ConstructUnitInstancesByNameDictionary()
    {
        Dictionary<string, IUnitInstance> unitInstancesByName = new(UnitInstanceCount());
    
        foreach (var unit in AllUnitInstances())
        {
            unitInstancesByName.TryAdd(unit.Name, unit);
        }

        return unitInstancesByName;
    }

    private IReadOnlyDictionary<string, IUnitInstance> ConstructUnitInstancesByPluralFormDictionary()
    {
        Dictionary<string, IUnitInstance> unitInstancesByPluralForm = new(UnitInstanceCount());

        foreach (var unit in AllUnitInstances())
        {
            unitInstancesByPluralForm.TryAdd(unit.PluralForm, unit);
        }

        return unitInstancesByPluralForm;
    }

    private ReadOnlyEquatableDictionary<string, IDerivableUnit> ConstructDerivationsByIDDictionary()
    {
        Dictionary<string, IDerivableUnit> derivationsByID = new(UnitDerivations.Count);

        foreach (var derivation in UnitDerivations)
        {
            if (derivation.DerivationID is not null)
            {
                derivationsByID.TryAdd(derivation.DerivationID, derivation);
            }
        }

        if (derivationsByID.Count is 0 && UnitDerivations.Count > 0)
        {
            derivationsByID.Add("default", UnitDerivations[0]);
        }

        return derivationsByID.AsReadOnlyEquatable();
    }
}
