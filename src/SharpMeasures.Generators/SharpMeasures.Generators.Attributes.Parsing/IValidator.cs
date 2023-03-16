namespace SharpMeasures.Generators.Attributes.Parsing;

using SharpMeasures.Generators.Diagnostics;

public sealed record class SimpleValidationContext : IValidationContext
{
    public DefinedType Type { get; }

    public SimpleValidationContext(DefinedType type)
    {
        Type = type;
    }
}

public interface IValidationContext
{
    public abstract DefinedType Type { get; }
}

public interface IValidator<in TContext, in TDefinition> where TContext : IValidationContext
{
    public abstract IValidityWithDiagnostics Validate(TContext context, TDefinition definition);
}

public interface IActionableValidator<in TContext, in TDefinition> : IValidator<TContext, TDefinition> where TContext : IValidationContext
{
    public abstract void OnStartValidation(TContext context, TDefinition definition);

    public abstract void OnValidated(TContext context, TDefinition definition);
    public abstract void OnInvalidated(TContext context, TDefinition definition);
}
