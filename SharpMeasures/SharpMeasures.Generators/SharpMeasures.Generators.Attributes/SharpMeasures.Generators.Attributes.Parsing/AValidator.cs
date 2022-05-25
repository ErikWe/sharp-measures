namespace SharpMeasures.Generators.Attributes.Parsing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;

public abstract class AValidator<TContext, TDefinition> : IValidator<TContext, TDefinition>
    where TContext : IValidatorContext
{
    public abstract IValidityWithDiagnostics CheckValidity(TContext context, TDefinition definition);

    protected IValidityWithDiagnostics CreateInvalidity(Diagnostic? diagnostics)
    {
        if (diagnostics is not null)
        {
            return ValidityWithDiagnostics.Invalid(diagnostics);
        }

        return ValidityWithDiagnostics.InvalidWithoutDiagnostics;
    }

    protected IValidityWithDiagnostics CreateValidity(Diagnostic? diagnostics)
    {
        if (diagnostics is not null)
        {
            return ValidityWithDiagnostics.ValidWithDiagnostics(diagnostics);
        }

        return ValidityWithDiagnostics.Valid;
    }
}

public abstract class AActionableValidator<TContext, TDefinition> : AValidator<TContext, TDefinition>, IActionableValidator<TContext, TDefinition>
    where TContext : IValidatorContext
{
    public virtual void OnStartValidation(TContext context, TDefinition definition) { }
    public virtual void OnInvalidated(TContext context, TDefinition definition) { }
    public virtual void OnValidated(TContext context, TDefinition definition) { }
}