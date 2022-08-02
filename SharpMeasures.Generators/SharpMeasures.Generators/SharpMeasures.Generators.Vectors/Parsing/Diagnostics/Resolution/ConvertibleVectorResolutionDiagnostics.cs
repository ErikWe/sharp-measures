namespace SharpMeasures.Generators.Vectors.Parsing.Diagnostics.Resolution;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Vectors.Parsing.ConvertibleVector;

internal class ConvertibleVectorResolutionDiagnostics : IConvertibleVectorGroupResolutionDiagnostics, IConvertibleIndividualVectorResolutionDiagnostics
{
    public static ConvertibleVectorResolutionDiagnostics Instance { get; } = new();

    private ConvertibleVectorResolutionDiagnostics() { }

    public Diagnostic TypeNotVector(IConvertibleIndividualVectorResolutionContext context, UnresolvedConvertibleVectorDefinition definition, int index)
    {
        return DiagnosticConstruction.TypeNotVector(definition.Locations.QuantitiesElements[index].AsRoslynLocation(), definition.VectorGroups[index].Name);
    }

    public Diagnostic TypeNotVectorGroup(IConvertibleVectorGroupResolutionContext context, UnresolvedConvertibleVectorDefinition definition, int index)
    {
        return DiagnosticConstruction.TypeNotVectorGroup(definition.Locations.QuantitiesElements[index].AsRoslynLocation(), definition.VectorGroups[index].Name);
    }
}
