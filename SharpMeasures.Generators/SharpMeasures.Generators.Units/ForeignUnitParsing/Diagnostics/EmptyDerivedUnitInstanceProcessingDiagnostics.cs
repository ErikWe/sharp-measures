namespace SharpMeasures.Generators.Units.ForeignUnitParsing.Diagnostics;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Units.Parsing.Abstractions;
using SharpMeasures.Generators.Units.Parsing.DerivedUnitInstance;

internal class EmptyDerivedUnitInstanceProcessingDiagnostics : EmptyUnitInstanceProcessingDiagnostics<RawDerivedUnitInstanceDefinition, DerivedUnitInstanceLocations>, IDerivedUnitInstanceProcessingDiagnostics
{
    new public static EmptyDerivedUnitInstanceProcessingDiagnostics Instance { get; } = new();

    Diagnostic? IDerivedUnitInstanceProcessingDiagnostics.EmptyUnitList(IUnitInstanceProcessingContext context, RawDerivedUnitInstanceDefinition definition) => null;
    Diagnostic? IDerivedUnitInstanceProcessingDiagnostics.EmptyUnitsElement(IUnitInstanceProcessingContext context, RawDerivedUnitInstanceDefinition definition, int index) => null;
    Diagnostic? IDerivedUnitInstanceProcessingDiagnostics.NullUnitsElement(IUnitInstanceProcessingContext context, RawDerivedUnitInstanceDefinition definition, int index) => null;
}
