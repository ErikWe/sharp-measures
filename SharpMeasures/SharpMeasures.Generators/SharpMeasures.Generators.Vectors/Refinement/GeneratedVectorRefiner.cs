namespace SharpMeasures.Generators.Vectors.Refinement;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Vectors.Parsing.GeneratedVector;

using System;
using System.Linq;

internal interface IGeneratedVectorRefinementDiagnostics
{
    public Diagnostic? TypeAlreadyUnit(IGeneratedVectorRefinementContext context, GeneratedVectorDefinition definition);
    public Diagnostic? TypeAlreadyScalar(IGeneratedVectorRefinementContext context, GeneratedVectorDefinition definition);
    public Diagnostic? TypeNotUnit(IGeneratedVectorRefinementContext context, GeneratedVectorDefinition definition);
    public Diagnostic? TypeNotScalar(IGeneratedVectorRefinementContext context, GeneratedVectorDefinition definition);
    public Diagnostic? UnrecognizedUnit(IGeneratedVectorRefinementContext context, GeneratedVectorDefinition definition);
}

internal interface IGeneratedVectorRefinementContext : IProcessingContext
{
    public abstract UnitPopulation UnitPopulation { get; }
    public abstract ScalarPopulation ScalarPopulation { get; }
    public abstract VectorPopulation VectorPopulation { get; }
}

internal class GeneratedVectorRefiner : IProcesser<IGeneratedVectorRefinementContext, GeneratedVectorDefinition, RefinedGeneratedVectorDefinition>
{
    private IGeneratedVectorRefinementDiagnostics Diagnostics { get; }

    public GeneratedVectorRefiner(IGeneratedVectorRefinementDiagnostics diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public IOptionalWithDiagnostics<RefinedGeneratedVectorDefinition> Process(IGeneratedVectorRefinementContext context, GeneratedVectorDefinition definition)
    {
        if (context.UnitPopulation.ContainsKey(context.Type.AsNamedType()))
        {
            return OptionalWithDiagnostics.Empty<RefinedGeneratedVectorDefinition>(Diagnostics.TypeAlreadyUnit(context, definition));
        }

        if (context.ScalarPopulation.ContainsKey(context.Type.AsNamedType()))
        {
            return OptionalWithDiagnostics.Empty<RefinedGeneratedVectorDefinition>(Diagnostics.TypeAlreadyScalar(context, definition));
        }

        if (context.VectorPopulation.ResizedVectorGroups.TryGetValue(context.Type.AsNamedType(), out var vectorGroup) is false)
        {
            return OptionalWithDiagnostics.Empty<RefinedGeneratedVectorDefinition>();
        }

        var processedUnit = ProcessUnit(context, definition);
        var allDiagnostics = processedUnit.Diagnostics;

        if (processedUnit.LacksResult)
        {
            return OptionalWithDiagnostics.Empty<RefinedGeneratedVectorDefinition>(allDiagnostics);
        }

        var processedScalar = ProcessScalar(context, definition);
        allDiagnostics = allDiagnostics.Concat(processedScalar.Diagnostics);

        var processedDefaultUnitName = ProcessDefaultUnitName(context, definition, processedUnit.Result);
        allDiagnostics = allDiagnostics.Concat(processedDefaultUnitName.Diagnostics);

        RefinedGeneratedVectorDefinition product = new(processedUnit.Result, processedScalar.Result, vectorGroup, definition.Dimension, processedDefaultUnitName.Result,
            definition.DefaultUnitSymbol, definition.GenerateDocumentation);

        return OptionalWithDiagnostics.Result(product, allDiagnostics);
    }

    private IOptionalWithDiagnostics<UnitInterface> ProcessUnit(IGeneratedVectorRefinementContext context, GeneratedVectorDefinition definition)
    {
        if (context.UnitPopulation.TryGetValue(definition.Unit, out UnitInterface unit) is false)
        {
            return OptionalWithDiagnostics.Empty<UnitInterface>(Diagnostics.TypeNotUnit(context, definition));
        }

        return OptionalWithDiagnostics.Result(unit);
    }

    private IOptionalWithDiagnostics<ScalarInterface?> ProcessScalar(IGeneratedVectorRefinementContext context, GeneratedVectorDefinition definition)
    {
        if (definition.Scalar is null)
        {
            return OptionalWithDiagnostics.Result<ScalarInterface?>(null);
        }

        if (context.ScalarPopulation.TryGetValue(definition.Scalar.Value, out ScalarInterface scalar))
        {
            return OptionalWithDiagnostics.Result<ScalarInterface?>(scalar);
        }

        return OptionalWithDiagnostics.Empty<ScalarInterface?>(Diagnostics.TypeNotScalar(context, definition));
    }

    private IResultWithDiagnostics<string?> ProcessDefaultUnitName(IGeneratedVectorRefinementContext context, GeneratedVectorDefinition definition, UnitInterface unit)
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
