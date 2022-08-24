namespace SharpMeasures.Generators.Units.Parsing.DerivedUnit;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;

using System.Collections.Generic;

internal interface IDerivedUnitValidationDiagnostics
{
    public abstract Diagnostic? UnitNotDerivable(IDerivedUnitValidationContext context, DerivedUnitDefinition definition);
    public abstract Diagnostic? AmbiguousSignatureNotSpecified(IDerivedUnitValidationContext context, DerivedUnitDefinition definition);
    public abstract Diagnostic? UnrecognizedDerivationID(IDerivedUnitValidationContext context, DerivedUnitDefinition definition);
    public abstract Diagnostic? InvalidUnitListLength(IDerivedUnitValidationContext context, DerivedUnitDefinition definition, IReadOnlyList<string> units);
    public abstract Diagnostic? UnrecognizedUnit(IDerivedUnitValidationContext context, DerivedUnitDefinition definition, int index, IUnitType unitType);
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
            .Validate(() => ValidateNotAmbiguousDerivation(context, definition));
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
}
