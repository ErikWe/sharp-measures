namespace SharpMeasures.Generators.Vectors.Parsing.Diagnostics.PostResolutionFilter;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Vectors.Parsing.ConvertibleVector;

internal class ConvertibleVectorPostResolutionFilterDiagnostics : IConvertibleVectorPostResolutionFilterDiagnostics
{
    public static ConvertibleVectorPostResolutionFilterDiagnostics Instance { get; } = new();

    private ConvertibleVectorPostResolutionFilterDiagnostics() { }

    public Diagnostic DuplicateVector(IConvertibleVectorPostResolutionFilterContext context, ConvertibleVectorDefinition definition, int index)
    {
        return DiagnosticConstruction.DuplicateQuantityListing(definition.Locations.QuantitiesElements[index].AsRoslynLocation(), definition.VectorGroups[index].Type.Name);
    }
}
