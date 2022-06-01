namespace SharpMeasures.Generators.Units.Validation;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Attributes.Parsing.Units;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Units.Validation.Diagnostics;

using System.Collections.Generic;

internal class DerivedUnitValidatorContext : IValidatorContext
{
    public DefinedType Type { get; }

    public NamedTypePopulation<UnitInterface> UnitPopulation { get; }

    public DerivedUnitValidatorContext(DefinedType type, NamedTypePopulation<UnitInterface> unitPopulation)
    {
        Type = type;
        UnitPopulation = unitPopulation;
    }
}

internal class DerivedUnitValidator : AValidator<DerivedUnitValidatorContext, DerivedUnitDefinition>
{
    public static DerivedUnitValidator Instance { get; } = new();

    private DerivedUnitValidator() { }

    public override IValidityWithDiagnostics CheckValidity(DerivedUnitValidatorContext context, DerivedUnitDefinition definition)
    {
        return CheckSignatureValidity(context, definition);
    }

    private static IValidityWithDiagnostics CheckSignatureValidity(DerivedUnitValidatorContext context, DerivedUnitDefinition definition)
    {
        IEnumerator<NamedType> signatureEnumerator = definition.Signature.Types.GetEnumerator();
        IEnumerator<string> unitEnumerator = definition.Units.GetEnumerator();

        int index = 0;
        while (signatureEnumerator.MoveNext() && unitEnumerator.MoveNext())
        {
            if (context.UnitPopulation.Population.TryGetValue(signatureEnumerator.Current, out UnitInterface unit) is false)
            {
                return ValidityWithDiagnostics.InvalidWithoutDiagnostics;
            }

            if (unit.UnitsByName.ContainsKey(unitEnumerator.Current) is false)
            {
                return ValidityWithDiagnostics.Invalid(DerivedUnitDiagnostics.UnrecognizedUnit(definition, index, unit.UnitType.AsNamedType()));
            }

            index += 1;
        }

        return ValidityWithDiagnostics.Valid;
    }
}
