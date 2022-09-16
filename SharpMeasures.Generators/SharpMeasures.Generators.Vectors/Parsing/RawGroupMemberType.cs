﻿namespace SharpMeasures.Generators.Vectors.Parsing;

using SharpMeasures.Generators.Quantities.Parsing.ConvertibleQuantity;
using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;
using SharpMeasures.Generators.Vectors.Parsing.Abstraction;
using SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVectorGroupMember;
using SharpMeasures.Generators.Vectors.Parsing.VectorConstant;

using System.Collections.Generic;

internal record class RawGroupMemberType : ARawVectorType<RawSharpMeasuresVectorGroupMemberDefinition>
{
    public RawGroupMemberType(DefinedType type, MinimalLocation typeLocation, RawSharpMeasuresVectorGroupMemberDefinition definition, IEnumerable<RawDerivedQuantityDefinition> derivations, IEnumerable<RawVectorConstantDefinition> constants,
        IEnumerable<RawConvertibleQuantityDefinition> conversions, IEnumerable<RawIncludeUnitsDefinition> unitInstanceInclusions, IEnumerable<RawExcludeUnitsDefinition> unitInstanceExclusions)
        : base(type, typeLocation, definition, derivations, constants, conversions, unitInstanceInclusions, unitInstanceExclusions) { }
}