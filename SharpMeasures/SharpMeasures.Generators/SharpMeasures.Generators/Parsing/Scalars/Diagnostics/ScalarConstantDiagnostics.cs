namespace SharpMeasures.Generators.Parsing.Scalars.Diagnostics;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.Scalars;
using SharpMeasures.Generators.Diagnostics;

internal class ScalarConstantDiagnostics : IScalarConstantDiagnostics
{
    public static ScalarConstantDiagnostics Instance { get; } = new();

    private ScalarConstantDiagnostics() { }

    public Diagnostic NameNullOrEmpty(IScalarConstantValidatorContext context, ScalarConstantDefinition definition)
    {
        return DiagnosticConstruction.InvalidConstantName(definition.ParsingData.Locations.Name.AsRoslynLocation(), definition.Name);
    }

    public Diagnostic DuplicateName(IScalarConstantValidatorContext context, ScalarConstantDefinition definition)
    {
        return DiagnosticConstruction.DuplicateConstantName(definition.ParsingData.Locations.Name.AsRoslynLocation(), definition.Name, context.Type.Name);
    }

    public Diagnostic UnitNullOrEmpty(IScalarConstantValidatorContext context, ScalarConstantDefinition definition)
    {
        return DiagnosticConstruction.InvalidUnitName(definition.ParsingData.Locations.Unit.AsRoslynLocation(), definition.Unit);
    }

    public Diagnostic InvalidMultiplesName(IScalarConstantValidatorContext context, ScalarConstantDefinition definition)
    {
        return DiagnosticConstruction.InvalidConstantMultiplesName(definition.ParsingData.Locations.MultiplesName.AsRoslynLocation(),
            definition.Name, definition.ParsingData.InterpretedMultiplesName);
    }

    public Diagnostic DuplicateMultiplesName(IScalarConstantValidatorContext context, ScalarConstantDefinition definition)
    {
        return DiagnosticConstruction.DuplicateConstantMultiplesName(definition.ParsingData.Locations.MultiplesName.AsRoslynLocation(),
            definition.ParsingData.InterpretedMultiplesName, context.Type.Name);
    }
}
