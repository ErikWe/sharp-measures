namespace SharpMeasures.Generators.Units.Parsing.BiasedUnit;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Units.Parsing.Abstractions;

using System.Globalization;

internal interface IBiasedUnitProcessingDiagnostics : IDependantUnitProcessingDiagnostics<UnprocessedBiasedUnitDefinition, BiasedUnitLocations>
{
    public abstract Diagnostic? NullExpression(IUnitProcessingContext context, UnprocessedBiasedUnitDefinition definition);
    public abstract Diagnostic? EmptyExpression(IUnitProcessingContext context, UnprocessedBiasedUnitDefinition definition);
}

internal class BiasedUnitProcesser : ADependantUnitProcesser<IUnitProcessingContext, UnprocessedBiasedUnitDefinition, BiasedUnitLocations, RawBiasedUnitDefinition>
{
    private IBiasedUnitProcessingDiagnostics Diagnostics { get; }

    public BiasedUnitProcesser(IBiasedUnitProcessingDiagnostics diagnostics) : base(diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override IOptionalWithDiagnostics<RawBiasedUnitDefinition> Process(IUnitProcessingContext context, UnprocessedBiasedUnitDefinition definition)
    {
        var requiredPropertiesSet = VerifyRequiredPropertiesSet(definition);
        var unitValidity = requiredPropertiesSet.Validate(context, definition, ValidateUnitName);
        var expressionValidity = unitValidity.Validate(context, definition, ValidateExpression);
        var processedPlural = unitValidity.Merge(context, definition, ProcessPlural);
        return processedPlural.Transform(definition, ProduceResult);
    }

    protected override IValidityWithDiagnostics VerifyRequiredPropertiesSet(UnprocessedBiasedUnitDefinition definition)
    {
        var requiredPropertiesSet = definition.Locations.ExplicitlySetBias || definition.Locations.ExplicitlySetExpression;

        return base.VerifyRequiredPropertiesSet(definition).Validate(ValidityWithDiagnostics.ConditionalWithoutDiagnostics(requiredPropertiesSet));
    }

    private IValidityWithDiagnostics ValidateExpression(IUnitProcessingContext context, UnprocessedBiasedUnitDefinition definition)
    {
        if (definition.Locations.ExplicitlySetExpression is false)
        {
            return ValidityWithDiagnostics.Valid;
        }

        return IterativeValidation.DiagnoseAndMergeWhileValid(context, definition, ValidateExpressionNotNull, ValidateExpressionNotEmpty);
    }

    private IValidityWithDiagnostics ValidateExpressionNotNull(IUnitProcessingContext context, UnprocessedBiasedUnitDefinition definition)
    {
        return ValidityWithDiagnostics.Conditional(definition.Expression is not null, () => Diagnostics.NullExpression(context, definition));
    }

    private IValidityWithDiagnostics ValidateExpressionNotEmpty(IUnitProcessingContext context, UnprocessedBiasedUnitDefinition definition)
    {
        return ValidityWithDiagnostics.Conditional(definition.Expression!.Length is not 0, () => Diagnostics.EmptyExpression(context, definition));
    }

    private static RawBiasedUnitDefinition ProduceResult(UnprocessedBiasedUnitDefinition definition, string interpretedPlural)
    {
        if (definition.Locations.ExplicitlySetBias)
        {
            return new(definition.Name!, interpretedPlural, definition.From!, definition.Bias.ToString(CultureInfo.InvariantCulture), definition.Locations);
        }

        return new(definition.Name!, interpretedPlural, definition.From!, definition.Expression!, definition.Locations);
    }
}
