namespace SharpMeasures.Generators.Scalars.Refinement.SharpMeasuresScalar;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators;
using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Scalars.Parsing.SharpMeasuresScalar;
using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Vectors;

using System;
using System.Linq;

internal interface ISharpMeasuresScalarRefinementDiagnostics
{
    public abstract Diagnostic? TypeAlreadyUnit(ISharpMeasuresScalarRefinementContext context, SharpMeasuresScalarDefinition definition);
    public abstract Diagnostic? TypeNotUnit(ISharpMeasuresScalarRefinementContext context, SharpMeasuresScalarDefinition definition);
    public abstract Diagnostic? UnitNotIncludingBiasTerm(ISharpMeasuresScalarRefinementContext context, SharpMeasuresScalarDefinition definition);
    public abstract Diagnostic? TypeNotVector(ISharpMeasuresScalarRefinementContext context, SharpMeasuresScalarDefinition definition);
    public abstract Diagnostic? UnrecognizedDefaultUnit(ISharpMeasuresScalarRefinementContext context, SharpMeasuresScalarDefinition definition);
    public abstract Diagnostic? DifferenceNotScalar(ISharpMeasuresScalarRefinementContext context, SharpMeasuresScalarDefinition definition);
    public abstract Diagnostic? ReciprocalNotScalar(ISharpMeasuresScalarRefinementContext context, SharpMeasuresScalarDefinition definition);
    public abstract Diagnostic? SquareNotScalar(ISharpMeasuresScalarRefinementContext context, SharpMeasuresScalarDefinition definition);
    public abstract Diagnostic? CubeNotScalar(ISharpMeasuresScalarRefinementContext context, SharpMeasuresScalarDefinition definition);
    public abstract Diagnostic? SquareRootNotScalar(ISharpMeasuresScalarRefinementContext context, SharpMeasuresScalarDefinition definition);
    public abstract Diagnostic? CubeRootNotScalar(ISharpMeasuresScalarRefinementContext context, SharpMeasuresScalarDefinition definition);
}

internal interface ISharpMeasuresScalarRefinementContext : IProcessingContext
{
    public abstract UnitPopulation UnitPopulation { get; }
    public abstract ScalarPopulation ScalarPopulation { get; }
    public abstract VectorPopulation VectorPopulation { get; }
}

internal class SharpMeasuresScalarRefiner : IProcesser<ISharpMeasuresScalarRefinementContext, SharpMeasuresScalarDefinition, RefinedSharpMeasuresScalarDefinition>
{
    private ISharpMeasuresScalarRefinementDiagnostics Diagnostics { get; }

    public SharpMeasuresScalarRefiner(ISharpMeasuresScalarRefinementDiagnostics diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public IOptionalWithDiagnostics<RefinedSharpMeasuresScalarDefinition> Process(ISharpMeasuresScalarRefinementContext context, SharpMeasuresScalarDefinition definition)
    {
        if (context.UnitPopulation.ContainsKey(context.Type.AsNamedType()))
        {
            var diagnostics = Diagnostics.TypeAlreadyUnit(context, definition);
            return OptionalWithDiagnostics.Empty<RefinedSharpMeasuresScalarDefinition>(diagnostics);
        }

        var processedUnit = ProcessUnit(context, definition);
        var allDiagnostics = processedUnit.Diagnostics;

        if (processedUnit.LacksResult)
        {
            return OptionalWithDiagnostics.Empty<RefinedSharpMeasuresScalarDefinition>(allDiagnostics);
        }

        var processedVector = ProcessVector(context, definition);
        allDiagnostics = allDiagnostics.Concat(processedVector.Diagnostics);

        var processedDifference = ProcessDifference(context, definition);

        var processedDefaultUnitName = ProcessDefaultUnitName(context, definition, processedUnit.Result);

        var processedReciprocal = ProcessPowerQuantity(context, definition, definition.Reciprocal, Diagnostics.ReciprocalNotScalar);
        var processedSquare = ProcessPowerQuantity(context, definition, definition.Square, Diagnostics.SquareNotScalar);
        var processedCube = ProcessPowerQuantity(context, definition, definition.Cube, Diagnostics.CubeNotScalar);
        var processedSquareRoot = ProcessPowerQuantity(context, definition, definition.SquareRoot, Diagnostics.SquareRootNotScalar);
        var processedCubeRoot = ProcessPowerQuantity(context, definition, definition.CubeRoot, Diagnostics.CubeRootNotScalar);

        allDiagnostics = allDiagnostics.Concat(processedDifference.Diagnostics).Concat(processedDefaultUnitName.Diagnostics).Concat(processedReciprocal.Diagnostics)
            .Concat(processedSquare.Diagnostics) .Concat(processedCube.Diagnostics).Concat(processedSquareRoot.Diagnostics).Concat(processedCubeRoot.Diagnostics);

        RefinedSharpMeasuresScalarDefinition product = new(processedUnit.Result, processedVector.NullableResult, definition.UseUnitBias, definition.ImplementSum,
            definition.ImplementDifference, processedDifference.Result, processedDefaultUnitName.Result, definition.DefaultUnitSymbol, processedReciprocal.Result,
            processedSquare.Result, processedCube.Result, processedSquareRoot.Result, processedCubeRoot.Result, definition.GenerateDocumentation);

        return OptionalWithDiagnostics.Result(product, allDiagnostics);
    }

    private IOptionalWithDiagnostics<UnitInterface> ProcessUnit(ISharpMeasuresScalarRefinementContext context, SharpMeasuresScalarDefinition definition)
    {
        if (context.UnitPopulation.TryGetValue(definition.Unit, out var unit) is false)
        {
            return OptionalWithDiagnostics.Empty<UnitInterface>(Diagnostics.TypeNotUnit(context, definition));
        }

        if (definition.UseUnitBias && unit.BiasTerm is false)
        {
            return OptionalWithDiagnostics.Empty<UnitInterface>(Diagnostics.UnitNotIncludingBiasTerm(context, definition));
        }

        return OptionalWithDiagnostics.Result(unit);
    }

    private IOptionalWithDiagnostics<ResizedVectorGroup> ProcessVector(ISharpMeasuresScalarRefinementContext context, SharpMeasuresScalarDefinition definition)
    {
        if (definition.Vector is null)
        {
            return OptionalWithDiagnostics.Empty<ResizedVectorGroup>();
        }

        if (context.VectorPopulation.ResizedVectorGroups.TryGetValue(definition.Vector.Value, out var vectorGroup) is false)
        {
            if (context.VectorPopulation.AllVectors.ContainsKey(definition.Vector.Value) is false)
            {
                return OptionalWithDiagnostics.Empty<ResizedVectorGroup>(Diagnostics.TypeNotVector(context, definition));
            }

            return OptionalWithDiagnostics.Empty<ResizedVectorGroup>();
        }

        return OptionalWithDiagnostics.Result(vectorGroup);
    }

    private IResultWithDiagnostics<ScalarInterface> ProcessDifference(ISharpMeasuresScalarRefinementContext context, SharpMeasuresScalarDefinition definition)
    {
        if (context.ScalarPopulation.TryGetValue(definition.Difference, out var scalar) is false)
        {
            return ResultWithDiagnostics.Construct(context.ScalarPopulation[context.Type.AsNamedType()], Diagnostics.DifferenceNotScalar(context, definition));
        }

        return ResultWithDiagnostics.Construct(scalar);
    }

    private IResultWithDiagnostics<string?> ProcessDefaultUnitName(ISharpMeasuresScalarRefinementContext context, SharpMeasuresScalarDefinition definition, UnitInterface unit)
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

    private static IResultWithDiagnostics<ScalarInterface?> ProcessPowerQuantity(ISharpMeasuresScalarRefinementContext context, SharpMeasuresScalarDefinition definition,
        NamedType? quantity, Func<ISharpMeasuresScalarRefinementContext, SharpMeasuresScalarDefinition, Diagnostic?> typeNotScalarDiagnosticsDelegate)
    {
        if (quantity is null)
        {
            return ResultWithDiagnostics.Construct<ScalarInterface?>(null);
        }

        if (context.ScalarPopulation.TryGetValue(quantity.Value, out var scalar) is false)
        {
            return ResultWithDiagnostics.Construct<ScalarInterface?>(null, typeNotScalarDiagnosticsDelegate(context, definition));
        }

        return ResultWithDiagnostics.Construct<ScalarInterface?>(scalar);
    }
}
