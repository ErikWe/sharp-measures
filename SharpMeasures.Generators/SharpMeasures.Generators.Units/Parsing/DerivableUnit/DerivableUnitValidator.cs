namespace SharpMeasures.Generators.Units.Parsing.DerivableUnit;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;

internal interface IDerivableUnitValidationDiagnostics
{
    public abstract Diagnostic? SignatureElementNotUnit(IDerivableUnitValidationContext context, DerivableUnitDefinition definition, int index);
}

internal interface IDerivableUnitValidationContext : IValidationContext
{
    public abstract IUnitPopulation UnitPopulation { get; }
}

internal class DerivableUnitValidator : AValidator<IDerivableUnitValidationContext, DerivableUnitDefinition>
{
    private IDerivableUnitValidationDiagnostics Diagnostics { get; }

    public DerivableUnitValidator(IDerivableUnitValidationDiagnostics diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override IValidityWithDiagnostics Validate(IDerivableUnitValidationContext context, DerivableUnitDefinition definition)
    {
        return ValidateSignatureElements(context, definition);
    }

    private IValidityWithDiagnostics ValidateSignatureElements(IDerivableUnitValidationContext context, DerivableUnitDefinition definition)
    {
        var validity = ValidityWithDiagnostics.Valid;

        for (int i = 0; i < definition.Signature.Count; i++)
        {
            validity = validity.Validate(() => ValidateSignatureElementIsUnit(context, definition, i));
        }

        return validity;
    }

    private IValidityWithDiagnostics ValidateSignatureElementIsUnit(IDerivableUnitValidationContext context, DerivableUnitDefinition definition, int index)
    {
        var signatureElementIsUnit = context.UnitPopulation.Units.ContainsKey(definition.Signature[index]);

        return ValidityWithDiagnostics.Conditional(signatureElementIsUnit, () => Diagnostics.SignatureElementNotUnit(context, definition, index));
    }
}
