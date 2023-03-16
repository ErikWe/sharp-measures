namespace SharpMeasures.Generators.Quantities.Parsing.QuantityProcess;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;

using System.Collections.Generic;
using System.Linq;

public interface IQuantityProcessProcessingDiagnostics
{
    public abstract Diagnostic? NullName(IQuantityProcessProcessingContext context, RawQuantityProcessDefinition definition);
    public abstract Diagnostic? EmptyName(IQuantityProcessProcessingContext context, RawQuantityProcessDefinition definition);
    public abstract Diagnostic? DuplicateProcess(IQuantityProcessProcessingContext context, RawQuantityProcessDefinition definition);
    public abstract Diagnostic? NullExpression(IQuantityProcessProcessingContext context, RawQuantityProcessDefinition definition);
    public abstract Diagnostic? EmptyExpression(IQuantityProcessProcessingContext context, RawQuantityProcessDefinition definition);

    public abstract Diagnostic? PropertyCannotBeUsedWithParameters(IQuantityProcessProcessingContext context, RawQuantityProcessDefinition definition);

    public abstract Diagnostic? UnmatchedParameterDefinitions(IQuantityProcessProcessingContext context, RawQuantityProcessDefinition definition);
    public abstract Diagnostic? NullParameterTypeElement(IQuantityProcessProcessingContext context, RawQuantityProcessDefinition definition, int index);
    public abstract Diagnostic? NullParameterNameElement(IQuantityProcessProcessingContext context, RawQuantityProcessDefinition definition, int index);
    public abstract Diagnostic? EmptyParameterNameElement(IQuantityProcessProcessingContext context, RawQuantityProcessDefinition definition, int index);
    public abstract Diagnostic? DuplicateParameterNameElement(IQuantityProcessProcessingContext context, RawQuantityProcessDefinition definition, int index);
}

public interface IQuantityProcessProcessingContext : IProcessingContext
{
    public abstract HashSet<string> ReservedNames { get; }
    public abstract HashSet<(string, IReadOnlyList<NamedType>)> ReservedMethodSignatures { get; }
}

public sealed class QuantityProcessProcesser : AActionableProcesser<IQuantityProcessProcessingContext, RawQuantityProcessDefinition, QuantityProcessDefinition>
{
    private IQuantityProcessProcessingDiagnostics Diagnostics { get; }

    public QuantityProcessProcesser(IQuantityProcessProcessingDiagnostics diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override void OnSuccessfulProcess(IQuantityProcessProcessingContext context, RawQuantityProcessDefinition definition, QuantityProcessDefinition product)
    {
        context.ReservedNames.Add(definition.Name!);

        if (product.ImplementAsProperty is false)
        {
            context.ReservedMethodSignatures.Add((definition.Name!, product.ParameterTypes));
        }
    }

    public override IOptionalWithDiagnostics<QuantityProcessDefinition> Process(IQuantityProcessProcessingContext context, RawQuantityProcessDefinition definition)
    {
        var validity = ValidateNameNotNull(context, definition)
            .Validate(() => ValidateNameNotEmpty(context, definition))
            .Validate(() => ValidateExpressionNotNull(context, definition))
            .Validate(() => ValidateExpressionNotEmpty(context, definition))
            .Validate(() => ValidateMatchedParameterDefinitions(context, definition));

        if (validity.IsInvalid)
        {
            return validity.AsEmptyOptional<QuantityProcessDefinition>();
        }

        var parameterTypes = ProcessParameterTypes(context, definition);

        if (parameterTypes.LacksResult)
        {
            return validity.AsEmptyOptional<QuantityProcessDefinition>().AddDiagnostics(parameterTypes);
        }

        var parameterNames = ProcessParameterNames(context, definition);

        if (parameterNames.LacksResult)
        {
            return validity.AsEmptyOptional<QuantityProcessDefinition>().AddDiagnostics(parameterTypes).AddDiagnostics(parameterNames);
        }

        validity = validity.Validate(() => ValidateNotDuplicate(context, definition, parameterTypes.Result));

        var implementAsProperty = ProcessImplementAsProperty(context, definition);

        QuantityProcessDefinition product = new(definition.Name!, definition.Result, definition.Expression!, implementAsProperty.Result, definition.ImplementStatically, parameterTypes.Result, parameterNames.Result, definition.Locations);
        var allDiagnostics = validity.Diagnostics.Concat(parameterTypes.Diagnostics).Concat(parameterNames.Diagnostics).Concat(implementAsProperty.Diagnostics);

        return OptionalWithDiagnostics.Result(product, allDiagnostics);
    }

    private IValidityWithDiagnostics ValidateNameNotNull(IQuantityProcessProcessingContext context, RawQuantityProcessDefinition definition)
    {
        return ValidityWithDiagnostics.Conditional(definition.Name is not null, () => Diagnostics.NullName(context, definition));
    }

    private IValidityWithDiagnostics ValidateNameNotEmpty(IQuantityProcessProcessingContext context, RawQuantityProcessDefinition definition)
    {
        return ValidityWithDiagnostics.Conditional(definition.Name!.Length > 0, () => Diagnostics.EmptyName(context, definition));
    }

    private IValidityWithDiagnostics ValidateExpressionNotNull(IQuantityProcessProcessingContext context, RawQuantityProcessDefinition definition)
    {
        return ValidityWithDiagnostics.Conditional(definition.Expression is not null, () => Diagnostics.NullExpression(context, definition));
    }

    private IValidityWithDiagnostics ValidateExpressionNotEmpty(IQuantityProcessProcessingContext context, RawQuantityProcessDefinition definition)
    {
        return ValidityWithDiagnostics.Conditional(definition.Expression!.Length > 0, () => Diagnostics.NullExpression(context, definition));
    }

    private IResultWithDiagnostics<bool> ProcessImplementAsProperty(IQuantityProcessProcessingContext context, RawQuantityProcessDefinition definition)
    {
        if (definition.ImplementAsProperty is false)
        {
            return ResultWithDiagnostics.Construct(false);
        }

        var canImplementAsProperty = definition.Locations.ExplicitlySetParameterTypes is false && definition.ParameterTypes.Count > 0;

        return ResultWithDiagnostics.ConditionalDiagnostics(canImplementAsProperty, canImplementAsProperty, () => Diagnostics.PropertyCannotBeUsedWithParameters(context, definition));
    }

    private IValidityWithDiagnostics ValidateMatchedParameterDefinitions(IQuantityProcessProcessingContext context, RawQuantityProcessDefinition definition)
    {
        var matchedDefinitions = definition.Locations.ExplicitlySetParameterNames is false || definition.ParameterTypes.Count == definition.ParameterNames.Count;

        return ValidityWithDiagnostics.Conditional(matchedDefinitions, () => Diagnostics.UnmatchedParameterDefinitions(context, definition));
    }

    private IOptionalWithDiagnostics<IReadOnlyList<NamedType>> ProcessParameterTypes(IQuantityProcessProcessingContext context, RawQuantityProcessDefinition definition)
    {
        var parameterTypes = new NamedType[definition.ParameterTypes.Count];

        for (var i = 0; i < definition.ParameterTypes.Count; i++)
        {
            if (definition.ParameterTypes[i] is not NamedType parameterType)
            {
                return OptionalWithDiagnostics.Empty<IReadOnlyList<NamedType>>(Diagnostics.NullParameterTypeElement(context, definition, i));
            }

            parameterTypes[i] = parameterType;
        }

        return OptionalWithDiagnostics.Result(parameterTypes as IReadOnlyList<NamedType>);
    }

    private IOptionalWithDiagnostics<IReadOnlyList<string>> ProcessParameterNames(IQuantityProcessProcessingContext context, RawQuantityProcessDefinition definition)
    {
        var parameterNames = new string[definition.ParameterNames.Count];
        HashSet<string> listedNames = new();

        for (var i = 0; i < definition.ParameterNames.Count; i++)
        {
            if (definition.ParameterNames[i] is not string parameterName)
            {
                return OptionalWithDiagnostics.Empty<IReadOnlyList<string>>(Diagnostics.NullParameterNameElement(context, definition, i));
            }

            if (parameterName.Length is 0)
            {
                return OptionalWithDiagnostics.Empty<IReadOnlyList<string>>(Diagnostics.EmptyParameterNameElement(context, definition, i));
            }

            if (listedNames.Add(parameterName) is false)
            {
                return OptionalWithDiagnostics.Empty<IReadOnlyList<string>>(Diagnostics.DuplicateParameterNameElement(context, definition, i));
            }

            parameterNames[i] = parameterName;
        }

        return OptionalWithDiagnostics.Result(parameterNames as IReadOnlyList<string>);
    }

    private IValidityWithDiagnostics ValidateNotDuplicate(IQuantityProcessProcessingContext context, RawQuantityProcessDefinition definition, IReadOnlyList<NamedType> parameterTypes)
    {
        var nameIsDuplicate = context.ReservedNames.Contains(definition.Name!) || (definition.ImplementAsProperty is false && context.ReservedMethodSignatures.Contains((definition.Name!, parameterTypes)));

        return ValidityWithDiagnostics.Conditional(nameIsDuplicate is false, () => Diagnostics.DuplicateProcess(context, definition));
    }
}
