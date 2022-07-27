namespace SharpMeasures.Generators.Vectors.Parsing;

using SharpMeasures.Generators.Quantities.Parsing.ConvertibleQuantity;
using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;
using SharpMeasures.Generators.Quantities.Parsing.UnitList;
using SharpMeasures.Generators.Vectors.Parsing.Abstraction;
using SharpMeasures.Generators.Vectors.Parsing.RegisterVectorGroupMember;
using SharpMeasures.Generators.Vectors.Parsing.SpecializedSharpMeasuresVector;
using SharpMeasures.Generators.Vectors.Parsing.VectorConstant;

using System;
using System.Collections.Generic;

internal record class RawIndividualVectorSpecializationType : ARawIndividualVectorType
{
    public RawSpecializedSharpMeasuresVectorDefinition Definition { get; }

    public RawIndividualVectorSpecializationType(DefinedType type, MinimalLocation typeLocation, RawSpecializedSharpMeasuresVectorDefinition definition,
        IEnumerable<RawDerivedQuantityDefinition> derivations, IEnumerable<RawVectorConstantDefinition> constants,
        IEnumerable<RawConvertibleQuantityDefinition> conversions, IEnumerable<RawUnitListDefinition> unitInclusions, IEnumerable<RawUnitListDefinition> unitExclusions)
        : base(type, typeLocation, Array.Empty<RawRegisterVectorGroupMemberDefinition>(), derivations, constants, conversions, unitInclusions, unitExclusions)
    {
        Definition = definition;
    }
}
