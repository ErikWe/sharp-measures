namespace SharpMeasures.Generators.Vectors;

using SharpMeasures.Generators.Quantities;

using System.Collections.Generic;

public interface IVectorType : IQuantityType
{
    new public abstract IVector Definition { get; }

    new public abstract IReadOnlyList<IVectorConstant> Constants { get; }
    public abstract IReadOnlyList<IConvertibleVector> ConvertibleVectors { get; }

    new public IReadOnlyDictionary<string, IVectorConstant> ConstantsByName { get; }
    new public IReadOnlyDictionary<string, IVectorConstant> ConstantsByMultiplesName { get; }
}
