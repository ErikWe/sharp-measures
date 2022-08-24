namespace SharpMeasures.Generators.Units.Parsing.BiasedUnit;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Units.Parsing.Abstractions;

internal interface IBiasedUnitValidationDiagnostics : IDependantUnitValidationDiagnostics<BiasedUnitDefinition, BiasedUnitLocations>
{
    public abstract Diagnostic? UnitNotIncludingBiasTerm(IBiasedUnitValidationContext context, BiasedUnitDefinition definition);
}

internal interface IBiasedUnitValidationContext : IDependantUnitValidationContext
{
    public abstract bool UnitIncludesBiasTerm { get; }
}

internal class BiasedUnitValidator : ADependantUnitValidator<IBiasedUnitValidationContext, BiasedUnitDefinition, BiasedUnitLocations>
{
    private IBiasedUnitValidationDiagnostics Diagnostics { get; }

    public BiasedUnitValidator(IBiasedUnitValidationDiagnostics diagnostics) : base(diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override IValidityWithDiagnostics Validate(IBiasedUnitValidationContext context, BiasedUnitDefinition definition)
    {
        return ValidateUnitIncludesBiasTerm(context, definition)
            .Validate(() => base.Validate(context, definition));
    }

    private IValidityWithDiagnostics ValidateUnitIncludesBiasTerm(IBiasedUnitValidationContext context, BiasedUnitDefinition definition)
    {
        return ValidityWithDiagnostics.Conditional(context.UnitIncludesBiasTerm, () => Diagnostics.UnitNotIncludingBiasTerm(context, definition));
    }
}
