namespace SharpMeasures.Generators.Units.Parsing;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Units.Parsing.BiasedUnit;
using SharpMeasures.Generators.Units.Parsing.DerivableUnit;
using SharpMeasures.Generators.Units.Parsing.DerivedUnit;
using SharpMeasures.Generators.Units.Parsing.FixedUnit;
using SharpMeasures.Generators.Units.Parsing.PrefixedUnit;
using SharpMeasures.Generators.Units.Parsing.ScaledUnit;
using SharpMeasures.Generators.Units.Parsing.SharpMeasuresUnit;
using SharpMeasures.Generators.Units.Parsing.UnitAlias;
using SharpMeasures.Generators.Unresolved.Units;
using SharpMeasures.Generators.Unresolved.Units.UnitInstances;

using System.Collections.Generic;
using System.Linq;

internal class UnresolvedUnitType : IUnresolvedUnitType
{
    public DefinedType Type { get; }
    public MinimalLocation TypeLocation { get; }

    public UnresolvedSharpMeasuresUnitDefinition Definition { get; }

    public UnresolvedFixedUnitDefinition? FixedUnit { get; }
    public IReadOnlyList<UnresolvedDerivableUnitDefinition> UnitDerivations => unitDerivations;

    public IReadOnlyList<UnresolvedUnitAliasDefinition> UnitAliases => unitAliases;
    public IReadOnlyList<UnresolvedBiasedUnitDefinition> BiasedUnits => biasedUnits;
    public IReadOnlyList<UnresolvedDerivedUnitDefinition> DerivedUnits => derivedUnits;
    public IReadOnlyList<UnresolvedPrefixedUnitDefinition> PrefixedUnits => prefixedUnits;
    public IReadOnlyList<UnresolvedScaledUnitDefinition> ScaledUnits => scaledUnits;

    public IReadOnlyDictionary<string, IUnresolvedUnitInstance> UnitsByName => unitsByName;
    public IReadOnlyDictionary<string, IUnresolvedUnitInstance> UnitsByPluralName => unitsByPluralName;
    public IReadOnlyDictionary<string, IUnresolvedDerivableUnit> DerivationsByID => derivationsByID;

    private ReadOnlyEquatableList<UnresolvedDerivableUnitDefinition> unitDerivations { get; }

    private ReadOnlyEquatableList<UnresolvedUnitAliasDefinition> unitAliases { get; }
    private ReadOnlyEquatableList<UnresolvedDerivedUnitDefinition> derivedUnits { get; }
    private ReadOnlyEquatableList<UnresolvedBiasedUnitDefinition> biasedUnits { get; }
    private ReadOnlyEquatableList<UnresolvedPrefixedUnitDefinition> prefixedUnits { get; }
    private ReadOnlyEquatableList<UnresolvedScaledUnitDefinition> scaledUnits { get; }

    private ReadOnlyEquatableDictionary<string, IUnresolvedUnitInstance> unitsByName { get; }
    private ReadOnlyEquatableDictionary<string, IUnresolvedUnitInstance> unitsByPluralName { get; }
    private ReadOnlyEquatableDictionary<string, IUnresolvedDerivableUnit> derivationsByID { get; }

    ISharpMeasuresObject ISharpMeasuresObjectType.Definition => Definition;
    IUnresolvedUnit IUnresolvedUnitType.Definition => Definition;

    IUnresolvedFixedUnit? IUnresolvedUnitType.FixedUnit => FixedUnit;
    IReadOnlyList<IUnresolvedDerivableUnit> IUnresolvedUnitType.UnitDerivations => UnitDerivations;

    IReadOnlyList<IUnresolvedUnitAlias> IUnresolvedUnitType.UnitAliases => UnitAliases;
    IReadOnlyList<IUnresolvedBiasedUnit> IUnresolvedUnitType.BiasedUnits => BiasedUnits;
    IReadOnlyList<IUnresolvedDerivedUnit> IUnresolvedUnitType.DerivedUnits => DerivedUnits;
    IReadOnlyList<IUnresolvedPrefixedUnit> IUnresolvedUnitType.PrefixedUnits => PrefixedUnits;
    IReadOnlyList<IUnresolvedScaledUnit> IUnresolvedUnitType.ScaledUnits => ScaledUnits;

    public UnresolvedUnitType(DefinedType type, MinimalLocation unitLocation, UnresolvedSharpMeasuresUnitDefinition definition, UnresolvedFixedUnitDefinition? fixedUnit,
        IReadOnlyList<UnresolvedDerivableUnitDefinition> unitDerivations, IReadOnlyList<UnresolvedUnitAliasDefinition> unitAliases,
        IReadOnlyList<UnresolvedDerivedUnitDefinition> derivedUnits, IReadOnlyList<UnresolvedBiasedUnitDefinition> biasedUnits,
        IReadOnlyList<UnresolvedPrefixedUnitDefinition> prefixedUnits, IReadOnlyList<UnresolvedScaledUnitDefinition> scaledUnits)
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

    private IEnumerable<IUnresolvedUnitInstance> AllUnits()
    {
        var allUnits = (UnitAliases as IEnumerable<IUnresolvedUnitInstance>).Concat(BiasedUnits).Concat(DerivedUnits).Concat(PrefixedUnits).Concat(ScaledUnits);

        if (FixedUnit is not null)
        {
            allUnits = allUnits.Concat(new[] { FixedUnit });
        }

        return allUnits;
    }

    private ReadOnlyEquatableDictionary<string, IUnresolvedUnitInstance> ConstructUnitsByNameDictionary()
        => AllUnits().ToDictionary(static (unit) => unit.Name).AsReadOnlyEquatable();

    private ReadOnlyEquatableDictionary<string, IUnresolvedUnitInstance> ConstructUnitsByPluralNameDictionary()
        => AllUnits().ToDictionary(static (unit) => unit.Plural).AsReadOnlyEquatable();

    private ReadOnlyEquatableDictionary<string, IUnresolvedDerivableUnit> ConstructDerivationsByIDDictionary()
    {
        Dictionary<string, IUnresolvedDerivableUnit> derivationsDictionary = new(UnitDerivations.Count);

        foreach (var derivation in UnitDerivations)
        {
            if (derivation.DerivationID is not null)
            {
                derivationsDictionary.Add(derivation.DerivationID, derivation);
            }
        }

        return derivationsDictionary.AsReadOnlyEquatable();
    }
}
