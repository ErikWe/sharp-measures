namespace SharpMeasures.Generators.Scalars.Parsing.Abstraction;

using SharpMeasures.Generators.Raw.Scalars;

using System.Collections.Generic;

internal interface IUnresolvedScalarPopulationWithData : IRawScalarPopulation
{
    public abstract IReadOnlyDictionary<NamedType, IRawScalarType> DuplicatelyDefined { get; }
}
