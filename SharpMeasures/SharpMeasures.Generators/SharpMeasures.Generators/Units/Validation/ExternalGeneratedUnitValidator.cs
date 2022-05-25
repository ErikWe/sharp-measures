namespace SharpMeasures.Generators.Units.Validation;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Attributes.Parsing.Units;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Parsing.Scalars;

internal class GeneratedUnitValidatorContext : IValidatorContext
{
    public DefinedType Type { get; }

    public NamedTypePopulation<ScalarInterface> ScalarPopulation { get; }

    public GeneratedUnitValidatorContext(DefinedType type, NamedTypePopulation<ScalarInterface> scalarPopulation)
    {
        Type = type;
        ScalarPopulation = scalarPopulation;
    }
}

internal class ExternalGeneratedUnitValidator : AValidator<GeneratedUnitValidatorContext, GeneratedUnitDefinition>
{
    public static ExternalGeneratedUnitValidator Instance { get; } = new();

    private ExternalGeneratedUnitValidator() { }

    public override IValidityWithDiagnostics CheckValidity(GeneratedUnitValidatorContext context, GeneratedUnitDefinition definition)
    {
        return CheckQuantityValidity(context, definition);
    }

    private static IValidityWithDiagnostics CheckQuantityValidity(GeneratedUnitValidatorContext context, GeneratedUnitDefinition definition)
    {
        if (context.ScalarPopulation.Population.ContainsKey(definition.Quantity) is false)
        {
            return ValidityWithDiagnostics.Invalid(CreateQuantityNotScalarDiagnostics(definition));
        }

        return ValidityWithDiagnostics.Valid;
    }

    private static Diagnostic CreateQuantityNotScalarDiagnostics(GeneratedUnitDefinition definition)
    {
        var location = definition.ParsingData.Locations.Quantity.AsRoslynLocation();

        if (string.IsNullOrEmpty(definition.Quantity.Name))
        {
            return DiagnosticConstruction.TypeNotScalar_Null(location);
        }

        return DiagnosticConstruction.TypeNotScalar(location, definition.Quantity.Name);
    }
}