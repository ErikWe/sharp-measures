namespace SharpMeasures.Generators.Vectors.Parsing.Diagnostics.Resolution;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Vectors.Parsing.ConvertibleVector;

internal class ConvertibleVectorResolutionDiagnostics : IConvertibleVectorResolutionDiagnostics
{
    public static ConvertibleVectorResolutionDiagnostics Instance { get; } = new();

    private ConvertibleVectorResolutionDiagnostics() { }

    public Diagnostic TypeNotVector(IConvertibleVectorResolutionContext context, UnresolvedConvertibleVectorDefinition definition, int index)
    {
        return DiagnosticConstruction.TypeNotVector(definition.Locations.QuantitiesElements[index].AsRoslynLocation(), definition.VectorGroups[index].Name);
    }
}
