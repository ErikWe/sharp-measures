namespace SharpMeasures.Generators.Vectors;

using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Unresolved.Units.UnitInstances;
using SharpMeasures.Generators.Unresolved.Vectors;

using System.Collections.Generic;

internal record class VectorGroupMemberType : IndividualVectorType, IVectorGroupMemberType
{
    IVectorGroupMember IVectorGroupMemberType.Definition => (IVectorGroupMember)Definition;

    public VectorGroupMemberType(DefinedType type, MinimalLocation typeLocation, IVectorGroupMember definition,
        IReadOnlyDictionary<int, IUnresolvedVectorGroupMemberType> MembersByDimension, IReadOnlyList<IDerivedQuantity> derivations, IReadOnlyList<IVectorConstant> constants,
        IReadOnlyList<IConvertibleVector> conversions, IReadOnlyList<IUnresolvedUnitInstance> includedUnits)
        : base(type, typeLocation, definition, MembersByDimension, derivations, constants, conversions, includedUnits) { }
}
