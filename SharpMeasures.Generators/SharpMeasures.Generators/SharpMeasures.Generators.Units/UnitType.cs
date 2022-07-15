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

    public FixedUnitDefinition? FixedUnit { get; }
    public IReadOnlyList<DerivableUnitDefinition> UnitDerivations => unitDerivations;

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

    public UnitType(DefinedType type, MinimalLocation unitLocation, SharpMeasuresUnitDefinition definition, FixedUnitDefinition fixedUnit,
        IReadOnlyList<DerivableUnitDefinition> unitDerivations, IReadOnlyList<UnitAliasDefinition> unitAliases, IReadOnlyList<DerivedUnitDefinition> derivedUnits,
        IReadOnlyList<BiasedUnitDefinition> biasedUnits, IReadOnlyList<PrefixedUnitDefinition> prefixedUnits, IReadOnlyList<ScaledUnitDefinition> scaledUnits)
    {
        Type = type;
        TypeLocation = unitLocation;

        Definition = definition;

        FixedUnit = fixedUnit;
        this.unitDerivations = unitDerivations.AsReadOnlyEquatable();

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
        => (new[] { FixedUnit } as IEnumerable<IUnitInstance>).Concat(UnitAliases).Concat(BiasedUnits).Concat(DerivedUnits).Concat(PrefixedUnits) .Concat(ScaledUnits);

    private ReadOnlyEquatableDictionary<string, IUnitInstance> ConstructUnitsByNameDictionary() => AllUnits().ToDictionary(static (unit) => unit.Name).AsReadOnlyEquatable();
    private ReadOnlyEquatableDictionary<string, IUnitInstance> ConstructUnitsByPluralNameDictionary() => AllUnits().ToDictionary(static (unit) => unit.Plural).AsReadOnlyEquatable();

    private ReadOnlyEquatableDictionary<string, IDerivableUnit> ConstructDerivationsByIDDictionary()
        => UnitDerivations.ToDictionary(static (derivation) => derivation.DerivationID, static (derivation) => derivation as IDerivableUnit).AsReadOnlyEquatable();
}
