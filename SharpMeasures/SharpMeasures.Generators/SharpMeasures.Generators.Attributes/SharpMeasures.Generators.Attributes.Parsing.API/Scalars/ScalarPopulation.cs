namespace SharpMeasures.Generators.Scalars;

using SharpMeasures.Equatables;

using System.Collections.Generic;

public class ScalarPopulation : ReadOnlyEquatableDictionary<NamedType, ScalarInterface>
{
    public ScalarPopulation(IReadOnlyDictionary<NamedType, ScalarInterface> population) : base(population) { }
}
