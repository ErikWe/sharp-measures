namespace SharpMeasures.Generators.Units.Parsing;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Units.Parsing.Abstractions;
using SharpMeasures.Generators.Units.Parsing.DerivableUnit;
using SharpMeasures.Generators.Units.Parsing.DerivedUnit;
using SharpMeasures.Generators.Units.Parsing.FixedUnit;
using SharpMeasures.Generators.Units.Parsing.GeneratedUnit;
using SharpMeasures.Generators.Units.Parsing.OffsetUnit;
using SharpMeasures.Generators.Units.Parsing.PrefixedUnit;
using SharpMeasures.Generators.Units.Parsing.ScaledUnit;
using SharpMeasures.Generators.Units.Parsing.UnitAlias;

using System.Collections.Generic;
using System.Linq;

internal record class RawParsedUnit
{
    public DefinedType UnitType { get; }
    public MinimalLocation UnitLocation { get; }
    public GeneratedUnitDefinition UnitDefinition { get; }

    public EquatableEnumerable<RawDerivableUnitDefinition> UnitDerivations { get; }

    public EquatableEnumerable<RawUnitAliasDefinition> UnitAliases { get; }
    public EquatableEnumerable<RawDerivedUnitDefinition> DerivedUnits { get; }
    public EquatableEnumerable<RawFixedUnitDefinition> FixedUnits { get; }
    public EquatableEnumerable<RawOffsetUnitDefinition> OffsetUnits { get; }
    public EquatableEnumerable<RawPrefixedUnitDefinition> PrefixedUnits { get; }
    public EquatableEnumerable<RawScaledUnitDefinition> ScaledUnits { get; }

    public RawParsedUnit(DefinedType unitType, MinimalLocation unitLocation, GeneratedUnitDefinition unitDefinition,
        IEnumerable<RawDerivableUnitDefinition> derivableUnitDefinitions, IEnumerable<RawUnitAliasDefinition> unitAliasDefinitions,
        IEnumerable<RawDerivedUnitDefinition> derivedUnitDefinitions, IEnumerable<RawFixedUnitDefinition> fixedUnitDefinitions,
        IEnumerable<RawOffsetUnitDefinition> offsetUnitDefinitions, IEnumerable<RawPrefixedUnitDefinition> prefixedUnitDefinitions,
        IEnumerable<RawScaledUnitDefinition> scaledUnitDefinitions)
    {
        UnitType = unitType;
        UnitLocation = unitLocation;
        UnitDefinition = unitDefinition;

        UnitDerivations = new(derivableUnitDefinitions);
        UnitAliases = new(unitAliasDefinitions);
        DerivedUnits = new(derivedUnitDefinitions);
        FixedUnits = new(fixedUnitDefinitions);
        OffsetUnits = new(offsetUnitDefinitions);
        PrefixedUnits = new(prefixedUnitDefinitions);
        ScaledUnits = new(scaledUnitDefinitions);
    }

    public IEnumerable<IRawUnitDefinition> GetUnitList()
    {
        return (UnitAliases as IEnumerable<IRawUnitDefinition>).Concat(DerivedUnits).Concat(FixedUnits).Concat(OffsetUnits)
            .Concat(PrefixedUnits).Concat(ScaledUnits);
    }

    public IEnumerable<IRawUnitDefinition> GetNonDependantUnitList()
    {
        return (DerivedUnits as IEnumerable<IRawUnitDefinition>).Concat(FixedUnits);
    }

    public IEnumerable<IRawDependantUnitDefinition> GetDependantUnitList()
    {
        return (UnitAliases as IEnumerable<IRawDependantUnitDefinition>).Concat(OffsetUnits).Concat(PrefixedUnits).Concat(ScaledUnits);
    }
}
