namespace SharpMeasures.Generators.Vectors;

using System.Collections.Generic;

public interface IVectorConstant
{
    public abstract string Name { get; }
    public abstract string Unit { get; }
    public abstract IReadOnlyCollection<double> Value { get; }

    public abstract bool GenerateMultiplesProperty { get; }
    public abstract string? Multiples { get; }
}
