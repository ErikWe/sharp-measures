namespace SharpMeasures.Generators.Vectors.Parsing;

using SharpMeasures.Generators.Quantities.Parsing.ConvertibleQuantity;
using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;
using SharpMeasures.Generators.Vectors.Parsing.Abstraction;
using SharpMeasures.Generators.Vectors.Parsing.SpecializedSharpMeasuresVectorGroup;

using System.Collections.Generic;

internal sealed record class RawGroupSpecializationType : ARawGroupType<RawSpecializedSharpMeasuresVectorGroupDefinition>
{
    public RawGroupSpecializationType(DefinedType type, RawSpecializedSharpMeasuresVectorGroupDefinition definition, IEnumerable<RawDerivedQuantityDefinition> derivations, IEnumerable<RawConvertibleQuantityDefinition> conversions, IEnumerable<RawIncludeUnitsDefinition> unitInstanceInclusions, IEnumerable<RawExcludeUnitsDefinition> unitInstanceExclusions)
        : base(type, definition, derivations, conversions, unitInstanceInclusions, unitInstanceExclusions) { }
}
