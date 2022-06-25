namespace SharpMeasures.Generators.Vectors.Populations;

using SharpMeasures.Equatables;

using System.Collections.Generic;

public class ResizedVectorPopulation : ReadOnlyEquatableDictionary<NamedType, ResizedVector>
{
    public ResizedVectorPopulation(IReadOnlyDictionary<NamedType, ResizedVector> items) : base(items) { }
}
