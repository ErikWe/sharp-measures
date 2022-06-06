namespace SharpMeasures.Generators.Scalars;

using SharpMeasures.Equatables;

using System.Collections.Generic;

internal class ScalarPopulation : EquatableDictionary<NamedType, ScalarInterface>
{
    public ScalarPopulation(IReadOnlyDictionary<NamedType, ScalarInterface> population) : base(population) { }
}
