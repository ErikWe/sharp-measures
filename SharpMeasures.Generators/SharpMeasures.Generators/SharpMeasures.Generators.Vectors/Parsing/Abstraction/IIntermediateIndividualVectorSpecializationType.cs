namespace SharpMeasures.Generators.Vectors.Parsing.Abstraction;

using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Unresolved.Units.UnitInstances;

using System.Collections.Generic;

internal interface IIntermediateIndividualVectorSpecializationType
{
    public DefinedType Type { get; }

    public abstract IIndividualVectorSpecialization Definition { get; }

    public abstract IReadOnlyList<IDerivedQuantity> Derivations { get; }
    public abstract IReadOnlyList<IVectorConstant> Constants { get; }
    public abstract IReadOnlyList<IConvertibleVector> Conversions { get; }

    public abstract IReadOnlyList<IUnresolvedUnitInstance> UnitInclusions { get; }
    public abstract IReadOnlyList<IUnresolvedUnitInstance> UnitExclusions { get; }
}
