namespace SharpMeasures.Generators.Vectors.Parsing;

using SharpMeasures.Generators.Quantities.Parsing.ConvertibleQuantity;
using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;
using SharpMeasures.Generators.Quantities.Parsing.UnitList;
using SharpMeasures.Generators.Vectors.Parsing.Abstraction;
using SharpMeasures.Generators.Vectors.Parsing.RegisterVectorGroupMember;
using SharpMeasures.Generators.Vectors.Parsing.SpecializedSharpMeasuresVectorGroup;

using System.Collections.Generic;

internal record class RawVectorGroupSpecializationType : ARawVectorGroupType
{
    public RawSpecializedSharpMeasuresVectorGroupDefinition Definition { get; }

    public RawVectorGroupSpecializationType(DefinedType type, MinimalLocation typeLocation, RawSpecializedSharpMeasuresVectorGroupDefinition definition,
        IEnumerable<RawRegisterVectorGroupMemberDefinition> members, IEnumerable<RawDerivedQuantityDefinition> derivations,
        IEnumerable<RawConvertibleQuantityDefinition> conversions, IEnumerable<RawUnitListDefinition> unitInclusions, IEnumerable<RawUnitListDefinition> unitExclusions)
        : base(type, typeLocation, members, derivations, conversions, unitInclusions, unitExclusions)
    {
        Definition = definition;
    }
}
