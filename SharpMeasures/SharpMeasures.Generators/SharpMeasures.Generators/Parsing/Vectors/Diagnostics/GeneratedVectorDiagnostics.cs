namespace SharpMeasures.Generators.Parsing.Vectors.Diagnostics;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Attributes.Parsing.Vectors;
using SharpMeasures.Generators.Diagnostics;

internal class GeneratedVectorDiagnostics : IGeneratedVectorDiagnostics
{
    public static GeneratedVectorDiagnostics Instance { get; } = new();

    private GeneratedVectorDiagnostics() { }

    public Diagnostic InvalidUnit(IValidatorContext context, GeneratedVectorDefinition definition)
    {
        return DiagnosticConstruction.TypeNotScalar_Null(definition.ParsingData.Locations.Unit.AsRoslynLocation());
    }

    public Diagnostic InvalidScalar(IValidatorContext context, GeneratedVectorDefinition definition)
    {
        return DiagnosticConstruction.TypeNotScalar_Null(definition.ParsingData.Locations.Scalar.AsRoslynLocation());
    }
}
