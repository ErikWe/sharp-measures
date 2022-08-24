namespace SharpMeasures.Generators.Vectors.Parsing.Diagnostics.Validation;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Vectors.Parsing.ConvertibleVector;

internal class ConvertibleVectorFilteringDiagnostics : IConvertibleVectorFilteringDiagnostics
{
    public static ConvertibleVectorFilteringDiagnostics Instance { get; } = new();

    private ConvertibleVectorFilteringDiagnostics() { }

    public Diagnostic TypeNotVector(IConvertibleVectorFilteringContext context, ConvertibleVectorDefinition definition, int index)
    {
        return DiagnosticConstruction.TypeNotVector(definition.Locations.QuantitiesElements[definition.LocationMap[index]].AsRoslynLocation(), definition.Vectors[index].Name);
    }

    public Diagnostic TypeNotVectorGroup(IConvertibleVectorFilteringContext context, ConvertibleVectorDefinition definition, int index)
    {
        return DiagnosticConstruction.TypeNotVectorGroup(definition.Locations.QuantitiesElements[definition.LocationMap[index]].AsRoslynLocation(), definition.Vectors[index].Name);
    }

    public Diagnostic TypeNotVectorGroupMember(IConvertibleVectorFilteringContext context, ConvertibleVectorDefinition definition, int index)
    {
        return DiagnosticConstruction.TypeNotVectorGroupMember(definition.Locations.QuantitiesElements[definition.LocationMap[index]].AsRoslynLocation(), definition.Vectors[index].Name);
    }

    public Diagnostic DuplicateVector(IConvertibleVectorFilteringContext context, ConvertibleVectorDefinition definition, int index)
    {
        return DiagnosticConstruction.DuplicateQuantityListing(definition.Locations.QuantitiesElements[index].AsRoslynLocation(), definition.Vectors[index].Name);
    }
}
