namespace SharpMeasures.Generators.Scalars.Parsing.Diagnostics.Empty.Validation;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;

internal sealed class EmptyIncludeUnitsFilteringDiagnostics : IIncludeUnitsFilteringDiagnostics
{
    public static EmptyIncludeUnitsFilteringDiagnostics Instance { get; } = new();

    private EmptyIncludeUnitsFilteringDiagnostics() { }

    Diagnostic? IIncludeUnitsFilteringDiagnostics.UnionInclusionStackingModeRedundant(IIncludeUnitsFilteringContext context, IncludeUnitsDefinition definition) => null;
    Diagnostic? IIncludeUnitsFilteringDiagnostics.UnitInstanceAlreadyIncluded(IIncludeUnitsFilteringContext context, IncludeUnitsDefinition definition, int index) => null;
    Diagnostic? IIncludeUnitsFilteringDiagnostics.UnitInstanceExcluded(IIncludeUnitsFilteringContext context, IncludeUnitsDefinition definition, int index) => null;
    Diagnostic? IIncludeUnitsFilteringDiagnostics.UnrecognizedUnitInstance(IIncludeUnitsFilteringContext context, IncludeUnitsDefinition definition, int index) => null;
}
