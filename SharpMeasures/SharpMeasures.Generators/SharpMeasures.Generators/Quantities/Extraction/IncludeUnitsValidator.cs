namespace SharpMeasures.Generators.Quantities.Extraction;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.Extraction;
using SharpMeasures.Generators.Attributes.Parsing.Quantities;
using SharpMeasures.Generators.Diagnostics;

using System.Collections.Generic;

internal class IncludeUnitsValidator : IValidator<IncludeUnitsDefinition>
{
    public static IncludeUnitsValidator Validator { get; } = new();

    private IncludeUnitsValidator() { }

    public ExtractionValidity Check(AttributeData attributeData, IncludeUnitsDefinition definition)
    {
        HashSet<string> units = new();

        for (int i = 0; i < definition.IncludedUnits.Count; i++)
        {
            if (string.IsNullOrEmpty(definition.IncludedUnits[i]))
            {
                Diagnostic diagnostics = CreateInvalidUnitNameDiagnostics(definition, i);
                return ExtractionValidity.Invalid(diagnostics);
            }

            if (units.Contains(definition.IncludedUnits[i]))
            {
                Diagnostic diagnostics = CreateDuplicateEntryDiagnostics(definition, i);
                return ExtractionValidity.Invalid(diagnostics);
            }

            units.Add(definition.IncludedUnits[i]);
        }

        return ExtractionValidity.Valid;
    }

    private static Diagnostic CreateInvalidUnitNameDiagnostics(IncludeUnitsDefinition definition, int index)
    {
        return DiagnosticConstruction.InvalidUnitName(definition.Locations.IncludedUnitsComponents[index], definition.IncludedUnits[index]);
    }

    private static Diagnostic CreateDuplicateEntryDiagnostics(IncludeUnitsDefinition definition, int index)
    {
        return DiagnosticConstruction.DuplicateListing_Unit(definition.Locations.IncludedUnitsComponents[index], definition.IncludedUnits[index]);
    }
}
