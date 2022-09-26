namespace SharpMeasures.Generators.Vectors.ForeignVectorParsing;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Vectors.Parsing;

using System.Threading;

public sealed class ForeignVectorProcesser
{
    public static (ForeignVectorProcessingResult ProcessingResult, IVectorPopulation Population) ProcessAndExtend(ForeignVectorParsingResult parsingResult, IVectorPopulation unextendedVectorPopulation, CancellationToken token)
    {
        var processer = new ForeignVectorProcesser();

        if (token.IsCancellationRequested is false)
        {
            processer.Process(parsingResult);
        }

        return (processer.Finalize(), processer.Extend(unextendedVectorPopulation));
    }

    private GroupBaseProcesser GroupBaseProcesser { get; } = new(VectorProcessingDiagnosticsStrategies.EmptyDiagnostics);
    private GroupSpecializationProcesser GroupSpecializationProcesser { get; } = new(VectorProcessingDiagnosticsStrategies.EmptyDiagnostics);
    private GroupMemberProcesser GroupMemberProcesser { get; } = new(VectorProcessingDiagnosticsStrategies.EmptyDiagnostics);

    private VectorBaseProcesser VectorBaseProcesser { get; } = new(VectorProcessingDiagnosticsStrategies.EmptyDiagnostics);
    private VectorSpecializationProcesser VectorSpecializationProcesser { get; } = new(VectorProcessingDiagnosticsStrategies.EmptyDiagnostics);

    private EquatableList<GroupBaseType> GroupBases { get; } = new();
    private EquatableList<GroupSpecializationType> GroupSpecializations { get; } = new();
    private EquatableList<GroupMemberType> GroupMembers { get; } = new();

    private EquatableList<VectorBaseType> VectorBases { get; } = new();
    private EquatableList<VectorSpecializationType> VectorSpecializations { get; } = new();

    private ForeignVectorProcesser() { }

    private void Process(ForeignVectorParsingResult parsingResult)
    {
        foreach (var rawGroupBase in parsingResult.GroupBases)
        {
            var groupBase = GroupBaseProcesser.Process(rawGroupBase);

            if (groupBase.HasResult)
            {
                GroupBases.Add(groupBase.Result);
            }
        }

        foreach (var rawGroupSpecialization in parsingResult.GroupSpecializations)
        {
            var groupSpecialization = GroupSpecializationProcesser.Process(rawGroupSpecialization);

            if (groupSpecialization.HasResult)
            {
                GroupSpecializations.Add(groupSpecialization.Result);
            }
        }

        foreach (var rawMember in parsingResult.GroupMembers)
        {
            var member = GroupMemberProcesser.Process(rawMember);

            if (member.HasResult)
            {
                GroupMembers.Add(member.Result);
            }
        }

        foreach (var rawVectorBase in parsingResult.VectorBases)
        {
            var vectorBase = VectorBaseProcesser.Process(rawVectorBase);

            if (vectorBase.HasResult)
            {
                VectorBases.Add(vectorBase.Result);
            }
        }

        foreach (var rawVectorSpecialization in parsingResult.VectorSpecializations)
        {
            var vectorSpecialization = VectorSpecializationProcesser.Process(rawVectorSpecialization);

            if (vectorSpecialization.HasResult)
            {
                VectorSpecializations.Add(vectorSpecialization.Result);
            }
        }
    }

    private ForeignVectorProcessingResult Finalize() => new(GroupBases, GroupSpecializations, GroupMembers, VectorBases, VectorSpecializations);
    private IVectorPopulation Extend(IVectorPopulation vectorPopulation) => ExtendedVectorPopulation.Build(vectorPopulation, Finalize());
}
