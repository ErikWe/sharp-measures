namespace SharpMeasures.Generators.Units.Validation;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Attributes.Parsing.Units;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Units.Parsing;

internal class DerivableUnitValidatorContext : IValidatorContext
{
    public DefinedType Type { get; }

    public NamedTypePopulation<UnitInterface> UnitPopulation { get; }

    public DerivableUnitValidatorContext(DefinedType type, NamedTypePopulation<UnitInterface> unitPopulation)
    {
        Type = type;
        UnitPopulation = unitPopulation;
    }
}

internal class ExternalDerivableUnitValidator : AValidator<DerivableUnitValidatorContext, DerivableUnitDefinition>
{
    public static ExternalDerivableUnitValidator Instance { get; } = new();

    private ExternalDerivableUnitValidator() { }

    public override IValidityWithDiagnostics CheckValidity(DerivableUnitValidatorContext context, DerivableUnitDefinition definition)
    {
        return CheckSignatureValidity(context, definition);
    }

    private static IValidityWithDiagnostics CheckSignatureValidity(DerivableUnitValidatorContext context, DerivableUnitDefinition definition)
    {
        for (int i = 0; i < definition.Signature.Count; i++)
        {
            if (context.UnitPopulation.Population.ContainsKey(definition.Signature[i]) is false)
            {
                return ValidityWithDiagnostics.Invalid(CreateTypeNotUnitDiagnostics(definition, i));
            }
        }

        return ValidityWithDiagnostics.Valid;
    }

    private static Diagnostic CreateTypeNotUnitDiagnostics(DerivableUnitDefinition definition, int index)
    {
        if (string.IsNullOrEmpty(definition.Signature[index].Name))
        {
            return DiagnosticConstruction.TypeNotUnit_Null(definition.ParsingData.Locations.SignatureElements[index].AsRoslynLocation());
        }

        return DiagnosticConstruction.TypeNotUnit(definition.ParsingData.Locations.SignatureElements[index].AsRoslynLocation(), definition.Signature[index].Name);
    }
}
