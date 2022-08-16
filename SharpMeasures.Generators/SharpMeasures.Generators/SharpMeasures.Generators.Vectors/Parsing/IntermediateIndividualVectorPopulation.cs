namespace SharpMeasures.Generators.Vectors.Parsing;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Vectors.Parsing.Abstraction;

using System.Collections.Generic;

internal class IntermediateIndividualVectorPopulation : IIntermediateIndividualVectorPopulation
{
    public IReadOnlyDictionary<NamedType, IVectorType> VectorBases => vectorBases;
    public IReadOnlyDictionary<NamedType, IIntermediateIndividualVectorSpecializationType> VectorSpecializations => vectorSpecializations;

    private ReadOnlyEquatableDictionary<NamedType, IVectorType> vectorBases { get; }
    private ReadOnlyEquatableDictionary<NamedType, IIntermediateIndividualVectorSpecializationType> vectorSpecializations { get; }

    public IntermediateIndividualVectorPopulation(IReadOnlyDictionary<NamedType, IVectorType> vectorBases,
        IReadOnlyDictionary<NamedType, IIntermediateIndividualVectorSpecializationType> vectorSpecializations)
    {
        this.vectorBases = vectorBases.AsReadOnlyEquatable();
        this.vectorSpecializations = vectorSpecializations.AsReadOnlyEquatable();
    }
}
