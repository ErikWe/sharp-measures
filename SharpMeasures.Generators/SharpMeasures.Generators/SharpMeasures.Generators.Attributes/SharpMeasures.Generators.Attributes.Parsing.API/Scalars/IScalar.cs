namespace SharpMeasures.Generators.Scalars;

using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Units.UnitInstances;
using SharpMeasures.Generators.Vectors;

public interface IScalar
{
    public abstract IUnitType Unit { get; }
    public abstract bool UseUnitBias { get; }

    public abstract IVectorType? Vector { get; }

    public abstract bool ImplementSum { get; }
    public abstract bool ImplementDifference { get; }
    public abstract IScalarType Difference { get; }

    public abstract IUnitInstance? DefaultUnitName { get; }
    public abstract string? DefaultUnitSymbol { get; }

    public abstract IScalarType? Reciprocal { get; }
    public abstract IScalarType? Square { get; }
    public abstract IScalarType? Cube { get; }
    public abstract IScalarType? SquareRoot { get; }
    public abstract IScalarType? CubeRoot { get; }

    public abstract bool GenerateDocumentation { get; }
}
