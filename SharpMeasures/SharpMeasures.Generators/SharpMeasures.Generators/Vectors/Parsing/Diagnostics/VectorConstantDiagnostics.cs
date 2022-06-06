namespace SharpMeasures.Generators.Vectors.Parsing.Diagnostics;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.Vectors;
using SharpMeasures.Generators.Diagnostics;

internal class VectorConstantDiagnostics : IVectorConstantDiagnostics
{
    public static VectorConstantDiagnostics Instance { get; } = new();

    private VectorConstantDiagnostics() { }

    public Diagnostic NullName(IVectorConstantProcessingContext context, RawVectorConstant definition)
    {
        return DiagnosticConstruction.InvalidConstantName_Null(definition.Locations.Name?.AsRoslynLocation());
    }

    public Diagnostic EmptyName(IVectorConstantProcessingContext context, RawVectorConstant definition) => NullName(context, definition);

    public Diagnostic DuplicateName(IVectorConstantProcessingContext context, RawVectorConstant definition)
    {
        return DiagnosticConstruction.DuplicateConstantName(definition.Locations.Name?.AsRoslynLocation(), definition.Name!, context.Type.Name);
    }

    public Diagnostic NullUnit(IVectorConstantProcessingContext context, RawVectorConstant definition)
    {
        return DiagnosticConstruction.UnrecognizedUnitName_Null(definition.Locations.Unit?.AsRoslynLocation(), context.Unit.Name);
    }

    public Diagnostic EmptyUnit(IVectorConstantProcessingContext context, RawVectorConstant definition) => NullUnit(context, definition);

    public Diagnostic InvalidValueDimension(IVectorConstantProcessingContext context, RawVectorConstant definition)
    {
        return DiagnosticConstruction.VectorConstantInvalidDimension(definition.Locations.ValueCollection?.AsRoslynLocation(), context.Dimension, context.Type.Name);
    }

    public Diagnostic NullMultiplesName(IVectorConstantProcessingContext context, RawVectorConstant definition)
    {
        return DiagnosticConstruction.InvalidConstantMultiplesName_Null(definition.Locations.MultiplesName?.AsRoslynLocation(), definition.Name!);
    }

    public Diagnostic EmptyMultiplesName(IVectorConstantProcessingContext context, RawVectorConstant definition) => NullMultiplesName(context, definition);

    public Diagnostic InvalidMultiplesName(IVectorConstantProcessingContext context, RawVectorConstant definition)
    {
        return DiagnosticConstruction.InvalidConstantMultiplesName(definition.Locations.MultiplesName?.AsRoslynLocation(),
            definition.Name!, definition.ParsingData.InterpretedMultiplesName!);
    }

    public Diagnostic DuplicateMultiplesName(IVectorConstantProcessingContext context, RawVectorConstant definition)
    {
        if (definition.Locations.ExplicitlySetMultiplesName)
        {
            return DiagnosticConstruction.DuplicateConstantMultiplesName(definition.Locations.MultiplesName?.AsRoslynLocation(),
                definition.ParsingData.InterpretedMultiplesName!, context.Type.Name);
        }

        return DiagnosticConstruction.DuplicateConstantMultiplesName(definition.Locations.Attribute.AsRoslynLocation(), definition.ParsingData.InterpretedMultiplesName!,
            context.Type.Name);
    }
}
