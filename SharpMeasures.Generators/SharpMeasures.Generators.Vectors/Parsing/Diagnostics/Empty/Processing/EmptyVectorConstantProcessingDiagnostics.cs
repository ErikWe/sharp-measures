namespace SharpMeasures.Generators.Vectors.Parsing.Diagnostics.Empty.Processing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Quantities.Parsing.QuantityConstant;
using SharpMeasures.Generators.Vectors.Parsing.VectorConstant;

internal sealed class EmptyVectorConstantProcessingDiagnostics : IVectorConstantProcessingDiagnostics
{
    public static EmptyVectorConstantProcessingDiagnostics Instance { get; } = new();

    private EmptyVectorConstantProcessingDiagnostics() { }

    public Diagnostic? DuplicateMultiples(IQuantityConstantProcessingContext context, RawVectorConstantDefinition definition, string interpretedMultiples) => null;
    public Diagnostic? DuplicateName(IQuantityConstantProcessingContext context, RawVectorConstantDefinition definition) => null;
    public Diagnostic? EmptyMultiples(IQuantityConstantProcessingContext context, RawVectorConstantDefinition definition) => null;
    public Diagnostic? EmptyName(IQuantityConstantProcessingContext context, RawVectorConstantDefinition definition) => null;
    public Diagnostic? EmptyUnitInstanceName(IQuantityConstantProcessingContext context, RawVectorConstantDefinition definition) => null;
    public Diagnostic? InvalidMultiples(IQuantityConstantProcessingContext context, RawVectorConstantDefinition definition) => null;
    public Diagnostic? MultiplesDisabledButNameSpecified(IQuantityConstantProcessingContext context, RawVectorConstantDefinition definition) => null;
    public Diagnostic? MultiplesReservedByName(IQuantityConstantProcessingContext context, RawVectorConstantDefinition definition, string interpretedMultiples) => null;
    public Diagnostic? NameAndMultiplesIdentical(IQuantityConstantProcessingContext context, RawVectorConstantDefinition definition) => null;
    public Diagnostic? NameReservedByMultiples(IQuantityConstantProcessingContext context, RawVectorConstantDefinition definition) => null;
    public Diagnostic? NullMultiples(IQuantityConstantProcessingContext context, RawVectorConstantDefinition definition) => null;
    public Diagnostic? NullName(IQuantityConstantProcessingContext context, RawVectorConstantDefinition definition) => null;
    public Diagnostic? NullUnitInstanceName(IQuantityConstantProcessingContext context, RawVectorConstantDefinition definition) => null;
    public Diagnostic? SetRegexSubstitutionButNotPattern(IQuantityConstantProcessingContext context, RawVectorConstantDefinition definition) => null;
}
