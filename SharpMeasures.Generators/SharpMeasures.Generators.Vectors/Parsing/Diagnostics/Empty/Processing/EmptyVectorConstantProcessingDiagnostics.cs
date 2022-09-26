namespace SharpMeasures.Generators.Vectors.Parsing.Diagnostics.Empty.Processing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Quantities.Parsing.QuantityConstant;
using SharpMeasures.Generators.Vectors.Parsing.VectorConstant;

internal sealed class EmptyVectorConstantProcessingDiagnostics : IVectorConstantProcessingDiagnostics
{
    public static EmptyVectorConstantProcessingDiagnostics Instance { get; } = new();

    private EmptyVectorConstantProcessingDiagnostics() { }

    Diagnostic? IQuantityConstantProcessingDiagnostics<RawVectorConstantDefinition, VectorConstantLocations>.DuplicateMultiples(IQuantityConstantProcessingContext context, RawVectorConstantDefinition definition, string interpretedMultiples) => null;
    Diagnostic? IQuantityConstantProcessingDiagnostics<RawVectorConstantDefinition, VectorConstantLocations>.DuplicateName(IQuantityConstantProcessingContext context, RawVectorConstantDefinition definition) => null;
    Diagnostic? IQuantityConstantProcessingDiagnostics<RawVectorConstantDefinition, VectorConstantLocations>.EmptyMultiples(IQuantityConstantProcessingContext context, RawVectorConstantDefinition definition) => null;
    Diagnostic? IQuantityConstantProcessingDiagnostics<RawVectorConstantDefinition, VectorConstantLocations>.EmptyName(IQuantityConstantProcessingContext context, RawVectorConstantDefinition definition) => null;
    Diagnostic? IQuantityConstantProcessingDiagnostics<RawVectorConstantDefinition, VectorConstantLocations>.EmptyUnitInstanceName(IQuantityConstantProcessingContext context, RawVectorConstantDefinition definition) => null;
    Diagnostic? IQuantityConstantProcessingDiagnostics<RawVectorConstantDefinition, VectorConstantLocations>.InvalidMultiples(IQuantityConstantProcessingContext context, RawVectorConstantDefinition definition) => null;
    Diagnostic? IQuantityConstantProcessingDiagnostics<RawVectorConstantDefinition, VectorConstantLocations>.MultiplesDisabledButNameSpecified(IQuantityConstantProcessingContext context, RawVectorConstantDefinition definition) => null;
    Diagnostic? IQuantityConstantProcessingDiagnostics<RawVectorConstantDefinition, VectorConstantLocations>.MultiplesReservedByName(IQuantityConstantProcessingContext context, RawVectorConstantDefinition definition, string interpretedMultiples) => null;
    Diagnostic? IQuantityConstantProcessingDiagnostics<RawVectorConstantDefinition, VectorConstantLocations>.NameAndMultiplesIdentical(IQuantityConstantProcessingContext context, RawVectorConstantDefinition definition) => null;
    Diagnostic? IQuantityConstantProcessingDiagnostics<RawVectorConstantDefinition, VectorConstantLocations>.NameReservedByMultiples(IQuantityConstantProcessingContext context, RawVectorConstantDefinition definition) => null;
    Diagnostic? IQuantityConstantProcessingDiagnostics<RawVectorConstantDefinition, VectorConstantLocations>.NullMultiples(IQuantityConstantProcessingContext context, RawVectorConstantDefinition definition) => null;
    Diagnostic? IQuantityConstantProcessingDiagnostics<RawVectorConstantDefinition, VectorConstantLocations>.NullName(IQuantityConstantProcessingContext context, RawVectorConstantDefinition definition) => null;
    Diagnostic? IQuantityConstantProcessingDiagnostics<RawVectorConstantDefinition, VectorConstantLocations>.NullUnitInstanceName(IQuantityConstantProcessingContext context, RawVectorConstantDefinition definition) => null;
    Diagnostic? IQuantityConstantProcessingDiagnostics<RawVectorConstantDefinition, VectorConstantLocations>.SetRegexSubstitutionButNotPattern(IQuantityConstantProcessingContext context, RawVectorConstantDefinition definition) => null;
}
