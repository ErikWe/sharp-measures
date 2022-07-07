namespace SharpMeasures.Generators.Units.Parsing.ScaledUnit;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Units.Parsing.Abstractions;

using System.Globalization;
using System.Linq;

internal interface IScaledUnitProcessingDiagnostics : IDependantUnitProcessingDiagnostics<RawScaledUnitDefinition, ScaledUnitLocations>
{
    public abstract Diagnostic? NullExpression(IUnitProcessingContext context, RawScaledUnitDefinition definition);
    public abstract Diagnostic? EmptyExpression(IUnitProcessingContext context, RawScaledUnitDefinition definition);
}

internal class ScaledUnitProcesser : ADependantUnitProcesser<IUnitProcessingContext, RawScaledUnitDefinition, ScaledUnitLocations, UnresolvedScaledUnitDefinition>
{
    private IScaledUnitProcessingDiagnostics Diagnostics { get; }

    public ScaledUnitProcesser(IScaledUnitProcessingDiagnostics diagnostics) : base(diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override IOptionalWithDiagnostics<UnresolvedScaledUnitDefinition> Process(IUnitProcessingContext context, RawScaledUnitDefinition definition)
    {
        if (VerifyRequiredPropertiesSet(definition) is false)
        {
            return OptionalWithDiagnostics.Empty<UnresolvedScaledUnitDefinition>();
        }

        var validity = IterativeValidity.DiagnoseAndMergeWhileValid(context, definition, CheckDependantUnitValidity, CheckExpressionValidity);
        var allDiagnostics = validity.Diagnostics;
        
        if (validity.IsInvalid)
        {
            return OptionalWithDiagnostics.Empty<UnresolvedScaledUnitDefinition>(allDiagnostics);
        }

        var processedPlural = ProcessPlural(context, definition);
        allDiagnostics = allDiagnostics.Concat(processedPlural.Diagnostics);

        if (processedPlural.LacksResult)
        {
            return OptionalWithDiagnostics.Empty<UnresolvedScaledUnitDefinition>(allDiagnostics);
        }

        var product = CreateProduct(definition, processedPlural.Result);
        return OptionalWithDiagnostics.Result(product, allDiagnostics);
    }

    protected override bool VerifyRequiredPropertiesSet(RawScaledUnitDefinition definition)
    {
        return base.VerifyRequiredPropertiesSet(definition) && (definition.Locations.ExplicitlySetScale || definition.Locations.ExplicitlySetExpression);
    }

    private IValidityWithDiagnostics CheckExpressionValidity(IUnitProcessingContext context, RawScaledUnitDefinition definition)
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

    private static UnresolvedScaledUnitDefinition CreateProduct(RawScaledUnitDefinition definition, string interpretedPlural)
    {
        if (definition.Locations.ExplicitlySetScale)
        {
            return new(definition.Name!, interpretedPlural, definition.From!, definition.Scale.ToString(CultureInfo.InvariantCulture), definition.Locations);
        }

        return new(definition.Name!, interpretedPlural, definition.From!, definition.Expression!, definition.Locations);
    }
}
