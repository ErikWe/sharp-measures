namespace SharpMeasures.Generators.Units.Parsing.Diagnostics.Empty.Processing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Units.Parsing.Abstractions;
using SharpMeasures.Generators.Units.Parsing.DerivedUnitInstance;

internal sealed class EmptyDerivedUnitInstanceProcessingDiagnostics : AEmptyUnitInstanceProcessingDiagnostics<RawDerivedUnitInstanceDefinition, DerivedUnitInstanceLocations>, IDerivedUnitInstanceProcessingDiagnostics
{
    public static EmptyDerivedUnitInstanceProcessingDiagnostics Instance { get; } = new();

    private EmptyDerivedUnitInstanceProcessingDiagnostics() { }

    public Diagnostic? EmptyUnitList(IUnitInstanceProcessingContext context, RawDerivedUnitInstanceDefinition definition) => null;
    public Diagnostic? EmptyUnitsElement(IUnitInstanceProcessingContext context, RawDerivedUnitInstanceDefinition definition, int index) => null;
    public Diagnostic? NullUnitsElement(IUnitInstanceProcessingContext context, RawDerivedUnitInstanceDefinition definition, int index) => null;
}
