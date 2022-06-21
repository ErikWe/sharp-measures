namespace SharpMeasures.Generators.Units.Parsing.ScaledUnit;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Units.Parsing.Abstractions;

internal interface IScaledUnitProcessingDiagnostics : IDependantUnitProcessingDiagnostics<RawScaledUnitDefinition>
{
    public abstract Diagnostic? NullExpression(IDependantUnitProcessingContext context, RawScaledUnitDefinition definition);
    public abstract Diagnostic? EmptyExpression(IDependantUnitProcessingContext context, RawScaledUnitDefinition definition);
}

internal class ScaledUnitProcesser : ADependantUnitProcesser<IDependantUnitProcessingContext, RawScaledUnitDefinition, ScaledUnitDefinition>
{
    private IScaledUnitProcessingDiagnostics Diagnostics { get; }

    public ScaledUnitProcesser(IScaledUnitProcessingDiagnostics diagnostics) : base(diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override IOptionalWithDiagnostics<ScaledUnitDefinition> Process(IDependantUnitProcessingContext context, RawScaledUnitDefinition definition)
    {
        if (VerifyRequiredPropertiesSet(definition) is false)
        {
            return OptionalWithDiagnostics.Empty<ScaledUnitDefinition>();
        }

        var validity = IterativeValidity.DiagnoseAndMergeWhileValid(context, definition, CheckDependantUnitValidity, CheckExpressionValidity);
        
        if (validity.IsInvalid)
        {
            return OptionalWithDiagnostics.Empty<ScaledUnitDefinition>(validity.Diagnostics);
        }

        ScaledUnitDefinition product = new(definition.Name!, definition.ParsingData.InterpretedPlural!, definition.From!, definition.Scale,
            definition.Expression, definition.Locations);
        return OptionalWithDiagnostics.Result(product, validity.Diagnostics);
    }

    protected override bool VerifyRequiredPropertiesSet(RawScaledUnitDefinition definition)
    {
        return base.VerifyRequiredPropertiesSet(definition) && (definition.Locations.ExplicitlySetScale || definition.Locations.ExplicitlySetExpression);
    }

    private IValidityWithDiagnostics CheckExpressionValidity(IDependantUnitProcessingContext context, RawScaledUnitDefinition definition)
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
}
