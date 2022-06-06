namespace SharpMeasures.Generators.Scalars.Processing;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Attributes.Parsing.Scalars;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Scalars.Processing.Diagnostics;
using SharpMeasures.Generators.Units;

using System.Collections.Generic;

internal class ScalarConstantProcessingContext : IProcessingContext
{
    public DefinedType Type { get; }
    public UnitInterface Unit { get; }

    public HashSet<string> UnitNames { get; }
    public HashSet<string> UnitPlurals { get; }

    public ScalarConstantProcessingContext(DefinedType type, UnitInterface unit, HashSet<string> unitNames, HashSet<string> unitPlurals)
    {
        Type = type;
        Unit = unit;

        UnitNames = unitNames;
        UnitPlurals = unitPlurals;
    }
}

internal class ScalarConstantProcesser : IProcesser<ScalarConstantProcessingContext, ScalarConstant, ProcessedScalarConstant>
{
    public static ScalarConstantProcesser Instance { get; } = new();

    private ScalarConstantProcesser() { }

    public IOptionalWithDiagnostics<ProcessedScalarConstant> Process(ScalarConstantProcessingContext context, ScalarConstant definition)
    {
        if (context.Unit.UnitsByName.TryGetValue(definition.Unit, out UnitInstance unit) is false)
        {
            return OptionalWithDiagnostics.Empty<ProcessedScalarConstant>(ScalarConstantDiagnostics.UnrecognizedUnit(context, definition));
        }

        if (context.UnitNames.Contains(definition.Name))
        {
            return OptionalWithDiagnostics.Empty<ProcessedScalarConstant>(ScalarConstantDiagnostics.ConstantSharesNameWithUnit(context, definition));
        }

        var processedMultiples = ProcessMultiples(context, definition);

        ProcessedScalarConstant product = new(definition.Name, definition.Value, unit, processedMultiples.Result.Generate, processedMultiples.Result.Name);
        return OptionalWithDiagnostics.Result(product, processedMultiples.Diagnostics);
    }

    private static IResultWithDiagnostics<(bool Generate, string? Name)> ProcessMultiples(ScalarConstantProcessingContext context, ScalarConstant definition)
    {
        if (definition.GenerateMultiplesProperty is false)
        {
            return ResultWithDiagnostics.Construct<(bool, string?)>((false, null));
        }

        if (context.UnitPlurals.Contains(definition.MultiplesName!))
        {
            return ResultWithDiagnostics.Construct<(bool, string?)>((false, null), ScalarConstantDiagnostics.ConstantMultiplesSharesNameWithUnitPlural(context, definition));
        }

        return ResultWithDiagnostics.Construct<(bool, string?)>((true, definition.MultiplesName));
    }
}
