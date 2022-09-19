namespace SharpMeasures.Generators.Vectors.ForeignVectorParsing;

using SharpMeasures.Equatables;

public interface IForeignVectorProcesser
{
    public abstract (IVectorPopulation Population, IForeignVectorValidator Validator) ProcessAndExtend(IVectorPopulation unextendedVectorPopulation);
}

internal sealed record class ForeignVectorProcesser : IForeignVectorProcesser
{
    private ForeignVectorParsingResult ParsingResult { get; }

    private EquatableList<GroupBaseType> GroupBases { get; } = new();
    private EquatableList<GroupSpecializationType> GroupSpecializations { get; } = new();
    private EquatableList<GroupMemberType> GroupMembers { get; } = new();

    private EquatableList<VectorBaseType> VectorBases { get; } = new();
    private EquatableList<VectorSpecializationType> VectorSpecializations { get; } = new();

    public ForeignVectorProcesser(ForeignVectorParsingResult parsingResult)
    {
        ParsingResult = parsingResult;
    }

    public (IVectorPopulation, IForeignVectorValidator) ProcessAndExtend(IVectorPopulation unextendedVectorPopulation)
    {
        ForeignGroupBaseProcesser groupBaseProcesser = new();
        ForeignGroupSpecializationProcesser groupSpecializationProcesser = new();

        ForeignVectorBaseProcesser vectorBaseProcesser = new();
        ForeignVectorSpecializationProcesser vectorSpecializationProcesser = new();

        foreach (var rawGroupBase in ParsingResult.GroupBases)
        {
            var groupBase = groupBaseProcesser.Process(rawGroupBase);

            if (groupBase.HasValue)
            {
                GroupBases.Add(groupBase.Value);
            }
        }

        foreach (var rawGroupSpecialization in ParsingResult.GroupSpecializations)
        {
            var groupSpecialization = groupSpecializationProcesser.Process(rawGroupSpecialization);

            if (groupSpecialization.HasValue)
            {
                GroupSpecializations.Add(groupSpecialization.Value);
            }
        }

        foreach (var rawMember in ParsingResult.GroupMembers)
        {
            var member = ForeignGroupMemberProcesser.Process(rawMember);

            if (member.HasValue)
            {
                GroupMembers.Add(member.Value);
            }
        }

        foreach (var rawVectorBase in ParsingResult.VectorBases)
        {
            var vectorBase = vectorBaseProcesser.Process(rawVectorBase);

            if (vectorBase.HasValue)
            {
                VectorBases.Add(vectorBase.Value);
            }
        }

        foreach (var rawVectorSpecialization in ParsingResult.VectorSpecializations)
        {
            var vectorSpecialization = vectorSpecializationProcesser.Process(rawVectorSpecialization);

            if (vectorSpecialization.HasValue)
            {
                VectorSpecializations.Add(vectorSpecialization.Value);
            }
        }

        var result = new ForeignVectorProcessingResult(GroupBases, GroupSpecializations, GroupMembers, VectorBases, VectorSpecializations);
        var extendedPopulation = ExtendedVectorPopulation.Build(unextendedVectorPopulation, result);
        var validator = new ForeignVectorValidator(result);

        return (extendedPopulation, validator);
    }
}
