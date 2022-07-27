namespace SharpMeasures.Generators.Vectors.Parsing;

using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;
using SharpMeasures.Generators.Quantities.Parsing.UnitList;
using SharpMeasures.Generators.Unresolved.Quantities;
using SharpMeasures.Generators.Unresolved.Vectors;
using SharpMeasures.Generators.Vectors.Parsing.Abstraction;
using SharpMeasures.Generators.Vectors.Parsing.ConvertibleVector;
using SharpMeasures.Generators.Vectors.Parsing.RegisterVectorGroupMember;
using SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVectorGroup;

using System.Collections.Generic;

internal record class UnresolvedVectorGroupBaseType : AUnresolvedVectorGroupType<UnresolvedSharpMeasuresVectorGroupDefinition>, IUnresolvedVectorGroupBaseType
{
    IUnresolvedVectorGroupBase IUnresolvedVectorGroupBaseType.Definition => Definition;
    IUnresolvedQuantityBase IUnresolvedQuantityBaseType.Definition => Definition;

    public UnresolvedVectorGroupBaseType(DefinedType type, MinimalLocation typeLocation, UnresolvedSharpMeasuresVectorGroupDefinition definition,
        IReadOnlyDictionary<int, UnresolvedRegisterVectorGroupMemberDefinition> registeredMembersByDimension, IReadOnlyList<UnresolvedDerivedQuantityDefinition> derivations,
        IReadOnlyList<UnresolvedConvertibleVectorDefinition> conversions, IReadOnlyList<UnresolvedUnitListDefinition> unitInclusions,
        IReadOnlyList<UnresolvedUnitListDefinition> unitExclusions)
        : base(type, typeLocation, definition, registeredMembersByDimension, derivations, conversions, unitInclusions, unitExclusions) { }
}
