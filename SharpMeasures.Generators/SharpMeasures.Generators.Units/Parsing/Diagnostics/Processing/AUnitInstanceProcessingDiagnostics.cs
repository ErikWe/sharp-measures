namespace SharpMeasures.Generators.Units.Parsing.Diagnostics.Processing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Units.Parsing.Abstractions;

internal abstract class AUnitInstanceProcessingDiagnostics<TRawDefinition, TLocations> : IUnitInstanceProcessingDiagnostics<TRawDefinition, TLocations>
    where TRawDefinition : IRawUnitInstance<TLocations>
    where TLocations : IUnitInstanceLocations
{
    public Diagnostic NullUnitInstanceName(IUnitInstanceProcessingContext context, TRawDefinition definition)
    {
        return DiagnosticConstruction.NullUnitInstanceName(definition.Locations.Name?.AsRoslynLocation());
    }

    public Diagnostic EmptyUnitInstanceName(IUnitInstanceProcessingContext context, TRawDefinition definition) => NullUnitInstanceName(context, definition);

    public Diagnostic DuplicateUnitInstanceName(IUnitInstanceProcessingContext context, TRawDefinition definition)
    {
        return DiagnosticConstruction.DuplicateUnitInstanceName(definition.Locations.Name?.AsRoslynLocation(), definition.Name!, context.Type.Name);
    }

    public Diagnostic UnitInstanceNameReservedByUnitInstancePluralForm(IUnitInstanceProcessingContext context, TRawDefinition definition)
    {
        return DiagnosticConstruction.UnitInstanceNameReservedByUnitInstancePluralForm(definition.Locations.Name?.AsRoslynLocation(), definition.Name!, context.Type.Name);
    }

    public Diagnostic NullUnitInstancePluralForm(IUnitInstanceProcessingContext context, TRawDefinition definition)
    {
        return DiagnosticConstruction.NullUnitInstancePluralForm(definition.Locations.PluralForm?.AsRoslynLocation());
    }

    public Diagnostic EmptyUnitInstancePluralForm(IUnitInstanceProcessingContext context, TRawDefinition definition) => NullUnitInstancePluralForm(context, definition);

    public Diagnostic InvalidUnitInstancePluralForm(IUnitInstanceProcessingContext context, TRawDefinition definition)
    {
        return DiagnosticConstruction.InvalidUnitInstancePluralForm(definition.Locations.PluralForm?.AsRoslynLocation(), definition.PluralForm!, definition.Name!);
    }

    public Diagnostic DuplicateUnitInstancePluralForm(IUnitInstanceProcessingContext context, TRawDefinition definition, string interpretedPluralForm)
    {
        return DiagnosticConstruction.DuplicateUnitInstancePluralForm(definition.Locations.PluralForm?.AsRoslynLocation(), interpretedPluralForm, context.Type.Name);
    }

    public Diagnostic UnitInstancePluralFormReservedByUnitInstanceName(IUnitInstanceProcessingContext context, TRawDefinition definition, string interpretedPluralForm)
    {
        return DiagnosticConstruction.UnitInstancePluralFormReservedByUnitInstanceName(definition.Locations.PluralForm?.AsRoslynLocation(), interpretedPluralForm, context.Type.Name);
    }
}
