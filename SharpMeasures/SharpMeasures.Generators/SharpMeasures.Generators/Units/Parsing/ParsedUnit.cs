namespace SharpMeasures.Generators.Units.Parsing;

using SharpMeasures.Generators.Attributes.Parsing.Units;
using SharpMeasures.Equatables;

using System.Collections.Generic;
using System.Linq;

internal record class ParsedUnit
{
    public DefinedType UnitType { get; }
    public MinimalLocation UnitLocation { get; }
    public GeneratedUnit UnitDefinition { get; }

    public EquatableEnumerable<DerivableUnit> UnitDerivations { get; }

    public EquatableEnumerable<UnitAlias> UnitAliases { get; }
    public EquatableEnumerable<DerivedUnit> DerivedUnits { get; }
    public EquatableEnumerable<FixedUnit> FixedUnits { get; }
    public EquatableEnumerable<OffsetUnit> OffsetUnits { get; }
    public EquatableEnumerable<PrefixedUnit> PrefixedUnits { get; }
    public EquatableEnumerable<ScaledUnit> ScaledUnits { get; }

    public ParsedUnit(DefinedType unitType, MinimalLocation unitLocation, GeneratedUnit unitDefinition,
        IEnumerable<DerivableUnit> unitDerivations, IEnumerable<UnitAlias> unitAliases,
        IEnumerable<DerivedUnit> derivedUnits, IEnumerable<FixedUnit> fixedUnits,
        IEnumerable<OffsetUnit> offsetUnits, IEnumerable<PrefixedUnit> prefixedUnits,
        IEnumerable<ScaledUnit> scaledUnits)
    {
        UnitType = unitType;
        UnitLocation = unitLocation;
        UnitDefinition = unitDefinition;

        UnitDerivations = new(unitDerivations);
        UnitAliases = new(unitAliases);
        DerivedUnits = new(derivedUnits);
        FixedUnits = new(fixedUnits);
        OffsetUnits = new(offsetUnits);
        PrefixedUnits = new(prefixedUnits);
        ScaledUnits = new(scaledUnits);
    }

    public IEnumerable<IUnitDefinition> GetUnitList()
    {
        return (UnitAliases as IEnumerable<IUnitDefinition>).Concat(DerivedUnits).Concat(FixedUnits).Concat(OffsetUnits)
            .Concat(PrefixedUnits).Concat(ScaledUnits);
    }

    public IEnumerable<IUnitDefinition> GetNonDependantUnitList()
    {
        return (DerivedUnits as IEnumerable<IUnitDefinition>).Concat(FixedUnits);
    }

    public IEnumerable<IDependantUnitDefinition> GetDependantUnitList()
    {
        return (UnitAliases as IEnumerable<IDependantUnitDefinition>).Concat(OffsetUnits).Concat(PrefixedUnits).Concat(ScaledUnits);
    }
}
