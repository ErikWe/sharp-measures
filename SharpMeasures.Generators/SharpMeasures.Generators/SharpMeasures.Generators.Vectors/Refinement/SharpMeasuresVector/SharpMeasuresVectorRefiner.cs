namespace SharpMeasures.Generators.Vectors.Refinement.SharpMeasuresVector;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Vectors;
using SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVector;

using System;
using System.Linq;

internal interface ISharpMeasuresVectorRefinementDiagnostics
{
    public Diagnostic? TypeAlreadyUnit(ISharpMeasuresVectorRefinementContext context, SharpMeasuresVectorDefinition definition);
    public Diagnostic? TypeAlreadyScalar(ISharpMeasuresVectorRefinementContext context, SharpMeasuresVectorDefinition definition);
    public Diagnostic? TypeNotUnit(ISharpMeasuresVectorRefinementContext context, SharpMeasuresVectorDefinition definition);
    public Diagnostic? TypeNotScalar(ISharpMeasuresVectorRefinementContext context, SharpMeasuresVectorDefinition definition);
    public Diagnostic? UnrecognizedUnit(ISharpMeasuresVectorRefinementContext context, SharpMeasuresVectorDefinition definition);
}

internal interface ISharpMeasuresVectorRefinementContext : IProcessingContext
{
    public abstract UnitPopulation UnitPopulation { get; }
    public abstract ScalarPopulation ScalarPopulation { get; }
    public abstract VectorPopulation VectorPopulation { get; }
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

        var processedDefaultUnitName = ProcessDefaultUnitName(context, definition, processedUnit.Result);
        allDiagnostics = allDiagnostics.Concat(processedDefaultUnitName.Diagnostics);

        RefinedSharpMeasuresVectorDefinition product = new(processedUnit.Result, processedScalar.Result, vectorGroup, definition.Dimension, processedDefaultUnitName.Result,
            definition.DefaultUnitSymbol, definition.GenerateDocumentation);

        return OptionalWithDiagnostics.Result(product, allDiagnostics);
    }

    private IOptionalWithDiagnostics<UnitInterface> ProcessUnit(ISharpMeasuresVectorRefinementContext context, SharpMeasuresVectorDefinition definition)
    {
        if (context.UnitPopulation.TryGetValue(definition.Unit, out var unit) is false)
        {
            return OptionalWithDiagnostics.Empty<UnitInterface>(Diagnostics.TypeNotUnit(context, definition));
        }

        return OptionalWithDiagnostics.Result(unit);
    }

    private IOptionalWithDiagnostics<ScalarInterface?> ProcessScalar(ISharpMeasuresVectorRefinementContext context, SharpMeasuresVectorDefinition definition)
    {
        if (definition.Scalar is null)
        {
            return OptionalWithDiagnostics.Result<ScalarInterface?>(null);
        }

        if (context.ScalarPopulation.TryGetValue(definition.Scalar.Value, out var scalar))
        {
            return OptionalWithDiagnostics.Result<ScalarInterface?>(scalar);
        }

        return OptionalWithDiagnostics.Empty<ScalarInterface?>(Diagnostics.TypeNotScalar(context, definition));
    }

    private IResultWithDiagnostics<string?> ProcessDefaultUnitName(ISharpMeasuresVectorRefinementContext context, SharpMeasuresVectorDefinition definition, UnitInterface unit)
    {
        if (definition.DefaultUnitName is null)
        {
            return ResultWithDiagnostics.Construct<string?>(null);
        }

        if (unit.UnitsByName.ContainsKey(definition.DefaultUnitName) is false)
        {
            return ResultWithDiagnostics.Construct<string?>(null, Diagnostics.UnrecognizedUnit(context, definition));
        }

        return ResultWithDiagnostics.Construct<string?>(definition.DefaultUnitName);
    }
}
