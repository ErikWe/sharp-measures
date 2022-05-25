namespace SharpMeasures.Generators.Quantities.Extraction;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Attributes.Parsing.Quantities;
using SharpMeasures.Generators.Diagnostics;

using System.Collections.Generic;

internal class ExcludeUnitsValidator : IValidator<ExcludeUnitsDefinition>
{
    public static ExcludeUnitsValidator Validator { get; } = new();

    private ExcludeUnitsValidator() { }

    public IValidityWithDiagnostics Check(AttributeData attributeData, ExcludeUnitsDefinition definition)
    {
        HashSet<string> units = new();

        for (int i = 0; i < definition.ExcludedUnits.Count; i++)
        {
            if (string.IsNullOrEmpty(definition.ExcludedUnits[i]))
            {
                return ValidityWithDiagnostics.Invalid(CreateInvalidUnitNameDiagnostics(definition, i));
            }

            if (units.Contains(definition.ExcludedUnits[i]))
            {
                return ValidityWithDiagnostics.Invalid(CreateDuplicateEntryDiagnostics(definition, i));
            }

            units.Add(definition.ExcludedUnits[i]);
        }

        return ValidityWithDiagnostics.Valid;
    }

    private static Diagnostic CreateInvalidUnitNameDiagnostics(ExcludeUnitsDefinition definition, int index)
    {
        return DiagnosticConstruction.InvalidUnitName(definition.Locations.ExcludedUnitsComponents[index], definition.ExcludedUnits[index]);
    }

    private static Diagnostic CreateDuplicateEntryDiagnostics(ExcludeUnitsDefinition definition, int index)
    {
        return DiagnosticConstruction.DuplicateListing_Unit(definition.Locations.ExcludedUnitsComponents[index], definition.ExcludedUnits[index]);
    }
}
