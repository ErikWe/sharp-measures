namespace SharpMeasures.Generators.Quantities.Parsing.Diagnostics.Empty.Validation;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;

public sealed class EmptyIncludeUnitsFilteringDiagnostics : IIncludeUnitsFilteringDiagnostics
{
    public static EmptyIncludeUnitsFilteringDiagnostics Instance { get; } = new();

    private EmptyIncludeUnitsFilteringDiagnostics() { }

    public Diagnostic? UnionInclusionStackingModeRedundant(IIncludeUnitsFilteringContext context, IncludeUnitsDefinition definition) => null;
    public Diagnostic? UnitInstanceAlreadyIncluded(IIncludeUnitsFilteringContext context, IncludeUnitsDefinition definition, int index) => null;
    public Diagnostic? UnitInstanceExcluded(IIncludeUnitsFilteringContext context, IncludeUnitsDefinition definition, int index) => null;
    public Diagnostic? UnrecognizedUnitInstance(IIncludeUnitsFilteringContext context, IncludeUnitsDefinition definition, int index) => null;
}
