namespace SharpMeasures.Generators.Units.Parsing.FixedUnit;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Units.Parsing.Abstractions;

internal interface IFixedUnitProcessingDiagnostics : IUnitProcessingDiagnostics<RawFixedUnitDefinition>
{
    public abstract Diagnostic? UnitNotSupportingBiasTerm(IFixedUnitProcessingContext context, RawFixedUnitDefinition definition);
}

internal interface IFixedUnitProcessingContext : IUnitProcessingContext
{
    public abstract bool UnitIncludesBiasTerm { get; }
}

internal class FixedUnitProcesser : AUnitProcesser<IFixedUnitProcessingContext, RawFixedUnitDefinition, FixedUnitDefinition>
{
    private IFixedUnitProcessingDiagnostics Diagnostics { get; }

    public FixedUnitProcesser(IFixedUnitProcessingDiagnostics diagnostics) : base(diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override IOptionalWithDiagnostics<FixedUnitDefinition> Process(IFixedUnitProcessingContext context, RawFixedUnitDefinition definition)
    {
        if (VerifyRequiredPropertiesSet(definition) is false)
        {
            return OptionalWithDiagnostics.Empty<FixedUnitDefinition>();
        }

        var validity = IterativeValidity.DiagnoseAndMergeWhileValid(context, definition, CheckUnitValidity, CheckBiasValidity);

        if (validity.IsInvalid)
        {
            return OptionalWithDiagnostics.Empty<FixedUnitDefinition>(validity.Diagnostics);
        }

        FixedUnitDefinition product = new(definition.Name!, definition.ParsingData.InterpretedPlural!, definition.Value, definition.Bias, definition.Locations);
        return OptionalWithDiagnostics.Result(product, validity.Diagnostics);
    }

    protected override bool VerifyRequiredPropertiesSet(RawFixedUnitDefinition definition)
    {
        return base.VerifyRequiredPropertiesSet(definition) && definition.Locations.ExplicitlySetValue;
    }

    private IValidityWithDiagnostics CheckBiasValidity(IFixedUnitProcessingContext context, RawFixedUnitDefinition definition)
    {
        if (definition.Locations.ExplicitlySetBias && definition.Bias is not 0)
        {
            return ValidityWithDiagnostics.ValidWithDiagnostics(Diagnostics.UnitNotSupportingBiasTerm(context, definition));
        }

        return ValidityWithDiagnostics.Valid;
    }
}
