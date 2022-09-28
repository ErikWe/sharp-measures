namespace SharpMeasures.Generators.Units.Parsing.Diagnostics.Empty.Processing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Units.Parsing.DerivableUnit;

internal sealed class EmptyDerivableUnitProcessingDiagnostics : IDerivableUnitProcessingDiagnostics
{
    public static EmptyDerivableUnitProcessingDiagnostics Instance { get; } = new();

    private EmptyDerivableUnitProcessingDiagnostics() { }

    public Diagnostic? DerivationSignatureNotPermutable(IDerivableUnitProcessingContext context, RawDerivableUnitDefinition definition) => null;
    public Diagnostic? DuplicateDerivationID(IDerivableUnitProcessingContext context, RawDerivableUnitDefinition definition) => null;
    public Diagnostic? DuplicateDerivationSignature(IDerivableUnitProcessingContext context, RawDerivableUnitDefinition definition) => null;
    public Diagnostic? EmptyExpression(IDerivableUnitProcessingContext context, RawDerivableUnitDefinition definition) => null;
    public Diagnostic? EmptySignature(IDerivableUnitProcessingContext context, RawDerivableUnitDefinition definition) => null;
    public Diagnostic? ExpressionDoesNotIncludeUnit(IDerivableUnitProcessingContext context, RawDerivableUnitDefinition definition, int index) => null;
    public Diagnostic? MultipleDerivationsButNotNamed(IDerivableUnitProcessingContext context, RawDerivableUnitDefinition definition) => null;
    public Diagnostic? NullExpression(IDerivableUnitProcessingContext context, RawDerivableUnitDefinition definition) => null;
    public Diagnostic? NullSignature(IDerivableUnitProcessingContext context, RawDerivableUnitDefinition definition) => null;
    public Diagnostic? NullSignatureElement(IDerivableUnitProcessingContext context, RawDerivableUnitDefinition definition, int index) => null;
    public Diagnostic? UnitIncludesBiasTerm(IDerivableUnitProcessingContext context, RawDerivableUnitDefinition definition) => null;
    public Diagnostic? UnmatchedExpressionUnit(IDerivableUnitProcessingContext context, RawDerivableUnitDefinition definition, int requestedIndex) => null;
}
