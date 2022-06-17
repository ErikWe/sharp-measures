namespace SharpMeasures.Generators.Vectors.Refinement.VectorConstant;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Vectors.Parsing.VectorConstant;
using SharpMeasures.Generators.Units;

internal interface IVectorConstantRefinementDiagnostics
{
    public abstract Diagnostic? NullUnit(IVectorConstantRefinementContext context, VectorConstantDefinition definition);
    public abstract Diagnostic? EmptyUnit(IVectorConstantRefinementContext context, VectorConstantDefinition definition);
    public abstract Diagnostic? UnrecognizedUnit(IVectorConstantRefinementContext context, VectorConstantDefinition definition);
}

internal interface IVectorConstantRefinementContext : IProcessingContext
{
    public abstract UnitInterface Unit { get; }
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
        if (definition.Unit is null)
        {
            return OptionalWithDiagnostics.Empty<RefinedVectorConstantDefinition>(Diagnostics.NullUnit(context, definition));
        }

        if (definition.Unit.Length is 0)
        {
            return OptionalWithDiagnostics.Empty<RefinedVectorConstantDefinition>(Diagnostics.EmptyUnit(context, definition));
        }

        if (context.Unit.UnitsByName.TryGetValue(definition.Unit, out var unit) is false)
        {
            return OptionalWithDiagnostics.Empty<RefinedVectorConstantDefinition>(Diagnostics.UnrecognizedUnit(context, definition));
        }

        RefinedVectorConstantDefinition product = new(definition.Name, unit, definition.Value, definition.GenerateMultiplesProperty, definition.MultiplesName);
        return OptionalWithDiagnostics.Result(product);
    }
}
