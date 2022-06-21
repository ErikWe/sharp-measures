namespace SharpMeasures.Generators.Units.Parsing;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Units.Parsing.Abstractions;
using SharpMeasures.Generators.Units.Parsing.DerivableUnit;
using SharpMeasures.Generators.Units.Parsing.DerivedUnit;
using SharpMeasures.Generators.Units.Parsing.FixedUnit;
using SharpMeasures.Generators.Units.Parsing.SharpMeasuresUnit;
using SharpMeasures.Generators.Units.Parsing.BiasedUnit;
using SharpMeasures.Generators.Units.Parsing.PrefixedUnit;
using SharpMeasures.Generators.Units.Parsing.ScaledUnit;
using SharpMeasures.Generators.Units.Parsing.UnitAlias;

using System.Collections.Generic;
using System.Linq;

internal class ParsedUnit
{
    public DefinedType UnitType { get; }
    public MinimalLocation UnitLocation { get; }
    public SharpMeasuresUnitDefinition UnitDefinition { get; }

    public EquatableEnumerable<DerivableUnitDefinition> UnitDerivations { get; }

    public EquatableEnumerable<UnitAliasDefinition> UnitAliases { get; }
    public EquatableEnumerable<DerivedUnitDefinition> DerivedUnits { get; }
    public EquatableEnumerable<FixedUnitDefinition> FixedUnits { get; }
    public EquatableEnumerable<BiasedUnitDefinition> BiasedUnits { get; }
    public EquatableEnumerable<PrefixedUnitDefinition> PrefixedUnits { get; }
    public EquatableEnumerable<ScaledUnitDefinition> ScaledUnits { get; }

    public ParsedUnit(DefinedType unitType, MinimalLocation unitLocation, SharpMeasuresUnitDefinition unitDefinition,
        IEnumerable<DerivableUnitDefinition> unitDerivations, IEnumerable<UnitAliasDefinition> unitAliases, IEnumerable<DerivedUnitDefinition> derivedUnits,
        IEnumerable<FixedUnitDefinition> fixedUnits, IEnumerable<BiasedUnitDefinition> biasedUnits, IEnumerable<PrefixedUnitDefinition> prefixedUnits,
        IEnumerable<ScaledUnitDefinition> scaledUnits)
    {
        UnitType = unitType;
        UnitLocation = unitLocation;
        UnitDefinition = unitDefinition;

        UnitDerivations = unitDerivations.AsEquatable();
        UnitAliases = unitAliases.AsEquatable();
        DerivedUnits = derivedUnits.AsEquatable();
        FixedUnits = fixedUnits.AsEquatable();
        BiasedUnits = biasedUnits.AsEquatable();
        PrefixedUnits = prefixedUnits.AsEquatable();
        ScaledUnits = scaledUnits.AsEquatable();
    }

    public IEnumerable<IUnitDefinition> GetUnitList()
    {
        return (UnitAliases as IEnumerable<IUnitDefinition>).Concat(DerivedUnits).Concat(FixedUnits).Concat(BiasedUnits)
            .Concat(PrefixedUnits).Concat(ScaledUnits);
    }

    public IEnumerable<IUnitDefinition> GetNonDependantUnitList()
    {
        return (DerivedUnits as IEnumerable<IUnitDefinition>).Concat(FixedUnits);
    }

    public IEnumerable<IDependantUnitDefinition> GetDependantUnitList()
    {
        return (UnitAliases as IEnumerable<IDependantUnitDefinition>).Concat(BiasedUnits).Concat(PrefixedUnits).Concat(ScaledUnits);
    }
}
