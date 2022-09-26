namespace SharpMeasures.Generators.Vectors.ForeignVectorParsing;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Vectors.Parsing;

using System.Threading;

public sealed class ForeignVectorResolver
{
    public static IResolvedVectorPopulation ResolveAndExtend(ForeignVectorProcessingResult processingResult, IUnitPopulation unitPopulation, IVectorPopulation vectorPopulation, IResolvedVectorPopulation unextendedVectorPopulation, CancellationToken token)
    {
        var resolver = new ForeignVectorResolver();

        if (token.IsCancellationRequested is false)
        {
            resolver.Resolve(processingResult, unitPopulation, vectorPopulation);
        }

        return resolver.Extend(unextendedVectorPopulation);
    }

    private EquatableList<ResolvedGroupType> Groups { get; } = new();
    private EquatableList<ResolvedVectorType> Vectors { get; } = new();

    private ForeignVectorResolver() { }

    private void Resolve(ForeignVectorProcessingResult processingResult, IUnitPopulation unitPopulation, IVectorPopulation vectorPopulation)
    {
        foreach (var processedGroupBase in processingResult.GroupBases)
        {
            var groupBase = GroupBaseResolver.Resolve(processedGroupBase, unitPopulation, vectorPopulation);

            if (groupBase.HasValue)
            {
                Groups.Add(groupBase.Value);
            }
        }

        foreach (var processedGroupSpecialization in processingResult.GroupSpecializations)
        {
            var groupSpecialization = GroupSpecializationResolver.Resolve(processedGroupSpecialization, unitPopulation, vectorPopulation);

            if (groupSpecialization.HasValue)
            {
                Groups.Add(groupSpecialization.Value);
            }
        }

        foreach (var processedMember in processingResult.GroupMembers)
        {
            var member = GroupMemberResolver.Resolve(processedMember, unitPopulation, vectorPopulation);

            if (member.HasValue)
            {
                Vectors.Add(member.Value);
            }
        }

        foreach (var processedVectorBase in processingResult.VectorBases)
        {
            var vectorBase = VectorBaseResolver.Resolve(processedVectorBase, unitPopulation);

            if (vectorBase.HasValue)
            {
                Vectors.Add(vectorBase.Value);
            }
        }

        foreach (var processedVectorSpecialization in processingResult.VectorSpecializations)
        {
            var vectorSpecialization = VectorSpecializationResolver.Resolve(processedVectorSpecialization, unitPopulation, vectorPopulation);

            if (vectorSpecialization.HasValue)
            {
                Vectors.Add(vectorSpecialization.Value);
            }
        }
    }

    private ForeignVectorResolutionResult Finalize() => new(Groups, Vectors);
    private IResolvedVectorPopulation Extend(IResolvedVectorPopulation vectorPopulation) => ExtendedResolvedVectorPopulation.Build(vectorPopulation, Finalize());
}
