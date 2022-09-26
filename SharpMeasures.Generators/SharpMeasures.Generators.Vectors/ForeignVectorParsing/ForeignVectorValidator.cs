namespace SharpMeasures.Generators.Vectors.ForeignVectorParsing;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Vectors.Parsing;

using System.Threading;

public sealed class ForeignVectorValidator
{
    public static (ForeignVectorProcessingResult ValidationResult, IVectorPopulation Population) ValidateAndExtend(ForeignVectorProcessingResult processingResult, IUnitPopulation unitPopulation, IScalarPopulation scalarPopulation, IVectorPopulation vectorPopulation, IVectorPopulation unextendedVectorPopulation, CancellationToken token)
    {
        var validator = new ForeignVectorValidator();

        if (token.IsCancellationRequested is false)
        {
            validator.Validate(processingResult, unitPopulation, scalarPopulation, vectorPopulation);
        }

        return (validator.Finalize(), validator.Extend(unextendedVectorPopulation));
    }

    private GroupBaseValidator GroupBaseValidator { get; } = new(VectorValidationDiagnosticsStrategies.EmptyDiagnostics);
    private GroupSpecializationValidator GroupSpecializationValidator { get; } = new(VectorValidationDiagnosticsStrategies.EmptyDiagnostics);
    private GroupMemberValidator GroupMemberValidator { get; } = new(VectorValidationDiagnosticsStrategies.EmptyDiagnostics);

    private VectorBaseValidator VectorBaseValidator { get; } = new(VectorValidationDiagnosticsStrategies.EmptyDiagnostics);
    private VectorSpecializationValidator VectorSpecializationValidator { get; } = new(VectorValidationDiagnosticsStrategies.EmptyDiagnostics);

    private EquatableList<GroupBaseType> GroupBases { get; } = new();
    private EquatableList<GroupSpecializationType> GroupSpecializations { get; } = new();
    private EquatableList<GroupMemberType> GroupMembers { get; } = new();

    private EquatableList<VectorBaseType> VectorBases { get; } = new();
    private EquatableList<VectorSpecializationType> VectorSpecializations { get; } = new();

    private ForeignVectorValidator() { }

    private void Validate(ForeignVectorProcessingResult processingResult, IUnitPopulation unitPopulation, IScalarPopulation scalarPopulation, IVectorPopulation vectorPopulation)
    {
        foreach (var processedGroupBase in processingResult.GroupBases)
        {
            var groupBase = GroupBaseValidator.Validate(processedGroupBase, VectorProcessingData.Empty, unitPopulation, scalarPopulation, vectorPopulation);

            if (groupBase.HasResult)
            {
                GroupBases.Add(groupBase.Result);
            }
        }

        foreach (var processedGroupSpecialization in processingResult.GroupSpecializations)
        {
            var groupSpecialization = GroupSpecializationValidator.Validate(processedGroupSpecialization, VectorProcessingData.Empty, unitPopulation, scalarPopulation, vectorPopulation);

            if (groupSpecialization.HasResult)
            {
                GroupSpecializations.Add(groupSpecialization.Result);
            }
        }

        foreach (var processedMember in processingResult.GroupMembers)
        {
            var member = GroupMemberValidator.Validate(processedMember, VectorProcessingData.Empty, unitPopulation, scalarPopulation, vectorPopulation);

            if (member.HasResult)
            {
                GroupMembers.Add(member.Result);
            }
        }

        foreach (var processedVectorBase in processingResult.VectorBases)
        {
            var vectorBase = VectorBaseValidator.Validate(processedVectorBase, VectorProcessingData.Empty, unitPopulation, scalarPopulation, vectorPopulation);

            if (vectorBase.HasResult)
            {
                VectorBases.Add(vectorBase.Result);
            }
        }

        foreach (var processedVectorSpecialization in processingResult.VectorSpecializations)
        {
            var vectorSpecialization = VectorSpecializationValidator.Validate(processedVectorSpecialization, VectorProcessingData.Empty, unitPopulation, scalarPopulation, vectorPopulation);

            if (vectorSpecialization.HasResult)
            {
                VectorSpecializations.Add(vectorSpecialization.Result);
            }
        }
    }

    private ForeignVectorProcessingResult Finalize() => new(GroupBases, GroupSpecializations, GroupMembers, VectorBases, VectorSpecializations);
    private IVectorPopulation Extend(IVectorPopulation vectorPopulation) => ExtendedVectorPopulation.Build(vectorPopulation, Finalize());
}
