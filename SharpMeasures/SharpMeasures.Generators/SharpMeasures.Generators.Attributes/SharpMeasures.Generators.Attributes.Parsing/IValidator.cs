namespace SharpMeasures.Generators.Attributes.Parsing;

using SharpMeasures.Generators.Diagnostics;

public interface IValidatorContext
{
    public abstract DefinedType Type { get; }
}

public interface IValidator<in TContext, in TDefinition>
    where TContext : IValidatorContext
{
    public abstract IValidityWithDiagnostics CheckValidity(TContext context, TDefinition definition);
}

public interface IActionableValidator<in TContext, in TDefinition> : IValidator<TContext, TDefinition>
    where TContext : IValidatorContext
{
    public abstract void OnStartValidation(TContext context, TDefinition definition);

    public abstract void OnValidated(TContext context, TDefinition definition);
    public abstract void OnInvalidated(TContext context, TDefinition definition);
}
