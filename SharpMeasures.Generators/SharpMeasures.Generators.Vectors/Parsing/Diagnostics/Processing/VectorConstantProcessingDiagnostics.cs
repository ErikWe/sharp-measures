namespace SharpMeasures.Generators.Vectors.Parsing.Diagnostics.Processing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Quantities.Parsing.Diagnostics.Processing;
using SharpMeasures.Generators.Quantities.Parsing.QuantityConstant;
using SharpMeasures.Generators.Vectors.Parsing.VectorConstant;

internal sealed class VectorConstantProcessingDiagnostics : IQuantityConstantProcessingDiagnostics<RawVectorConstantDefinition, VectorConstantLocations>
{
    private QuantityConstantProcessingDiagnostics<RawVectorConstantDefinition, VectorConstantLocations> QuantityInstance { get; }

    public VectorConstantProcessingDiagnostics(NamedType unit)
    {
        QuantityInstance = new(unit);
    }

    public VectorConstantProcessingDiagnostics()
    {
        QuantityInstance = new();
    }

    public Diagnostic? NullName(IQuantityConstantProcessingContext context, RawVectorConstantDefinition definition) => QuantityInstance.NullName(context, definition);
    public Diagnostic? EmptyName(IQuantityConstantProcessingContext context, RawVectorConstantDefinition definition) => QuantityInstance.EmptyName(context, definition);
    public Diagnostic? DuplicateName(IQuantityConstantProcessingContext context, RawVectorConstantDefinition definition) => QuantityInstance.DuplicateName(context, definition);
    public Diagnostic? NameReservedByMultiples(IQuantityConstantProcessingContext context, RawVectorConstantDefinition definition) => QuantityInstance.NameReservedByMultiples(context, definition);
    public Diagnostic? NullUnitInstanceName(IQuantityConstantProcessingContext context, RawVectorConstantDefinition definition) => QuantityInstance.NullUnitInstanceName(context, definition);
    public Diagnostic? EmptyUnitInstanceName(IQuantityConstantProcessingContext context, RawVectorConstantDefinition definition) => QuantityInstance.EmptyUnitInstanceName(context, definition);
    public Diagnostic? NullMultiples(IQuantityConstantProcessingContext context, RawVectorConstantDefinition definition) => QuantityInstance.NullMultiples(context, definition);
    public Diagnostic? EmptyMultiples(IQuantityConstantProcessingContext context, RawVectorConstantDefinition definition) => QuantityInstance.EmptyMultiples(context, definition);
    public Diagnostic? InvalidMultiples(IQuantityConstantProcessingContext context, RawVectorConstantDefinition definition) => QuantityInstance.InvalidMultiples(context, definition);
    public Diagnostic? DuplicateMultiples(IQuantityConstantProcessingContext context, RawVectorConstantDefinition definition, string interpretedMultiples) => QuantityInstance.DuplicateMultiples(context, definition, interpretedMultiples);
    public Diagnostic? MultiplesReservedByName(IQuantityConstantProcessingContext context, RawVectorConstantDefinition definition, string interpretedMultiples) => QuantityInstance.MultiplesReservedByName(context, definition, interpretedMultiples);
    public Diagnostic? NameAndMultiplesIdentical(IQuantityConstantProcessingContext context, RawVectorConstantDefinition definition) => QuantityInstance.NameAndMultiplesIdentical(context, definition);
    public Diagnostic? MultiplesDisabledButNameSpecified(IQuantityConstantProcessingContext context, RawVectorConstantDefinition definition) => QuantityInstance.MultiplesDisabledButNameSpecified(context, definition);
    public Diagnostic? SetRegexSubstitutionButNotPattern(IQuantityConstantProcessingContext context, RawVectorConstantDefinition definition) => QuantityInstance.SetRegexSubstitutionButNotPattern(context, definition);
}
