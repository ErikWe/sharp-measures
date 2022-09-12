namespace SharpMeasures.Generators.Quantities.Parsing.Diagnostics.Validation;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.DefaultUnitInstance;
using SharpMeasures.Generators.Units;

public class DefaultUnitInstanceValidationDiagnostics : IDefaultUnitInstanceValidationDiagnostics
{
    public static DefaultUnitInstanceValidationDiagnostics Instance { get; } = new();

    private DefaultUnitInstanceValidationDiagnostics() { }

    public Diagnostic UnrecognizedDefaultUnitInstance(IProcessingContext context, IDefaultUnitInstanceDefinition definition, IUnitType unit)
    {
        return DiagnosticConstruction.UnrecognizedUnitInstanceName(definition.DefaultUnitInstanceLocations.DefaultUnitInstanceName?.AsRoslynLocation(), definition.DefaultUnitInstanceName!, unit.Type.Name);
    }
}
