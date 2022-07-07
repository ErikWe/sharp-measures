namespace SharpMeasures.Generators.Units.Parsing.Abstractions;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;

internal interface IDependantUnitProcessingDiagnostics<in TDefinition, in TLocations> : IUnitProcessingDiagnostics<TDefinition, TLocations>
    where TDefinition : IRawDependantUnitDefinition<TLocations>
    where TLocations : IDependantUnitLocations
{
    public abstract Diagnostic? NullDependency(IUnitProcessingContext context, TDefinition definition);
    public abstract Diagnostic? EmptyDependency(IUnitProcessingContext context, TDefinition definition);
    public abstract Diagnostic? DependantOnSelf(IUnitProcessingContext context, TDefinition definition);
}

internal abstract class ADependantUnitProcesser<TContext, TDefinition, TLocations, TProduct>
    : AUnitProcesser<TContext, TDefinition, TLocations, TProduct>
    where TContext : IUnitProcessingContext
    where TDefinition : IRawDependantUnitDefinition<TLocations>
    where TLocations : IDependantUnitLocations
    where TProduct : IUnresolvedDependantUnitDefinition<TLocations>
{
    private IDependantUnitProcessingDiagnostics<TDefinition, TLocations> Diagnostics { get; }

    protected ADependantUnitProcesser(IDependantUnitProcessingDiagnostics<TDefinition, TLocations> diagnostics) : base(diagnostics)
    {
        Diagnostics = diagnostics;
    }

    protected override bool VerifyRequiredPropertiesSet(TDefinition definition)
    {
        return base.VerifyRequiredPropertiesSet(definition) && definition.Locations.ExplicitlySetDependantOn;
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

        if (definition.DependantOn == definition.Name)
        {
            return ValidityWithDiagnostics.Invalid(Diagnostics.DependantOnSelf(context, definition));
        }

        return ValidityWithDiagnostics.Valid;
    }
}
