namespace SharpMeasures.Generators.Scalars.ForeignScalarParsing.Diagnostics;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Quantities.Parsing.QuantityConstant;
using SharpMeasures.Generators.Scalars.Parsing.ScalarConstant;

internal class EmptyScalarConstantProcessingDiagnostics : IQuantityConstantProcessingDiagnostics<RawScalarConstantDefinition, ScalarConstantLocations>
{
    public static EmptyScalarConstantProcessingDiagnostics Instance { get; } = new();

    Diagnostic? IQuantityConstantProcessingDiagnostics<RawScalarConstantDefinition, ScalarConstantLocations>.DuplicateMultiples(IQuantityConstantProcessingContext context, RawScalarConstantDefinition definition, string interpretedMultiples) => null;
    Diagnostic? IQuantityConstantProcessingDiagnostics<RawScalarConstantDefinition, ScalarConstantLocations>.DuplicateName(IQuantityConstantProcessingContext context, RawScalarConstantDefinition definition) => null;
    Diagnostic? IQuantityConstantProcessingDiagnostics<RawScalarConstantDefinition, ScalarConstantLocations>.EmptyMultiples(IQuantityConstantProcessingContext context, RawScalarConstantDefinition definition) => null;
    Diagnostic? IQuantityConstantProcessingDiagnostics<RawScalarConstantDefinition, ScalarConstantLocations>.EmptyName(IQuantityConstantProcessingContext context, RawScalarConstantDefinition definition) => null;
    Diagnostic? IQuantityConstantProcessingDiagnostics<RawScalarConstantDefinition, ScalarConstantLocations>.EmptyUnitInstanceName(IQuantityConstantProcessingContext context, RawScalarConstantDefinition definition) => null;
    Diagnostic? IQuantityConstantProcessingDiagnostics<RawScalarConstantDefinition, ScalarConstantLocations>.InvalidMultiples(IQuantityConstantProcessingContext context, RawScalarConstantDefinition definition) => null;
    Diagnostic? IQuantityConstantProcessingDiagnostics<RawScalarConstantDefinition, ScalarConstantLocations>.MultiplesDisabledButNameSpecified(IQuantityConstantProcessingContext context, RawScalarConstantDefinition definition) => null;
    Diagnostic? IQuantityConstantProcessingDiagnostics<RawScalarConstantDefinition, ScalarConstantLocations>.MultiplesReservedByName(IQuantityConstantProcessingContext context, RawScalarConstantDefinition definition, string interpretedMultiples) => null;
    Diagnostic? IQuantityConstantProcessingDiagnostics<RawScalarConstantDefinition, ScalarConstantLocations>.NameAndMultiplesIdentical(IQuantityConstantProcessingContext context, RawScalarConstantDefinition definition) => null;
    Diagnostic? IQuantityConstantProcessingDiagnostics<RawScalarConstantDefinition, ScalarConstantLocations>.NameReservedByMultiples(IQuantityConstantProcessingContext context, RawScalarConstantDefinition definition) => null;
    Diagnostic? IQuantityConstantProcessingDiagnostics<RawScalarConstantDefinition, ScalarConstantLocations>.NullMultiples(IQuantityConstantProcessingContext context, RawScalarConstantDefinition definition) => null;
    Diagnostic? IQuantityConstantProcessingDiagnostics<RawScalarConstantDefinition, ScalarConstantLocations>.NullName(IQuantityConstantProcessingContext context, RawScalarConstantDefinition definition) => null;
    Diagnostic? IQuantityConstantProcessingDiagnostics<RawScalarConstantDefinition, ScalarConstantLocations>.NullUnitInstanceName(IQuantityConstantProcessingContext context, RawScalarConstantDefinition definition) => null;
    Diagnostic? IQuantityConstantProcessingDiagnostics<RawScalarConstantDefinition, ScalarConstantLocations>.SetRegexSubstitutionButNotPattern(IQuantityConstantProcessingContext context, RawScalarConstantDefinition definition) => null;
}
