namespace SharpMeasures.Generators.Scalars.Parsing.Diagnostics.Resolution;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Scalars.Parsing.ConvertibleScalar;

internal class ConvertibleScalarResolutionDiagnostics : IConvertibleScalarResolutionDiagnostics
{
    public static ConvertibleScalarResolutionDiagnostics Instance { get; } = new();

    private ConvertibleScalarResolutionDiagnostics() { }

    public Diagnostic TypeNotScalar(IConvertibleScalarResolutionContext context, UnresolvedConvertibleScalarDefinition definition, int index)
    {
        return DiagnosticConstruction.TypeNotScalar(definition.Locations.QuantitiesElements[index].AsRoslynLocation(), definition.Scalars[index].Name);
    }

    public Diagnostic ScalarNotUnbiased(IConvertibleScalarResolutionContext context, UnresolvedConvertibleScalarDefinition definition, int index)
    {
        return DiagnosticConstruction.TypeNotUnbiasedScalar(definition.Locations.QuantitiesElements[index].AsRoslynLocation(), definition.Scalars[index].Name);
    }

    public Diagnostic ScalarNotBiased(IConvertibleScalarResolutionContext context, UnresolvedConvertibleScalarDefinition definition, int index)
    {
        return DiagnosticConstruction.TypeNotBiasedScalar(definition.Locations.QuantitiesElements[index].AsRoslynLocation(), definition.Scalars[index].Name);
    }
}
