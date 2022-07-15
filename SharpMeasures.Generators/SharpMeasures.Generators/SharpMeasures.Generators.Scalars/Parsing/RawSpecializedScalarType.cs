namespace SharpMeasures.Generators.Scalars.Parsing;

using SharpMeasures.Generators.Quantities.Parsing.ConvertibleQuantity;
using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;
using SharpMeasures.Generators.Quantities.Parsing.UnitList;
using SharpMeasures.Generators.Scalars.Parsing.Abstractions;
using SharpMeasures.Generators.Scalars.Parsing.ScalarConstant;
using SharpMeasures.Generators.Scalars.Parsing.SpecializedSharpMeasuresScalar;

using System.Collections.Generic;

internal record class RawSpecializedScalarType : ARawScalarType
{
    public RawSpecializedSharpMeasuresScalarDefinition Definition { get; }

    public RawSpecializedScalarType(DefinedType type, MinimalLocation typeLocation, RawSpecializedSharpMeasuresScalarDefinition definition,
        IEnumerable<RawDerivedQuantityDefinition> derivations, IEnumerable<RawScalarConstantDefinition> constants,
        IEnumerable<RawConvertibleQuantityDefinition> convertibleScalars, IEnumerable<RawUnitListDefinition> baseInclusions,
        IEnumerable<RawUnitListDefinition> baseExclusions, IEnumerable<RawUnitListDefinition> unitInclusions, IEnumerable<RawUnitListDefinition> unitExclusions)
        : base(type, typeLocation, derivations, constants, convertibleScalars, baseInclusions, baseExclusions, unitInclusions, unitExclusions)
    {
        Definition = definition;
    }
}
