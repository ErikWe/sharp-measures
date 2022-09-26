namespace SharpMeasures.Generators.Scalars.Parsing.Diagnostics.Empty.Validation;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Scalars.Parsing.ExcludeUnitBases;

internal sealed class EmptyExcludeUnitBasesFilteringDiagnostics : IExcludeUnitBasesFilteringDiagnostics
{
    public static EmptyExcludeUnitBasesFilteringDiagnostics Instance { get; } = new();

    private EmptyExcludeUnitBasesFilteringDiagnostics() { }

    Diagnostic? IExcludeUnitBasesFilteringDiagnostics.UnrecognizedUnitInstance(IExcludeUnitBasesFilteringContext context, ExcludeUnitBasesDefinition definition, int index) => null;
    Diagnostic? IExcludeUnitBasesFilteringDiagnostics.UnitInstanceAlreadyExcluded(IExcludeUnitBasesFilteringContext context, ExcludeUnitBasesDefinition definition, int index) => null;
}
