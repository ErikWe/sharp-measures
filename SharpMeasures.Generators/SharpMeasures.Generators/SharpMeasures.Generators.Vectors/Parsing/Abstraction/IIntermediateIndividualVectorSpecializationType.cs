namespace SharpMeasures.Generators.Vectors.Parsing.Abstraction;

using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Unresolved.Units.UnitInstances;
using SharpMeasures.Generators.Unresolved.Vectors;

using System.Collections.Generic;

internal interface IIntermediateIndividualVectorSpecializationType
{
    public DefinedType Type { get; }
    public MinimalLocation TypeLocation { get; }

    public abstract IIndividualVectorSpecialization Definition { get; }

    public abstract IReadOnlyList<IDerivedQuantity> Derivations { get; }
    public abstract IReadOnlyList<IVectorConstant> Constants { get; }
    public abstract IReadOnlyList<IConvertibleVector> Conversions { get; }

    public abstract IReadOnlyDictionary<int, IUnresolvedVectorGroupMemberType> MembersByDimension { get; }

    public abstract IReadOnlyList<IUnresolvedUnitInstance> UnitInclusions { get; }
    public abstract IReadOnlyList<IUnresolvedUnitInstance> UnitExclusions { get; }
}
