namespace SharpMeasures.Generators.Vectors.ForeignVectorParsing;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Units;

public interface IForeignVectorResolver
{
    public abstract IResolvedVectorPopulation ResolveAndExtend(IUnitPopulation unitPopulation, IVectorPopulation vectorPopulation, IResolvedVectorPopulation unextendedVectorPopulation);
}

internal sealed record class ForeignVectorResolver : IForeignVectorResolver
{
    private ForeignVectorProcessingResult ProcessingResult { get; }

    private EquatableList<ResolvedGroupType> Groups { get; } = new();
    private EquatableList<ResolvedVectorType> Vectors { get; } = new();

    public ForeignVectorResolver(ForeignVectorProcessingResult processingResult)
    {
        ProcessingResult = processingResult;
    }

    public IResolvedVectorPopulation ResolveAndExtend(IUnitPopulation unitPopulation, IVectorPopulation vectorPopulation, IResolvedVectorPopulation unextendedVectorPopulation)
    {
        foreach (var processedGroupBase in ProcessingResult.GroupBases)
        {
            var groupBase = ForeignGroupBaseResolver.Resolve(processedGroupBase, unitPopulation, vectorPopulation);

            if (groupBase.HasValue)
            {
                Groups.Add(groupBase.Value);
            }
        }

        foreach (var processedGroupSpecialization in ProcessingResult.GroupSpecializations)
        {
            var groupSpecialization = ForeignGroupSpecializationResolver.Resolve(processedGroupSpecialization, unitPopulation, vectorPopulation);

            if (groupSpecialization.HasValue)
            {
                Groups.Add(groupSpecialization.Value);
            }
        }

        foreach (var processedMember in ProcessingResult.GroupMembers)
        {
            var member = ForeignGroupMemberResolver.Resolve(processedMember, unitPopulation, vectorPopulation);

            if (member.HasValue)
            {
                Vectors.Add(member.Value);
            }
        }

        foreach (var processedVectorBase in ProcessingResult.VectorBases)
        {
            var vectorBase = ForeignVectorBaseResolver.Resolve(processedVectorBase, unitPopulation);

            if (vectorBase.HasValue)
            {
                Vectors.Add(vectorBase.Value);
            }
        }

        foreach (var processedVectorSpecialization in ProcessingResult.VectorSpecializations)
        {
            var vectorSpecialization = ForeignVectorSpecializationResolver.Resolve(processedVectorSpecialization, unitPopulation, vectorPopulation);

            if (vectorSpecialization.HasValue)
            {
                Vectors.Add(vectorSpecialization.Value);
            }
        }

        var result = new ForeignVectorResolutionResult(Groups, Vectors);

        return ExtendedResolvedVectorPopulation.Build(unextendedVectorPopulation, result);
    }
}
