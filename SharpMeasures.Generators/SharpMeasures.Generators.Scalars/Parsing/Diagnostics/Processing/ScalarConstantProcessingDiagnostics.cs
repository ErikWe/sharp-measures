namespace SharpMeasures.Generators.Scalars.Parsing.Diagnostics.Processing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Quantities.Parsing.Diagnostics.Processing;
using SharpMeasures.Generators.Quantities.Parsing.QuantityConstant;
using SharpMeasures.Generators.Scalars.Parsing.ScalarConstant;

internal sealed class ScalarConstantProcessingDiagnostics : IScalarConstantProcessingDiagnostics
{
    private QuantityConstantProcessingDiagnostics<RawScalarConstantDefinition, ScalarConstantLocations> QuantityInstance { get; }

    public ScalarConstantProcessingDiagnostics(NamedType unit)
    {
        QuantityInstance = new(unit);
    }

    public ScalarConstantProcessingDiagnostics()
    {
        QuantityInstance = new();
    }

    public Diagnostic? NullName(IQuantityConstantProcessingContext context, RawScalarConstantDefinition definition) => QuantityInstance.NullName(context, definition);
    public Diagnostic? EmptyName(IQuantityConstantProcessingContext context, RawScalarConstantDefinition definition) => QuantityInstance.EmptyName(context, definition);
    public Diagnostic? DuplicateName(IQuantityConstantProcessingContext context, RawScalarConstantDefinition definition) => QuantityInstance.DuplicateName(context, definition);
    public Diagnostic? NameReservedByMultiples(IQuantityConstantProcessingContext context, RawScalarConstantDefinition definition) => QuantityInstance.NameReservedByMultiples(context, definition);
    public Diagnostic? NullUnitInstanceName(IQuantityConstantProcessingContext context, RawScalarConstantDefinition definition) => QuantityInstance.NullUnitInstanceName(context, definition);
    public Diagnostic? EmptyUnitInstanceName(IQuantityConstantProcessingContext context, RawScalarConstantDefinition definition) => QuantityInstance.EmptyUnitInstanceName(context, definition);
    public Diagnostic? NullMultiples(IQuantityConstantProcessingContext context, RawScalarConstantDefinition definition) => QuantityInstance.NullMultiples(context, definition);
    public Diagnostic? EmptyMultiples(IQuantityConstantProcessingContext context, RawScalarConstantDefinition definition) => QuantityInstance.EmptyMultiples(context, definition);
    public Diagnostic? InvalidMultiples(IQuantityConstantProcessingContext context, RawScalarConstantDefinition definition) => QuantityInstance.InvalidMultiples(context, definition);
    public Diagnostic? DuplicateMultiples(IQuantityConstantProcessingContext context, RawScalarConstantDefinition definition, string interpretedMultiples) => QuantityInstance.DuplicateMultiples(context, definition, interpretedMultiples);
    public Diagnostic? MultiplesReservedByName(IQuantityConstantProcessingContext context, RawScalarConstantDefinition definition, string interpretedMultiples) => QuantityInstance.MultiplesReservedByName(context, definition, interpretedMultiples);
    public Diagnostic? NameAndMultiplesIdentical(IQuantityConstantProcessingContext context, RawScalarConstantDefinition definition) => QuantityInstance.NameAndMultiplesIdentical(context, definition);
    public Diagnostic? MultiplesDisabledButNameSpecified(IQuantityConstantProcessingContext context, RawScalarConstantDefinition definition) => QuantityInstance.MultiplesDisabledButNameSpecified(context, definition);
    public Diagnostic? SetRegexSubstitutionButNotPattern(IQuantityConstantProcessingContext context, RawScalarConstantDefinition definition) => QuantityInstance.SetRegexSubstitutionButNotPattern(context, definition);
}
