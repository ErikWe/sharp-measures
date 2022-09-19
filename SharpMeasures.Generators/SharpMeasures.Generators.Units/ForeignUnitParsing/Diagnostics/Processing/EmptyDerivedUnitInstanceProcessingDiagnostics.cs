namespace SharpMeasures.Generators.Units.ForeignUnitParsing.Diagnostics.Processing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Units.Parsing.Abstractions;
using SharpMeasures.Generators.Units.Parsing.DerivedUnitInstance;

internal sealed class EmptyDerivedUnitInstanceProcessingDiagnostics : AEmptyUnitInstanceProcessingDiagnostics<RawDerivedUnitInstanceDefinition, DerivedUnitInstanceLocations>, IDerivedUnitInstanceProcessingDiagnostics
{
    public static EmptyDerivedUnitInstanceProcessingDiagnostics Instance { get; } = new();

    private EmptyDerivedUnitInstanceProcessingDiagnostics() { }

    Diagnostic? IDerivedUnitInstanceProcessingDiagnostics.EmptyUnitList(IUnitInstanceProcessingContext context, RawDerivedUnitInstanceDefinition definition) => null;
    Diagnostic? IDerivedUnitInstanceProcessingDiagnostics.EmptyUnitsElement(IUnitInstanceProcessingContext context, RawDerivedUnitInstanceDefinition definition, int index) => null;
    Diagnostic? IDerivedUnitInstanceProcessingDiagnostics.NullUnitsElement(IUnitInstanceProcessingContext context, RawDerivedUnitInstanceDefinition definition, int index) => null;
}
