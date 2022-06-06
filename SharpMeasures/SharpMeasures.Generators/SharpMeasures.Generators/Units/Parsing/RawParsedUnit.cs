namespace SharpMeasures.Generators.Units.Parsing;

using SharpMeasures.Generators.Attributes.Parsing.Units;
using SharpMeasures.Equatables;

using System.Collections.Generic;
using System.Linq;

internal record class RawParsedUnit
{
    public DefinedType UnitType { get; }
    public MinimalLocation UnitLocation { get; }
    public GeneratedUnit UnitDefinition { get; }

    public EquatableEnumerable<RawDerivableUnit> UnitDerivations { get; }

    public EquatableEnumerable<RawUnitAlias> UnitAliases { get; }
    public EquatableEnumerable<RawDerivedUnit> DerivedUnits { get; }
    public EquatableEnumerable<RawFixedUnit> FixedUnits { get; }
    public EquatableEnumerable<RawOffsetUnit> OffsetUnits { get; }
    public EquatableEnumerable<RawPrefixedUnit> PrefixedUnits { get; }
    public EquatableEnumerable<RawScaledUnit> ScaledUnits { get; }

    public RawParsedUnit(DefinedType unitType, MinimalLocation unitLocation, GeneratedUnit unitDefinition,
        IEnumerable<RawDerivableUnit> derivableUnitDefinitions, IEnumerable<RawUnitAlias> unitAliasDefinitions,
        IEnumerable<RawDerivedUnit> derivedUnitDefinitions, IEnumerable<RawFixedUnit> fixedUnitDefinitions,
        IEnumerable<RawOffsetUnit> offsetUnitDefinitions, IEnumerable<RawPrefixedUnit> prefixedUnitDefinitions,
        IEnumerable<RawScaledUnit> scaledUnitDefinitions)
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
