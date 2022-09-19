namespace SharpMeasures.Generators.Scalars.ForeignScalarParsing.Diagnostics.Validation;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;

internal sealed class EmptyExcludeUnitsFilteringDiagnostics : IExcludeUnitsFilteringDiagnostics
{
    public static EmptyExcludeUnitsFilteringDiagnostics Instance { get; } = new();

    private EmptyExcludeUnitsFilteringDiagnostics() { }

    Diagnostic? IExcludeUnitsFilteringDiagnostics.UnrecognizedUnitInstance(IExcludeUnitsFilteringContext context, ExcludeUnitsDefinition definition, int index) => null;
    Diagnostic? IExcludeUnitsFilteringDiagnostics.UnitInstanceAlreadyExcluded(IExcludeUnitsFilteringContext context, ExcludeUnitsDefinition definition, int index) => null;
}
