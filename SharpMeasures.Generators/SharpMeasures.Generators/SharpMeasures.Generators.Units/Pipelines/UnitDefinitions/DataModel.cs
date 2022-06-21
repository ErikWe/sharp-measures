namespace SharpMeasures.Generators.Units.Pipelines.UnitDefinitions;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Units.Documentation;
using SharpMeasures.Generators.Units.Parsing.DerivedUnit;
using SharpMeasures.Generators.Units.Parsing.FixedUnit;
using SharpMeasures.Generators.Units.Parsing.BiasedUnit;
using SharpMeasures.Generators.Units.Parsing.PrefixedUnit;
using SharpMeasures.Generators.Units.Parsing.ScaledUnit;
using SharpMeasures.Generators.Units.Parsing.UnitAlias;

using System.Collections.Generic;

internal readonly record struct DataModel(DefinedType Unit, NamedType Quantity, bool Biased, IDocumentationStrategy Documentation,
    EquatableEnumerable<UnitAliasDefinition> UnitAliases, EquatableEnumerable<DerivedUnitDefinition> DerivedUnits,
    EquatableEnumerable<FixedUnitDefinition> FixedUnits, EquatableEnumerable<BiasedUnitDefinition> BiasedUnits,
    EquatableEnumerable<PrefixedUnitDefinition> PrefixedUnits, EquatableEnumerable<ScaledUnitDefinition> ScaledUnits)
{
    public DataModel(DefinedType unit, NamedType quantity, bool biased, IDocumentationStrategy documentation, IEnumerable<UnitAliasDefinition> unitAliases,
        IEnumerable<DerivedUnitDefinition> derivedUnits, IEnumerable<FixedUnitDefinition> fixedUnits, IEnumerable<BiasedUnitDefinition> biasedUnits,
        IEnumerable<PrefixedUnitDefinition> prefixedUnits, IEnumerable<ScaledUnitDefinition> scaledUnits)
        : this(unit, quantity, biased, documentation, unitAliases.AsEquatable(), derivedUnits.AsEquatable(), fixedUnits.AsEquatable(), biasedUnits.AsEquatable(),
              prefixedUnits.AsEquatable(), scaledUnits.AsEquatable()) { }
}
