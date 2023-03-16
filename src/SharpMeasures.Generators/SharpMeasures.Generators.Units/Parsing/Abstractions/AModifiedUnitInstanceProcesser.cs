namespace SharpMeasures.Generators.Units.Parsing.Abstractions;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;

internal interface IModifiedUnitInstanceProcessingDiagnostics<in TDefinition, in TLocations> : IUnitInstanceProcessingDiagnostics<TDefinition, TLocations>
    where TDefinition : IRawModifiedUnitInstance<TLocations>
    where TLocations : IModifiedUnitInstanceLocations
{
    public abstract Diagnostic? NullOriginalUnitInstance(IUnitInstanceProcessingContext context, TDefinition definition);
    public abstract Diagnostic? EmptyOriginalUnitInstance(IUnitInstanceProcessingContext context, TDefinition definition);
    public abstract Diagnostic? OriginalUnitInstanceIsSelf(IUnitInstanceProcessingContext context, TDefinition definition);
}

internal abstract class AModifiedUnitInstanceProcesser<TContext, TDefinition, TLocations, TProduct> : AUnitInstanceProcesser<TContext, TDefinition, TLocations, TProduct>
    where TContext : IUnitInstanceProcessingContext
    where TDefinition : IRawModifiedUnitInstance<TLocations>
    where TLocations : IModifiedUnitInstanceLocations
    where TProduct : IModifiedUnitInstance
{
    private IModifiedUnitInstanceProcessingDiagnostics<TDefinition, TLocations> Diagnostics { get; }

    protected AModifiedUnitInstanceProcesser(IModifiedUnitInstanceProcessingDiagnostics<TDefinition, TLocations> diagnostics) : base(diagnostics)
    {
        Diagnostics = diagnostics;
    }

    protected override IValidityWithDiagnostics VerifyRequiredPropertiesSet(TDefinition definition)
    {
        return base.VerifyRequiredPropertiesSet(definition).Validate(ValidityWithDiagnostics.ConditionalWithoutDiagnostics(definition.Locations.ExplicitlySetOriginalUnitInstance));
    }

    protected IValidityWithDiagnostics ValidateOriginalUnitInstance(TContext context, TDefinition definition)
    {
        return ValidateOriginalUnitInstanceNotNull(context, definition)
            .Validate(() => ValidateOriginalUnitInstanceNotEmpty(context, definition))
            .Validate(() => ValidateOriginalUnitInstanceNotSelf(context, definition));
    }

    private IValidityWithDiagnostics ValidateOriginalUnitInstanceNotNull(TContext context, TDefinition definition)
    {
        return ValidityWithDiagnostics.Conditional(definition.OriginalUnitInstance is not null, () => Diagnostics.NullOriginalUnitInstance(context, definition));
    }

    private IValidityWithDiagnostics ValidateOriginalUnitInstanceNotEmpty(TContext context, TDefinition definition)
    {
        return ValidityWithDiagnostics.Conditional(definition.OriginalUnitInstance!.Length > 0, () => Diagnostics.EmptyOriginalUnitInstance(context, definition));
    }

    private IValidityWithDiagnostics ValidateOriginalUnitInstanceNotSelf(TContext context, TDefinition definition)
    {
        return ValidityWithDiagnostics.Conditional(definition.OriginalUnitInstance != definition.Name, () => Diagnostics.OriginalUnitInstanceIsSelf(context, definition));
    }
}
