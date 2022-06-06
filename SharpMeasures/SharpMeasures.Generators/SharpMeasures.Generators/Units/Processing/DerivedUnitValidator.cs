namespace SharpMeasures.Generators.Units.Processing;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Attributes.Parsing.Units;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Units.Processing.Diagnostics;

using System.Collections.Generic;

internal class DerivedUnitValidatorContext : IValidatorContext
{
    public DefinedType Type { get; }

    public UnitPopulation UnitPopulation { get; }

    public DerivedUnitValidatorContext(DefinedType type, UnitPopulation unitPopulation)
    {
        Type = type;
        UnitPopulation = unitPopulation;
    }
}

internal class DerivedUnitValidator : AValidator<DerivedUnitValidatorContext, DerivedUnit>
{
    public static DerivedUnitValidator Instance { get; } = new();

    private DerivedUnitValidator() { }

    public override IValidityWithDiagnostics CheckValidity(DerivedUnitValidatorContext context, DerivedUnit definition)
    {
        return CheckSignatureValidity(context, definition);
    }

    private static IValidityWithDiagnostics CheckSignatureValidity(DerivedUnitValidatorContext context, DerivedUnit definition)
    {
        IEnumerator<NamedType> signatureEnumerator = definition.Signature.Types.GetEnumerator();
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
                return ValidityWithDiagnostics.Invalid(DerivedUnitDiagnostics.UnrecognizedUnit(definition, index, unit.UnitType.AsNamedType()));
            }

            index += 1;
        }

        return ValidityWithDiagnostics.Valid;
    }
}
