namespace SharpMeasures.Generators.Units.Parsing.Diagnostics;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.Units;
using SharpMeasures.Generators.Diagnostics;

internal abstract class ADependantUnitDiagnostics<TDefinition> : AUnitDiagnostics<TDefinition>, IDependantUnitDiagnostics<TDefinition>
    where TDefinition : IDependantUnitDefinition
{
    public Diagnostic DependencyNullOrEmpty(IDependantUnitValidatorContext context, TDefinition definition)
    {
        return DiagnosticConstruction.InvalidUnitName(definition.ParsingData.Locations.DependantOn.AsRoslynLocation(), definition.DependantOn);
    }

    public Diagnostic UnrecognizedDependency(IDependantUnitValidatorContext context, TDefinition definition)
    {
        return DiagnosticConstruction.UnrecognizedUnitName(definition.ParsingData.Locations.DependantOn.AsRoslynLocation(), definition.DependantOn, context.Type.Name);
    }

    public Diagnostic DependantOnSelf(IDependantUnitValidatorContext context, TDefinition definition)
    {
        return DiagnosticConstruction.CyclicDependency(definition.ParsingData.Locations.Attribute.AsRoslynLocation(), definition.Name, context.Type.Name);
    }
}
