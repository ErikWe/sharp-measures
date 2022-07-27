namespace SharpMeasures.Generators.Vectors;

using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Unresolved.Units.UnitInstances;

using System.Collections.Generic;

internal record class VectorGroupMemberType : IndividualVectorType, IVectorGroupMemberType
{
    IVectorGroupMember IVectorGroupMemberType.Definition => (IVectorGroupMember)Definition;

    public VectorGroupMemberType(DefinedType type, MinimalLocation typeLocation, IVectorGroupMember definition,
        IReadOnlyDictionary<int, IRegisteredVectorGroupMember> registeredMembersByDimension,
        IReadOnlyList<IDerivedQuantity> derivations, IReadOnlyList<IVectorConstant> constants, IReadOnlyList<IConvertibleVector> conversions,
        IReadOnlyList<IUnresolvedUnitInstance> includedUnits)
        : base(type, typeLocation, definition, registeredMembersByDimension, derivations, constants, conversions, includedUnits) { }
}
