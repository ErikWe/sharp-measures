namespace SharpMeasures.Generators.Vectors;

using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Raw.Units.UnitInstances;
using SharpMeasures.Generators.Raw.Vectors.Groups;
using SharpMeasures.Generators.Vectors.Groups;
using System.Collections.Generic;

internal record class VectorGroupMemberType : IndividualVectorType, IVectorGroupMemberType
{
    IVectorGroupMember IVectorGroupMemberType.Definition => (IVectorGroupMember)Definition;

    public VectorGroupMemberType(DefinedType type, MinimalLocation typeLocation, IVectorGroupMember definition,
        IReadOnlyDictionary<int, IRawVectorGroupMemberType> MembersByDimension, IReadOnlyList<IDerivedQuantity> derivations, IReadOnlyList<IVectorConstant> constants,
        IReadOnlyList<IConvertibleVector> conversions, IReadOnlyList<IRawUnitInstance> includedUnits)
        : base(type, typeLocation, definition, MembersByDimension, derivations, constants, conversions, includedUnits) { }
}
