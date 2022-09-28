namespace SharpMeasures.Generators.Scalars.Parsing.Diagnostics.Empty.Validation;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Scalars.Parsing.IncludeUnitBases;

internal sealed class EmptyIncludeUnitBasesFilteringDiagnostics : IIncludeUnitBasesFilteringDiagnostics
{
    public static EmptyIncludeUnitBasesFilteringDiagnostics Instance { get; } = new();

    private EmptyIncludeUnitBasesFilteringDiagnostics() { }

    public Diagnostic? UnionInclusionStackingModeRedundant(IIncludeUnitBasesFilteringContext context, IncludeUnitBasesDefinition definition) => null;
    public Diagnostic? UnrecognizedUnitInstance(IIncludeUnitBasesFilteringContext context, IncludeUnitBasesDefinition definition, int index) => null;
    public Diagnostic? UnitInstanceAlreadyIncluded(IIncludeUnitBasesFilteringContext context, IncludeUnitBasesDefinition definition, int index) => null;
    public Diagnostic? UnitInstanceExcluded(IIncludeUnitBasesFilteringContext context, IncludeUnitBasesDefinition definition, int index) => null;
}
