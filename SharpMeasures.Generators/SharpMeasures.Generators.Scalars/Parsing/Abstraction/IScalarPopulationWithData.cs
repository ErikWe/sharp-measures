namespace SharpMeasures.Generators.Scalars.Parsing.Abstraction;

using System.Collections.Generic;

internal interface IScalarPopulationWithData : IScalarPopulation
{
    public abstract IReadOnlyDictionary<NamedType, IScalarBaseType> DuplicatelyDefinedScalarBases { get; }
    public abstract IReadOnlyDictionary<NamedType, IScalarSpecializationType> DuplicatelyDefinedScalarSpecializations { get; }
    public abstract IReadOnlyDictionary<NamedType, IScalarSpecializationType> ScalarSpecializationsAlreadyDefinedAsScalarBases { get; }
}
