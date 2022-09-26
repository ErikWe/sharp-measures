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
using System.Linq;

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

        UnitInstancesByName = ConstructUnitInstancesByNameDictionary();
        UnitInstancesByPluralForm = ConstructUnitInstancesByPluralFormDictionary();
        DerivationsByID = ConstructDerivationsByIDDictionary();
    }

    private IEnumerable<IUnitInstance> AllUnitInstances()
    {
        var allUnitInstances = (UnitInstanceAliases as IEnumerable<IUnitInstance>).Concat(BiasedUnitInstances).Concat(DerivedUnitInstances).Concat(PrefixedUnitInstances) .Concat(ScaledUnitInstances);

        if (FixedUnitInstance is not null)
        {
            allUnitInstances = allUnitInstances.Concat(new[] { FixedUnitInstance });
        }

        return allUnitInstances;
    }

    private ReadOnlyEquatableDictionary<string, IUnitInstance> ConstructUnitInstancesByNameDictionary() => AllUnitInstances().ToDictionary(static (unitInstance) => unitInstance.Name).AsReadOnlyEquatable();
    private ReadOnlyEquatableDictionary<string, IUnitInstance> ConstructUnitInstancesByPluralFormDictionary() => AllUnitInstances().ToDictionary(static (unitInstance) => unitInstance.PluralForm).AsReadOnlyEquatable();

    private ReadOnlyEquatableDictionary<string, IDerivableUnit> ConstructDerivationsByIDDictionary()
    {
        Dictionary<string, IDerivableUnit> derivationsDictionary = new(UnitDerivations.Count);

        foreach (var derivation in UnitDerivations)
        {
            if (derivation.DerivationID is not null)
            {
                derivationsDictionary.Add(derivation.DerivationID, derivation);
            }
        }

        if (derivationsDictionary.Count is 0 && UnitDerivations.Count > 0)
        {
            derivationsDictionary.Add("default", UnitDerivations.Single());
        }

        return derivationsDictionary.AsReadOnlyEquatable();
    }
}
