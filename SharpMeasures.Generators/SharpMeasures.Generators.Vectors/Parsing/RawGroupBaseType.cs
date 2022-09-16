namespace SharpMeasures.Generators.Vectors.Parsing;

using SharpMeasures.Generators.Quantities.Parsing.ConvertibleQuantity;
using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;
using SharpMeasures.Generators.Vectors.Parsing.Abstraction;
using SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVectorGroup;

using System.Collections.Generic;

internal record class RawGroupBaseType : ARawGroupType<RawSharpMeasuresVectorGroupDefinition>
{
    public RawGroupBaseType(DefinedType type, MinimalLocation typeLocation, RawSharpMeasuresVectorGroupDefinition definition, IEnumerable<RawDerivedQuantityDefinition> derivations,
        IEnumerable<RawConvertibleQuantityDefinition> conversions, IEnumerable<RawIncludeUnitsDefinition> unitInstanceInclusions, IEnumerable<RawExcludeUnitsDefinition> unitInstanceExclusions)
        : base(type, typeLocation, definition, derivations, conversions, unitInstanceInclusions, unitInstanceExclusions) { }
}
