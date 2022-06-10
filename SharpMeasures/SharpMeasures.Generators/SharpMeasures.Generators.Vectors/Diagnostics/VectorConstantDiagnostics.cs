namespace SharpMeasures.Generators.Vectors.Diagnostics;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Vectors.Parsing.VectorConstant;
using SharpMeasures.Generators.Vectors.Refinement;

internal class VectorConstantDiagnostics : IVectorConstantProcessingDiagnostics, IVectorConstantRefinementDiagnostics
{
    public static VectorConstantDiagnostics Instance { get; } = new();

    private VectorConstantDiagnostics() { }

    public Diagnostic NullName(IVectorConstantProcessingContext context, RawVectorConstantDefinition definition)
    {
        return DiagnosticConstruction.NullConstantName(definition.Locations.Name?.AsRoslynLocation());
    }

    public Diagnostic EmptyName(IVectorConstantProcessingContext context, RawVectorConstantDefinition definition) => NullName(context, definition);

    public Diagnostic DuplicateName(IVectorConstantProcessingContext context, RawVectorConstantDefinition definition)
    {
        return DiagnosticConstruction.DuplicateConstantName(definition.Locations.Name?.AsRoslynLocation(), definition.Name!, context.Type.Name);
    }

    public Diagnostic InvalidValueDimension(IVectorConstantProcessingContext context, RawVectorConstantDefinition definition)
    {
        return DiagnosticConstruction.VectorConstantInvalidDimension(definition.Locations.ValueCollection?.AsRoslynLocation(), context.Dimension, context.Type.Name);
    }

    public Diagnostic NullMultiplesName(IVectorConstantProcessingContext context, RawVectorConstantDefinition definition)
    {
        return DiagnosticConstruction.NullConstantMultiplesName(definition.Locations.MultiplesName?.AsRoslynLocation(), definition.Name!);
    }

    public Diagnostic EmptyMultiplesName(IVectorConstantProcessingContext context, RawVectorConstantDefinition definition) => NullMultiplesName(context, definition);

    public Diagnostic InvalidMultiplesName(IVectorConstantProcessingContext context, RawVectorConstantDefinition definition)
    {
        return DiagnosticConstruction.InvalidConstantMultiplesName(definition.Locations.MultiplesName?.AsRoslynLocation(),
            definition.Name!, definition.ParsingData.InterpretedMultiplesName!);
    }

    public Diagnostic DuplicateMultiplesName(IVectorConstantProcessingContext context, RawVectorConstantDefinition definition)
    {
        if (definition.Locations.ExplicitlySetMultiplesName)
        {
            return DiagnosticConstruction.DuplicateConstantMultiplesName(definition.Locations.MultiplesName?.AsRoslynLocation(),
                definition.ParsingData.InterpretedMultiplesName!, context.Type.Name);
        }

        return DiagnosticConstruction.DuplicateConstantMultiplesName(definition.Locations.Attribute.AsRoslynLocation(), definition.ParsingData.InterpretedMultiplesName!,
            context.Type.Name);
    }

    public Diagnostic NullUnit(IVectorConstantRefinementContext context, VectorConstantDefinition definition)
    {
        return DiagnosticConstruction.NullUnrecognizedUnitName(definition.Locations.Unit?.AsRoslynLocation(), context.Unit.UnitType.Name);
    }

    public Diagnostic EmptyUnit(IVectorConstantRefinementContext context, VectorConstantDefinition definition)
        => NullUnit(context, definition);

    public Diagnostic UnrecognizedUnit(IVectorConstantRefinementContext context, VectorConstantDefinition definition)
    {
        return DiagnosticConstruction.UnrecognizedUnitName(definition.Locations.Unit?.AsRoslynLocation(), definition.Unit!, context.Unit.UnitType.Name);
    }
}
