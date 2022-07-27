namespace SharpMeasures.Generators.Vectors;

using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Unresolved.Vectors;

using System.Collections.Generic;

public interface IConvertibleVector : IConvertibleQuantity
{
    public abstract IReadOnlyList<IUnresolvedVectorGroupType> VectorGroups { get; }
}
