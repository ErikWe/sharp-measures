namespace SharpMeasures.Generators.Units.Pipelines.UnitDefinitions;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Documentation;
using SharpMeasures.Generators.Units.Parsing.DerivedUnit;
using SharpMeasures.Generators.Units.Parsing.FixedUnit;
using SharpMeasures.Generators.Units.Parsing.OffsetUnit;
using SharpMeasures.Generators.Units.Parsing.PrefixedUnit;
using SharpMeasures.Generators.Units.Parsing.ScaledUnit;
using SharpMeasures.Generators.Units.Parsing.UnitAlias;

using System.Collections.Generic;

internal readonly record struct DataModel(DefinedType Unit, NamedType Quantity, bool Biased, DocumentationFile Documentation,
    EquatableEnumerable<UnitAliasDefinition> UnitAliases, EquatableEnumerable<DerivedUnitDefinition> DerivedUnits,
    EquatableEnumerable<FixedUnitDefinition> FixedUnits, EquatableEnumerable<OffsetUnitDefinition> OffsetUnits,
    EquatableEnumerable<PrefixedUnitDefinition> PrefixedUnits, EquatableEnumerable<ScaledUnitDefinition> ScaledUnits)
{
    public DataModel(DefinedType unit, NamedType quantity, bool biased, DocumentationFile documentation, IEnumerable<UnitAliasDefinition> unitAliases,
        IEnumerable<DerivedUnitDefinition> derivedUnits, IEnumerable<FixedUnitDefinition> fixedUnits, IEnumerable<OffsetUnitDefinition> offsetUnits,
        IEnumerable<PrefixedUnitDefinition> prefixedUnits, IEnumerable<ScaledUnitDefinition> scaledUnits)
        : this(unit, quantity, biased, documentation, new(unitAliases), new(derivedUnits), new(fixedUnits), new(offsetUnits), new(prefixedUnits),
              new(scaledUnits)) { }
}
