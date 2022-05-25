namespace SharpMeasures.Generators.Units.Parsing.Diagnostics;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.Units;
using SharpMeasures.Generators.Diagnostics;

internal class DerivableUnitDiagnostics : IDerivableUnitDiagnostics
{
    public static DerivableUnitDiagnostics Instance { get; } = new();

    private DerivableUnitDiagnostics() { }

    public Diagnostic MissingExpression(IDerivableUnitValidatorContext context, DerivableUnitDefinition definition)
    {
        return DiagnosticConstruction.InvalidUnitDerivationExpression(definition.ParsingData.Locations.Expression.AsRoslynLocation(), definition.Expression);
    }

    public Diagnostic EmptySignature(IDerivableUnitValidatorContext context, DerivableUnitDefinition definition)
    {
        return DiagnosticConstruction.EmptyUnitDerivationSignature(definition.ParsingData.Locations.SignatureCollection.AsRoslynLocation());
    }

    public Diagnostic SignatureElementNull(IDerivableUnitValidatorContext context, DerivableUnitDefinition definition, int index)
    {
        return DiagnosticConstruction.TypeNotUnit_Null(definition.ParsingData.Locations.SignatureElements[index].AsRoslynLocation());
    }

    public Diagnostic DuplicateSignature(IDerivableUnitValidatorContext context, DerivableUnitDefinition definition)
    {
        return DiagnosticConstruction.DuplicateUnitDerivationSignature(definition.ParsingData.Locations.SignatureCollection.AsRoslynLocation(), context.Type.Name);
    }
}
