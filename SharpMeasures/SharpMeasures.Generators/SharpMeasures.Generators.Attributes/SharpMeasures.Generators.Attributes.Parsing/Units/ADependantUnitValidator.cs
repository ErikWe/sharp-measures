namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;

using System.Collections.Generic;

public interface IDependantUnitDiagnostics<in TDefinition> : IUnitDiagnostics<TDefinition>
    where TDefinition : IDependantUnitDefinition
{
    public abstract Diagnostic? DependencyNullOrEmpty(IDependantUnitValidatorContext context, TDefinition definition);
    public abstract Diagnostic? UnrecognizedDependency(IDependantUnitValidatorContext context, TDefinition definition);
    public abstract Diagnostic? DependantOnSelf(IDependantUnitValidatorContext context, TDefinition definition);
}

public interface IDependantUnitValidatorContext : IUnitValidatorContext
{
    public abstract HashSet<string> AvailableUnitDependencies { get; }
}

public abstract class ADependantUnitValidator<TContext, TDefinition> : AUnitValidator<TContext, TDefinition>
    where TContext : IDependantUnitValidatorContext
    where TDefinition : IDependantUnitDefinition
{
    private IDependantUnitDiagnostics<TDefinition> Diagnostics { get; }

    protected ADependantUnitValidator(IDependantUnitDiagnostics<TDefinition> diagnostics) : base(diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override IValidityWithDiagnostics CheckValidity(TContext context, TDefinition definition)
    {
        return ValidityWithDiagnostics.DiagnoseAndMergeWhileValid(context, definition, base.CheckValidity, CheckDependantOnValidity);
    }

    private IValidityWithDiagnostics CheckDependantOnValidity(TContext context, TDefinition definition)
    {
        if (string.IsNullOrEmpty(definition.DependantOn))
        {
            return CreateInvalidity(Diagnostics.DependencyNullOrEmpty(context, definition));
        }

        if (context.AvailableUnitDependencies.Contains(definition.DependantOn) is false)
        {
            return CreateInvalidity(Diagnostics.UnrecognizedDependency(context, definition));
        }

        if (definition.DependantOn == definition.Name)
        {
            return CreateInvalidity(Diagnostics.DependantOnSelf(context, definition));
        }

        return ValidityWithDiagnostics.Valid;
    }
}
