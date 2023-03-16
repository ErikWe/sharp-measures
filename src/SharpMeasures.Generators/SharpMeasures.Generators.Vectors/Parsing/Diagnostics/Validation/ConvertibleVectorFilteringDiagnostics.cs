namespace SharpMeasures.Generators.Vectors.Parsing.Diagnostics.Validation;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Vectors.Parsing.ConvertibleVector;

internal sealed class ConvertibleVectorFilteringDiagnostics : IConvertibleVectorFilteringDiagnostics
{
    public static ConvertibleVectorFilteringDiagnostics Instance { get; } = new();

    private ConvertibleVectorFilteringDiagnostics() { }

    public Diagnostic TypeNotVector(IConvertibleVectorFilteringContext context, ConvertibleVectorDefinition definition, int index)
    {
        return DiagnosticConstruction.TypeNotVector(definition.Locations.QuantitiesElements[definition.LocationMap[index]].AsRoslynLocation(), definition.Quantities[index].Name);
    }

    public Diagnostic TypeNotVectorGroup(IConvertibleVectorFilteringContext context, ConvertibleVectorDefinition definition, int index)
    {
        return DiagnosticConstruction.TypeNotVectorGroup(definition.Locations.QuantitiesElements[definition.LocationMap[index]].AsRoslynLocation(), definition.Quantities[index].Name);
    }

    public Diagnostic DuplicateVector(IConvertibleVectorFilteringContext context, ConvertibleVectorDefinition definition, int index)
    {
        return DiagnosticConstruction.DuplicateQuantityListing(definition.Locations.QuantitiesElements[definition.LocationMap[index]].AsRoslynLocation(), definition.Quantities[index].Name);
    }

    public Diagnostic VectorUnexpectedDimension(IConvertibleVectorFilteringContext context, ConvertibleVectorDefinition definition, int index, int dimension)
    {
        return DiagnosticConstruction.VectorUnexpectedDimension(definition.Locations.QuantitiesElements[definition.LocationMap[index]].AsRoslynLocation(), definition.Quantities[index].Name, context.Dimension, dimension);
    }

    public Diagnostic VectorGroupLacksMemberOfMatchingDimension(IConvertibleVectorFilteringContext context, ConvertibleVectorDefinition definition, int index)
    {
        return DiagnosticConstruction.VectorGroupsLacksMemberOfDimension(definition.Locations.QuantitiesElements[definition.LocationMap[index]].AsRoslynLocation(), definition.Quantities[index].Name, context.Dimension);
    }
}
