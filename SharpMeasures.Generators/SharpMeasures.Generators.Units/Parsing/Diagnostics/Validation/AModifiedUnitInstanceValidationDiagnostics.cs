namespace SharpMeasures.Generators.Units.Parsing.Diagnostics.Validation;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Units.Parsing.Abstractions;

internal abstract class AModifiedUnitInstanceValidationDiagnostics<TDefinition> : IModifiedUnitInstanceValidationDiagnostics<TDefinition> where TDefinition : IModifiedUnitInstance
{
    public Diagnostic UnrecognizedOriginalUnitInstance(IModifiedUnitInstanceValidationContext context, TDefinition definition)
    {
        return DiagnosticConstruction.UnrecognizedUnitInstanceName(definition.Locations.OriginalUnitInstance?.AsRoslynLocation(), definition.OriginalUnitInstance!, context.Type.Name);
    }

    public Diagnostic CyclicallyModified(IModifiedUnitInstanceValidationContext context, TDefinition definition)
    {
        return DiagnosticConstruction.CyclicallyModifiedUnitInstances(definition.Locations.OriginalUnitInstance?.AsRoslynLocation(), definition.Name, context.Type.Name);
    }
}
