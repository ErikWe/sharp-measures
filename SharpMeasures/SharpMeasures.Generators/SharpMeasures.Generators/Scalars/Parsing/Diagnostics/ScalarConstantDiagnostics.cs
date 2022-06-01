namespace SharpMeasures.Generators.Scalars.Parsing.Diagnostics;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.Scalars;
using SharpMeasures.Generators.Diagnostics;

internal class ScalarConstantDiagnostics : IScalarConstantDiagnostics
{
    public static ScalarConstantDiagnostics Instance { get; } = new();

    private ScalarConstantDiagnostics() { }

    public Diagnostic NullName(IScalarConstantProcessingContext context, RawScalarConstantDefinition definition)
    {
        return DiagnosticConstruction.InvalidConstantName_Null(definition.Locations.Name?.AsRoslynLocation());
    }

    public Diagnostic EmptyName(IScalarConstantProcessingContext context, RawScalarConstantDefinition definition) => NullName(context, definition);

    public Diagnostic DuplicateName(IScalarConstantProcessingContext context, RawScalarConstantDefinition definition)
    {
        return DiagnosticConstruction.DuplicateConstantName(definition.Locations.Name?.AsRoslynLocation(), definition.Name!, context.Type.Name);
    }

    public Diagnostic NullUnit(IScalarConstantProcessingContext context, RawScalarConstantDefinition definition)
    {
        return DiagnosticConstruction.UnrecognizedUnitName_Null(definition.Locations.Unit?.AsRoslynLocation(), context.Unit.Name);
    }

    public Diagnostic EmptyUnit(IScalarConstantProcessingContext context, RawScalarConstantDefinition definition) => NullUnit(context, definition);

    public Diagnostic NullMultiplesName(IScalarConstantProcessingContext context, RawScalarConstantDefinition definition)
    {
        return DiagnosticConstruction.InvalidConstantMultiplesName_Null(definition.Locations.MultiplesName?.AsRoslynLocation(), definition.Name!);
    }

    public Diagnostic EmptyMultiplesName(IScalarConstantProcessingContext context, RawScalarConstantDefinition definition) => NullMultiplesName(context, definition);

    public Diagnostic InvalidMultiplesName(IScalarConstantProcessingContext context, RawScalarConstantDefinition definition)
    {
        return DiagnosticConstruction.InvalidConstantMultiplesName(definition.Locations.MultiplesName?.AsRoslynLocation(),
            definition.Name!, definition.ParsingData.InterpretedMultiplesName!);
    }

    public Diagnostic DuplicateMultiplesName(IScalarConstantProcessingContext context, RawScalarConstantDefinition definition)
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
