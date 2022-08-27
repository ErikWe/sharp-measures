namespace SharpMeasures.Generators.Scalars.Parsing.Abstraction;

using System.Collections.Generic;

internal interface IScalarPopulationWithData : IScalarPopulation
{
    public abstract IReadOnlyDictionary<NamedType, IScalarType> DuplicatelyDefinedScalars { get; }
}
