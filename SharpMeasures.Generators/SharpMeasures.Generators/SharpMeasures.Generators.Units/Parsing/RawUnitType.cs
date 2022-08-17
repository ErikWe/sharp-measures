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

    public UnprocessedSharpMeasuresUnitDefinition Definition { get; }

    public UnprocessedFixedUnitDefinition? FixedUnit { get; }
    public IEnumerable<UnprocessedDerivableUnitDefinition> UnitDerivations => unitDerivations;

    public IEnumerable<UnprocessedUnitAliasDefinition> UnitAliases => unitAliases;
    public IEnumerable<UnprocessedBiasedUnitDefinition> BiasedUnits => biasedUnits;
    public IEnumerable<UnprocessedDerivedUnitDefinition> DerivedUnits => derivedUnits;
    public IEnumerable<UnprocessedPrefixedUnitDefinition> PrefixedUnits => prefixedUnits;
    public IEnumerable<UnprocessedScaledUnitDefinition> ScaledUnits => scaledUnits;

    private EquatableEnumerable<UnprocessedDerivableUnitDefinition> unitDerivations { get; }

    private EquatableEnumerable<UnprocessedUnitAliasDefinition> unitAliases { get; }
    private EquatableEnumerable<UnprocessedDerivedUnitDefinition> derivedUnits { get; }
    private EquatableEnumerable<UnprocessedBiasedUnitDefinition> biasedUnits { get; }
    private EquatableEnumerable<UnprocessedPrefixedUnitDefinition> prefixedUnits { get; }
    private EquatableEnumerable<UnprocessedScaledUnitDefinition> scaledUnits { get; }

    public RawUnitType(DefinedType type, MinimalLocation unitLocation, UnprocessedSharpMeasuresUnitDefinition unitDefinition, UnprocessedFixedUnitDefinition? fixedUnit,
        IEnumerable<UnprocessedDerivableUnitDefinition> unitDerivations, IEnumerable<UnprocessedUnitAliasDefinition> unitAliases, IEnumerable<UnprocessedDerivedUnitDefinition> derivedUnits,
        IEnumerable<UnprocessedBiasedUnitDefinition> biasedUnits, IEnumerable<UnprocessedPrefixedUnitDefinition> prefixedUnits, IEnumerable<UnprocessedScaledUnitDefinition> scaledUnits)
    {
        Type = type;
        TypeLocation = unitLocation;
        Definition = unitDefinition;

        FixedUnit = fixedUnit;
        this.unitDerivations = unitDerivations.AsEquatable();

        this.unitAliases = unitAliases.AsEquatable();
        this.derivedUnits = derivedUnits.AsEquatable();
        this.biasedUnits = biasedUnits.AsEquatable();
        this.prefixedUnits = prefixedUnits.AsEquatable();
        this.scaledUnits = scaledUnits.AsEquatable();
    }

    public IEnumerable<IUnprocessedUnitDefinition<IUnitLocations>> AllUnitInstances => (new[] { FixedUnit } as IEnumerable<IUnprocessedUnitDefinition<IUnitLocations>>)
        .Concat(UnitAliases).Concat(BiasedUnits).Concat(DerivedUnits).Concat(PrefixedUnits).Concat(ScaledUnits);
}
