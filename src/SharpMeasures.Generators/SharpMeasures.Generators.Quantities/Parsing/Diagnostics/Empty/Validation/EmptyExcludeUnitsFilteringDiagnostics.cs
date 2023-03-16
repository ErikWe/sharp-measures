namespace SharpMeasures.Generators.Quantities.Parsing.Diagnostics.Empty.Validation;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;

public sealed class EmptyExcludeUnitsFilteringDiagnostics : IExcludeUnitsFilteringDiagnostics
{
    public static EmptyExcludeUnitsFilteringDiagnostics Instance { get; } = new();

    private EmptyExcludeUnitsFilteringDiagnostics() { }

    public Diagnostic? UnrecognizedUnitInstance(IExcludeUnitsFilteringContext context, ExcludeUnitsDefinition definition, int index) => null;
    public Diagnostic? UnitInstanceAlreadyExcluded(IExcludeUnitsFilteringContext context, ExcludeUnitsDefinition definition, int index) => null;
}
