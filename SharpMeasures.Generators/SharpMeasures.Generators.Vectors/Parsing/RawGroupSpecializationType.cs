namespace SharpMeasures.Generators.Vectors.Parsing;

using SharpMeasures.Generators.Quantities.Parsing.ConvertibleQuantity;
using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;
using SharpMeasures.Generators.Vectors.Parsing.Abstraction;
using SharpMeasures.Generators.Vectors.Parsing.SpecializedSharpMeasuresVectorGroup;

using System.Collections.Generic;

internal record class RawGroupSpecializationType : ARawGroupType<RawSpecializedSharpMeasuresVectorGroupDefinition>
{
    public RawGroupSpecializationType(DefinedType type, MinimalLocation typeLocation, RawSpecializedSharpMeasuresVectorGroupDefinition definition, IEnumerable<RawDerivedQuantityDefinition> derivations,
        IEnumerable<RawConvertibleQuantityDefinition> conversions, IEnumerable<RawIncludeUnitsDefinition> unitInstanceInclusions, IEnumerable<RawExcludeUnitsDefinition> unitInstanceExclusions)
        : base(type, typeLocation, definition, derivations, conversions, unitInstanceInclusions, unitInstanceExclusions) { }
}
