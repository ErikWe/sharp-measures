namespace SharpMeasures.Generators.Raw.Vectors.Groups;

using SharpMeasures.Generators.Raw.Quantities;
using System.Collections.Generic;

public interface IRawVectorGroupType : IRawQuantityType
{
    new public abstract IRawVectorGroup Definition { get; }

    new public abstract IReadOnlyList<IRawConvertibleVectorGroup> Conversions { get; }
}
