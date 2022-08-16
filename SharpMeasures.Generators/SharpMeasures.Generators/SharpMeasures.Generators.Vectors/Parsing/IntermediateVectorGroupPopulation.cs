namespace SharpMeasures.Generators.Vectors.Parsing;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Vectors.Groups;
using SharpMeasures.Generators.Vectors.Parsing.Abstraction;

using System.Collections.Generic;

internal class IntermediateVectorGroupPopulation : IIntermediateVectorGroupPopulation
{
    public IReadOnlyDictionary<NamedType, IVectorGroupType> VectorGroupBases => vectorGroupBases;
    public IReadOnlyDictionary<NamedType, IIntermediateVectorGroupSpecializationType> VectorGroupSpecializations => vectorGroupSpecializations;
    public IReadOnlyDictionary<NamedType, IIntermediateVectorGroupMemberType> VectorGroupMembers => vectorGroupMembers;

    private ReadOnlyEquatableDictionary<NamedType, IVectorGroupType> vectorGroupBases { get; }
    private ReadOnlyEquatableDictionary<NamedType, IIntermediateVectorGroupSpecializationType> vectorGroupSpecializations { get; }
    private ReadOnlyEquatableDictionary<NamedType, IIntermediateVectorGroupMemberType> vectorGroupMembers { get; }

    public IntermediateVectorGroupPopulation(IReadOnlyDictionary<NamedType, IVectorGroupType> vectorGroupBases,
        IReadOnlyDictionary<NamedType, IIntermediateVectorGroupSpecializationType> vectorGroupSpecializations,
        IReadOnlyDictionary<NamedType, IIntermediateVectorGroupMemberType> vectorGroupMembers)
    {
        this.vectorGroupBases = vectorGroupBases.AsReadOnlyEquatable();
        this.vectorGroupSpecializations = vectorGroupSpecializations.AsReadOnlyEquatable();
        this.vectorGroupMembers = vectorGroupMembers.AsReadOnlyEquatable();
    }
}
