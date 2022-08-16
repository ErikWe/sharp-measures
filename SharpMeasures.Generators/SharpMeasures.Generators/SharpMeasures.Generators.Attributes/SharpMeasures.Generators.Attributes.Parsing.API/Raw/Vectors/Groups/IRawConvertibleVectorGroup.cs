namespace SharpMeasures.Generators.Raw.Vectors.Groups;

using SharpMeasures.Generators.Raw.Quantities;

using System.Collections.Generic;

public interface IRawConvertibleVectorGroup : IRawConvertibleQuantity
{
    public abstract IReadOnlyList<NamedType> VectorGroups { get; }
}
