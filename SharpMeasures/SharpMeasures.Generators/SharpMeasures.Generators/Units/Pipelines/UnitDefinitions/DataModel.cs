namespace SharpMeasures.Generators.Units.Pipelines.UnitDefinitions;

using SharpMeasures.Generators.Attributes.Parsing.Units;
using SharpMeasures.Generators.Documentation;

using System.Collections.Generic;

internal readonly record struct DataModel(DefinedType Unit, NamedType Quantity, bool Biased, DocumentationFile Documentation,
    IEnumerable<UnitAliasDefinition> UnitAliases, IEnumerable<DerivedUnitDefinition> DerivedUnits,
    IEnumerable<FixedUnitDefinition> FixedUnits, IEnumerable<OffsetUnitDefinition> OffsetUnits,
    IEnumerable<PrefixedUnitDefinition> PrefixedUnits, IEnumerable<ScaledUnitDefinition> ScaledUnits);