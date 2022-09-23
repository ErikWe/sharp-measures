namespace SharpMeasures.Generators.Vectors.ForeignVectorParsing;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;

using System.Threading;

public interface IForeignVectorValidator
{
    public abstract (IVectorPopulation Population, IForeignVectorResolver Resolver) ValidateAndExtend(IUnitPopulation unitPopulation, IScalarPopulation scalarPopulation, IVectorPopulation vectorPopulation, IVectorPopulation unextendedVectorPopulation, CancellationToken token);
}

internal sealed record class ForeignVectorValidator : IForeignVectorValidator
{
    private ForeignVectorProcessingResult ProcessingResult { get; }

    private EquatableList<GroupBaseType> GroupBases { get; } = new();
    private EquatableList<GroupSpecializationType> GroupSpecializations { get; } = new();
    private EquatableList<GroupMemberType> GroupMembers { get; } = new();

    private EquatableList<VectorBaseType> VectorBases { get; } = new();
    private EquatableList<VectorSpecializationType> VectorSpecializations { get; } = new();

    public ForeignVectorValidator(ForeignVectorProcessingResult processingResult)
    {
        ProcessingResult = processingResult;
    }

    public (IVectorPopulation, IForeignVectorResolver) ValidateAndExtend(IUnitPopulation unitPopulation, IScalarPopulation scalarPopulation, IVectorPopulation vectorPopulation, IVectorPopulation unextendedVectorPopulation, CancellationToken token)
    {
        if (token.IsCancellationRequested is false)
        {
            foreach (var processedGroupBase in ProcessingResult.GroupBases)
            {
                var groupBase = ForeignGroupBaseValidator.Validate(processedGroupBase, unitPopulation, scalarPopulation, vectorPopulation);

                if (groupBase.HasValue)
                {
                    GroupBases.Add(groupBase.Value);
                }
            }

            foreach (var processedGroupSpecialization in ProcessingResult.GroupSpecializations)
            {
                var groupSpecialization = ForeignGroupSpecializationValidator.Validate(processedGroupSpecialization, unitPopulation, scalarPopulation, vectorPopulation);

                if (groupSpecialization.HasValue)
                {
                    GroupSpecializations.Add(groupSpecialization.Value);
                }
            }

            foreach (var processedMember in ProcessingResult.GroupMembers)
            {
                var member = ForeignGroupMemberValidator.Validate(processedMember, unitPopulation, scalarPopulation, vectorPopulation);

                if (member.HasValue)
                {
                    GroupMembers.Add(member.Value);
                }
            }

            foreach (var processedVectorBase in ProcessingResult.VectorBases)
            {
                var vectorBase = ForeignVectorBaseValidator.Validate(processedVectorBase, unitPopulation, scalarPopulation, vectorPopulation);

                if (vectorBase.HasValue)
                {
                    VectorBases.Add(vectorBase.Value);
                }
            }

            foreach (var processedVectorSpecialization in ProcessingResult.VectorSpecializations)
            {
                var vectorSpecialization = ForeignVectorSpecializationValidator.Validate(processedVectorSpecialization, unitPopulation, scalarPopulation, vectorPopulation);

                if (vectorSpecialization.HasValue)
                {
                    VectorSpecializations.Add(vectorSpecialization.Value);
                }
            }
        }

        var result = new ForeignVectorProcessingResult(GroupBases, GroupSpecializations, GroupMembers, VectorBases, VectorSpecializations);
        var extendedPopulation = ExtendedVectorPopulation.Build(unextendedVectorPopulation, result);
        var validator = new ForeignVectorResolver(result);

        return (extendedPopulation, validator);
    }
}
