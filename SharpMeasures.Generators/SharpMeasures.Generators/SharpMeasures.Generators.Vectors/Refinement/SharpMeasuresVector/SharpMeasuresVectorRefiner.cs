namespace SharpMeasures.Generators.Vectors.Refinement.SharpMeasuresVector;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Vectors;
using SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVector;
using SharpMeasures.Generators.Vectors.Populations;
using SharpMeasures.Generators.Vectors.Populations;

using System.Linq;

internal interface ISharpMeasuresVectorRefinementDiagnostics
{
    public abstract Diagnostic? TypeAlreadyUnit(ISharpMeasuresVectorRefinementContext context, SharpMeasuresVectorDefinition definition);
    public abstract Diagnostic? TypeAlreadyScalar(ISharpMeasuresVectorRefinementContext context, SharpMeasuresVectorDefinition definition);
    public abstract Diagnostic? TypeAlreadyVector(ISharpMeasuresVectorRefinementContext context, SharpMeasuresVectorDefinition definition);
    public abstract Diagnostic? TypeNotUnit(ISharpMeasuresVectorRefinementContext context, SharpMeasuresVectorDefinition definition);
    public abstract Diagnostic? TypeNotScalar(ISharpMeasuresVectorRefinementContext context, SharpMeasuresVectorDefinition definition);
    public abstract Diagnostic? UnrecognizedDefaultUnit(ISharpMeasuresVectorRefinementContext context, SharpMeasuresVectorDefinition definition);
    public abstract Diagnostic? DifferenceNotVector(ISharpMeasuresVectorRefinementContext context, SharpMeasuresVectorDefinition definition);
}

internal interface ISharpMeasuresVectorRefinementContext : IProcessingContext
{
    public abstract UnitPopulation UnitPopulation { get; }
    public abstract IScalarPopulation ScalarPopulation { get; }
    public abstract VectorPopulation VectorPopulation { get; }

    public abstract VectorPopulationErrors VectorPopulationData { get; }
}

internal class SharpMeasuresVectorRefiner : IProcesser<ISharpMeasuresVectorRefinementContext, SharpMeasuresVectorDefinition, RefinedSharpMeasuresVectorDefinition>
{
    private ISharpMeasuresVectorRefinementDiagnostics Diagnostics { get; }

    public SharpMeasuresVectorRefiner(ISharpMeasuresVectorRefinementDiagnostics diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public IOptionalWithDiagnostics<RefinedSharpMeasuresVectorDefinition> Process(ISharpMeasuresVectorRefinementContext context, SharpMeasuresVectorDefinition definition)
    {
        if (context.UnitPopulation.ContainsKey(context.Type.AsNamedType()))
        {
            return OptionalWithDiagnostics.Empty<RefinedSharpMeasuresVectorDefinition>(Diagnostics.TypeAlreadyUnit(context, definition));
        }

        if (context.ScalarPopulation.ContainsKey(context.Type.AsNamedType()))
        {
            return OptionalWithDiagnostics.Empty<RefinedSharpMeasuresVectorDefinition>(Diagnostics.TypeAlreadyScalar(context, definition));
        }

        if (context.VectorPopulationData.NonUniquelyDefinedTypes.ContainsKey(context.Type.AsNamedType()))
        {
            return OptionalWithDiagnostics.Empty<RefinedSharpMeasuresVectorDefinition>(Diagnostics.TypeAlreadyVector(context, definition));
        }

        if (context.VectorPopulation.ResizedVectorGroups.TryGetValue(context.Type.AsNamedType(), out var vectorGroup) is false)
        {
            return OptionalWithDiagnostics.Empty<RefinedSharpMeasuresVectorDefinition>();
        }

        var processedUnit = ProcessUnit(context, definition);
        var allDiagnostics = processedUnit.Diagnostics;

        if (processedUnit.LacksResult)
        {
            return OptionalWithDiagnostics.Empty<RefinedSharpMeasuresVectorDefinition>(allDiagnostics);
        }

        var processedScalar = ProcessScalar(context, definition);
        allDiagnostics = allDiagnostics.Concat(processedScalar.Diagnostics);

        var processedDifference = ProcessDifference(context, definition);
        allDiagnostics = allDiagnostics.Concat(processedDifference.Diagnostics);

        var processedDefaultUnitName = ProcessDefaultUnitName(context, definition, processedUnit.Result);
        allDiagnostics = allDiagnostics.Concat(processedDefaultUnitName.Diagnostics);

        RefinedSharpMeasuresVectorDefinition product = new(processedUnit.Result, processedScalar.Result, vectorGroup, definition.Dimension,
            definition.ImplementSum, definition.ImplementDifference, definition.Difference, processedDefaultUnitName.Result, definition.DefaultUnitSymbol,
            definition.GenerateDocumentation);

        return OptionalWithDiagnostics.Result(product, allDiagnostics);
    }

    private IOptionalWithDiagnostics<IUnitType> ProcessUnit(ISharpMeasuresVectorRefinementContext context, SharpMeasuresVectorDefinition definition)
    {
        if (context.UnitPopulation.TryGetValue(definition.Unit, out var unit) is false)
        {
            return OptionalWithDiagnostics.Empty<IUnitType>(Diagnostics.TypeNotUnit(context, definition));
        }

        return OptionalWithDiagnostics.Result(unit);
    }

    private IResultWithDiagnostics<IScalarType?> ProcessScalar(ISharpMeasuresVectorRefinementContext context, SharpMeasuresVectorDefinition definition)
    {
        if (definition.Scalar is null)
        {
            return ResultWithDiagnostics.Construct<IScalarType?>(null);
        }

        if (context.ScalarPopulation.TryGetValue(definition.Scalar.Value, out var scalar))
        {
            return ResultWithDiagnostics.Construct<IScalarType?>(scalar);
        }

        return ResultWithDiagnostics.Construct<IScalarType?>(null, Diagnostics.TypeNotScalar(context, definition));
    }

    private IResultWithDiagnostics<IVector> ProcessDifference(ISharpMeasuresVectorRefinementContext context, SharpMeasuresVectorDefinition definition)
    {
        if (context.VectorPopulation.AllVectors.TryGetValue(definition.Difference, out var vector) is false)
        {
            return ResultWithDiagnostics.Construct(context.VectorPopulation.AllVectors[context.Type.AsNamedType()], Diagnostics.DifferenceNotVector(context, definition));
        }

        return ResultWithDiagnostics.Construct(vector);
    }

    private IResultWithDiagnostics<string?> ProcessDefaultUnitName(ISharpMeasuresVectorRefinementContext context, SharpMeasuresVectorDefinition definition, IUnitType unit)
    {
        if (definition.DefaultUnitName is null)
        {
            return ResultWithDiagnostics.Construct<string?>(null);
        }

        if (unit.UnitsByName.ContainsKey(definition.DefaultUnitName) is false)
        {
            return ResultWithDiagnostics.Construct<string?>(null, Diagnostics.UnrecognizedDefaultUnit(context, definition));
        }

        return ResultWithDiagnostics.Construct<string?>(definition.DefaultUnitName);
    }
}
