namespace SharpMeasures.Generators.Scalars.Parsing;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Scalars.Parsing.Abstraction;

using System.Collections.Generic;

internal class IntermediateScalarPopulation : IIntermediateScalarPopulation
{
    public IReadOnlyDictionary<NamedType, IScalarType> ScalarBases => scalarBases;
    public IReadOnlyDictionary<NamedType, IIntermediateScalarSpecializationType> ScalarSpecializations => scalarSpecializations;

    private ReadOnlyEquatableDictionary<NamedType, IScalarType> scalarBases { get; }
    private ReadOnlyEquatableDictionary<NamedType, IIntermediateScalarSpecializationType> scalarSpecializations { get; }

    public IntermediateScalarPopulation(IReadOnlyDictionary<NamedType, IScalarType> scalarBases, IReadOnlyDictionary<NamedType, IIntermediateScalarSpecializationType> scalarSpecializations)
    {
        this.scalarBases = scalarBases.AsReadOnlyEquatable();
        this.scalarSpecializations = scalarSpecializations.AsReadOnlyEquatable();
    }
}
