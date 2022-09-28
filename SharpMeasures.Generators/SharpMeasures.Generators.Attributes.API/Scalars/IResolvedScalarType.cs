namespace SharpMeasures.Generators.Scalars;

using SharpMeasures.Generators.Quantities;

using System.Collections.Generic;

public interface IResolvedScalarType : IResolvedQuantityType
{
    public abstract bool UseUnitBias { get; }

    public abstract NamedType? Vector { get; }

    public abstract IReadOnlyList<IProcessedQuantity> Processes { get; }
    public abstract IReadOnlyList<IScalarConstant> Constants { get; }

    public abstract IReadOnlyList<IProcessedQuantity> InheritedProcesses { get; }

    public abstract IReadOnlyList<string> IncludedUnitBaseInstanceNames { get; }
}
