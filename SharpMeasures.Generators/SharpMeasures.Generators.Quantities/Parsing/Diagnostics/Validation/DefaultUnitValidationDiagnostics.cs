namespace SharpMeasures.Generators.Quantities.Parsing.Diagnostics.Validation;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.DefaultUnit;
using SharpMeasures.Generators.Units;

public class DefaultUnitValidationDiagnostics : IDefaultUnitValidationDiagnostics
{
    public static DefaultUnitValidationDiagnostics Instance { get; } = new();

    private DefaultUnitValidationDiagnostics() { }

    public Diagnostic UnrecognizedDefaultUnit(IProcessingContext context, IDefaultUnitDefinition definition, IUnitType unit)
    {
        return DiagnosticConstruction.UnrecognizedUnitName(definition.DefaultUnitLocations.DefaultUnitName?.AsRoslynLocation(), definition.DefaultUnitName!, unit.Type.Name);
    }
}
