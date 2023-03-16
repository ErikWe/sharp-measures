namespace SharpMeasures.Generators.Units.Parsing.FixedUnitInstance;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Units.Parsing.Abstractions;

internal interface IFixedUnitInstanceProcessingDiagnostics : IUnitInstanceProcessingDiagnostics<RawFixedUnitInstanceDefinition, FixedUnitInstanceLocations>
{
    public abstract Diagnostic? UnitIsDerivable(IFixedUnitInstanceProcessingContext context, RawFixedUnitInstanceDefinition definition);
}

internal interface IFixedUnitInstanceProcessingContext : IUnitInstanceProcessingContext
{
    public abstract bool UnitIsDerivable { get; }
}

internal sealed class FixedUnitInstanceProcesser : AUnitInstanceProcesser<IFixedUnitInstanceProcessingContext, RawFixedUnitInstanceDefinition, FixedUnitInstanceLocations, FixedUnitInstanceDefinition>
{
    private IFixedUnitInstanceProcessingDiagnostics Diagnostics { get; }

    public FixedUnitInstanceProcesser(IFixedUnitInstanceProcessingDiagnostics diagnostics) : base(diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override IOptionalWithDiagnostics<FixedUnitInstanceDefinition> Process(IFixedUnitInstanceProcessingContext context, RawFixedUnitInstanceDefinition definition)
    {
        return VerifyRequiredPropertiesSet(definition)
            .Validate(() => ValidateUnitInstanceName(context, definition))
            .Validate(() => ValidateUnitNotDerivable(context, definition))
            .Merge(() => ProcessUnitInstancePluralForm(context, definition))
            .Transform((processedPluralForm) => ProduceResult(definition, processedPluralForm));
    }

    private static FixedUnitInstanceDefinition ProduceResult(RawFixedUnitInstanceDefinition definition, string processedPluralForm) => new(definition.Name!, processedPluralForm, definition.Locations);

    private IValidityWithDiagnostics ValidateUnitNotDerivable(IFixedUnitInstanceProcessingContext context, RawFixedUnitInstanceDefinition definition)
    {
        return ValidityWithDiagnostics.ValidWithConditionalDiagnostics(context.UnitIsDerivable, () => Diagnostics.UnitIsDerivable(context, definition));
    }
}
