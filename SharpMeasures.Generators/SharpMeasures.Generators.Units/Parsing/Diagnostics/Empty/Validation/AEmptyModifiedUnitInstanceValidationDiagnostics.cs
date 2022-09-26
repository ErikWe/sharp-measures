namespace SharpMeasures.Generators.Units.Parsing.Diagnostics.Empty.Validation;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Units.Parsing.Abstractions;

internal abstract class AEmptyModifiedUnitInstanceValidationDiagnostics<TDefinition> : IModifiedUnitInstanceValidationDiagnostics<TDefinition> where TDefinition : IModifiedUnitInstance
{
    Diagnostic? IModifiedUnitInstanceValidationDiagnostics<TDefinition>.CyclicallyModified(IModifiedUnitInstanceValidationContext context, TDefinition definition) => null;
    Diagnostic? IModifiedUnitInstanceValidationDiagnostics<TDefinition>.UnrecognizedOriginalUnitInstance(IModifiedUnitInstanceValidationContext context, TDefinition definition) => null;
}
