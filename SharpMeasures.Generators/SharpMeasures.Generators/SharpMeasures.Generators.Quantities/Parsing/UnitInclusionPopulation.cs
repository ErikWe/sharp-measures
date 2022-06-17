namespace SharpMeasures.Generators.Quantities.Parsing;

using SharpMeasures.Equatables;

using System.Collections.Generic;

public class UnitInclusionPopulation : ReadOnlyEquatableDictionary<NamedType, UnitInclusion>
{
    public UnitInclusionPopulation(IReadOnlyDictionary<NamedType, UnitInclusion> items) : base(items) { }
}
