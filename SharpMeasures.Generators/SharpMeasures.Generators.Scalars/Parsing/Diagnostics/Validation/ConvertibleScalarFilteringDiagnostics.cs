namespace SharpMeasures.Generators.Scalars.Parsing.Diagnostics.Validation;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Scalars.Parsing.ConvertibleScalar;

internal class ConvertibleScalarFilteringDiagnostics : IConvertibleScalarFilteringDiagnostics
{
    public static ConvertibleScalarFilteringDiagnostics Instance { get; } = new();

    private ConvertibleScalarFilteringDiagnostics() { }

    public Diagnostic TypeNotScalar(IConvertibleScalarFilteringContext context, ConvertibleScalarDefinition definition, int index)
    {
        return DiagnosticConstruction.TypeNotScalar(definition.Locations.QuantitiesElements[definition.LocationMap[index]].AsRoslynLocation(), definition.Quantities[index].Name);
    }

    public Diagnostic ScalarNotUnbiased(IConvertibleScalarFilteringContext context, ConvertibleScalarDefinition definition, int index)
    {
        return DiagnosticConstruction.TypeNotUnbiasedScalar(definition.Locations.QuantitiesElements[definition.LocationMap[index]].AsRoslynLocation(), definition.Quantities[index].Name);
    }

    public Diagnostic ScalarNotBiased(IConvertibleScalarFilteringContext context, ConvertibleScalarDefinition definition, int index)
    {
        return DiagnosticConstruction.TypeNotBiasedScalar(definition.Locations.QuantitiesElements[definition.LocationMap[index]].AsRoslynLocation(), definition.Quantities[index].Name);
    }

    public Diagnostic DuplicateScalar(IConvertibleScalarFilteringContext context, ConvertibleScalarDefinition definition, int index)
    {
        return DiagnosticConstruction.DuplicateQuantityListing(definition.Locations.QuantitiesElements[index].AsRoslynLocation(), definition.Quantities[index].Name);
    }
}
