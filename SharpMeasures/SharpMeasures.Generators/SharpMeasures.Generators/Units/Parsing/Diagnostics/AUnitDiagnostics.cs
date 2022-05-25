namespace SharpMeasures.Generators.Units.Parsing.Diagnostics;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.Units;
using SharpMeasures.Generators.Diagnostics;

internal abstract class AUnitDiagnostics<TDefinition> : IUnitDiagnostics<TDefinition>
    where TDefinition : IUnitDefinition
{
    public Diagnostic UnitNameNullOrEmpty(IUnitValidatorContext context, TDefinition definition)
    {
        return DiagnosticConstruction.InvalidUnitName(definition.ParsingData.Locations.Name.AsRoslynLocation(), definition.Name);
    }

    public Diagnostic DuplicateUnitName(IUnitValidatorContext context, TDefinition definition)
    {
        return DiagnosticConstruction.DuplicateUnitName(definition.ParsingData.Locations.Name.AsRoslynLocation(), definition.Name, context.Type.Name);
    }

    public Diagnostic InvalidUnitPluralForm(IUnitValidatorContext context, TDefinition definition)
    {
        return DiagnosticConstruction.InvalidUnitPluralForm(definition.ParsingData.Locations.Plural.AsRoslynLocation(), definition.Plural, definition.Name);
    }

    public Diagnostic DuplicateUnitPluralForm(IUnitValidatorContext context, TDefinition definition)
    {
        return DiagnosticConstruction.DuplicateUnitPluralForm(definition.ParsingData.Locations.Plural.AsRoslynLocation(),
            definition.ParsingData.InterpretedPlural, context.Type.Name);
    }
}
