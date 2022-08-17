﻿namespace SharpMeasures.Generators.Scalars.Parsing;

using SharpMeasures.Generators.Quantities.Parsing.ConvertibleQuantity;
using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;
using SharpMeasures.Generators.Quantities.Parsing.UnitList;
using SharpMeasures.Generators.Scalars.Parsing.Abstraction;
using SharpMeasures.Generators.Scalars.Parsing.ScalarConstant;
using SharpMeasures.Generators.Scalars.Parsing.SpecializedSharpMeasuresScalar;

using System.Collections.Generic;

internal record class RawScalarSpecializationType : ARawScalarType
{
    public RawSpecializedSharpMeasuresScalarDefinition Definition { get; }

    public RawScalarSpecializationType(DefinedType type, MinimalLocation typeLocation, RawSpecializedSharpMeasuresScalarDefinition definition,
        IEnumerable<UnprocessedDerivedQuantityDefinition> derivations, IEnumerable<RawScalarConstantDefinition> constants,
        IEnumerable<UnprocessedConvertibleQuantityDefinition> conversions, IEnumerable<UnprocessedUnitListDefinition> baseInclusions,
        IEnumerable<UnprocessedUnitListDefinition> baseExclusions, IEnumerable<UnprocessedUnitListDefinition> unitInclusions, IEnumerable<UnprocessedUnitListDefinition> unitExclusions)
        : base(type, typeLocation, derivations, constants, conversions, baseInclusions, baseExclusions, unitInclusions, unitExclusions)
    {
        Definition = definition;
    }
}
