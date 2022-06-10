namespace SharpMeasures.Generators.Units.Refinement.DerivedUnit;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Units.Parsing.DerivedUnit;

using System.Collections.Generic;

internal interface IDerivedUnitValidatorDiagnostics
{
    public abstract Diagnostic? UnrecognizedUnit(IDerivedUnitValidatorContext context, DerivedUnitDefinition definition, int index);
}

internal interface IDerivedUnitValidatorContext : Attributes.Parsing.IValidationContext
{
    public abstract UnitPopulation UnitPopulation { get; }
}

internal class DerivedUnitValidator : AValidator<IDerivedUnitValidatorContext, DerivedUnitDefinition>
{
    private IDerivedUnitValidatorDiagnostics Diagnostics { get; }

    public DerivedUnitValidator(IDerivedUnitValidatorDiagnostics diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override IValidityWithDiagnostics CheckValidity(IDerivedUnitValidatorContext context, DerivedUnitDefinition definition)
    {
        return CheckSignatureValidity(context, definition);
    }

    private IValidityWithDiagnostics CheckSignatureValidity(IDerivedUnitValidatorContext context, DerivedUnitDefinition definition)
    {
        IEnumerator<NamedType> signatureEnumerator = definition.Signature.GetEnumerator();
        IEnumerator<string> unitEnumerator = definition.Units.GetEnumerator();

        int index = 0;
        while (signatureEnumerator.MoveNext() && unitEnumerator.MoveNext())
        {
            if (context.UnitPopulation.TryGetValue(signatureEnumerator.Current, out UnitInterface unit) is false)
            {
                return ValidityWithDiagnostics.InvalidWithoutDiagnostics;
            }

            if (unit.UnitsByName.ContainsKey(unitEnumerator.Current) is false)
            {
                return ValidityWithDiagnostics.Invalid(Diagnostics.UnrecognizedUnit(context, definition, index));
            }

            index += 1;
        }

        return ValidityWithDiagnostics.Valid;
    }
}
