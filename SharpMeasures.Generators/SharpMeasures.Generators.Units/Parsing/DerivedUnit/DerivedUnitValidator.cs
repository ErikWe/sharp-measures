namespace SharpMeasures.Generators.Units.Parsing.DerivedUnit;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;

using System.Collections.Generic;
using System.Linq;

internal interface IDerivedUnitValidationDiagnostics
{
    public abstract Diagnostic? UnitNotDerivable(IDerivedUnitValidationContext context, DerivedUnitDefinition definition);
    public abstract Diagnostic? AmbiguousSignatureNotSpecified(IDerivedUnitValidationContext context, DerivedUnitDefinition definition);
    public abstract Diagnostic? UnrecognizedDerivationID(IDerivedUnitValidationContext context, DerivedUnitDefinition definition);
    public abstract Diagnostic? InvalidUnitListLength(IDerivedUnitValidationContext context, DerivedUnitDefinition definition, int signatureLength);
    public abstract Diagnostic? UnrecognizedUnit(IDerivedUnitValidationContext context, DerivedUnitDefinition definition, int index, NamedType unitType);
}

internal interface IDerivedUnitValidationContext : IValidationContext
{
    public abstract IDerivableUnit? UnnamedDerivation { get; }
    public abstract IReadOnlyDictionary<string, IDerivableUnit> DerivationsByID { get; }
    public abstract IUnitPopulation UnitPopulation { get; }
}

internal class DerivedUnitValidator : AValidator<IDerivedUnitValidationContext, DerivedUnitDefinition>
{
    private IDerivedUnitValidationDiagnostics Diagnostics { get; }

    public DerivedUnitValidator(IDerivedUnitValidationDiagnostics diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override IValidityWithDiagnostics Validate(IDerivedUnitValidationContext context, DerivedUnitDefinition definition)
    {
        return ValidateUnitDerivable(context, definition)
            .Validate(() => ValidateNotAmbiguousDerivation(context, definition))
            .Merge(() => ValidateDerivationExists(context, definition))
            .Validate((derivation) => ValidateUnitCountMatchesSignature(context, definition, derivation))
            .Validate((derivation) => ValidateUnitsAreDefined(context, definition, derivation))
            .Reduce();
    }

    private IValidityWithDiagnostics ValidateUnitDerivable(IDerivedUnitValidationContext context, DerivedUnitDefinition definition)
    {
        var unitDerivable = context.DerivationsByID.Count > 0 || context.UnnamedDerivation is not null;

        return ValidityWithDiagnostics.Conditional(unitDerivable, () => Diagnostics.UnitNotDerivable(context, definition));
    }

    private IValidityWithDiagnostics ValidateNotAmbiguousDerivation(IDerivedUnitValidationContext context, DerivedUnitDefinition definition)
    {
        var ambiguousDerivation = (definition.DerivationID is null || definition.DerivationID.Length is 0) && (context.DerivationsByID.Count is not 1 && context.UnnamedDerivation is null);

        return ValidityWithDiagnostics.Conditional(ambiguousDerivation is false, () => Diagnostics.AmbiguousSignatureNotSpecified(context, definition));
    }

    private IOptionalWithDiagnostics<IDerivableUnit> ValidateDerivationExists(IDerivedUnitValidationContext context, DerivedUnitDefinition definition)
    {
        if (definition.DerivationID is null || definition.DerivationID.Length is 0)
        {
            if (context.UnnamedDerivation is not null)
            {
                return OptionalWithDiagnostics.Result(context.UnnamedDerivation);
            }

            return OptionalWithDiagnostics.Result(context.DerivationsByID.Values.First());
        }

        var derivationExists = context.DerivationsByID.TryGetValue(definition.DerivationID, out var derivation);

        return OptionalWithDiagnostics.Conditional(derivationExists, derivation, () => Diagnostics.UnrecognizedDerivationID(context, definition));
    }

    private IValidityWithDiagnostics ValidateUnitCountMatchesSignature(IDerivedUnitValidationContext context, DerivedUnitDefinition definition, IDerivableUnit derivation)
    {
        return ValidityWithDiagnostics.Conditional(definition.Units.Count == derivation.Signature.Count, () => Diagnostics.InvalidUnitListLength(context, definition, derivation.Signature.Count));
    }

    private IValidityWithDiagnostics ValidateUnitsAreDefined(IDerivedUnitValidationContext context, DerivedUnitDefinition definition, IDerivableUnit derivation)
    {
        var validity = ValidityWithDiagnostics.Valid;

        for (int i = 0; i < definition.Units.Count; i++)
        {
            validity = validity.Validate(() => ValidateUnitIsDefined(context, definition, derivation, i));
        }

        return validity;
    }

    private IValidityWithDiagnostics ValidateUnitIsDefined(IDerivedUnitValidationContext context, DerivedUnitDefinition definition, IDerivableUnit derivation, int index)
    {
        if (context.UnitPopulation.Units.TryGetValue(derivation.Signature[index], out var unit) is false)
        {
            return ValidityWithDiagnostics.InvalidWithoutDiagnostics;
        }

        var unitIsDefined = unit.UnitsByName.ContainsKey(definition.Units[index]);

        return ValidityWithDiagnostics.Conditional(unitIsDefined, () => Diagnostics.UnrecognizedUnit(context, definition, index, unit.Type.AsNamedType()));
    }
}
