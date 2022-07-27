namespace SharpMeasures.Generators.Vectors.Parsing;

using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;
using SharpMeasures.Generators.Quantities.Parsing.UnitList;
using SharpMeasures.Generators.Unresolved.Quantities;
using SharpMeasures.Generators.Unresolved.Vectors;
using SharpMeasures.Generators.Vectors.Parsing.Abstraction;
using SharpMeasures.Generators.Vectors.Parsing.ConvertibleVector;
using SharpMeasures.Generators.Vectors.Parsing.RegisterVectorGroupMember;
using SharpMeasures.Generators.Vectors.Parsing.SpecializedSharpMeasuresVector;
using SharpMeasures.Generators.Vectors.Parsing.VectorConstant;

using System.Collections.Generic;

internal record class UnresolvedIndividualVectorSpecializationType : AUnresolvedIndividualVectorType<UnresolvedSpecializedSharpMeasuresVectorDefinition>,
    IUnresolvedIndividualVectorSpecializationType
{
    IUnresolvedQuantitySpecialization IUnresolvedQuantitySpecializationType.Definition => Definition;
    IUnresolvedIndividualVectorSpecialization IUnresolvedIndividualVectorSpecializationType.Definition => Definition;
    IUnresolvedVectorGroupSpecialization IUnresolvedVectorGroupSpecializationType.Definition => Definition;

    public UnresolvedIndividualVectorSpecializationType(DefinedType type, MinimalLocation typeLocation, UnresolvedSpecializedSharpMeasuresVectorDefinition definition,
        IReadOnlyDictionary<int, UnresolvedRegisterVectorGroupMemberDefinition> registeredMembersByDimension, IReadOnlyList<UnresolvedDerivedQuantityDefinition> derivations,
        IReadOnlyList<UnresolvedVectorConstantDefinition> constants, IReadOnlyList<UnresolvedConvertibleVectorDefinition> conversions,
        IReadOnlyList<UnresolvedUnitListDefinition> unitInclusions, IReadOnlyList<UnresolvedUnitListDefinition> unitExclusions)
        : base(type, typeLocation, definition, registeredMembersByDimension, derivations, constants, conversions, unitInclusions, unitExclusions) { }
}
