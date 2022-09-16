namespace SharpMeasures.Generators.Vectors.ForeignVectorParsing.Diagnostics;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Quantities.Parsing.QuantityConstant;
using SharpMeasures.Generators.Vectors.Parsing.VectorConstant;

internal class EmptyVectorConstantProcessingDiagnostics : IQuantityConstantProcessingDiagnostics<RawVectorConstantDefinition, VectorConstantLocations>
{
    public static EmptyVectorConstantProcessingDiagnostics Instance { get; } = new();

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
