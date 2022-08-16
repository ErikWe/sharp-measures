namespace SharpMeasures.Generators.Units.Parsing.SharpMeasuresUnit;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Units.Parsing.Abstractions;
using SharpMeasures.Generators.Raw.Scalars;

internal interface ISharpMeasuresUnitResolutionDiagnostics
{
    public abstract Diagnostic? TypeAlreadyUnit(ISharpMeasuresUnitResolutionContext context, RawSharpMeasuresUnitDefinition definition);
    public abstract Diagnostic? QuantityNotScalar(ISharpMeasuresUnitResolutionContext context, RawSharpMeasuresUnitDefinition definition);
    public abstract Diagnostic? QuantityBiased(ISharpMeasuresUnitResolutionContext context, RawSharpMeasuresUnitDefinition definition);
}

internal interface ISharpMeasuresUnitResolutionContext : IProcessingContext
{
    public abstract IRawUnitPopulationWithData UnitPopulation { get; }
    public abstract IRawScalarPopulation ScalarPopulation { get; }
}

internal class SharpMeasuresUnitResolver : AProcesser<ISharpMeasuresUnitResolutionContext, RawSharpMeasuresUnitDefinition, SharpMeasuresUnitDefinition>
{
    private ISharpMeasuresUnitResolutionDiagnostics Diagnostics { get; }

    public SharpMeasuresUnitResolver(ISharpMeasuresUnitResolutionDiagnostics diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override IOptionalWithDiagnostics<SharpMeasuresUnitDefinition> Process(ISharpMeasuresUnitResolutionContext context, RawSharpMeasuresUnitDefinition definition)
    {
        var typeNotAlreadyUnit = ValidateTypeNotAlreadyUnit(context, definition);
        var scalarBase = typeNotAlreadyUnit.Merge(context, definition, ResolveQuantity).Validate(context, definition, ValidateQuantityNotBiased);
        return scalarBase.Transform(definition, ProduceResult);
    }

    private IValidityWithDiagnostics ValidateTypeNotAlreadyUnit(ISharpMeasuresUnitResolutionContext context, RawSharpMeasuresUnitDefinition definition)
    {
        var typeAlreadyUnit = context.UnitPopulation.DuplicatelyDefinedUnits.ContainsKey(context.Type.AsNamedType());

        return ValidityWithDiagnostics.Conditional(typeAlreadyUnit is false, () => Diagnostics.TypeAlreadyUnit(context, definition));
    }

    private IOptionalWithDiagnostics<IRawScalarBaseType> ResolveQuantity(ISharpMeasuresUnitResolutionContext context, RawSharpMeasuresUnitDefinition definition)
    {
        var scalarBaseCorrectlyResolved = context.ScalarPopulation.ScalarBases.TryGetValue(definition.Quantity, out var scalarBase);

        return OptionalWithDiagnostics.Conditional(scalarBaseCorrectlyResolved, scalarBase, () => Diagnostics.QuantityNotScalar(context, definition));
    }

    private IValidityWithDiagnostics ValidateQuantityNotBiased(ISharpMeasuresUnitResolutionContext context, RawSharpMeasuresUnitDefinition definition, IRawScalarBaseType scalarBase)
    {
        return ValidityWithDiagnostics.Conditional(scalarBase.Definition.UseUnitBias, () => Diagnostics.QuantityBiased(context, definition));
    }

    private static SharpMeasuresUnitDefinition ProduceResult(RawSharpMeasuresUnitDefinition definition, IRawScalarBaseType scalarBase)
    {
        return new(scalarBase, definition.BiasTerm, definition.GenerateDocumentation, definition.Locations);
    }
}
