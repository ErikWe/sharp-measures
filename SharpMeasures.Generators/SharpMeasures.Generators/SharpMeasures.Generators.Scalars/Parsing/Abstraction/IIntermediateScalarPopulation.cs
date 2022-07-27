namespace SharpMeasures.Generators.Scalars.Parsing.Abstraction;

using System.Collections.Generic;

internal interface IIntermediateScalarPopulation
{
    public abstract IReadOnlyDictionary<NamedType, IScalarType> ScalarBases { get; }
    public abstract IReadOnlyDictionary<NamedType, IIntermediateScalarSpecializationType> ScalarSpecializations { get; }
}
