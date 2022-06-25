namespace SharpMeasures.Generators.Vectors.Populations;

using SharpMeasures.Equatables;

using System.Collections.Generic;

public class ResizedGroupPopulation : ReadOnlyEquatableDictionary<NamedType, ResizedGroup>
{
    public ResizedGroupPopulation(IReadOnlyDictionary<NamedType, ResizedGroup> items) : base(items) { }
}
