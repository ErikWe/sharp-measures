namespace SharpMeasures.Generators.Units.Parsing.BiasedUnitInstance;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Units.Parsing.Abstractions;

internal interface IBiasedUnitInstanceValidationDiagnostics : IModifiedUnitInstanceValidationDiagnostics<BiasedUnitInstanceDefinition>
{
    public abstract Diagnostic? UnitNotIncludingBiasTerm(IBiasedUnitInstanceValidationContext context, BiasedUnitInstanceDefinition definition);
}

internal interface IBiasedUnitInstanceValidationContext : IModifiedUnitInstanceValidationContext
{
    public abstract bool UnitIncludesBiasTerm { get; }
}

internal sealed class BiasedUnitInstanceValidator : AModifiedUnitInstanceValidator<IBiasedUnitInstanceValidationContext, BiasedUnitInstanceDefinition>
{
    private IBiasedUnitInstanceValidationDiagnostics Diagnostics { get; }

    public BiasedUnitInstanceValidator(IBiasedUnitInstanceValidationDiagnostics diagnostics) : base(diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override IValidityWithDiagnostics Validate(IBiasedUnitInstanceValidationContext context, BiasedUnitInstanceDefinition definition)
    {
        return base.Validate(context, definition)
            .Validate(() => ValidateUnitIncludesBiasTerm(context, definition));
    }

    private IValidityWithDiagnostics ValidateUnitIncludesBiasTerm(IBiasedUnitInstanceValidationContext context, BiasedUnitInstanceDefinition definition)
    {
        return ValidityWithDiagnostics.Conditional(context.UnitIncludesBiasTerm, () => Diagnostics.UnitNotIncludingBiasTerm(context, definition));
    }
}
