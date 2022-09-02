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

internal class UnitType : IUnitType
{
    public DefinedType Type { get; }
    public MinimalLocation TypeLocation { get; }

    public SharpMeasuresUnitDefinition Definition { get; }

    public IReadOnlyList<DerivableUnitDefinition> UnitDerivations => unitDerivations;

    public FixedUnitInstanceDefinition? FixedUnitInstance { get; }
    public IReadOnlyList<UnitInstanceAliasDefinition> UnitInstanceAliases => unitInstanceAliases;
    public IReadOnlyList<BiasedUnitInstanceDefinition> BiasedUnitInstances => biasedUnitInstances;
    public IReadOnlyList<DerivedUnitInstanceDefinition> DerivedUnitInstances => derivedUnitInstances;
    public IReadOnlyList<PrefixedUnitInstanceDefinition> PrefixedUnitInstances => prefixedUnitInstances;
    public IReadOnlyList<ScaledUnitInstanceDefinition> ScaledUnitInstances => scaledUnitInstances;

    public IReadOnlyDictionary<string, IUnitInstance> UnitInstancesByName => unitInstancesByName;
    public IReadOnlyDictionary<string, IUnitInstance> UnitInstancesByPluralForm => unitInstancesByPluralForm;
    public IReadOnlyDictionary<string, IDerivableUnit> DerivationsByID => derivationsByID;

    private ReadOnlyEquatableList<DerivableUnitDefinition> unitDerivations { get; }

    private ReadOnlyEquatableList<UnitInstanceAliasDefinition> unitInstanceAliases { get; }
    private ReadOnlyEquatableList<DerivedUnitInstanceDefinition> derivedUnitInstances { get; }
    private ReadOnlyEquatableList<BiasedUnitInstanceDefinition> biasedUnitInstances { get; }
    private ReadOnlyEquatableList<PrefixedUnitInstanceDefinition> prefixedUnitInstances { get; }
    private ReadOnlyEquatableList<ScaledUnitInstanceDefinition> scaledUnitInstances { get; }

    private ReadOnlyEquatableDictionary<string, IUnitInstance> unitInstancesByName { get; }
    private ReadOnlyEquatableDictionary<string, IUnitInstance> unitInstancesByPluralForm { get; }
    private ReadOnlyEquatableDictionary<string, IDerivableUnit> derivationsByID { get; }

    ISharpMeasuresObject ISharpMeasuresObjectType.Definition => Definition;
    IUnit IUnitType.Definition => Definition;

    IFixedUnitInstance? IUnitType.FixedUnitInstance => FixedUnitInstance;
    IReadOnlyList<IDerivableUnit> IUnitType.UnitDerivations => UnitDerivations;

    IReadOnlyList<IUnitInstanceAlias> IUnitType.UnitInstanceAliases => UnitInstanceAliases;
    IReadOnlyList<IBiasedUnitInstance> IUnitType.BiasedUnitInstances => BiasedUnitInstances;
    IReadOnlyList<IDerivedUnitInstance> IUnitType.DerivedUnitInstances => DerivedUnitInstances;
    IReadOnlyList<IPrefixedUnitInstance> IUnitType.PrefixedUnitInstances => PrefixedUnitInstances;
    IReadOnlyList<IScaledUnitInstance> IUnitType.ScaledUnitInstances => ScaledUnitInstances;

    public UnitType(DefinedType type, MinimalLocation unitLocation, SharpMeasuresUnitDefinition definition, IReadOnlyList<DerivableUnitDefinition> unitDerivations, FixedUnitInstanceDefinition? fixedUnitInstance,
        IReadOnlyList<UnitInstanceAliasDefinition> unitInstanceAliases, IReadOnlyList<DerivedUnitInstanceDefinition> derivedUnitInstances, IReadOnlyList<BiasedUnitInstanceDefinition> biasedUnitInstances, IReadOnlyList<PrefixedUnitInstanceDefinition> prefixedUnitInstances,
        IReadOnlyList<ScaledUnitInstanceDefinition> scaledUnitInstances)
    {
        Type = type;
        TypeLocation = unitLocation;

        Definition = definition;

        this.unitDerivations = unitDerivations.AsReadOnlyEquatable();

        FixedUnitInstance = fixedUnitInstance;
        this.unitInstanceAliases = unitInstanceAliases.AsReadOnlyEquatable();
        this.derivedUnitInstances = derivedUnitInstances.AsReadOnlyEquatable();
        this.biasedUnitInstances = biasedUnitInstances.AsReadOnlyEquatable();
        this.prefixedUnitInstances = prefixedUnitInstances.AsReadOnlyEquatable();
        this.scaledUnitInstances = scaledUnitInstances.AsReadOnlyEquatable();

        unitInstancesByName = ConstructUnitInstancesByNameDictionary();
        unitInstancesByPluralForm = ConstructUnitInstancesByPluralFormDictionary();
        derivationsByID = ConstructDerivationsByIDDictionary();
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
