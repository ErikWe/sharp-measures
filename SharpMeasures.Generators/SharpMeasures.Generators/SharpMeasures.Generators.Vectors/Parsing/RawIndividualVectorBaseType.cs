namespace SharpMeasures.Generators.Vectors.Parsing;

using SharpMeasures.Generators.Quantities.Parsing.ConvertibleQuantity;
using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;
using SharpMeasures.Generators.Quantities.Parsing.UnitList;
using SharpMeasures.Generators.Vectors.Parsing.Abstraction;
using SharpMeasures.Generators.Vectors.Parsing.RegisterVectorGroupMember;
using SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVector;
using SharpMeasures.Generators.Vectors.Parsing.VectorConstant;

using System.Collections.Generic;

internal record class RawIndividualVectorBaseType : ARawIndividualVectorType
{
    public RawSharpMeasuresVectorDefinition Definition { get; }

    public RawIndividualVectorBaseType(DefinedType type, MinimalLocation typeLocation, RawSharpMeasuresVectorDefinition definition, IEnumerable<RawDerivedQuantityDefinition> derivations,
        IEnumerable<RawVectorConstantDefinition> constants, IEnumerable<RawConvertibleQuantityDefinition> conversions, IEnumerable<RawUnitListDefinition> unitInclusions,
        IEnumerable<RawUnitListDefinition> unitExclusions)
        : base(type, typeLocation, MockVectorGroupMembers(type, definition), derivations, constants, conversions, unitInclusions, unitExclusions)
    {
        Definition = definition;
    }

    private static IEnumerable<RawRegisterVectorGroupMemberDefinition> MockVectorGroupMembers(DefinedType type, RawSharpMeasuresVectorDefinition definition)
    {
        RawRegisterVectorGroupMemberDefinition mocked = RawRegisterVectorGroupMemberDefinition.Empty;

        mocked = mocked with
        {
            Vector = type.AsNamedType(),
            Dimension = definition.Dimension
        };

        yield return mocked;
    }
}
