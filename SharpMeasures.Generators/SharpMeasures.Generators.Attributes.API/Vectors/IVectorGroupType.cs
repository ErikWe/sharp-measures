namespace SharpMeasures.Generators.Vectors;

using SharpMeasures.Generators.Quantities;

using System.Collections.Generic;

public interface IVectorGroupType : IQuantityType
{
    new public abstract IVectorGroup Definition { get; }
}
