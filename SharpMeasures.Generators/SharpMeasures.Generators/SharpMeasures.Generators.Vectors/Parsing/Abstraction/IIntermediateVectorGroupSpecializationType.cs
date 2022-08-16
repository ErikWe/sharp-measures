namespace SharpMeasures.Generators.Vectors.Parsing.Abstraction;

using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Raw.Units.UnitInstances;
using SharpMeasures.Generators.Raw.Vectors.Groups;
using System.Collections.Generic;

internal interface IIntermediateVectorGroupSpecializationType
{
    public DefinedType Type { get; }

    public abstract IVectorGroupSpecialization Definition { get; }

    public abstract IReadOnlyDictionary<int, IRawVectorGroupMemberType> MembersByDimension { get; }

    public abstract IReadOnlyList<IDerivedQuantity> Derivations { get; }
    public abstract IReadOnlyList<IConvertibleVector> Conversions { get; }

    public abstract IReadOnlyList<IRawUnitInstance> UnitInclusions { get; }
    public abstract IReadOnlyList<IRawUnitInstance> UnitExclusions { get; }
}
