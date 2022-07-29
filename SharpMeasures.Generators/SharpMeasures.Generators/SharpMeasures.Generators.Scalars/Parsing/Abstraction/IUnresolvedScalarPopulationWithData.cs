namespace SharpMeasures.Generators.Scalars.Parsing.Abstraction;

using SharpMeasures.Generators.Unresolved.Scalars;

using System.Collections.Generic;

internal interface IUnresolvedScalarPopulationWithData : IUnresolvedScalarPopulation
{
    public abstract IReadOnlyDictionary<NamedType, IUnresolvedScalarType> DuplicatelyDefined { get; }

    public abstract IReadOnlyDictionary<NamedType, IUnresolvedScalarSpecializationType> UnassignedSpecializations { get; }
}
