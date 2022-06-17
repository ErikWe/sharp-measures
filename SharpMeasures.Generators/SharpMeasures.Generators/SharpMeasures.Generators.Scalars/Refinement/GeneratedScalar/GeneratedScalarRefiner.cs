namespace SharpMeasures.Generators.Scalars.Refinement.GeneratedScalar;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators;
using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Scalars.Parsing.GeneratedScalar;
using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Vectors;

using System;
using System.Linq;

internal interface IGeneratedScalarRefinementDiagnostics
{
    public abstract Diagnostic? TypeAlreadyUnit(IGeneratedScalarRefinementContext context, GeneratedScalarDefinition definition);
    public abstract Diagnostic? TypeNotUnit(IGeneratedScalarRefinementContext context, GeneratedScalarDefinition definition);
    public abstract Diagnostic? UnitNotIncludingBiasTerm(IGeneratedScalarRefinementContext context, GeneratedScalarDefinition definition);
    public abstract Diagnostic? TypeNotVector(IGeneratedScalarRefinementContext context, GeneratedScalarDefinition definition);
    public abstract Diagnostic? UnrecognizedDefaultUnit(IGeneratedScalarRefinementContext context, GeneratedScalarDefinition definition);
    public abstract Diagnostic? DifferenceNotScalar(IGeneratedScalarRefinementContext context, GeneratedScalarDefinition definition);
    public abstract Diagnostic? ReciprocalNotScalar(IGeneratedScalarRefinementContext context, GeneratedScalarDefinition definition);
    public abstract Diagnostic? SquareNotScalar(IGeneratedScalarRefinementContext context, GeneratedScalarDefinition definition);
    public abstract Diagnostic? CubeNotScalar(IGeneratedScalarRefinementContext context, GeneratedScalarDefinition definition);
    public abstract Diagnostic? SquareRootNotScalar(IGeneratedScalarRefinementContext context, GeneratedScalarDefinition definition);
    public abstract Diagnostic? CubeRootNotScalar(IGeneratedScalarRefinementContext context, GeneratedScalarDefinition definition);
}

internal interface IGeneratedScalarRefinementContext : IProcessingContext
{
    public abstract UnitPopulation UnitPopulation { get; }
    public abstract ScalarPopulation ScalarPopulation { get; }
    public abstract VectorPopulation VectorPopulation { get; }
}

internal class GeneratedScalarRefiner : IProcesser<IGeneratedScalarRefinementContext, GeneratedScalarDefinition, RefinedGeneratedScalarDefinition>
{
    private IGeneratedScalarRefinementDiagnostics Diagnostics { get; }

    public GeneratedScalarRefiner(IGeneratedScalarRefinementDiagnostics diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public IOptionalWithDiagnostics<RefinedGeneratedScalarDefinition> Process(IGeneratedScalarRefinementContext context, GeneratedScalarDefinition definition)
    {
        if (context.UnitPopulation.ContainsKey(context.Type.AsNamedType()))
        {
            var diagnostics = Diagnostics.TypeAlreadyUnit(context, definition);
            return OptionalWithDiagnostics.Empty<RefinedGeneratedScalarDefinition>(diagnostics);
        }

        var processedUnit = ProcessUnit(context, definition);
        var allDiagnostics = processedUnit.Diagnostics;

        if (processedUnit.LacksResult)
        {
            return OptionalWithDiagnostics.Empty<RefinedGeneratedScalarDefinition>(allDiagnostics);
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

        RefinedGeneratedScalarDefinition product = new(processedUnit.Result, processedVector.NullableResult, definition.UseUnitBias, definition.ImplementSum,
            definition.ImplementDifference, processedDifference.Result, processedDefaultUnitName.Result, definition.DefaultUnitSymbol, processedReciprocal.Result,
            processedSquare.Result, processedCube.Result, processedSquareRoot.Result, processedCubeRoot.Result, definition.GenerateDocumentation);

        return OptionalWithDiagnostics.Result(product, allDiagnostics);
    }

    private IOptionalWithDiagnostics<UnitInterface> ProcessUnit(IGeneratedScalarRefinementContext context, GeneratedScalarDefinition definition)
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

    private IOptionalWithDiagnostics<ResizedVectorGroup> ProcessVector(IGeneratedScalarRefinementContext context, GeneratedScalarDefinition definition)
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

    private IResultWithDiagnostics<ScalarInterface> ProcessDifference(IGeneratedScalarRefinementContext context, GeneratedScalarDefinition definition)
    {
        if (context.ScalarPopulation.TryGetValue(definition.Difference, out var scalar) is false)
        {
            return ResultWithDiagnostics.Construct(context.ScalarPopulation[context.Type.AsNamedType()], Diagnostics.DifferenceNotScalar(context, definition));
        }

        return ResultWithDiagnostics.Construct(scalar);
    }

    private IResultWithDiagnostics<string?> ProcessDefaultUnitName(IGeneratedScalarRefinementContext context, GeneratedScalarDefinition definition, UnitInterface unit)
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

    private static IResultWithDiagnostics<ScalarInterface?> ProcessPowerQuantity(IGeneratedScalarRefinementContext context, GeneratedScalarDefinition definition,
        NamedType? quantity, Func<IGeneratedScalarRefinementContext, GeneratedScalarDefinition, Diagnostic?> typeNotScalarDiagnosticsDelegate)
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
