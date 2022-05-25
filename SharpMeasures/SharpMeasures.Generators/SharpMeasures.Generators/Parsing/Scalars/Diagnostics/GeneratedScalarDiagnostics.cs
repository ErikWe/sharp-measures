namespace SharpMeasures.Generators.Parsing.Scalars.Diagnostics;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Attributes.Parsing.Scalars;
using SharpMeasures.Generators.Diagnostics;

internal class GeneratedScalarDiagnostics : IGeneratedScalarDiagnostics
{
    public static GeneratedScalarDiagnostics Instance { get; } = new();

    private GeneratedScalarDiagnostics() { }

    public Diagnostic InvalidUnit(IValidatorContext context, GeneratedScalarDefinition definition)
    {
        return DiagnosticConstruction.TypeNotScalar_Null(definition.ParsingData.Locations.Unit.AsRoslynLocation());
    }

    public Diagnostic InvalidVector(IValidatorContext context, GeneratedScalarDefinition definition)
    {
        return DiagnosticConstruction.TypeNotVector_Null(definition.ParsingData.Locations.Vector.AsRoslynLocation());
    }
}
