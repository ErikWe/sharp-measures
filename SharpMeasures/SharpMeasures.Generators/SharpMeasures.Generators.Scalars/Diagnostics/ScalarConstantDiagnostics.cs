namespace SharpMeasures.Generators.Scalars.Diagnostics;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Scalars.Parsing.ScalarConstant;
using SharpMeasures.Generators.Scalars.Refinement.ScalarConstant;

internal class ScalarConstantDiagnostics : IScalarConstantDiagnostics, IScalarConstantRefinementDiagnostics
{
    public static ScalarConstantDiagnostics Instance { get; } = new();

    private ScalarConstantDiagnostics() { }

    public Diagnostic NullName(IScalarConstantProcessingContext context, RawScalarConstantDefinition definition)
    {
        return DiagnosticConstruction.NullConstantName(definition.Locations.Name?.AsRoslynLocation());
    }

    public Diagnostic EmptyName(IScalarConstantProcessingContext context, RawScalarConstantDefinition definition) => NullName(context, definition);

    public Diagnostic DuplicateName(IScalarConstantProcessingContext context, RawScalarConstantDefinition definition)
    {
        return DiagnosticConstruction.DuplicateConstantName(definition.Locations.Name?.AsRoslynLocation(), definition.Name!, context.Type.Name);
    }

    public Diagnostic NullUnit(IScalarConstantProcessingContext context, RawScalarConstantDefinition definition)
    {
        return DiagnosticConstruction.NullUnrecognizedUnitName(definition.Locations.Unit?.AsRoslynLocation(), context.Unit.Name);
    }

    public Diagnostic EmptyUnit(IScalarConstantProcessingContext context, RawScalarConstantDefinition definition) => NullUnit(context, definition);

    public Diagnostic NullMultiplesName(IScalarConstantProcessingContext context, RawScalarConstantDefinition definition)
    {
        return DiagnosticConstruction.NullConstantMultiplesName(definition.Locations.MultiplesName?.AsRoslynLocation(), definition.Name!);
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

    public Diagnostic UnrecognizedUnit(IScalarConstantRefinementContext context, ScalarConstantDefinition definition)
    {
        return DiagnosticConstruction.UnrecognizedUnitName(definition.Locations.Unit?.AsRoslynLocation(), definition.Unit, context.Unit.UnitType.Name);
    }

    public Diagnostic ConstantSharesNameWithUnit(IScalarConstantRefinementContext context, ScalarConstantDefinition definition)
    {
        return DiagnosticConstruction.ConstantSharesNameWithUnit(definition.Locations.Name?.AsRoslynLocation(), definition.Name, context.Type.Name);
    }

    public Diagnostic ConstantMultiplesSharesNameWithUnitPlural(IScalarConstantRefinementContext context, ScalarConstantDefinition definition)
    {
        if (definition.Locations.ExplicitlySetMultiplesName)
        {
            return DiagnosticConstruction.ConstantSharesNameWithUnit(definition.Locations.MultiplesName?.AsRoslynLocation(), definition.MultiplesName!,
                context.Type.Name);
        }

        return DiagnosticConstruction.ConstantSharesNameWithUnit(definition.Locations.Attribute.AsRoslynLocation(), definition.MultiplesName!,
            context.Type.Name);
    }
}
