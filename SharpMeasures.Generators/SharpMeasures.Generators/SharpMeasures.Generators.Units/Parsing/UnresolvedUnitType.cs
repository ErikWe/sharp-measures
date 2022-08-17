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
using SharpMeasures.Generators.Raw.Units;
using SharpMeasures.Generators.Raw.Units.UnitInstances;

using System.Collections.Generic;
using System.Linq;

internal class UnresolvedUnitType : IRawUnitType
{
    public DefinedType Type { get; }
    public MinimalLocation TypeLocation { get; }

    public RawSharpMeasuresUnitDefinition Definition { get; }

    public RawFixedUnitDefinition? FixedUnit { get; }
    public IReadOnlyList<RawDerivableUnitDefinition> UnitDerivations => unitDerivations;

    public IReadOnlyList<RawUnitAliasDefinition> UnitAliases => unitAliases;
    public IReadOnlyList<RawBiasedUnitDefinition> BiasedUnits => biasedUnits;
    public IReadOnlyList<RawDerivedUnitDefinition> DerivedUnits => derivedUnits;
    public IReadOnlyList<RawPrefixedUnitDefinition> PrefixedUnits => prefixedUnits;
    public IReadOnlyList<RawScaledUnitDefinition> ScaledUnits => scaledUnits;

    public IReadOnlyDictionary<string, IRawUnitInstance> UnitsByName => unitsByName;
    public IReadOnlyDictionary<string, IRawUnitInstance> UnitsByPluralName => unitsByPluralName;
    public IReadOnlyDictionary<string, IRawDerivableUnit> DerivationsByID => derivationsByID;

    private ReadOnlyEquatableList<RawDerivableUnitDefinition> unitDerivations { get; }

    private ReadOnlyEquatableList<RawUnitAliasDefinition> unitAliases { get; }
    private ReadOnlyEquatableList<RawDerivedUnitDefinition> derivedUnits { get; }
    private ReadOnlyEquatableList<RawBiasedUnitDefinition> biasedUnits { get; }
    private ReadOnlyEquatableList<RawPrefixedUnitDefinition> prefixedUnits { get; }
    private ReadOnlyEquatableList<RawScaledUnitDefinition> scaledUnits { get; }

    private ReadOnlyEquatableDictionary<string, IRawUnitInstance> unitsByName { get; }
    private ReadOnlyEquatableDictionary<string, IRawUnitInstance> unitsByPluralName { get; }
    private ReadOnlyEquatableDictionary<string, IRawDerivableUnit> derivationsByID { get; }

    ISharpMeasuresObject ISharpMeasuresObjectType.Definition => Definition;
    IRawUnit IRawUnitType.Definition => Definition;

    IRawFixedUnit? IRawUnitType.FixedUnit => FixedUnit;
    IReadOnlyList<IRawDerivableUnit> IRawUnitType.UnitDerivations => UnitDerivations;

    IReadOnlyList<IRawUnitAlias> IRawUnitType.UnitAliases => UnitAliases;
    IReadOnlyList<IRawBiasedUnit> IRawUnitType.BiasedUnits => BiasedUnits;
    IReadOnlyList<IRawDerivedUnit> IRawUnitType.DerivedUnits => DerivedUnits;
    IReadOnlyList<IRawPrefixedUnit> IRawUnitType.PrefixedUnits => PrefixedUnits;
    IReadOnlyList<IRawScaledUnit> IRawUnitType.ScaledUnits => ScaledUnits;

    public UnresolvedUnitType(DefinedType type, MinimalLocation unitLocation, RawSharpMeasuresUnitDefinition definition, RawFixedUnitDefinition? fixedUnit,
        IReadOnlyList<RawDerivableUnitDefinition> unitDerivations, IReadOnlyList<RawUnitAliasDefinition> unitAliases,
        IReadOnlyList<RawDerivedUnitDefinition> derivedUnits, IReadOnlyList<RawBiasedUnitDefinition> biasedUnits,
        IReadOnlyList<RawPrefixedUnitDefinition> prefixedUnits, IReadOnlyList<RawScaledUnitDefinition> scaledUnits)
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

    private IEnumerable<IRawUnitInstance> AllUnits()
    {
        var allUnits = (UnitAliases as IEnumerable<IRawUnitInstance>).Concat(BiasedUnits).Concat(DerivedUnits).Concat(PrefixedUnits).Concat(ScaledUnits);

        if (FixedUnit is not null)
        {
            allUnits = allUnits.Concat(new[] { FixedUnit });
        }

        return allUnits;
    }

    private ReadOnlyEquatableDictionary<string, IRawUnitInstance> ConstructUnitsByNameDictionary()
        => AllUnits().ToDictionary(static (unit) => unit.Name).AsReadOnlyEquatable();

    private ReadOnlyEquatableDictionary<string, IRawUnitInstance> ConstructUnitsByPluralNameDictionary()
        => AllUnits().ToDictionary(static (unit) => unit.Plural).AsReadOnlyEquatable();

    private ReadOnlyEquatableDictionary<string, IRawDerivableUnit> ConstructDerivationsByIDDictionary()
    {
        Dictionary<string, IRawDerivableUnit> derivationsDictionary = new(UnitDerivations.Count);

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
