namespace SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVectorGroupMember;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;

internal interface ISharpMeasuresVectorGroupMemberProcessingDiagnostics
{
    public abstract Diagnostic? NullVectorGroup(IProcessingContext context, RawSharpMeasuresVectorGroupMemberDefinition definition);

    public abstract Diagnostic? MissingDimension(IProcessingContext context, RawSharpMeasuresVectorGroupMemberDefinition definition);
    public abstract Diagnostic? InvalidDimension(IProcessingContext context, RawSharpMeasuresVectorGroupMemberDefinition definition);
    public abstract Diagnostic? InvalidInterpretedDimension(IProcessingContext context, RawSharpMeasuresVectorGroupMemberDefinition definition, int dimension);
    public abstract Diagnostic? VectorNameAndDimensionConflict(IProcessingContext context, RawSharpMeasuresVectorGroupMemberDefinition definition, int interpretedDimension);
}

internal sealed class SharpMeasuresVectorGroupMemberProcesser : AProcesser<IProcessingContext, RawSharpMeasuresVectorGroupMemberDefinition, SharpMeasuresVectorGroupMemberDefinition>
{
    private ISharpMeasuresVectorGroupMemberProcessingDiagnostics Diagnostics { get; }

    public SharpMeasuresVectorGroupMemberProcesser(ISharpMeasuresVectorGroupMemberProcessingDiagnostics diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override IOptionalWithDiagnostics<SharpMeasuresVectorGroupMemberDefinition> Process(IProcessingContext context, RawSharpMeasuresVectorGroupMemberDefinition definition)
    {
        return VerifyRequiredPropertiesSet(definition)
            .Validate(() => ValidateVectorGroupNotNull(context, definition))
            .Validate(() => ValidateVectorGroupNotUndefined(definition))
            .Validate(() => ValidateSpecifiedDimensionNotInvalid(context, definition))
            .Merge(() => ProcessDimension(context, definition))
            .Validate((dimension) => ValidateInterpretedDimensionNotInvalid(context, definition, dimension))
            .Validate((dimension) => ValidateSpecifiedAndInterpretedDimensionNotConflicts(context, definition, dimension))
            .Transform((dimension) => ProduceResult(definition, dimension));
    }

    private static SharpMeasuresVectorGroupMemberDefinition ProduceResult(RawSharpMeasuresVectorGroupMemberDefinition definition, int dimension)
    {
        return new(definition.VectorGroup!.Value, definition.InheritDerivations, definition.InheritConversions, definition.InheritUnits, definition.InheritDerivationsFromMembers ?? definition.InheritDerivations, definition.InheritProcessesFromMembers,
            definition.InheritConstantsFromMembers, definition.InheritConversionsFromMembers ?? definition.InheritConversions, definition.InheritUnitsFromMembers ?? definition.InheritUnits, dimension, definition.GenerateDocumentation, definition.Locations);
    }

    private static IValidityWithDiagnostics VerifyRequiredPropertiesSet(RawSharpMeasuresVectorGroupMemberDefinition definition)
    {
        return ValidityWithDiagnostics.ConditionalWithoutDiagnostics(definition.Locations.ExplicitlySetVectorGroup);
    }

    private IValidityWithDiagnostics ValidateVectorGroupNotNull(IProcessingContext context, RawSharpMeasuresVectorGroupMemberDefinition definition)
    {
        return ValidityWithDiagnostics.Conditional(definition.VectorGroup is not null, () => Diagnostics.NullVectorGroup(context, definition));
    }

    private static IValidityWithDiagnostics ValidateVectorGroupNotUndefined(RawSharpMeasuresVectorGroupMemberDefinition definition)
    {
        return ValidityWithDiagnostics.ConditionalWithoutDiagnostics(definition.VectorGroup!.Value != NamedType.Empty);
    }

    private IValidityWithDiagnostics ValidateSpecifiedDimensionNotInvalid(IProcessingContext context, RawSharpMeasuresVectorGroupMemberDefinition definition)
    {
        var invalidSpecifiedDimension = definition.Locations.ExplicitlySetDimension && DimensionParsingUtility.CheckVectorDimensionValidity(definition.Dimension!.Value) is false;

        return ValidityWithDiagnostics.Conditional(invalidSpecifiedDimension is false, () => Diagnostics.InvalidDimension(context, definition));
    }

    private IOptionalWithDiagnostics<int> ProcessDimension(IProcessingContext context, RawSharpMeasuresVectorGroupMemberDefinition definition)
    {
        int? dimension = DimensionParsingUtility.InterpretDimensionFromName(context.Type.Name) ?? definition.Dimension;

        return OptionalWithDiagnostics.Conditional(dimension is not null, () => dimension!.Value, () => Diagnostics.MissingDimension(context, definition));
    }

    private IValidityWithDiagnostics ValidateInterpretedDimensionNotInvalid(IProcessingContext context, RawSharpMeasuresVectorGroupMemberDefinition definition, int dimension)
    {
        var invalidInterpretedDimension = definition.Locations.ExplicitlySetDimension is false && DimensionParsingUtility.CheckVectorDimensionValidity(dimension) is false;

        return ValidityWithDiagnostics.Conditional(invalidInterpretedDimension is false, () => Diagnostics.InvalidInterpretedDimension(context, definition, dimension));
    }

    private IValidityWithDiagnostics ValidateSpecifiedAndInterpretedDimensionNotConflicts(IProcessingContext context, RawSharpMeasuresVectorGroupMemberDefinition definition, int dimension)
    {
        var specifiedAndInterpretedDimensionConflicts = definition.Locations.ExplicitlySetDimension && dimension != definition.Dimension;

        return ValidityWithDiagnostics.Conditional(specifiedAndInterpretedDimensionConflicts is false, () => Diagnostics.VectorNameAndDimensionConflict(context, definition, dimension));
    }
}
