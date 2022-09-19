namespace SharpMeasures.Generators.Units.Parsing.Abstractions;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;

using System.Collections.Generic;

internal interface IModifiedUnitInstanceValidationDiagnostics<in TDefinition> where TDefinition : IModifiedUnitInstance
{
    public abstract Diagnostic? UnrecognizedOriginalUnitInstance(IModifiedUnitInstanceValidationContext context, TDefinition definition);
    public abstract Diagnostic? CyclicallyModified(IModifiedUnitInstanceValidationContext context, TDefinition definition);
}

internal interface IModifiedUnitInstanceValidationContext : IValidationContext
{
    public abstract IReadOnlyDictionary<string, IUnitInstance> UnitInstancesByName { get; }

    public abstract HashSet<IModifiedUnitInstance> CyclicallyModifiedUnits { get; } 
}

internal abstract class AModifiedUnitInstanceValidator<TContext, TDefinition> : AActionableValidator<TContext, TDefinition>
    where TContext : IModifiedUnitInstanceValidationContext
    where TDefinition : IModifiedUnitInstance
{
    private IModifiedUnitInstanceValidationDiagnostics<TDefinition> Diagnostics { get; }

    protected AModifiedUnitInstanceValidator(IModifiedUnitInstanceValidationDiagnostics<TDefinition> diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override IValidityWithDiagnostics Validate(TContext context, TDefinition definition)
    {
        return ValidateOriginalUnitInstanceIsUnitInstance(context, definition)
            .Validate(() => ValidateNotCyclicallyModified(context, definition));
    }

    private IValidityWithDiagnostics ValidateOriginalUnitInstanceIsUnitInstance(TContext context, TDefinition definition)
    {
        var originalUnitInstanceCorrectlyResolved = context.UnitInstancesByName.ContainsKey(definition.OriginalUnitInstance);

        return ValidityWithDiagnostics.Conditional(originalUnitInstanceCorrectlyResolved, () => Diagnostics.UnrecognizedOriginalUnitInstance(context, definition));
    }

    private IValidityWithDiagnostics ValidateNotCyclicallyModified(TContext context, TDefinition definition)
    {
        var cyclicallyModified = context.CyclicallyModifiedUnits.Contains(definition);

        return ValidityWithDiagnostics.Conditional(cyclicallyModified is false, () => Diagnostics.CyclicallyModified(context, definition));
    }
}
