namespace SharpMeasures.Generators.Units.ForeignUnitParsing.Diagnostics;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Units.Parsing.DerivableUnit;

internal class EmptyDerivableUnitProcessingDiagnostics : IDerivableUnitProcessingDiagnostics
{
    public static EmptyDerivableUnitProcessingDiagnostics Instance { get; } = new();

    Diagnostic? IDerivableUnitProcessingDiagnostics.DerivationSignatureNotPermutable(IDerivableUnitProcessingContext context, RawDerivableUnitDefinition definition) => null;
    Diagnostic? IDerivableUnitProcessingDiagnostics.DuplicateDerivationID(IDerivableUnitProcessingContext context, RawDerivableUnitDefinition definition) => null;
    Diagnostic? IDerivableUnitProcessingDiagnostics.DuplicateDerivationSignature(IDerivableUnitProcessingContext context, RawDerivableUnitDefinition definition) => null;
    Diagnostic? IDerivableUnitProcessingDiagnostics.EmptyExpression(IDerivableUnitProcessingContext context, RawDerivableUnitDefinition definition) => null;
    Diagnostic? IDerivableUnitProcessingDiagnostics.EmptySignature(IDerivableUnitProcessingContext context, RawDerivableUnitDefinition definition) => null;
    Diagnostic? IDerivableUnitProcessingDiagnostics.ExpressionDoesNotIncludeUnit(IDerivableUnitProcessingContext context, RawDerivableUnitDefinition definition, int index) => null;
    Diagnostic? IDerivableUnitProcessingDiagnostics.MultipleDerivationsButNotNamed(IDerivableUnitProcessingContext context, RawDerivableUnitDefinition definition) => null;
    Diagnostic? IDerivableUnitProcessingDiagnostics.NullExpression(IDerivableUnitProcessingContext context, RawDerivableUnitDefinition definition) => null;
    Diagnostic? IDerivableUnitProcessingDiagnostics.NullSignature(IDerivableUnitProcessingContext context, RawDerivableUnitDefinition definition) => null;
    Diagnostic? IDerivableUnitProcessingDiagnostics.NullSignatureElement(IDerivableUnitProcessingContext context, RawDerivableUnitDefinition definition, int index) => null;
    Diagnostic? IDerivableUnitProcessingDiagnostics.UnitIncludesBiasTerm(IDerivableUnitProcessingContext context, RawDerivableUnitDefinition definition) => null;
    Diagnostic? IDerivableUnitProcessingDiagnostics.UnmatchedExpressionUnit(IDerivableUnitProcessingContext context, RawDerivableUnitDefinition definition, int requestedIndex) => null;
}
