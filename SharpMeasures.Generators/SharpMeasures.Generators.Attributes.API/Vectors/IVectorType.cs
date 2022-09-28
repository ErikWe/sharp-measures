namespace SharpMeasures.Generators.Vectors;

using SharpMeasures.Generators.Quantities;

using System.Collections.Generic;

public interface IVectorType : IQuantityType
{
    new public abstract IVector Definition { get; }

    public abstract IReadOnlyList<IProcessedQuantity> Processes { get; }
    public abstract IReadOnlyList<IVectorConstant> Constants { get; }

    public abstract IReadOnlyDictionary<string, IVectorConstant> ConstantsByName { get; }
    public abstract IReadOnlyDictionary<string, IVectorConstant> ConstantsByMultiplesName { get; }
}
