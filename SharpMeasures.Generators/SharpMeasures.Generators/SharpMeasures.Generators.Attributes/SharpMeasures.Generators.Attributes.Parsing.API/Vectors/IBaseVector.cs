namespace SharpMeasures.Generators.Vectors;

using SharpMeasures.Generators.Unresolved.Units;
using SharpMeasures.Generators.Unresolved.Vectors;

public interface IBaseVector : IVector
{
    public abstract IUnresolvedUnitType Unit { get; }

    new public abstract IUnresolvedVectorType Difference { get; }
}
