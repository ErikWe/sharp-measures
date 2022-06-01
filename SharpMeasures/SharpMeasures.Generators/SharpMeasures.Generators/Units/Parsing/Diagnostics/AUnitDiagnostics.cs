namespace SharpMeasures.Generators.Units.Parsing.Diagnostics;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.Units;
using SharpMeasures.Generators.Diagnostics;

internal abstract class AUnitDiagnostics<TDefinition> : IUnitDiagnostics<TDefinition>
    where TDefinition : IRawUnitDefinition
{
    public Diagnostic NullUnitName(IUnitProcessingContext context, TDefinition definition)
    {
        return DiagnosticConstruction.InvalidUnitName_Null(definition.Locations.Name?.AsRoslynLocation());
    }

    public Diagnostic EmptyUnitName(IUnitProcessingContext context, TDefinition definition) => NullUnitName(context, definition);

    public Diagnostic DuplicateUnitName(IUnitProcessingContext context, TDefinition definition)
    {
        return DiagnosticConstruction.DuplicateUnitName(definition.Locations.Name?.AsRoslynLocation(), definition.Name!, context.Type.Name);
    }

    public Diagnostic NullUnitPluralForm(IUnitProcessingContext context, TDefinition definition)
    {
        return DiagnosticConstruction.InvalidUnitPluralForm_Null(definition.Locations.Plural?.AsRoslynLocation());
    }

    public Diagnostic EmptyUnitPluralForm(IUnitProcessingContext context, TDefinition definition) => NullUnitPluralForm(context, definition);

    public Diagnostic InvalidUnitPluralForm(IUnitProcessingContext context, TDefinition definition)
    {
        return DiagnosticConstruction.InvalidUnitPluralForm(definition.Locations.Plural?.AsRoslynLocation(), definition.Plural!, definition.Name!);
    }

    public Diagnostic DuplicateUnitPluralForm(IUnitProcessingContext context, TDefinition definition)
    {
        return DiagnosticConstruction.DuplicateUnitPluralForm(definition.Locations.Plural?.AsRoslynLocation(),
            definition.ParsingData.InterpretedPlural!, context.Type.Name);
    }
}
