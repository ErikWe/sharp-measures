namespace SharpMeasures.Generators.Scalars.Parsing.Diagnostics.Empty.Validation;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Scalars.Parsing.IncludeUnitBases;

internal sealed class EmptyIncludeUnitBasesFilteringDiagnostics : IIncludeUnitBasesFilteringDiagnostics
{
    public static EmptyIncludeUnitBasesFilteringDiagnostics Instance { get; } = new();

    private EmptyIncludeUnitBasesFilteringDiagnostics() { }

    Diagnostic? IIncludeUnitBasesFilteringDiagnostics.UnionInclusionStackingModeRedundant(IIncludeUnitBasesFilteringContext context, IncludeUnitBasesDefinition definition) => null;
    Diagnostic? IIncludeUnitBasesFilteringDiagnostics.UnrecognizedUnitInstance(IIncludeUnitBasesFilteringContext context, IncludeUnitBasesDefinition definition, int index) => null;
    Diagnostic? IIncludeUnitBasesFilteringDiagnostics.UnitInstanceAlreadyIncluded(IIncludeUnitBasesFilteringContext context, IncludeUnitBasesDefinition definition, int index) => null;
    Diagnostic? IIncludeUnitBasesFilteringDiagnostics.UnitInstanceExcluded(IIncludeUnitBasesFilteringContext context, IncludeUnitBasesDefinition definition, int index) => null;
}
