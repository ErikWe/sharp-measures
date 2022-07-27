namespace SharpMeasures.Generators.Scalars.Parsing.Abstraction;

using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Unresolved.Units.UnitInstances;

using System.Collections.Generic;

internal interface IIntermediateScalarSpecializationType
{
    public DefinedType Type { get; }

    public abstract IScalarSpecialization Definition { get; }

    public abstract IReadOnlyList<IDerivedQuantity> Derivations { get; }
    public abstract IReadOnlyList<IScalarConstant> Constants { get; }
    public abstract IReadOnlyList<IConvertibleScalar> Conversions { get; }

    public abstract IReadOnlyList<IUnresolvedUnitInstance> BaseInclusions { get; }
    public abstract IReadOnlyList<IUnresolvedUnitInstance> BaseExclusions { get; }

    public abstract IReadOnlyList<IUnresolvedUnitInstance> UnitInclusions { get; }
    public abstract IReadOnlyList<IUnresolvedUnitInstance> UnitExclusions { get; }
}
