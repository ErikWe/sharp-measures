namespace SharpMeasures.Generators.Units.Parsing.FixedUnit;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Units.Parsing.Abstractions;

internal interface IFixedUnitProcessingDiagnostics : IUnitProcessingDiagnostics<RawFixedUnitDefinition, FixedUnitLocations>
{
    public abstract Diagnostic? UnitIsDerivable(IFixedUnitProcessingContext context, RawFixedUnitDefinition definition);
}

internal interface IFixedUnitProcessingContext : IUnitProcessingContext
{
    public abstract bool UnitIsDerivable { get; }
}

internal class FixedUnitProcesser : AUnitProcesser<IFixedUnitProcessingContext, RawFixedUnitDefinition, FixedUnitLocations, FixedUnitDefinition>
{
    private IFixedUnitProcessingDiagnostics Diagnostics { get; }

    public FixedUnitProcesser(IFixedUnitProcessingDiagnostics diagnostics) : base(diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override IOptionalWithDiagnostics<FixedUnitDefinition> Process(IFixedUnitProcessingContext context, RawFixedUnitDefinition definition)
    {
        return VerifyRequiredPropertiesSet(definition)
            .Validate(() => ValidateUnitName(context, definition))
            .Validate(() => ValidateUnitNotDerivable(context, definition))
            .Merge(() => ProcessPlural(context, definition))
            .Transform((processedPlural) => ProduceResult(definition, processedPlural));
    }

    private static FixedUnitDefinition ProduceResult(RawFixedUnitDefinition definition, string processedPlural)
    {
        return new(definition.Name!, processedPlural, definition.Locations);
    }

    private IValidityWithDiagnostics ValidateUnitNotDerivable(IFixedUnitProcessingContext context, RawFixedUnitDefinition definition)
    {
        return ValidityWithDiagnostics.ValidWithConditionalDiagnostics(context.UnitIsDerivable, () => Diagnostics.UnitIsDerivable(context, definition));
    }
}
