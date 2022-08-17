﻿namespace SharpMeasures.Generators.Vectors.Parsing;

using SharpMeasures.Generators.Quantities.Parsing.ConvertibleQuantity;
using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;
using SharpMeasures.Generators.Quantities.Parsing.UnitList;
using SharpMeasures.Generators.Vectors.Parsing.Abstraction;
using SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVectorGroup;

using System.Collections.Generic;

internal record class RawVectorGroupBaseType : ARawVectorGroupType
{
    public RawSharpMeasuresVectorGroupDefinition Definition { get; }

    public RawVectorGroupBaseType(DefinedType type, MinimalLocation typeLocation, RawSharpMeasuresVectorGroupDefinition definition, IEnumerable<UnprocessedDerivedQuantityDefinition> derivations,
        IEnumerable<UnprocessedConvertibleQuantityDefinition> conversions, IEnumerable<UnprocessedUnitListDefinition> unitInclusions, IEnumerable<UnprocessedUnitListDefinition> unitExclusions)
        : base(type, typeLocation, derivations, conversions, unitInclusions, unitExclusions)
    {
        Definition = definition;
    }
}
