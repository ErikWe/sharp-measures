namespace SharpMeasures.Generators.Units.ForeignUnitParsing.Diagnostics;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Units.Parsing.Abstractions;

internal class EmptyUnitInstanceProcessingDiagnostics<TDefinition, TLocations> : IUnitInstanceProcessingDiagnostics<TDefinition, TLocations>
    where TDefinition : IRawUnitInstance<TLocations>
    where TLocations : IUnitInstanceLocations
{
    public static EmptyUnitInstanceProcessingDiagnostics<TDefinition, TLocations> Instance { get; } = new();

    Diagnostic? IUnitInstanceProcessingDiagnostics<TDefinition, TLocations>.DuplicateUnitInstanceName(IUnitInstanceProcessingContext context, TDefinition definition) => null;
    Diagnostic? IUnitInstanceProcessingDiagnostics<TDefinition, TLocations>.DuplicateUnitInstancePluralForm(IUnitInstanceProcessingContext context, TDefinition definition, string interpretedPluralForm) => null;
    Diagnostic? IUnitInstanceProcessingDiagnostics<TDefinition, TLocations>.EmptyUnitInstanceName(IUnitInstanceProcessingContext context, TDefinition definition) => null;
    Diagnostic? IUnitInstanceProcessingDiagnostics<TDefinition, TLocations>.EmptyUnitInstancePluralForm(IUnitInstanceProcessingContext context, TDefinition definition) => null;
    Diagnostic? IUnitInstanceProcessingDiagnostics<TDefinition, TLocations>.InvalidUnitInstancePluralForm(IUnitInstanceProcessingContext context, TDefinition definition) => null;
    Diagnostic? IUnitInstanceProcessingDiagnostics<TDefinition, TLocations>.NullUnitInstanceName(IUnitInstanceProcessingContext context, TDefinition definition) => null;
    Diagnostic? IUnitInstanceProcessingDiagnostics<TDefinition, TLocations>.NullUnitInstancePluralForm(IUnitInstanceProcessingContext context, TDefinition definition) => null;
    Diagnostic? IUnitInstanceProcessingDiagnostics<TDefinition, TLocations>.UnitInstanceNameReservedByUnitInstancePluralForm(IUnitInstanceProcessingContext context, TDefinition definition) => null;
    Diagnostic? IUnitInstanceProcessingDiagnostics<TDefinition, TLocations>.UnitInstancePluralFormReservedByUnitInstanceName(IUnitInstanceProcessingContext context, TDefinition definition, string interpretedPluralForm) => null;
}
