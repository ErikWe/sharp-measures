namespace SharpMeasures.Generators.Units.Pipeline.UnitDefinitionsPipeline;

using SharpMeasures.Generators.Attributes.Parsing.Units;
using SharpMeasures.Generators.Documentation;
using SharpMeasures.Generators.Utility;

using System.Collections.Generic;

internal readonly record struct DataModel(DefinedType TypeDefinition, NamedType Quantity, bool Biased, DocumentationFile Documentation,
    IReadOnlyList<UnitAliasParameters> UnitAliases, IReadOnlyList<DerivedUnitParameters> DerivedUnits,
    IReadOnlyList<FixedUnitParameters> FixedUnits, IReadOnlyList<OffsetUnitParameters> OffsetUnits,
    IReadOnlyList<PrefixedUnitParameters> PrefixedUnits, IReadOnlyList<ScaledUnitParameters> ScaledUnits);