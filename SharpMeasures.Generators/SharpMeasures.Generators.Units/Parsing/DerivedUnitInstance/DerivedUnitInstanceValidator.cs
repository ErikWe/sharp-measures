namespace SharpMeasures.Generators.Units.Parsing.DerivedUnitInstance;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;

using System.Collections.Generic;
using System.Linq;

internal interface IDerivedUnitInstanceValidationDiagnostics
{
    public abstract Diagnostic? UnitNotDerivable(IDerivedUnitInstanceValidationContext context, DerivedUnitInstanceDefinition definition);
    public abstract Diagnostic? AmbiguousSignatureNotSpecified(IDerivedUnitInstanceValidationContext context, DerivedUnitInstanceDefinition definition);
    public abstract Diagnostic? UnrecognizedDerivationID(IDerivedUnitInstanceValidationContext context, DerivedUnitInstanceDefinition definition);
    public abstract Diagnostic? InvalidUnitListLength(IDerivedUnitInstanceValidationContext context, DerivedUnitInstanceDefinition definition, int signatureLength);
    public abstract Diagnostic? UnrecognizedUnitInstance(IDerivedUnitInstanceValidationContext context, DerivedUnitInstanceDefinition definition, int index, NamedType unitType);
}

internal interface IDerivedUnitInstanceValidationContext : IValidationContext
{
    public abstract IDerivableUnit? UnnamedDerivation { get; }
    public abstract IReadOnlyDictionary<string, IDerivableUnit> DerivationsByID { get; }
    public abstract IUnitPopulation UnitPopulation { get; }
}

internal sealed class DerivedUnitInstanceValidator : AValidator<IDerivedUnitInstanceValidationContext, DerivedUnitInstanceDefinition>
{
    private IDerivedUnitInstanceValidationDiagnostics Diagnostics { get; }

    public DerivedUnitInstanceValidator(IDerivedUnitInstanceValidationDiagnostics diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override IValidityWithDiagnostics Validate(IDerivedUnitInstanceValidationContext context, DerivedUnitInstanceDefinition definition)
    {
        return ValidateUnitDerivable(context, definition)
            .Validate(() => ValidateNotAmbiguousDerivation(context, definition))
            .Merge(() => ValidateDerivationExists(context, definition))
            .Validate((derivation) => ValidateUnitCountMatchesSignature(context, definition, derivation))
            .Validate((derivation) => ValidateUnitInstancesAreDefined(context, definition, derivation))
            .Reduce();
    }

    private IValidityWithDiagnostics ValidateUnitDerivable(IDerivedUnitInstanceValidationContext context, DerivedUnitInstanceDefinition definition)
    {
        var unitDerivable = context.DerivationsByID.Count > 0 || context.UnnamedDerivation is not null;

        return ValidityWithDiagnostics.Conditional(unitDerivable, () => Diagnostics.UnitNotDerivable(context, definition));
    }

    private IValidityWithDiagnostics ValidateNotAmbiguousDerivation(IDerivedUnitInstanceValidationContext context, DerivedUnitInstanceDefinition definition)
    {
        var ambiguousDerivation = (definition.DerivationID is null || definition.DerivationID.Length is 0) && (context.DerivationsByID.Count is not 1 && context.UnnamedDerivation is null);

        return ValidityWithDiagnostics.Conditional(ambiguousDerivation is false, () => Diagnostics.AmbiguousSignatureNotSpecified(context, definition));
    }

    private IOptionalWithDiagnostics<IDerivableUnit> ValidateDerivationExists(IDerivedUnitInstanceValidationContext context, DerivedUnitInstanceDefinition definition)
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

    private IValidityWithDiagnostics ValidateUnitCountMatchesSignature(IDerivedUnitInstanceValidationContext context, DerivedUnitInstanceDefinition definition, IDerivableUnit derivation)
    {
        return ValidityWithDiagnostics.Conditional(definition.Units.Count == derivation.Signature.Count, () => Diagnostics.InvalidUnitListLength(context, definition, derivation.Signature.Count));
    }

    private IValidityWithDiagnostics ValidateUnitInstancesAreDefined(IDerivedUnitInstanceValidationContext context, DerivedUnitInstanceDefinition definition, IDerivableUnit derivation)
    {
        var validity = ValidityWithDiagnostics.Valid;

        for (int i = 0; i < definition.Units.Count; i++)
        {
            validity = validity.Validate(() => ValidateUnitInstanceIsDefined(context, definition, derivation, i));
        }

        return validity;
    }

    private IValidityWithDiagnostics ValidateUnitInstanceIsDefined(IDerivedUnitInstanceValidationContext context, DerivedUnitInstanceDefinition definition, IDerivableUnit derivation, int index)
    {
        if (context.UnitPopulation.Units.TryGetValue(derivation.Signature[index], out var unit) is false)
        {
            return ValidityWithDiagnostics.InvalidWithoutDiagnostics;
        }

        var unitIsDefined = unit.UnitInstancesByName.ContainsKey(definition.Units[index]);

        return ValidityWithDiagnostics.Conditional(unitIsDefined, () => Diagnostics.UnrecognizedUnitInstance(context, definition, index, unit.Type.AsNamedType()));
    }
}
