namespace SharpMeasures.Generators.Scalars.Refinement.ScalarConstant;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Scalars.Parsing.ScalarConstant;
using SharpMeasures.Generators.Units;

using System;
using System.Collections.Generic;
using System.Linq;

internal interface IScalarConstantRefinementDiagnostics
{
    public abstract Diagnostic? UnrecognizedUnit(IScalarConstantRefinementContext context, ScalarConstantDefinition definition);
    public abstract Diagnostic? ConstantSharesNameWithUnit(IScalarConstantRefinementContext context, ScalarConstantDefinition definition);
    public abstract Diagnostic? ConstantMultiplesSharesNameWithUnit(IScalarConstantRefinementContext context, ScalarConstantDefinition definition);
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
        var nameValidity = CheckNameValidity(context, definition);
        var allDiagnostics = nameValidity.Diagnostics;

        if (nameValidity.IsInvalid)
        {
            return OptionalWithDiagnostics.Empty<RefinedScalarConstantDefinition>(allDiagnostics);
        }

        var processedUnit = ProcessUnit(context, definition);
        allDiagnostics = allDiagnostics.Concat(processedUnit.Diagnostics);

        if (processedUnit.LacksResult)
        {
            return OptionalWithDiagnostics.Empty<RefinedScalarConstantDefinition>(allDiagnostics);
        }

        var processedMultiples = ProcessMultiples(context, definition);
        allDiagnostics = allDiagnostics.Concat(processedMultiples.Diagnostics);

        RefinedScalarConstantDefinition product = new(definition.Name, processedUnit.Result, definition.Value, processedMultiples.Result.Generate,
            processedMultiples.Result.Name);

        return OptionalWithDiagnostics.Result(product, allDiagnostics);
    }

    private IValidityWithDiagnostics CheckNameValidity(IScalarConstantRefinementContext context, ScalarConstantDefinition definition)
    {
        if (definition.Name.StartsWith("One", StringComparison.InvariantCulture) && context.IncludedBases.Contains(definition.Name.Substring(3))
            || context.IncludedUnits.Contains(definition.Name))
        {
            return ValidityWithDiagnostics.Invalid(Diagnostics.ConstantSharesNameWithUnit(context, definition));
        }

        return ValidityWithDiagnostics.Valid;
    }

    private IOptionalWithDiagnostics<UnitInstance> ProcessUnit(IScalarConstantRefinementContext context, ScalarConstantDefinition definition)
    {
        if (context.Unit.UnitsByName.TryGetValue(definition.Unit, out var unit) is false)
        {
            return OptionalWithDiagnostics.Empty<UnitInstance>(Diagnostics.UnrecognizedUnit(context, definition));
        }

        return OptionalWithDiagnostics.Result(unit);
    }

    private IResultWithDiagnostics<(bool Generate, string? Name)> ProcessMultiples(IScalarConstantRefinementContext context, ScalarConstantDefinition definition)
    {
        if (definition.GenerateMultiplesProperty is false)
        {
            return ResultWithDiagnostics.Construct<(bool, string?)>((false, null));
        }

        if (definition.Multiples!.StartsWith("One", StringComparison.InvariantCulture) && context.IncludedBases.Contains(definition.Multiples!.Substring(3))
            || context.IncludedUnits.Contains(definition.Multiples!))
        {
            return ResultWithDiagnostics.Construct<(bool, string?)>((false, null), Diagnostics.ConstantMultiplesSharesNameWithUnit(context, definition));
        }

        return ResultWithDiagnostics.Construct<(bool, string?)>((true, definition.Multiples));
    }
}
