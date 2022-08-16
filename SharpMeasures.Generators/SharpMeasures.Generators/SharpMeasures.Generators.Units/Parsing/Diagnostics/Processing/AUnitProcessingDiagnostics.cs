namespace SharpMeasures.Generators.Units.Parsing.Diagnostics.Processing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Units.Parsing.Abstractions;

internal abstract class AUnitProcessingDiagnostics<TRawDefinition, TLocations> : IUnitProcessingDiagnostics<TRawDefinition, TLocations>
    where TRawDefinition : IUnprocessedUnitDefinition<TLocations>
    where TLocations : IUnitLocations
{
    public Diagnostic NullUnitName(IUnitProcessingContext context, TRawDefinition definition)
    {
        return DiagnosticConstruction.NullUnitName(definition.Locations.Name?.AsRoslynLocation());
    }

    public Diagnostic EmptyUnitName(IUnitProcessingContext context, TRawDefinition definition) => NullUnitName(context, definition);

    public Diagnostic DuplicateUnitName(IUnitProcessingContext context, TRawDefinition definition)
    {
        return DiagnosticConstruction.DuplicateUnitName(definition.Locations.Name?.AsRoslynLocation(), definition.Name!, context.Type.Name);
    }

    public Diagnostic NullUnitPluralForm(IUnitProcessingContext context, TRawDefinition definition)
    {
        return DiagnosticConstruction.NullUnitPluralForm(definition.Locations.Plural?.AsRoslynLocation());
    }

    public Diagnostic EmptyUnitPluralForm(IUnitProcessingContext context, TRawDefinition definition) => NullUnitPluralForm(context, definition);

    public Diagnostic InvalidUnitPluralForm(IUnitProcessingContext context, TRawDefinition definition)
    {
        return DiagnosticConstruction.InvalidUnitPluralForm(definition.Locations.Plural?.AsRoslynLocation(), definition.Plural!, definition.Name!);
    }

    public Diagnostic DuplicateUnitPluralForm(IUnitProcessingContext context, TRawDefinition definition, string interpretedPlural)
    {
        return DiagnosticConstruction.DuplicateUnitPluralForm(definition.Locations.Plural?.AsRoslynLocation(), interpretedPlural, context.Type.Name);
    }
}
