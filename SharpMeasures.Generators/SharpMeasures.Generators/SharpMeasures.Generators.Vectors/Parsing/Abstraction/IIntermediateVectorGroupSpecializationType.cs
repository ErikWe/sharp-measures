namespace SharpMeasures.Generators.Vectors.Parsing.Abstraction;

using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Unresolved.Units.UnitInstances;
using SharpMeasures.Generators.Unresolved.Vectors;

using System.Collections.Generic;

internal interface IIntermediateVectorGroupSpecializationType
{
    public DefinedType Type { get; }

    public abstract IVectorGroupSpecialization Definition { get; }

    public abstract IReadOnlyDictionary<int, IUnresolvedVectorGroupMemberType> MembersByDimension { get; }

    public abstract IReadOnlyList<IDerivedQuantity> Derivations { get; }
    public abstract IReadOnlyList<IConvertibleVector> Conversions { get; }

    public abstract IReadOnlyList<IUnresolvedUnitInstance> UnitInclusions { get; }
    public abstract IReadOnlyList<IUnresolvedUnitInstance> UnitExclusions { get; }
}
