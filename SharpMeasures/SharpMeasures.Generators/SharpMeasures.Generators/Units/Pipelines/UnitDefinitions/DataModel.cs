namespace SharpMeasures.Generators.Units.Pipelines.UnitDefinitions;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Attributes.Parsing.Units;
using SharpMeasures.Generators.Documentation;

using System.Collections.Generic;

internal readonly record struct DataModel(DefinedType Unit, NamedType Quantity, bool Biased, DocumentationFile Documentation,
    EquatableEnumerable<UnitAlias> UnitAliases, EquatableEnumerable<DerivedUnit> DerivedUnits,
    EquatableEnumerable<FixedUnit> FixedUnits, EquatableEnumerable<OffsetUnit> OffsetUnits,
    EquatableEnumerable<PrefixedUnit> PrefixedUnits, EquatableEnumerable<ScaledUnit> ScaledUnits)
{
    public DataModel(DefinedType unit, NamedType quantity, bool biased, DocumentationFile documentation, IEnumerable<UnitAlias> unitAliases,
        IEnumerable<DerivedUnit> derivedUnits, IEnumerable<FixedUnit> fixedUnits, IEnumerable<OffsetUnit> offsetUnits, IEnumerable<PrefixedUnit> prefixedUnits,
        IEnumerable<ScaledUnit> scaledUnits)
        : this(unit, quantity, biased, documentation, new(unitAliases), new(derivedUnits), new(fixedUnits), new(offsetUnits), new(prefixedUnits),
              new(scaledUnits)) { }
}
