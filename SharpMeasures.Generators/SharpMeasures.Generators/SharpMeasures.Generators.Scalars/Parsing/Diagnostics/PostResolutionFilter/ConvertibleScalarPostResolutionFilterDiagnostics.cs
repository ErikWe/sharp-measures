namespace SharpMeasures.Generators.Scalars.Parsing.Diagnostics.PostResolutionFilter;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Scalars.Parsing.ConvertibleScalar;

internal class ConvertibleScalarPostResolutionFilterDiagnostics : IConvertibleScalarPostResolutionFilterDiagnostics
{
    public static ConvertibleScalarPostResolutionFilterDiagnostics Instance { get; } = new();

    private ConvertibleScalarPostResolutionFilterDiagnostics() { }

    public Diagnostic DuplicateScalar(IConvertibleScalarPostResolutionFilterContext context, ConvertibleScalarDefinition definition, int index)
    {
        return DiagnosticConstruction.DuplicateQuantityListing(definition.Locations.QuantitiesElements[index].AsRoslynLocation(), definition.Scalars[index].Type.Name);
    }
}
