namespace SharpMeasures.Generators.Unresolved.Vectors;

using SharpMeasures.Generators.Unresolved.Quantities;

using System.Collections.Generic;

public interface IUnresolvedVectorGroupType : IUnresolvedQuantityType
{
    new public abstract IUnresolvedVectorGroup Definition { get; }

    new public abstract IReadOnlyList<IUnresolvedConvertibleVector> Conversions { get; }
}
