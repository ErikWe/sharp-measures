namespace SharpMeasures.Generators.Quantities.Parsing.ProcessedQuantity;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;

using System.Collections.Generic;
using System.Linq;

public interface IProcessedQuantityProcessingDiagnostics
{
    public abstract Diagnostic? NullName(IProcessedQuantityProcessingContext context, RawProcessedQuantityDefinition definition);
    public abstract Diagnostic? EmptyName(IProcessedQuantityProcessingContext context, RawProcessedQuantityDefinition definition);
    public abstract Diagnostic? DuplicateName(IProcessedQuantityProcessingContext context, RawProcessedQuantityDefinition definition);
    public abstract Diagnostic? NullExpression(IProcessedQuantityProcessingContext context, RawProcessedQuantityDefinition definition);
    public abstract Diagnostic? EmptyExpression(IProcessedQuantityProcessingContext context, RawProcessedQuantityDefinition definition);

    public abstract Diagnostic? PropertyCannotBeUsedWithParameters(IProcessedQuantityProcessingContext context, RawProcessedQuantityDefinition definition);

    public abstract Diagnostic? UnmatchedParameterDefinitions(IProcessedQuantityProcessingContext context, RawProcessedQuantityDefinition definition);
    public abstract Diagnostic? NullParameterTypeElement(IProcessedQuantityProcessingContext context, RawProcessedQuantityDefinition definition, int index);
    public abstract Diagnostic? NullParameterNameElement(IProcessedQuantityProcessingContext context, RawProcessedQuantityDefinition definition, int index);
    public abstract Diagnostic? EmptyParameterNameElement(IProcessedQuantityProcessingContext context, RawProcessedQuantityDefinition definition, int index);
    public abstract Diagnostic? DuplicateParameterNameElement(IProcessedQuantityProcessingContext context, RawProcessedQuantityDefinition definition, int index);
}

public interface IProcessedQuantityProcessingContext : IProcessingContext
{
    public abstract HashSet<string> ReservedNames { get; }
}

public sealed class ProcessedQuantityProcesser : AActionableProcesser<IProcessedQuantityProcessingContext, RawProcessedQuantityDefinition, ProcessedQuantityDefinition>
{
    private IProcessedQuantityProcessingDiagnostics Diagnostics { get; }

    public ProcessedQuantityProcesser(IProcessedQuantityProcessingDiagnostics diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override void OnSuccessfulProcess(IProcessedQuantityProcessingContext context, RawProcessedQuantityDefinition definition, ProcessedQuantityDefinition product)
    {
        context.ReservedNames.Add(definition.Name!);
    }

    public override IOptionalWithDiagnostics<ProcessedQuantityDefinition> Process(IProcessedQuantityProcessingContext context, RawProcessedQuantityDefinition definition)
    {
        var validity = ValidateNameNotNull(context, definition)
            .Validate(() => ValidateNameNotEmpty(context, definition))
            .Validate(() => ValidateNameNotDuplicate(context, definition))
            .Validate(() => ValidateExpressionNotNull(context, definition))
            .Validate(() => ValidateExpressionNotEmpty(context, definition))
            .Validate(() => ValidateMatchedParameterDefinitions(context, definition))
            .Validate(() => ValidateParameterTypeElementsNotNull(context, definition))
            .Validate(() => ValidateParameterNameElementsNotNullOrEmptyOrDuplicate(context, definition));

        if (validity.IsInvalid)
        {
            return validity.AsEmptyOptional<ProcessedQuantityDefinition>();
        }

        var implementAsProperty = ProcessImplementAsProperty(context, definition);

        ProcessedQuantityDefinition product = new(definition.Name!, definition.Result, definition.Expression!, implementAsProperty.Result, definition.ImplementStatically, resultsInCurrentType: definition.Result is null, definition.ParameterTypes.Select(static (type) => type!.Value).ToList(), definition.ParameterNames.Select(static (type) => type!).ToList(), definition.Locations);
        var allDiagnostics = validity.Diagnostics.Concat(implementAsProperty.Diagnostics);

        return OptionalWithDiagnostics.Result(product, allDiagnostics);
    }

    private IValidityWithDiagnostics ValidateNameNotNull(IProcessedQuantityProcessingContext context, RawProcessedQuantityDefinition definition)
    {
        return ValidityWithDiagnostics.Conditional(definition.Name is not null, () => Diagnostics.NullName(context, definition));
    }

    private IValidityWithDiagnostics ValidateNameNotEmpty(IProcessedQuantityProcessingContext context, RawProcessedQuantityDefinition definition)
    {
        return ValidityWithDiagnostics.Conditional(definition.Name!.Length > 0, () => Diagnostics.EmptyName(context, definition));
    }

    private IValidityWithDiagnostics ValidateNameNotDuplicate(IProcessedQuantityProcessingContext context, RawProcessedQuantityDefinition definition)
    {
        var nameIsDuplicate = context.ReservedNames.Contains(definition.Name!);

        return ValidityWithDiagnostics.Conditional(nameIsDuplicate is false, () => Diagnostics.DuplicateName(context, definition));
    }

    private IValidityWithDiagnostics ValidateExpressionNotNull(IProcessedQuantityProcessingContext context, RawProcessedQuantityDefinition definition)
    {
        return ValidityWithDiagnostics.Conditional(definition.Expression is not null, () => Diagnostics.NullExpression(context, definition));
    }

    private IValidityWithDiagnostics ValidateExpressionNotEmpty(IProcessedQuantityProcessingContext context, RawProcessedQuantityDefinition definition)
    {
        return ValidityWithDiagnostics.Conditional(definition.Expression!.Length > 0, () => Diagnostics.NullExpression(context, definition));
    }

    private IResultWithDiagnostics<bool> ProcessImplementAsProperty(IProcessedQuantityProcessingContext context, RawProcessedQuantityDefinition definition)
    {
        if (definition.ImplementAsProperty is false)
        {
            return ResultWithDiagnostics.Construct(false);
        }

        var canImplementAsProperty = definition.Locations.ExplicitlySetParameterTypes is false && definition.ParameterTypes.Count > 0;

        return ResultWithDiagnostics.ConditionalDiagnostics(canImplementAsProperty, canImplementAsProperty, () => Diagnostics.PropertyCannotBeUsedWithParameters(context, definition));
    }

    private IValidityWithDiagnostics ValidateMatchedParameterDefinitions(IProcessedQuantityProcessingContext context, RawProcessedQuantityDefinition definition)
    {
        var matchedDefinitions = definition.ParameterTypes.Count == definition.ParameterNames.Count;

        return ValidityWithDiagnostics.Conditional(matchedDefinitions, () => Diagnostics.UnmatchedParameterDefinitions(context, definition));
    }

    private IValidityWithDiagnostics ValidateParameterTypeElementsNotNull(IProcessedQuantityProcessingContext context, RawProcessedQuantityDefinition definition)
    {
        for (int i = 0; i < definition.ParameterTypes.Count; i++)
        {
            if (definition.ParameterTypes[i] is null)
            {
                return ValidityWithDiagnostics.Invalid(Diagnostics.NullParameterTypeElement(context, definition, i));
            }
        }

        return ValidityWithDiagnostics.Valid;
    }

    private IValidityWithDiagnostics ValidateParameterNameElementsNotNullOrEmptyOrDuplicate(IProcessedQuantityProcessingContext context, RawProcessedQuantityDefinition definition)
    {
        HashSet<string> listedNames = new();

        for (int i = 0; i < definition.ParameterNames.Count; i++)
        {
            if (definition.ParameterNames[i] is null)
            {
                return ValidityWithDiagnostics.Invalid(Diagnostics.NullParameterNameElement(context, definition, i));
            }

            if (definition.ParameterNames[i]!.Length is 0)
            {
                return ValidityWithDiagnostics.Invalid(Diagnostics.EmptyParameterNameElement(context, definition, i));
            }

            if (listedNames.Add(definition.ParameterNames[i]!) is false)
            {
                return ValidityWithDiagnostics.Invalid(Diagnostics.DuplicateParameterNameElement(context, definition, i));
            }
        }

        return ValidityWithDiagnostics.Valid;
    }
}
