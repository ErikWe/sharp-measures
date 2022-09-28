namespace SharpMeasures.Generators.Units.Parsing.Diagnostics.Empty.Validation;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Units.Parsing.Abstractions;

internal abstract class AEmptyModifiedUnitInstanceValidationDiagnostics<TDefinition> : IModifiedUnitInstanceValidationDiagnostics<TDefinition> where TDefinition : IModifiedUnitInstance
{
    public Diagnostic? CyclicallyModified(IModifiedUnitInstanceValidationContext context, TDefinition definition) => null;
    public Diagnostic? UnrecognizedOriginalUnitInstance(IModifiedUnitInstanceValidationContext context, TDefinition definition) => null;
}
