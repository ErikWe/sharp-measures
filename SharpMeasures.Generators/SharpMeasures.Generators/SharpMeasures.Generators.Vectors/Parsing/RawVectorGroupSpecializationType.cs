namespace SharpMeasures.Generators.Vectors.Parsing;

using SharpMeasures.Generators.Quantities.Parsing.ConvertibleQuantity;
using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;
using SharpMeasures.Generators.Quantities.Parsing.UnitList;
using SharpMeasures.Generators.Vectors.Parsing.Abstraction;
using SharpMeasures.Generators.Vectors.Parsing.SpecializedSharpMeasuresVectorGroup;

using System.Collections.Generic;

internal record class RawVectorGroupSpecializationType : ARawVectorGroupType
{
    public RawSpecializedSharpMeasuresVectorGroupDefinition Definition { get; }

    public RawVectorGroupSpecializationType(DefinedType type, MinimalLocation typeLocation, RawSpecializedSharpMeasuresVectorGroupDefinition definition, IEnumerable<UnprocessedDerivedQuantityDefinition> derivations,
        IEnumerable<UnprocessedConvertibleQuantityDefinition> conversions, IEnumerable<UnprocessedUnitListDefinition> unitInclusions, IEnumerable<UnprocessedUnitListDefinition> unitExclusions)
        : base(type, typeLocation, derivations, conversions, unitInclusions, unitExclusions)
    {
        Definition = definition;
    }
}
