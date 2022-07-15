namespace SharpMeasures.Generators.Vectors.Diagnostics;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Vectors.Parsing.VectorConstant;
using SharpMeasures.Generators.Vectors.Refinement.VectorConstant;

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

    public Diagnostic NullUnit(IVectorConstantProcessingContext context, RawVectorConstantDefinition definition)
    {
        return DiagnosticConstruction.NullUnrecognizedUnitNameUnknownType(definition.Locations.Unit?.AsRoslynLocation());
    }

    public Diagnostic EmptyUnit(IVectorConstantProcessingContext context, RawVectorConstantDefinition definition)
        => NullUnit(context, definition);

    public Diagnostic InvalidValueDimension(IVectorConstantProcessingContext context, RawVectorConstantDefinition definition)
    {
        return DiagnosticConstruction.VectorConstantInvalidDimension(definition.Locations.ValueCollection?.AsRoslynLocation(), context.Dimension,
            definition.Value.Count, context.Type.Name);
    }

    public Diagnostic NullMultiples(IVectorConstantProcessingContext context, RawVectorConstantDefinition definition)
    {
        return DiagnosticConstruction.NullConstantMultiplesName(definition.Locations.Multiples?.AsRoslynLocation(), definition.Name!);
    }

    public Diagnostic EmptyMultiples(IVectorConstantProcessingContext context, RawVectorConstantDefinition definition) => NullMultiples(context, definition);

    public Diagnostic InvalidMultiples(IVectorConstantProcessingContext context, RawVectorConstantDefinition definition)
    {
        return DiagnosticConstruction.InvalidConstantMultiplesName(definition.Locations.Multiples?.AsRoslynLocation(),
            definition.ParsingData.InterpretedMultiples!, definition.Name!);
    }

    public Diagnostic DuplicateMultiples(IVectorConstantProcessingContext context, RawVectorConstantDefinition definition)
    {
        if (definition.Locations.ExplicitlySetMultiples)
        {
            return DiagnosticConstruction.DuplicateConstantMultiplesName(definition.Locations.Multiples?.AsRoslynLocation(),
                definition.ParsingData.InterpretedMultiples!, context.Type.Name);
        }

        return DiagnosticConstruction.DuplicateConstantMultiplesName(definition.Locations.AttributeName.AsRoslynLocation(),
            definition.ParsingData.InterpretedMultiples!, context.Type.Name);
    }

    public Diagnostic MultiplesDisabledButNameSpecified(IVectorConstantProcessingContext context, RawVectorConstantDefinition definition)
    {
        return DiagnosticConstruction.ConstantMultiplesDisabledButNameSpecified(definition.Locations.Multiples?.AsRoslynLocation(), definition.Name!);
    }

    public Diagnostic UnrecognizedUnit(IVectorConstantRefinementContext context, VectorConstantDefinition definition)
    {
        return DiagnosticConstruction.UnrecognizedUnitName(definition.Locations.Unit?.AsRoslynLocation(), definition.Unit!, context.Unit.Type.Name);
    }

    public Diagnostic ConstantSharesNameWithUnit(IVectorConstantRefinementContext context, VectorConstantDefinition definition)
    {
        return DiagnosticConstruction.ConstantSharesNameWithUnit(definition.Locations.Name?.AsRoslynLocation(), definition.Name, context.Type.Name);
    }

    public Diagnostic ConstantMultiplesSharesNameWithUnit(IVectorConstantRefinementContext context, VectorConstantDefinition definition)
    {
        if (definition.Locations.ExplicitlySetMultiples)
        {
            return DiagnosticConstruction.ConstantSharesNameWithUnit(definition.Locations.Multiples?.AsRoslynLocation(), definition.Multiples!,
                context.Type.Name);
        }

        return DiagnosticConstruction.ConstantSharesNameWithUnit(definition.Locations.AttributeName.AsRoslynLocation(), definition.Multiples!,
            context.Type.Name);
    }
}
