namespace SharpMeasures.Generators.Units.Pipeline.UnitDefinitionsPipeline;

using SharpMeasures.Generators.Attributes.Parsing.Units;
using SharpMeasures.Generators.Documentation;
using SharpMeasures.Generators.Utility;

using System.Collections.Generic;

internal readonly record struct DataModel(DefinedType TypeDefinition, NamedType Quantity, bool Biased, DocumentationFile Documentation,
    IReadOnlyList<UnitAliasDefinition> UnitAliases, IReadOnlyList<DerivedUnitDefinition> DerivedUnits,
    IReadOnlyList<FixedUnitDefinition> FixedUnits, IReadOnlyList<OffsetUnitDefinition> OffsetUnits,
    IReadOnlyList<PrefixedUnitDefinition> PrefixedUnits, IReadOnlyList<ScaledUnitDefinition> ScaledUnits);