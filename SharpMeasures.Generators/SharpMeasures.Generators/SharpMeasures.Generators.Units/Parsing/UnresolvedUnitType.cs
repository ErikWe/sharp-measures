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

    public UnresolvedSharpMeasuresUnitDefinition UnitDefinition { get; }

    public UnresolvedFixedUnitDefinition? FixedUnit { get; }
    public IReadOnlyList<UnresolvedDerivableUnitDefinition> UnitDerivations => unitDerivations;

    public IReadOnlyList<UnresolvedUnitAliasDefinition> UnitAliases => unitAliases;
    public IReadOnlyList<UnresolvedBiasedUnitDefinition> BiasedUnits => biasedUnits;
    public IReadOnlyList<UnresolvedDerivedUnitDefinition> DerivedUnits => derivedUnits;
    public IReadOnlyList<UnresolvedPrefixedUnitDefinition> PrefixedUnits => prefixedUnits;
    public IReadOnlyList<UnresolvedScaledUnitDefinition> ScaledUnits => scaledUnits;

    public IReadOnlyDictionary<string, IUnresolvedUnitInstance> UnitsByName => unitsByName;
    public IReadOnlyDictionary<string, IUnresolvedDerivableUnit> DerivationsByID => derivationsByID;

    private ReadOnlyEquatableList<UnresolvedDerivableUnitDefinition> unitDerivations { get; }

    private ReadOnlyEquatableList<UnresolvedUnitAliasDefinition> unitAliases { get; }
    private ReadOnlyEquatableList<UnresolvedDerivedUnitDefinition> derivedUnits { get; }
    private ReadOnlyEquatableList<UnresolvedBiasedUnitDefinition> biasedUnits { get; }
    private ReadOnlyEquatableList<UnresolvedPrefixedUnitDefinition> prefixedUnits { get; }
    private ReadOnlyEquatableList<UnresolvedScaledUnitDefinition> scaledUnits { get; }

    private ReadOnlyEquatableDictionary<string, IUnresolvedUnitInstance> unitsByName { get; }
    private ReadOnlyEquatableDictionary<string, IUnresolvedDerivableUnit> derivationsByID { get; }

    IUnresolvedUnit IUnresolvedUnitType.UnitDefinition => UnitDefinition;

    IUnresolvedFixedUnit? IUnresolvedUnitType.FixedUnit => FixedUnit;
    IReadOnlyList<IUnresolvedDerivableUnit> IUnresolvedUnitType.UnitDerivations => UnitDerivations;

    IReadOnlyList<IUnresolvedUnitAlias> IUnresolvedUnitType.UnitAliases => UnitAliases;
    IReadOnlyList<IUnresolvedBiasedUnit> IUnresolvedUnitType.BiasedUnits => BiasedUnits;
    IReadOnlyList<IUnresolvedDerivedUnit> IUnresolvedUnitType.DerivedUnits => DerivedUnits;
    IReadOnlyList<IUnresolvedPrefixedUnit> IUnresolvedUnitType.PrefixedUnits => PrefixedUnits;
    IReadOnlyList<IUnresolvedScaledUnit> IUnresolvedUnitType.ScaledUnits => ScaledUnits;

    public UnresolvedUnitType(DefinedType type, MinimalLocation unitLocation, UnresolvedSharpMeasuresUnitDefinition unitDefinition, UnresolvedFixedUnitDefinition fixedUnit,
        IReadOnlyList<UnresolvedDerivableUnitDefinition> unitDerivations, IReadOnlyList<UnresolvedUnitAliasDefinition> unitAliases,
        IReadOnlyList<UnresolvedDerivedUnitDefinition> derivedUnits, IReadOnlyList<UnresolvedBiasedUnitDefinition> biasedUnits,
        IReadOnlyList<UnresolvedPrefixedUnitDefinition> prefixedUnits, IReadOnlyList<UnresolvedScaledUnitDefinition> scaledUnits)
    {
        Type = type;

        TypeLocation = unitLocation;
        UnitDefinition = unitDefinition;

        FixedUnit = fixedUnit;
        this.unitDerivations = unitDerivations.AsReadOnlyEquatable();

        this.unitAliases = unitAliases.AsReadOnlyEquatable();
        this.derivedUnits = derivedUnits.AsReadOnlyEquatable();
        this.biasedUnits = biasedUnits.AsReadOnlyEquatable();
        this.prefixedUnits = prefixedUnits.AsReadOnlyEquatable();
        this.scaledUnits = scaledUnits.AsReadOnlyEquatable();

        unitsByName = ConstructUnitsByNameDictionary();
        derivationsByID = ConstructDerivationsByIDDictionary();
    }

    private ReadOnlyEquatableDictionary<string, IUnresolvedUnitInstance> ConstructUnitsByNameDictionary()
    {
        var allUnits = (new[] { FixedUnit } as IEnumerable<IUnresolvedUnitInstance>).Concat(UnitAliases).Concat(BiasedUnits).Concat(DerivedUnits).Concat(PrefixedUnits)
            .Concat(ScaledUnits);

        return allUnits.ToDictionary(static (unit) => unit.Name).AsReadOnlyEquatable();
    }

    private ReadOnlyEquatableDictionary<string, IUnresolvedDerivableUnit> ConstructDerivationsByIDDictionary()
    {
        return UnitDerivations.ToDictionary(static (derivation) => derivation.DerivationID, static (derivation) => derivation as IUnresolvedDerivableUnit).AsReadOnlyEquatable();
    }
}
