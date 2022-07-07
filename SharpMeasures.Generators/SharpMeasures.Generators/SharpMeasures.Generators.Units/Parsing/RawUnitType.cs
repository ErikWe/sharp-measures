namespace SharpMeasures.Generators.Units.Parsing;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Units.Parsing.Abstractions;
using SharpMeasures.Generators.Units.Parsing.BiasedUnit;
using SharpMeasures.Generators.Units.Parsing.DerivableUnit;
using SharpMeasures.Generators.Units.Parsing.DerivedUnit;
using SharpMeasures.Generators.Units.Parsing.FixedUnit;
using SharpMeasures.Generators.Units.Parsing.PrefixedUnit;
using SharpMeasures.Generators.Units.Parsing.ScaledUnit;
using SharpMeasures.Generators.Units.Parsing.SharpMeasuresUnit;
using SharpMeasures.Generators.Units.Parsing.UnitAlias;

using System.Collections.Generic;
using System.Linq;

internal record class RawUnitType
{
    public DefinedType Type { get; }
    public MinimalLocation TypeLocation { get; }

    public RawSharpMeasuresUnitDefinition UnitDefinition { get; }

    public RawFixedUnitDefinition? FixedUnit { get; }
    public IEnumerable<RawDerivableUnitDefinition> UnitDerivations => unitDerivations;

    public IEnumerable<RawUnitAliasDefinition> UnitAliases => unitAliases;
    public IEnumerable<RawBiasedUnitDefinition> BiasedUnits => biasedUnits;
    public IEnumerable<RawDerivedUnitDefinition> DerivedUnits => derivedUnits;
    public IEnumerable<RawPrefixedUnitDefinition> PrefixedUnits => prefixedUnits;
    public IEnumerable<RawScaledUnitDefinition> ScaledUnits => scaledUnits;

    private EquatableEnumerable<RawDerivableUnitDefinition> unitDerivations { get; }

    private EquatableEnumerable<RawUnitAliasDefinition> unitAliases { get; }
    private EquatableEnumerable<RawDerivedUnitDefinition> derivedUnits { get; }
    private EquatableEnumerable<RawBiasedUnitDefinition> biasedUnits { get; }
    private EquatableEnumerable<RawPrefixedUnitDefinition> prefixedUnits { get; }
    private EquatableEnumerable<RawScaledUnitDefinition> scaledUnits { get; }

    public RawUnitType(DefinedType type, MinimalLocation unitLocation, RawSharpMeasuresUnitDefinition unitDefinition, RawFixedUnitDefinition? fixedUnit,
        IEnumerable<RawDerivableUnitDefinition> unitDerivations, IEnumerable<RawUnitAliasDefinition> unitAliases, IEnumerable<RawDerivedUnitDefinition> derivedUnits,
        IEnumerable<RawBiasedUnitDefinition> biasedUnits, IEnumerable<RawPrefixedUnitDefinition> prefixedUnits, IEnumerable<RawScaledUnitDefinition> scaledUnits)
    {
        Type = type;
        TypeLocation = unitLocation;
        UnitDefinition = unitDefinition;

        FixedUnit = fixedUnit;
        this.unitDerivations = unitDerivations.AsEquatable();

        this.unitAliases = unitAliases.AsEquatable();
        this.derivedUnits = derivedUnits.AsEquatable();
        this.biasedUnits = biasedUnits.AsEquatable();
        this.prefixedUnits = prefixedUnits.AsEquatable();
        this.scaledUnits = scaledUnits.AsEquatable();
    }

    public IEnumerable<IRawUnitDefinition<IUnitLocations>> AllUnitInstances => (new[] { FixedUnit } as IEnumerable<IRawUnitDefinition<IUnitLocations>>)
        .Concat(UnitAliases).Concat(BiasedUnits).Concat(DerivedUnits).Concat(PrefixedUnits).Concat(ScaledUnits);
}
