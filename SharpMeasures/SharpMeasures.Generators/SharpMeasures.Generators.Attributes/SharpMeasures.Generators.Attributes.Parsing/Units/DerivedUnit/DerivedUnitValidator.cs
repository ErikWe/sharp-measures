namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;

using System;
using System.Collections.Generic;

public interface IDerivedUnitDiagnostics : IUnitDiagnostics<DerivedUnitDefinition>
{
    public abstract Diagnostic? EmptySignature(IDerivedUnitValidatorContext context, DerivedUnitDefinition definition);
    public abstract Diagnostic? IncompatibleSignatureAndUnitLists(IDerivedUnitValidatorContext context, DerivedUnitDefinition definition);
    public abstract Diagnostic? UnrecognizedSignature(IDerivedUnitValidatorContext context, DerivedUnitDefinition definition);
    public abstract Diagnostic? SignatureElementNull(IDerivedUnitValidatorContext context, DerivedUnitDefinition definition, int index);
    public abstract Diagnostic? UnitElementNullOrEmpty(IDerivedUnitValidatorContext context, DerivedUnitDefinition definition, int index);
}

public interface IDerivedUnitValidatorContext : IUnitValidatorContext
{
    public abstract HashSet<DerivableSignature> AvailableSignatures { get; }
}

public class DerivedUnitValidator : AUnitValidator<IDerivedUnitValidatorContext, DerivedUnitDefinition>
{
    private IDerivedUnitDiagnostics Diagnostics { get; }

    public DerivedUnitValidator(IDerivedUnitDiagnostics diagnostics) : base(diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override IValidityWithDiagnostics CheckValidity(IDerivedUnitValidatorContext context, DerivedUnitDefinition definition)
    {
        if (definition is null)
        {
            throw new ArgumentNullException(nameof(definition));
        }

        return ValidityWithDiagnostics.DiagnoseAndMergeWhileValid(context, definition, base.CheckValidity, CheckSignatureValidity);
    }

    private IValidityWithDiagnostics CheckSignatureValidity(IDerivedUnitValidatorContext context, DerivedUnitDefinition definition)
    {
        if (definition.Signature.Count is 0)
        {
            return CreateInvalidity(Diagnostics.EmptySignature(context, definition));
        }

        if (definition.Signature.Count != definition.Units.Count)
        {
            return CreateInvalidity(Diagnostics.IncompatibleSignatureAndUnitLists(context, definition));
        }

        for (int i = 0; i < definition.Signature.Count; i++)
        {
            if (string.IsNullOrEmpty(definition.Signature[i].Name))
            {
                return CreateInvalidity(Diagnostics.SignatureElementNull(context, definition, i));
            }
        }

        for (int i = 0; i < definition.Units.Count; i++)
        {
            if (string.IsNullOrEmpty(definition.Units[i]))
            {
                return CreateInvalidity(Diagnostics.UnitElementNullOrEmpty(context, definition, i));
            }
        }

        if (context.AvailableSignatures.Contains(new DerivableSignature(definition.Signature)) is false)
        {
            return CreateInvalidity(Diagnostics.UnrecognizedSignature(context, definition));
        }

        return ValidityWithDiagnostics.Valid;
    }
}
