namespace SharpMeasures.Generators.Vectors.Refinement.VectorConstant;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Vectors.Parsing.VectorConstant;
using SharpMeasures.Generators.Units;

using System.Collections.Generic;
using System.Linq;

internal interface IVectorConstantRefinementDiagnostics
{
    public abstract Diagnostic? UnrecognizedUnit(IVectorConstantRefinementContext context, VectorConstantDefinition definition);
    public abstract Diagnostic? ConstantSharesNameWithUnit(IVectorConstantRefinementContext context, VectorConstantDefinition definition);
    public abstract Diagnostic? ConstantMultiplesSharesNameWithUnit(IVectorConstantRefinementContext context, VectorConstantDefinition definition);
}

internal interface IVectorConstantRefinementContext : IProcessingContext
{
    public abstract IUnitType Unit { get; }

    public HashSet<string> IncludedUnits { get; }
}

internal class VectorConstantRefiner : IProcesser<IVectorConstantRefinementContext, VectorConstantDefinition, RefinedVectorConstantDefinition>
{
    private IVectorConstantRefinementDiagnostics Diagnostics { get; }

    public VectorConstantRefiner(IVectorConstantRefinementDiagnostics diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public IOptionalWithDiagnostics<RefinedVectorConstantDefinition> Process(IVectorConstantRefinementContext context, VectorConstantDefinition definition)
    {
        var nameValidity = CheckNameValidity(context, definition);
        var allDiagnostics = nameValidity.Diagnostics;

        if (nameValidity.IsInvalid)
        {
            return OptionalWithDiagnostics.Empty<RefinedVectorConstantDefinition>(allDiagnostics);
        }

        var processedUnit = ProcessUnit(context, definition);
        allDiagnostics = allDiagnostics.Concat(processedUnit.Diagnostics);

        if (processedUnit.LacksResult)
        {
            return OptionalWithDiagnostics.Empty<RefinedVectorConstantDefinition>(allDiagnostics);
        }

        var processedMultiples = ProcessMultiples(context, definition);
        allDiagnostics = allDiagnostics.Concat(processedMultiples.Diagnostics);

        RefinedVectorConstantDefinition product = new(definition.Name, processedUnit.Result, definition.Value, processedMultiples.Result.Generate,
            processedMultiples.Result.Name);

        return OptionalWithDiagnostics.Result(product, allDiagnostics);
    }

    private IValidityWithDiagnostics CheckNameValidity(IVectorConstantRefinementContext context, VectorConstantDefinition definition)
    {
        if (context.IncludedUnits.Contains(definition.Name))
        {
            return ValidityWithDiagnostics.Invalid(Diagnostics.ConstantSharesNameWithUnit(context, definition));
        }

        return ValidityWithDiagnostics.Valid;
    }

    private IOptionalWithDiagnostics<UnitInstance> ProcessUnit(IVectorConstantRefinementContext context, VectorConstantDefinition definition)
    {
        if (context.Unit.UnitsByName.TryGetValue(definition.Unit, out var unit) is false)
        {
            return OptionalWithDiagnostics.Empty<UnitInstance>(Diagnostics.UnrecognizedUnit(context, definition));
        }

        return OptionalWithDiagnostics.Result(unit);
    }

    private IResultWithDiagnostics<(bool Generate, string? Name)> ProcessMultiples(IVectorConstantRefinementContext context, VectorConstantDefinition definition)
    {
        if (definition.GenerateMultiplesProperty is false)
        {
            return ResultWithDiagnostics.Construct<(bool, string?)>((false, null));
        }

        if (context.IncludedUnits.Contains(definition.Multiples!))
        {
            return ResultWithDiagnostics.Construct<(bool, string?)>((false, null), Diagnostics.ConstantMultiplesSharesNameWithUnit(context, definition));
        }

        return ResultWithDiagnostics.Construct<(bool, string?)>((true, definition.Multiples));
    }
}
