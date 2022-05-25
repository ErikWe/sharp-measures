namespace SharpMeasures.Generators.Units.Pipeline.UnitDefinitions;

using SharpMeasures.Generators.Attributes.Parsing.Units;
using SharpMeasures.Generators.Documentation;
using SharpMeasures.Generators.Utility;

using System.Collections.Generic;

internal readonly record struct DataModel(DefinedType Unit, NamedType Quantity, MinimalLocation UnitLocation, bool Biased, DocumentationFile Documentation,
    IReadOnlyList<CacheableUnitAliasDefinition> UnitAliases, IReadOnlyList<CacheableDerivedUnitDefinition> DerivedUnits,
    IReadOnlyList<CacheableFixedUnitDefinition> FixedUnits, IReadOnlyList<CacheableOffsetUnitDefinition> OffsetUnits,
    IReadOnlyList<CacheablePrefixedUnitDefinition> PrefixedUnits, IReadOnlyList<CacheableScaledUnitDefinition> ScaledUnits);