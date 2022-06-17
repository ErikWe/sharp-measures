namespace SharpMeasures.Generators.Scalars.Refinement.ScalarConstant;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Scalars.Parsing.ScalarConstant;
using SharpMeasures.Generators.Units;

using System.Collections.Generic;

internal interface IScalarConstantRefinementDiagnostics
{
    public abstract Diagnostic? UnrecognizedUnit(IScalarConstantRefinementContext context, ScalarConstantDefinition definition);
    public abstract Diagnostic? ConstantSharesNameWithUnit(IScalarConstantRefinementContext context, ScalarConstantDefinition definition);
    public abstract Diagnostic? ConstantMultiplesSharesNameWithUnitPlural(IScalarConstantRefinementContext context, ScalarConstantDefinition definition);
}

internal interface IScalarConstantRefinementContext : IProcessingContext
{
    public UnitInterface Unit { get; }

    public HashSet<string> IncludedBases { get; }
    public HashSet<string> IncludedUnits { get; }
}

internal class ScalarConstantRefiner : IProcesser<IScalarConstantRefinementContext, ScalarConstantDefinition, RefinedScalarConstantDefinition>
{
    private IScalarConstantRefinementDiagnostics Diagnostics { get; }

    public ScalarConstantRefiner(IScalarConstantRefinementDiagnostics diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public IOptionalWithDiagnostics<RefinedScalarConstantDefinition> Process(IScalarConstantRefinementContext context, ScalarConstantDefinition definition)
    {
        if (context.Unit.UnitsByName.TryGetValue(definition.Unit, out var unit) is false)
        {
            return OptionalWithDiagnostics.Empty<RefinedScalarConstantDefinition>(Diagnostics.UnrecognizedUnit(context, definition));
        }

        if (context.IncludedBases.Contains(definition.Name))
        {
            return OptionalWithDiagnostics.Empty<RefinedScalarConstantDefinition>(Diagnostics.ConstantSharesNameWithUnit(context, definition));
        }

        var processedMultiples = ProcessMultiples(context, definition);

        RefinedScalarConstantDefinition product = new(definition.Name, unit, definition.Value, processedMultiples.Result.Generate, processedMultiples.Result.Name);
        return OptionalWithDiagnostics.Result(product, processedMultiples.Diagnostics);
    }

    private IResultWithDiagnostics<(bool Generate, string? Name)> ProcessMultiples(IScalarConstantRefinementContext context, ScalarConstantDefinition definition)
    {
        if (definition.GenerateMultiplesProperty is false)
        {
            return ResultWithDiagnostics.Construct<(bool, string?)>((false, null));
        }

        if (context.IncludedUnits.Contains(definition.MultiplesName!))
        {
            return ResultWithDiagnostics.Construct<(bool, string?)>((false, null), Diagnostics.ConstantMultiplesSharesNameWithUnitPlural(context, definition));
        }

        return ResultWithDiagnostics.Construct<(bool, string?)>((true, definition.MultiplesName));
    }
}
