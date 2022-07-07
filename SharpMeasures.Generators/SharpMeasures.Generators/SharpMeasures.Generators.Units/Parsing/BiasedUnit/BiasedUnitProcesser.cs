namespace SharpMeasures.Generators.Units.Parsing.BiasedUnit;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Units.Parsing.Abstractions;

using System.Globalization;
using System.Linq;

internal interface IBiasedUnitProcessingDiagnostics : IDependantUnitProcessingDiagnostics<RawBiasedUnitDefinition, BiasedUnitLocations>
{
    public abstract Diagnostic? NullExpression(IUnitProcessingContext context, RawBiasedUnitDefinition definition);
    public abstract Diagnostic? EmptyExpression(IUnitProcessingContext context, RawBiasedUnitDefinition definition);
}

internal class BiasedUnitProcesser : ADependantUnitProcesser<IUnitProcessingContext, RawBiasedUnitDefinition, BiasedUnitLocations, UnresolvedBiasedUnitDefinition>
{
    private IBiasedUnitProcessingDiagnostics Diagnostics { get; }

    public BiasedUnitProcesser(IBiasedUnitProcessingDiagnostics diagnostics) : base(diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override IOptionalWithDiagnostics<UnresolvedBiasedUnitDefinition> Process(IUnitProcessingContext context, RawBiasedUnitDefinition definition)
    {
        if (VerifyRequiredPropertiesSet(definition) is false)
        {
            return OptionalWithDiagnostics.Empty<UnresolvedBiasedUnitDefinition>();
        }

        var validity = IterativeValidity.DiagnoseAndMergeWhileValid(context, definition, CheckDependantUnitValidity, CheckExpressionValidity);
        var allDiagnostics = validity.Diagnostics;

        if (validity.IsInvalid)
        {
            return OptionalWithDiagnostics.Empty<UnresolvedBiasedUnitDefinition>(allDiagnostics);
        }

        var processedPlural = ProcessPlural(context, definition);
        allDiagnostics = allDiagnostics.Concat(processedPlural.Diagnostics);

        if (processedPlural.LacksResult)
        {
            return OptionalWithDiagnostics.Empty<UnresolvedBiasedUnitDefinition>(allDiagnostics);
        }

        var product = CreateProduct(definition, processedPlural.Result);
        return OptionalWithDiagnostics.Result(product, allDiagnostics);
    }

    protected override bool VerifyRequiredPropertiesSet(RawBiasedUnitDefinition definition)
    {
        return base.VerifyRequiredPropertiesSet(definition) && (definition.Locations.ExplicitlySetBias || definition.Locations.ExplicitlySetExpression);
    }

    private IValidityWithDiagnostics CheckExpressionValidity(IUnitProcessingContext context, RawBiasedUnitDefinition definition)
    {
        if (definition.Locations.ExplicitlySetExpression is false)
        {
            return ValidityWithDiagnostics.Valid;
        }

        if (definition.Expression is null)
        {
            return ValidityWithDiagnostics.Invalid(Diagnostics.NullExpression(context, definition));
        }

        if (definition.Expression.Length is 0)
        {
            return ValidityWithDiagnostics.Invalid(Diagnostics.EmptyExpression(context, definition));
        }

        return ValidityWithDiagnostics.Valid;
    }

    private static UnresolvedBiasedUnitDefinition CreateProduct(RawBiasedUnitDefinition definition, string interpretedPlural)
    {
        if (definition.Locations.ExplicitlySetBias)
        {
            return new(definition.Name!, interpretedPlural, definition.From!, definition.Bias.ToString(CultureInfo.InvariantCulture), definition.Locations);
        }

        return new(definition.Name!, interpretedPlural, definition.From!, definition.Expression!, definition.Locations);
    }
}
