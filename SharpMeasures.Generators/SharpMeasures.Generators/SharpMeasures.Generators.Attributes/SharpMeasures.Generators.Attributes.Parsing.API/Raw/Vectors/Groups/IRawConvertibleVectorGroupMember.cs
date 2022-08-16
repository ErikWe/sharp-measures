namespace SharpMeasures.Generators.Raw.Vectors.Groups;

using SharpMeasures.Generators.Raw.Quantities;

using System.Collections.Generic;

public interface IRawConvertibleVectorGroupMember : IRawConvertibleQuantity
{
    public abstract IReadOnlyList<NamedType> Vectors { get; }
}
