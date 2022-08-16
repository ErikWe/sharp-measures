namespace SharpMeasures.Generators.Vectors.Groups;

using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Raw.Vectors.Groups;

using System.Collections.Generic;

public interface IConvertibleVectorGroup : IConvertibleQuantity
{
    public abstract IReadOnlyList<IRawVectorGroupType> VectorGroups { get; }
}
