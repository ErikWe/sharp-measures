namespace SharpMeasures.Generators.Attributes.Parsing;

using SharpMeasures.Generators.Diagnostics;

public abstract class AValidator<TContext, TDefinition> : IValidator<TContext, TDefinition>
    where TContext : IValidationContext
{
    public abstract IValidityWithDiagnostics CheckValidity(TContext context, TDefinition definition);
}

public abstract class AActionableValidator<TContext, TDefinition> : AValidator<TContext, TDefinition>, IActionableValidator<TContext, TDefinition>
    where TContext : IValidationContext
{
    public virtual void OnStartValidation(TContext context, TDefinition definition) { }
    public virtual void OnInvalidated(TContext context, TDefinition definition) { }
    public virtual void OnValidated(TContext context, TDefinition definition) { }
}