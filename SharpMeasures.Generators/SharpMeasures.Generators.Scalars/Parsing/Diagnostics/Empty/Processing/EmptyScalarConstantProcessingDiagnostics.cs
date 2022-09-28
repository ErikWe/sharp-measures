namespace SharpMeasures.Generators.Scalars.Parsing.Diagnostics.Empty.Processing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Quantities.Parsing.QuantityConstant;
using SharpMeasures.Generators.Scalars.Parsing.ScalarConstant;

internal sealed class EmptyScalarConstantProcessingDiagnostics : IScalarConstantProcessingDiagnostics
{
    public static EmptyScalarConstantProcessingDiagnostics Instance { get; } = new();

    private EmptyScalarConstantProcessingDiagnostics() { }

    public Diagnostic? DuplicateMultiples(IQuantityConstantProcessingContext context, RawScalarConstantDefinition definition, string interpretedMultiples) => null;
    public Diagnostic? DuplicateName(IQuantityConstantProcessingContext context, RawScalarConstantDefinition definition) => null;
    public Diagnostic? EmptyMultiples(IQuantityConstantProcessingContext context, RawScalarConstantDefinition definition) => null;
    public Diagnostic? EmptyName(IQuantityConstantProcessingContext context, RawScalarConstantDefinition definition) => null;
    public Diagnostic? EmptyUnitInstanceName(IQuantityConstantProcessingContext context, RawScalarConstantDefinition definition) => null;
    public Diagnostic? InvalidMultiples(IQuantityConstantProcessingContext context, RawScalarConstantDefinition definition) => null;
    public Diagnostic? MultiplesDisabledButNameSpecified(IQuantityConstantProcessingContext context, RawScalarConstantDefinition definition) => null;
    public Diagnostic? MultiplesReservedByName(IQuantityConstantProcessingContext context, RawScalarConstantDefinition definition, string interpretedMultiples) => null;
    public Diagnostic? NameAndMultiplesIdentical(IQuantityConstantProcessingContext context, RawScalarConstantDefinition definition) => null;
    public Diagnostic? NameReservedByMultiples(IQuantityConstantProcessingContext context, RawScalarConstantDefinition definition) => null;
    public Diagnostic? NullMultiples(IQuantityConstantProcessingContext context, RawScalarConstantDefinition definition) => null;
    public Diagnostic? NullName(IQuantityConstantProcessingContext context, RawScalarConstantDefinition definition) => null;
    public Diagnostic? NullUnitInstanceName(IQuantityConstantProcessingContext context, RawScalarConstantDefinition definition) => null;
    public Diagnostic? SetRegexSubstitutionButNotPattern(IQuantityConstantProcessingContext context, RawScalarConstantDefinition definition) => null;
}
