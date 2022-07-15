namespace SharpMeasures.Generators.Vectors;

using System.Collections.Generic;

public interface IVectorGroupType : ISharpMeasuresObjectType
{
    new public abstract IVectorGroup Definition { get; }

    public abstract IReadOnlyList<IConvertibleVector> ConvertibleVectors { get; }
}
