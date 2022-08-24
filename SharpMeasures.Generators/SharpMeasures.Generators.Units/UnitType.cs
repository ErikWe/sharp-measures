namespace SharpMeasures.Generators.Units;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Units.Parsing.BiasedUnit;
using SharpMeasures.Generators.Units.Parsing.DerivableUnit;
using SharpMeasures.Generators.Units.Parsing.DerivedUnit;
using SharpMeasures.Generators.Units.Parsing.FixedUnit;
using SharpMeasures.Generators.Units.Parsing.PrefixedUnit;
using SharpMeasures.Generators.Units.Parsing.ScaledUnit;
using SharpMeasures.Generators.Units.Parsing.SharpMeasuresUnit;
using SharpMeasures.Generators.Units.Parsing.UnitAlias;
using SharpMeasures.Generators.Units.UnitInstances;

using System.Collections.Generic;
using System.Linq;

internal class UnitType : IUnitType
{
    public DefinedType Type { get; }
    public MinimalLocation TypeLocation { get; }

    public SharpMeasuresUnitDefinition Definition { get; }

    public IReadOnlyList<DerivableUnitDefinition> UnitDerivations => unitDerivations;

    public FixedUnitDefinition? FixedUnit { get; }
    public IReadOnlyList<UnitAliasDefinition> UnitAliases => unitAliases;
    public IReadOnlyList<BiasedUnitDefinition> BiasedUnits => biasedUnits;
    public IReadOnlyList<DerivedUnitDefinition> DerivedUnits => derivedUnits;
    public IReadOnlyList<PrefixedUnitDefinition> PrefixedUnits => prefixedUnits;
    public IReadOnlyList<ScaledUnitDefinition> ScaledUnits => scaledUnits;

    public IReadOnlyDictionary<string, IUnitInstance> UnitsByName => unitsByName;
    public IReadOnlyDictionary<string, IUnitInstance> UnitsByPluralName => unitsByPluralName;
    public IReadOnlyDictionary<string, IDerivableUnit> DerivationsByID => derivationsByID;

    private ReadOnlyEquatableList<DerivableUnitDefinition> unitDerivations { get; }

    private ReadOnlyEquatableList<UnitAliasDefinition> unitAliases { get; }
    private ReadOnlyEquatableList<DerivedUnitDefinition> derivedUnits { get; }
    private ReadOnlyEquatableList<BiasedUnitDefinition> biasedUnits { get; }
    private ReadOnlyEquatableList<PrefixedUnitDefinition> prefixedUnits { get; }
    private ReadOnlyEquatableList<ScaledUnitDefinition> scaledUnits { get; }

    private ReadOnlyEquatableDictionary<string, IUnitInstance> unitsByName { get; }
    private ReadOnlyEquatableDictionary<string, IUnitInstance> unitsByPluralName { get; }
    private ReadOnlyEquatableDictionary<string, IDerivableUnit> derivationsByID { get; }

    ISharpMeasuresObject ISharpMeasuresObjectType.Definition => Definition;
    IUnit IUnitType.Definition => Definition;

    IFixedUnit? IUnitType.FixedUnit => FixedUnit;
    IReadOnlyList<IDerivableUnit> IUnitType.UnitDerivations => UnitDerivations;

    IReadOnlyList<IUnitAlias> IUnitType.UnitAliases => UnitAliases;
    IReadOnlyList<IBiasedUnit> IUnitType.BiasedUnits => BiasedUnits;
    IReadOnlyList<IDerivedUnit> IUnitType.DerivedUnits => DerivedUnits;
    IReadOnlyList<IPrefixedUnit> IUnitType.PrefixedUnits => PrefixedUnits;
    IReadOnlyList<IScaledUnit> IUnitType.ScaledUnits => ScaledUnits;

    public UnitType(DefinedType type, MinimalLocation unitLocation, SharpMeasuresUnitDefinition definition, IReadOnlyList<DerivableUnitDefinition> unitDerivations, FixedUnitDefinition? fixedUnit,
        IReadOnlyList<UnitAliasDefinition> unitAliases, IReadOnlyList<DerivedUnitDefinition> derivedUnits, IReadOnlyList<BiasedUnitDefinition> biasedUnits, IReadOnlyList<PrefixedUnitDefinition> prefixedUnits,
        IReadOnlyList<ScaledUnitDefinition> scaledUnits)
    {
        Type = type;
        TypeLocation = unitLocation;

        Definition = definition;

        this.unitDerivations = unitDerivations.AsReadOnlyEquatable();

        FixedUnit = fixedUnit;
        this.unitAliases = unitAliases.AsReadOnlyEquatable();
        this.derivedUnits = derivedUnits.AsReadOnlyEquatable();
        this.biasedUnits = biasedUnits.AsReadOnlyEquatable();
        this.prefixedUnits = prefixedUnits.AsReadOnlyEquatable();
        this.scaledUnits = scaledUnits.AsReadOnlyEquatable();

        unitsByName = ConstructUnitsByNameDictionary();
        unitsByPluralName = ConstructUnitsByPluralNameDictionary();
        derivationsByID = ConstructDerivationsByIDDictionary();
    }

    private IEnumerable<IUnitInstance> AllUnits()
    {
        var allUnits = (UnitAliases as IEnumerable<IUnitInstance>).Concat(BiasedUnits).Concat(DerivedUnits).Concat(PrefixedUnits) .Concat(ScaledUnits);

        if (FixedUnit is not null)
        {
            allUnits = allUnits.Concat(new[] { FixedUnit });
        }

        return allUnits;
    }

    private ReadOnlyEquatableDictionary<string, IUnitInstance> ConstructUnitsByNameDictionary() => AllUnits().ToDictionary(static (unit) => unit.Name).AsReadOnlyEquatable();
    private ReadOnlyEquatableDictionary<string, IUnitInstance> ConstructUnitsByPluralNameDictionary() => AllUnits().ToDictionary(static (unit) => unit.Plural).AsReadOnlyEquatable();

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
