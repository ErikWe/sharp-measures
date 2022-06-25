namespace SharpMeasures.Generators.Vectors.Populations;

using SharpMeasures.Equatables;

using System.Collections.Generic;

public class BaseVectorPopulation : ReadOnlyEquatableDictionary<NamedType, BaseVector>
{
    public BaseVectorPopulation(IReadOnlyDictionary<NamedType, BaseVector> items) : base(items) { }
}
