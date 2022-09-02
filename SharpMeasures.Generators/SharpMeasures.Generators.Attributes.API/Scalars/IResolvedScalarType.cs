namespace SharpMeasures.Generators.Scalars;

using SharpMeasures.Generators.Quantities;

using System.Collections.Generic;

public interface IResolvedScalarType : IResolvedQuantityType
{
    public abstract bool UseUnitBias { get; }

    public abstract NamedType? Vector { get; }

    public abstract NamedType? Reciprocal { get; }
    public abstract NamedType? Square { get; }
    public abstract NamedType? Cube { get; }
    public abstract NamedType? SquareRoot { get; }
    public abstract NamedType? CubeRoot { get; }

    public abstract IReadOnlyList<IScalarConstant> Constants { get; }

    public abstract IReadOnlyList<string> IncludedUnitBaseInstancesNames { get; }
}
