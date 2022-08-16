namespace SharpMeasures.Generators.Units.Parsing.BiasedUnit;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Raw.Units.UnitInstances;
using SharpMeasures.Generators.Units.Parsing.Abstractions;

internal interface IBiasedUnitResolutionDiagnostics : IDependantUnitResolutionDiagnostics<RawBiasedUnitDefinition, BiasedUnitLocations>
{
    public abstract Diagnostic? UnitNotIncludingBiasTerm(IBiasedUnitResolutionContext context, RawBiasedUnitDefinition definition);
}

internal interface IBiasedUnitResolutionContext : IDependantUnitResolutionContext
{
    public abstract bool UnitIncludesBiasTerm { get; }
}

internal class BiasedUnitResolver : ADependantUnitResolver<IBiasedUnitResolutionContext, RawBiasedUnitDefinition, BiasedUnitLocations, BiasedUnitDefinition>
{
    private IBiasedUnitResolutionDiagnostics Diagnostics { get; }

    public BiasedUnitResolver(IBiasedUnitResolutionDiagnostics diagnostics) : base(diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override IOptionalWithDiagnostics<BiasedUnitDefinition> Process(IBiasedUnitResolutionContext context, RawBiasedUnitDefinition definition)
    {
        var unitBiased = ValidateUnitIncludesBiasTerm(context, definition);
        var dependantOn = unitBiased.Merge(context, definition, ProcessDependantOn);
        return dependantOn.Transform(definition, ProduceResult);
    }

    private IValidityWithDiagnostics ValidateUnitIncludesBiasTerm(IBiasedUnitResolutionContext context, RawBiasedUnitDefinition definition)
    {
        return ValidityWithDiagnostics.Conditional(context.UnitIncludesBiasTerm, () => Diagnostics.UnitNotIncludingBiasTerm(context, definition));
    }

    private static BiasedUnitDefinition ProduceResult(RawBiasedUnitDefinition definition, IRawUnitInstance dependantOn)
    {
        return new(definition.Name, definition.Plural, dependantOn, definition.Expression, definition.Locations);
    }
}
