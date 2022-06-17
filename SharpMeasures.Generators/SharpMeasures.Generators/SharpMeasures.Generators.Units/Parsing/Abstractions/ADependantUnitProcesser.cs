namespace SharpMeasures.Generators.Units.Parsing.Abstractions;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;

using System.Collections.Generic;

internal interface IDependantUnitProcessingDiagnostics<in TDefinition> : IUnitProcessingDiagnostics<TDefinition>
    where TDefinition : IRawDependantUnitDefinition
{
    public abstract Diagnostic? NullDependency(IDependantUnitProcessingContext context, TDefinition definition);
    public abstract Diagnostic? EmptyDependency(IDependantUnitProcessingContext context, TDefinition definition);
    public abstract Diagnostic? UnrecognizedDependency(IDependantUnitProcessingContext context, TDefinition definition);
    public abstract Diagnostic? DependantOnSelf(IDependantUnitProcessingContext context, TDefinition definition);
}

internal interface IDependantUnitProcessingContext : IUnitProcessingContext
{
    public abstract HashSet<string> AvailableUnitDependencies { get; }
}

internal abstract class ADependantUnitProcesser<TContext, TDefinition, TProduct> : AUnitProcesser<TContext, TDefinition, TProduct>
    where TContext : IDependantUnitProcessingContext
    where TDefinition : IRawDependantUnitDefinition
    where TProduct : IDependantUnitDefinition
{
    private IDependantUnitProcessingDiagnostics<TDefinition> Diagnostics { get; }

    protected ADependantUnitProcesser(IDependantUnitProcessingDiagnostics<TDefinition> diagnostics) : base(diagnostics)
    {
        Diagnostics = diagnostics;
    }

    protected IValidityWithDiagnostics CheckDependantUnitValidity(TContext context, TDefinition definition)
    {
        return IterativeValidity.DiagnoseAndMergeWhileValid(context, definition, CheckUnitValidity, CheckDependantOnValidity);
    }

    protected IValidityWithDiagnostics CheckDependantOnValidity(TContext context, TDefinition definition)
    {
        if (definition.DependantOn is null)
        {
            return ValidityWithDiagnostics.Invalid(Diagnostics.NullDependency(context, definition));
        }

        if (definition.DependantOn.Length is 0)
        {
            return ValidityWithDiagnostics.Invalid(Diagnostics.EmptyDependency(context, definition));
        }

        if (context.AvailableUnitDependencies.Contains(definition.DependantOn) is false)
        {
            return ValidityWithDiagnostics.Invalid(Diagnostics.UnrecognizedDependency(context, definition));
        }

        if (definition.DependantOn == definition.Name)
        {
            return ValidityWithDiagnostics.Invalid(Diagnostics.DependantOnSelf(context, definition));
        }

        return ValidityWithDiagnostics.Valid;
    }
}
