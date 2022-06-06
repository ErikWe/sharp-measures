namespace SharpMeasures.Generators.Vectors.Processing;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Attributes.Parsing.Vectors;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Vectors.Processing.Diagnostics;
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;

using System;
using System.Linq;

internal class GeneratedVectorProcessingContext : IProcessingContext
{
    public DefinedType Type { get; }

    public UnitPopulation UnitPopulation { get; }
    public ScalarPopulation ScalarPopulation { get; }

    public GeneratedVectorProcessingContext(DefinedType type, UnitPopulation unitPopulation, ScalarPopulation scalarPopulation)
    {
        Type = type;

        UnitPopulation = unitPopulation;
        ScalarPopulation = scalarPopulation;
    }
}

internal class GeneratedVectorProcesser : IProcesser<GeneratedVectorProcessingContext, GeneratedVector, ProcessedGeneratedVector>
{
    public static GeneratedVectorProcesser Instance { get; } = new();

    private GeneratedVectorProcesser() { }

    public IOptionalWithDiagnostics<ProcessedGeneratedVector> Process(GeneratedVectorProcessingContext context, GeneratedVector definition)
    {
        if (context is null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        if (definition is null)
        {
            throw new ArgumentNullException(nameof(definition));
        }

        if (context.UnitPopulation.ContainsKey(context.Type.AsNamedType()))
        {
            var diagnostics = GeneratedVectorDiagnostics.TypeAlreadyUnit(definition.Locations.Attribute, context.Type.AsNamedType());
            return OptionalWithDiagnostics.Empty<ProcessedGeneratedVector>(diagnostics);
        }

        if (context.ScalarPopulation.ContainsKey(context.Type.AsNamedType()))
        {
            var diagnostics = GeneratedVectorDiagnostics.TypeAlreadyScalar(definition.Locations.Attribute, context.Type.AsNamedType());
            return OptionalWithDiagnostics.Empty<ProcessedGeneratedVector>(diagnostics);
        }

        var processedUnit = ProcessUnit(context, definition);
        var allDiagnostics = processedUnit.Diagnostics;

        if (processedUnit.LacksResult)
        {
            return OptionalWithDiagnostics.Empty<ProcessedGeneratedVector>(allDiagnostics);
        }

        var processedScalar = ProcessScalar(context, definition);
        allDiagnostics = allDiagnostics.Concat(processedScalar.Diagnostics);

        var processedDefaultUnitName = ProcessDefaultUnitName(definition, processedUnit.Result);
        allDiagnostics = allDiagnostics.Concat(processedDefaultUnitName.Diagnostics);

        ProcessedGeneratedVector product = new(processedUnit.Result, processedScalar.Result, processedDefaultUnitName.Result, definition.DefaultUnitSymbol);

        return OptionalWithDiagnostics.Result(product, allDiagnostics);
    }

    private static IOptionalWithDiagnostics<UnitInterface> ProcessUnit(GeneratedVectorProcessingContext context, GeneratedVector definition)
    {
        if (context.UnitPopulation.TryGetValue(definition.Unit, out UnitInterface unit) is false)
        {
            return OptionalWithDiagnostics.Empty<UnitInterface>(GeneratedVectorDiagnostics.TypeNotUnit(definition));
        }

        return OptionalWithDiagnostics.Result(unit);
    }

    private static IOptionalWithDiagnostics<ScalarInterface?> ProcessScalar(GeneratedVectorProcessingContext context, GeneratedVector definition)
    {
        if (definition.Scalar is null)
        {
            return OptionalWithDiagnostics.Result<ScalarInterface?>(null);
        }

        if (context.ScalarPopulation.TryGetValue(definition.Scalar.Value, out ScalarInterface scalar))
        {
            return OptionalWithDiagnostics.Result<ScalarInterface?>(scalar);
        }

        return OptionalWithDiagnostics.Empty<ScalarInterface?>(GeneratedVectorDiagnostics.TypeNotScalar(definition));
    }

    private static IResultWithDiagnostics<string?> ProcessDefaultUnitName(GeneratedVector definition, UnitInterface unit)
    {
        if (definition.DefaultUnitName is null)
        {
            return ResultWithDiagnostics.Construct<string?>(null);
        }

        if (unit.UnitsByName.ContainsKey(definition.DefaultUnitName) is false)
        {
            return ResultWithDiagnostics.Construct<string?>(null, GeneratedVectorDiagnostics.UnrecognizedUnit(definition, unit));
        }

        return ResultWithDiagnostics.Construct<string?>(definition.DefaultUnitName);
    }
}
