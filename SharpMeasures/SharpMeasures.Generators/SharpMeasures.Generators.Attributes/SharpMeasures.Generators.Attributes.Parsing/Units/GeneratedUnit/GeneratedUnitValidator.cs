namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;

using System;

public interface IGeneratedUnitDiagnostics
{
    public abstract Diagnostic? InvalidQuantity(IValidatorContext context, GeneratedUnitDefinition definition);
}

public class GeneratedUnitValidator : AValidator<IValidatorContext, GeneratedUnitDefinition>
{
    private IGeneratedUnitDiagnostics Diagnostics { get; }

    public GeneratedUnitValidator(IGeneratedUnitDiagnostics diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override IValidityWithDiagnostics CheckValidity(IValidatorContext context, GeneratedUnitDefinition definition)
    {
        if (definition is null)
        {
            throw new ArgumentNullException(nameof(definition));
        }

        return CheckQuantityValidity(context, definition);
    }

    private IValidityWithDiagnostics CheckQuantityValidity(IValidatorContext context, GeneratedUnitDefinition definition)
    {
        if (string.IsNullOrEmpty(definition.Quantity.Name))
        {
            return CreateInvalidity(Diagnostics.InvalidQuantity(context, definition));
        }

        return ValidityWithDiagnostics.Valid;
    }
}
