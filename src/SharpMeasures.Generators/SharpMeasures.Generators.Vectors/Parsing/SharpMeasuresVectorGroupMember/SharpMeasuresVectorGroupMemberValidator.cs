namespace SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVectorGroupMember;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;

internal interface ISharpMeasuresVectorGroupMemberValidationDiagnostics
{
    public abstract Diagnostic? TypeAlreadyUnit(ISharpMeasuresVectorGroupMemberValidationContext context, SharpMeasuresVectorGroupMemberDefinition definition);
    public abstract Diagnostic? TypeAlreadyScalar(ISharpMeasuresVectorGroupMemberValidationContext context, SharpMeasuresVectorGroupMemberDefinition definition);
    public abstract Diagnostic? TypeAlreadyVector(ISharpMeasuresVectorGroupMemberValidationContext context, SharpMeasuresVectorGroupMemberDefinition definition);
    public abstract Diagnostic? TypeAlreadyVectorGroup(ISharpMeasuresVectorGroupMemberValidationContext context, SharpMeasuresVectorGroupMemberDefinition definition);
    public abstract Diagnostic? TypeNotVectorGroup(ISharpMeasuresVectorGroupMemberValidationContext context, SharpMeasuresVectorGroupMemberDefinition definition);
    public abstract Diagnostic? VectorGroupAlreadyContainsDimension(ISharpMeasuresVectorGroupMemberValidationContext context, SharpMeasuresVectorGroupMemberDefinition definition);
}

internal interface ISharpMeasuresVectorGroupMemberValidationContext : IValidationContext
{
    public abstract VectorProcessingData ProcessingData { get; }

    public abstract IUnitPopulation UnitPopulation { get; }
    public abstract IScalarPopulation ScalarPopulation { get; }
    public abstract IVectorPopulation VectorPopulation { get; }
}

internal sealed class SharpMeasuresVectorGroupMemberValidator : IValidator<ISharpMeasuresVectorGroupMemberValidationContext, SharpMeasuresVectorGroupMemberDefinition>
{
    private ISharpMeasuresVectorGroupMemberValidationDiagnostics Diagnostics { get; }

    public SharpMeasuresVectorGroupMemberValidator(ISharpMeasuresVectorGroupMemberValidationDiagnostics diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public IValidityWithDiagnostics Validate(ISharpMeasuresVectorGroupMemberValidationContext context, SharpMeasuresVectorGroupMemberDefinition definition)
    {
        return ValidateTypeNotAlreadyUnit(context, definition)
            .Validate(() => ValidateTypeNotAlreadyScalar(context, definition))
            .Validate(() => ValidateTypeNotAlreadyVector(context, definition))
            .Validate(() => ValidateTypeNotAlreadyVectorGroup(context, definition))
            .Validate(() => ValidateTypeNotDuplicatelyDefined(context))
            .Validate(() => ValidateVectorGroupIsVectorGroup(context, definition))
            .Validate(() => ValidateVectorGroupNotAlreadyHasDimension(context, definition));
    }

    private IValidityWithDiagnostics ValidateTypeNotAlreadyUnit(ISharpMeasuresVectorGroupMemberValidationContext context, SharpMeasuresVectorGroupMemberDefinition definition)
    {
        var typeAlreadyUnit = context.UnitPopulation.Units.ContainsKey(context.Type.AsNamedType());

        return ValidityWithDiagnostics.Conditional(typeAlreadyUnit is false, () => Diagnostics.TypeAlreadyUnit(context, definition));
    }

    private IValidityWithDiagnostics ValidateTypeNotAlreadyScalar(ISharpMeasuresVectorGroupMemberValidationContext context, SharpMeasuresVectorGroupMemberDefinition definition)
    {
        var typeAlreadyScalar = context.ScalarPopulation.Scalars.ContainsKey(context.Type.AsNamedType());

        return ValidityWithDiagnostics.Conditional(typeAlreadyScalar is false, () => Diagnostics.TypeAlreadyScalar(context, definition));
    }

    private IValidityWithDiagnostics ValidateTypeNotAlreadyVector(ISharpMeasuresVectorGroupMemberValidationContext context, SharpMeasuresVectorGroupMemberDefinition definition)
    {
        var typeAlreadyVector = context.VectorPopulation.Vectors.ContainsKey(context.Type.AsNamedType());

        return ValidityWithDiagnostics.Conditional(typeAlreadyVector is false, () => Diagnostics.TypeAlreadyVector(context, definition));
    }

    private IValidityWithDiagnostics ValidateTypeNotAlreadyVectorGroup(ISharpMeasuresVectorGroupMemberValidationContext context, SharpMeasuresVectorGroupMemberDefinition definition)
    {
        var typeAlreadyVectorGroup = context.VectorPopulation.Groups.ContainsKey(context.Type.AsNamedType());

        return ValidityWithDiagnostics.Conditional(typeAlreadyVectorGroup is false, () => Diagnostics.TypeAlreadyVectorGroup(context, definition));
    }

    private static IValidityWithDiagnostics ValidateTypeNotDuplicatelyDefined(ISharpMeasuresVectorGroupMemberValidationContext context)
    {
        var typeDuplicatelyDefined = context.ProcessingData.DuplicatelyDefinedGroupMembers.ContainsKey(context.Type.AsNamedType());

        return ValidityWithDiagnostics.ConditionalWithoutDiagnostics(typeDuplicatelyDefined is false);
    }

    private IValidityWithDiagnostics ValidateVectorGroupIsVectorGroup(ISharpMeasuresVectorGroupMemberValidationContext context, SharpMeasuresVectorGroupMemberDefinition definition)
    {
        var vectorGroupIsVectorGroup = context.VectorPopulation.Groups.ContainsKey(definition.VectorGroup);

        return ValidityWithDiagnostics.Conditional(vectorGroupIsVectorGroup, () => Diagnostics.TypeNotVectorGroup(context, definition));
    }

    private IValidityWithDiagnostics ValidateVectorGroupNotAlreadyHasDimension(ISharpMeasuresVectorGroupMemberValidationContext context, SharpMeasuresVectorGroupMemberDefinition definition)
    {
        var vectorGroupNotHavingDimension = context.VectorPopulation.GroupMembersByGroup[definition.VectorGroup].GroupMembersByDimension[definition.Dimension].Type == context.Type;

        return ValidityWithDiagnostics.Conditional(vectorGroupNotHavingDimension, () => Diagnostics.VectorGroupAlreadyContainsDimension(context, definition));
    }
}
