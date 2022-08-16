namespace SharpMeasures.Generators.Units.Parsing.Abstractions;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;

internal interface IDependantUnitProcessingDiagnostics<in TDefinition, in TLocations> : IUnitProcessingDiagnostics<TDefinition, TLocations>
    where TDefinition : IUnprocessedDependantUnitDefinition<TLocations>
    where TLocations : IDependantUnitLocations
{
    public abstract Diagnostic? NullDependency(IUnitProcessingContext context, TDefinition definition);
    public abstract Diagnostic? EmptyDependency(IUnitProcessingContext context, TDefinition definition);
    public abstract Diagnostic? DependantOnSelf(IUnitProcessingContext context, TDefinition definition);
}

internal abstract class ADependantUnitProcesser<TContext, TDefinition, TLocations, TProduct> : AUnitProcesser<TContext, TDefinition, TLocations, TProduct>
    where TContext : IUnitProcessingContext
    where TDefinition : IUnprocessedDependantUnitDefinition<TLocations>
    where TLocations : IDependantUnitLocations
    where TProduct : IRawDependantUnitDefinition<TLocations>
{
    private IDependantUnitProcessingDiagnostics<TDefinition, TLocations> Diagnostics { get; }

    protected ADependantUnitProcesser(IDependantUnitProcessingDiagnostics<TDefinition, TLocations> diagnostics) : base(diagnostics)
    {
        Diagnostics = diagnostics;
    }

    protected override IValidityWithDiagnostics VerifyRequiredPropertiesSet(TDefinition definition)
    {
        return base.VerifyRequiredPropertiesSet(definition).Validate(ValidityWithDiagnostics.ConditionalWithoutDiagnostics(definition.Locations.ExplicitlySetDependantOn));
    }

    protected IValidityWithDiagnostics CheckDependantOnValidity(TContext context, TDefinition definition)
    {
        return IterativeValidation.DiagnoseAndMergeWhileValid(context, definition, ValidateDependantOnNotNull, ValidateDependantOnNotEmpty, ValidateNotDependantOnSelf);
    }

    private IValidityWithDiagnostics ValidateDependantOnNotNull(TContext context, TDefinition definition)
    {
        return ValidityWithDiagnostics.Conditional(definition.DependantOn is not null, () => Diagnostics.NullDependency(context, definition));
    }

    private IValidityWithDiagnostics ValidateDependantOnNotEmpty(TContext context, TDefinition definition)
    {
        return ValidityWithDiagnostics.Conditional(definition.DependantOn!.Length is not 0, () => Diagnostics.EmptyDependency(context, definition));
    }

    private IValidityWithDiagnostics ValidateNotDependantOnSelf(TContext context, TDefinition definition)
    {
        return ValidityWithDiagnostics.Conditional(definition.DependantOn == definition.Name, () => Diagnostics.DependantOnSelf(context, definition));
    }
}
