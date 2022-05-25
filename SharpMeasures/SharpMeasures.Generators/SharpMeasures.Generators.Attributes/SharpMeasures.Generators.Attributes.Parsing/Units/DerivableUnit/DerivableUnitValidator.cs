namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;

using System;
using System.Collections.Generic;

public interface IDerivableUnitDiagnostics
{
    public abstract Diagnostic? MissingExpression(IDerivableUnitValidatorContext context, DerivableUnitDefinition definition);
    public abstract Diagnostic? EmptySignature(IDerivableUnitValidatorContext context, DerivableUnitDefinition definition);
    public abstract Diagnostic? SignatureElementNull(IDerivableUnitValidatorContext context, DerivableUnitDefinition definition, int index);
    public abstract Diagnostic? DuplicateSignature(IDerivableUnitValidatorContext context, DerivableUnitDefinition definition);
}

public interface IDerivableUnitValidatorContext : IValidatorContext
{
    public abstract HashSet<DerivableSignature> ReservedSignatures { get; }
}

public class DerivableUnitValidator : AActionableValidator<IDerivableUnitValidatorContext, DerivableUnitDefinition>
{
    private IDerivableUnitDiagnostics Diagnostics { get; }

    public DerivableUnitValidator(IDerivableUnitDiagnostics diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override void OnValidated(IDerivableUnitValidatorContext context, DerivableUnitDefinition definition)
    {
        if (context is null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        
        if (definition is null)
        {
            throw new ArgumentNullException(nameof(definition));
        }

        context.ReservedSignatures.Add(new DerivableSignature(definition.Signature));
    }

    public override IValidityWithDiagnostics CheckValidity(IDerivableUnitValidatorContext context, DerivableUnitDefinition definition)
    {
        if (definition is null)
        {
            throw new ArgumentNullException(nameof(definition));
        }

        return ValidityWithDiagnostics.DiagnoseAndMergeWhileValid(context, definition, CheckExpressionValidity, CheckSignatureValidity);
    }

    private IValidityWithDiagnostics CheckExpressionValidity(IDerivableUnitValidatorContext context, DerivableUnitDefinition definition)
    {
        if (string.IsNullOrEmpty(definition.Expression))
        {
            return CreateInvalidity(Diagnostics.MissingExpression(context, definition));
        }

        return ValidityWithDiagnostics.Valid;
    }

    private IValidityWithDiagnostics CheckSignatureValidity(IDerivableUnitValidatorContext context, DerivableUnitDefinition definition)
    {
        if (definition.Signature.Count is 0)
        {
            return CreateInvalidity(Diagnostics.EmptySignature(context, definition));
        }

        for (int i = 0; i < definition.Signature.Count; i++)
        {
            if (string.IsNullOrEmpty(definition.Signature[i].Name))
            {
                return CreateInvalidity(Diagnostics.SignatureElementNull(context, definition, i));
            }
        }

        if (context.ReservedSignatures.Contains(new DerivableSignature(definition.Signature)))
        {
            return CreateInvalidity(Diagnostics.DuplicateSignature(context, definition));
        }

        return ValidityWithDiagnostics.Valid;
    }
}
