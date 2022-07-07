namespace SharpMeasures.Generators.Scalars.Diagnostics;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Scalars.Parsing.ScalarConstant;
using SharpMeasures.Generators.Scalars.Refinement.ScalarConstant;

internal class ScalarConstantDiagnostics : IScalarConstantDiagnostics, IScalarConstantRefinementDiagnostics
{
    private NamedType? Unit { get; }

    public ScalarConstantDiagnostics(NamedType unit)
    {
        Unit = unit;
    }

    public ScalarConstantDiagnostics()
    {
        Unit = null;
    }

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
        if (Unit is null)
        {
            return DiagnosticConstruction.NullUnrecognizedUnitNameUnknownType(definition.Locations.Unit?.AsRoslynLocation());
        }

        return DiagnosticConstruction.NullUnrecognizedUnitName(definition.Locations.Unit?.AsRoslynLocation(), Unit.Value.Name);
    }

    public Diagnostic EmptyUnit(IScalarConstantProcessingContext context, RawScalarConstantDefinition definition) => NullUnit(context, definition);

    public Diagnostic NullMultiples(IScalarConstantProcessingContext context, RawScalarConstantDefinition definition)
    {
        return DiagnosticConstruction.NullConstantMultiplesName(definition.Locations.Multiples?.AsRoslynLocation(), definition.Name!);
    }

    public Diagnostic EmptyMultiples(IScalarConstantProcessingContext context, RawScalarConstantDefinition definition) => NullMultiples(context, definition);

    public Diagnostic InvalidMultiples(IScalarConstantProcessingContext context, RawScalarConstantDefinition definition)
    {
        return DiagnosticConstruction.InvalidConstantMultiplesName(definition.Locations.Multiples?.AsRoslynLocation(),
            definition.Name!, definition.ParsingData.InterpretedMultiples!);
    }

    public Diagnostic DuplicateMultiples(IScalarConstantProcessingContext context, RawScalarConstantDefinition definition)
    {
        if (definition.Locations.ExplicitlySetMultiples)
        {
            return DiagnosticConstruction.DuplicateConstantMultiplesName(definition.Locations.Multiples?.AsRoslynLocation(),
                definition.ParsingData.InterpretedMultiples!, context.Type.Name);
        }

        return DiagnosticConstruction.DuplicateConstantMultiplesName(definition.Locations.AttributeName.AsRoslynLocation(),
            definition.ParsingData.InterpretedMultiples!, context.Type.Name);
    }

    public Diagnostic MultiplesDisabledButNameSpecified(IScalarConstantProcessingContext context, RawScalarConstantDefinition definition)
    {
        return DiagnosticConstruction.ConstantMultiplesDisabledButNameSpecified(definition.Locations.Multiples?.AsRoslynLocation(), definition.Name!);
    }

    public Diagnostic UnrecognizedUnit(IScalarConstantRefinementContext context, ScalarConstantDefinition definition)
    {
        return DiagnosticConstruction.UnrecognizedUnitName(definition.Locations.Unit?.AsRoslynLocation(), definition.Unit, context.Unit.Type.Name);
    }

    public Diagnostic ConstantSharesNameWithUnit(IScalarConstantRefinementContext context, ScalarConstantDefinition definition)
    {
        return DiagnosticConstruction.ConstantSharesNameWithUnit(definition.Locations.Name?.AsRoslynLocation(), definition.Name, context.Type.Name);
    }

    public Diagnostic ConstantMultiplesSharesNameWithUnit(IScalarConstantRefinementContext context, ScalarConstantDefinition definition)
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
