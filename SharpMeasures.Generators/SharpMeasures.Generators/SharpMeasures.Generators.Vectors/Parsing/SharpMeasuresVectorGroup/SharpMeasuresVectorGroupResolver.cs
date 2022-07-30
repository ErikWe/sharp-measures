namespace SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVectorGroup;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Unresolved.Scalars;
using SharpMeasures.Generators.Unresolved.Units;
using SharpMeasures.Generators.Unresolved.Units.UnitInstances;
using SharpMeasures.Generators.Unresolved.Vectors;
using SharpMeasures.Generators.Vectors.Parsing.Abstraction;

using System.Linq;

internal interface ISharpMeasuresVectorGroupResolutionDiagnostics
{
    public abstract Diagnostic? TypeAlreadyUnit(ISharpMeasuresVectorGroupResolutionContext context, UnresolvedSharpMeasuresVectorGroupDefinition definition);
    public abstract Diagnostic? TypeAlreadyScalar(ISharpMeasuresVectorGroupResolutionContext context, UnresolvedSharpMeasuresVectorGroupDefinition definition);
    public abstract Diagnostic? TypeAlreadyVector(ISharpMeasuresVectorGroupResolutionContext context, UnresolvedSharpMeasuresVectorGroupDefinition definition);
    public abstract Diagnostic? TypeNotUnit(ISharpMeasuresVectorGroupResolutionContext context, UnresolvedSharpMeasuresVectorGroupDefinition definition);
    public abstract Diagnostic? TypeNotScalar(ISharpMeasuresVectorGroupResolutionContext context, UnresolvedSharpMeasuresVectorGroupDefinition definition);
    public abstract Diagnostic? DifferenceNotVectorGroup(ISharpMeasuresVectorGroupResolutionContext context, UnresolvedSharpMeasuresVectorGroupDefinition definition);
    public abstract Diagnostic? UnrecognizedDefaultUnit(ISharpMeasuresVectorGroupResolutionContext context, UnresolvedSharpMeasuresVectorGroupDefinition definition);
}

internal interface ISharpMeasuresVectorGroupResolutionContext : IProcessingContext
{
    public abstract IUnresolvedUnitPopulation UnitPopulation { get; }
    public abstract IUnresolvedScalarPopulation ScalarPopulation { get; }
    public abstract IUnresolvedVectorPopulationWithData VectorPopulation { get; }
}

internal class SharpMeasuresVectorGroupResolver : IProcesser<ISharpMeasuresVectorGroupResolutionContext, UnresolvedSharpMeasuresVectorGroupDefinition,
    SharpMeasuresVectorGroupDefinition>
{
    private ISharpMeasuresVectorGroupResolutionDiagnostics Diagnostics { get; }

    public SharpMeasuresVectorGroupResolver(ISharpMeasuresVectorGroupResolutionDiagnostics diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public IOptionalWithDiagnostics<SharpMeasuresVectorGroupDefinition> Process(ISharpMeasuresVectorGroupResolutionContext context,
        UnresolvedSharpMeasuresVectorGroupDefinition definition)
    {
        if (context.UnitPopulation.Units.ContainsKey(context.Type.AsNamedType()))
        {
            return OptionalWithDiagnostics.Empty<SharpMeasuresVectorGroupDefinition>(Diagnostics.TypeAlreadyUnit(context, definition));
        }

        if (context.ScalarPopulation.Scalars.ContainsKey(context.Type.AsNamedType()))
        {
            return OptionalWithDiagnostics.Empty<SharpMeasuresVectorGroupDefinition>(Diagnostics.TypeAlreadyScalar(context, definition));
        }

        if (context.VectorPopulation.DuplicatelyDefined.ContainsKey(context.Type.AsNamedType()))
        {
            return OptionalWithDiagnostics.Empty<SharpMeasuresVectorGroupDefinition>(Diagnostics.TypeAlreadyVector(context, definition));
        }

        var processedUnit = ProcessUnit(context, definition);
        var allDiagnostics = processedUnit.Diagnostics;

        if (processedUnit.LacksResult)
        {
            return OptionalWithDiagnostics.Empty<SharpMeasuresVectorGroupDefinition>(allDiagnostics);
        }

        var processedScalar = ProcessScalar(context, definition);
        var processedDifference = ProcessDifference(context, definition);
        var processedDefaultUnitName = ProcessDefaultUnitName(context, definition, processedUnit.Result);

        allDiagnostics = allDiagnostics.Concat(processedScalar.Diagnostics).Concat(processedDifference.Diagnostics).Concat(processedDefaultUnitName.Diagnostics);

        SharpMeasuresVectorGroupDefinition product = new(processedUnit.Result, processedScalar.NullableResult, definition.ImplementSum, definition.ImplementDifference,
            processedDifference.Result, processedDefaultUnitName.Result, definition.DefaultUnitSymbol, definition.GenerateDocumentation, definition.Locations);

        return OptionalWithDiagnostics.Result(product, allDiagnostics);
    }

    private IOptionalWithDiagnostics<IUnresolvedUnitType> ProcessUnit(ISharpMeasuresVectorGroupResolutionContext context,
        UnresolvedSharpMeasuresVectorGroupDefinition definition)
    {
        if (context.UnitPopulation.Units.TryGetValue(definition.Unit, out var unit) is false)
        {
            return OptionalWithDiagnostics.Empty<IUnresolvedUnitType>(Diagnostics.TypeNotUnit(context, definition));
        }

        return OptionalWithDiagnostics.Result(unit);
    }

    private IOptionalWithDiagnostics<IUnresolvedScalarType> ProcessScalar(ISharpMeasuresVectorGroupResolutionContext context,
        UnresolvedSharpMeasuresVectorGroupDefinition definition)
    {
        if (definition.Scalar is null)
        {
            return OptionalWithDiagnostics.EmptyWithoutDiagnostics<IUnresolvedScalarType>();
        }

        if (context.ScalarPopulation.Scalars.TryGetValue(definition.Scalar.Value, out var scalar) is false)
        {
            return OptionalWithDiagnostics.Empty<IUnresolvedScalarType>(Diagnostics.TypeNotScalar(context, definition));
        }

        return OptionalWithDiagnostics.Result(scalar);
    }

    private IResultWithDiagnostics<IUnresolvedVectorGroupType> ProcessDifference(ISharpMeasuresVectorGroupResolutionContext context,
        UnresolvedSharpMeasuresVectorGroupDefinition definition)
    {
        if (context.VectorPopulation.VectorGroups.TryGetValue(definition.Difference, out var vectorGroup) is false)
        {
            var diagnostics = Diagnostics.DifferenceNotVectorGroup(context, definition);
            var selfType = context.VectorPopulation.VectorGroups[context.Type.AsNamedType()];

            return ResultWithDiagnostics.Construct(selfType, diagnostics);
        }

        return ResultWithDiagnostics.Construct(vectorGroup);
    }

    private IResultWithDiagnostics<IUnresolvedUnitInstance?> ProcessDefaultUnitName(ISharpMeasuresVectorGroupResolutionContext context,
        UnresolvedSharpMeasuresVectorGroupDefinition definition, IUnresolvedUnitType unit)
    {
        if (definition.DefaultUnitName is null)
        {
            return ResultWithDiagnostics.Construct<IUnresolvedUnitInstance?>(null);
        }

        if (unit.UnitsByName.TryGetValue(definition.DefaultUnitName, out var unitInstance) is false)
        {
            return ResultWithDiagnostics.Construct<IUnresolvedUnitInstance?>(null, Diagnostics.UnrecognizedDefaultUnit(context, definition));
        }

        return ResultWithDiagnostics.Construct<IUnresolvedUnitInstance?>(unitInstance);
    }
}
