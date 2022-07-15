namespace SharpMeasures.Generators.Unresolved.Vectors;

using SharpMeasures.Generators.Unresolved.Quantities;

using System.Collections.Generic;

public interface IUnresolvedVectorType : IUnresolvedQuantityType
{
    new public abstract IUnresolvedVector Definition { get; }

    new public abstract IReadOnlyList<IUnresolvedVectorConstant> Constants { get; }
    public abstract IReadOnlyList<IUnresolvedConvertibleQuantity> ConvertibleVectors { get; }
}
