namespace SharpMeasures.Generators.Units.Parsing.Diagnostics.Empty.Processing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Units.Parsing.Abstractions;

internal abstract class AEmptyUnitInstanceProcessingDiagnostics<TDefinition, TLocations> : IUnitInstanceProcessingDiagnostics<TDefinition, TLocations>
    where TDefinition : IRawUnitInstance<TLocations>
    where TLocations : IUnitInstanceLocations
{
    public Diagnostic? DuplicateUnitInstanceName(IUnitInstanceProcessingContext context, TDefinition definition) => null;
    public Diagnostic? DuplicateUnitInstancePluralForm(IUnitInstanceProcessingContext context, TDefinition definition, string interpretedPluralForm) => null;
    public Diagnostic? EmptyUnitInstanceName(IUnitInstanceProcessingContext context, TDefinition definition) => null;
    public Diagnostic? EmptyUnitInstancePluralForm(IUnitInstanceProcessingContext context, TDefinition definition) => null;
    public Diagnostic? InvalidUnitInstancePluralForm(IUnitInstanceProcessingContext context, TDefinition definition) => null;
    public Diagnostic? NullUnitInstanceName(IUnitInstanceProcessingContext context, TDefinition definition) => null;
    public Diagnostic? NullUnitInstancePluralForm(IUnitInstanceProcessingContext context, TDefinition definition) => null;
    public Diagnostic? UnitInstanceNameReservedByUnitInstancePluralForm(IUnitInstanceProcessingContext context, TDefinition definition) => null;
    public Diagnostic? UnitInstancePluralFormReservedByUnitInstanceName(IUnitInstanceProcessingContext context, TDefinition definition, string interpretedPluralForm) => null;
}
