namespace SharpMeasures.Generators.Raw.Vectors;

using SharpMeasures.Generators.Raw.Quantities;

using System.Collections.Generic;

public interface IRawVectorType : IRawQuantityType
{
    new public abstract IRawVector Definition { get; }

    public abstract IReadOnlyList<IRawVectorConstant> Constants { get; }
}
