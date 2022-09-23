namespace SharpMeasures.Generators.Scalars.ForeignScalarParsing.Diagnostics.Validation;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Quantities.Parsing.DefaultUnitInstance;
using SharpMeasures.Generators.Scalars.Parsing.SpecializedSharpMeasuresScalar;
using SharpMeasures.Generators.Units;

internal sealed class SpecializedSharpMeasuresScalarValidationDiagnostics : ISpecializedSharpMeasuresScalarValidationDiagnostics
{
    public static SpecializedSharpMeasuresScalarValidationDiagnostics Instance { get; } = new();

    private SpecializedSharpMeasuresScalarValidationDiagnostics() { }

    Diagnostic? ISpecializedSharpMeasuresScalarValidationDiagnostics.TypeAlreadyUnit(ISpecializedSharpMeasuresScalarValidationContext context, SpecializedSharpMeasuresScalarDefinition definition) => null;
    Diagnostic? ISpecializedSharpMeasuresScalarValidationDiagnostics.TypeAlreadyScalarBase(ISpecializedSharpMeasuresScalarValidationContext context, SpecializedSharpMeasuresScalarDefinition definition) => null;
    Diagnostic? ISpecializedSharpMeasuresScalarValidationDiagnostics.OriginalNotScalar(ISpecializedSharpMeasuresScalarValidationContext context, SpecializedSharpMeasuresScalarDefinition definition) => null;
    Diagnostic? ISpecializedSharpMeasuresScalarValidationDiagnostics.RootScalarNotResolved(ISpecializedSharpMeasuresScalarValidationContext context, SpecializedSharpMeasuresScalarDefinition definition) => null;
    Diagnostic? ISpecializedSharpMeasuresScalarValidationDiagnostics.TypeNotVector(ISpecializedSharpMeasuresScalarValidationContext context, SpecializedSharpMeasuresScalarDefinition definition) => null;
    Diagnostic? ISpecializedSharpMeasuresScalarValidationDiagnostics.DifferenceNotScalar(ISpecializedSharpMeasuresScalarValidationContext context, SpecializedSharpMeasuresScalarDefinition definition) => null;
    Diagnostic? IDefaultUnitInstanceValidationDiagnostics.UnrecognizedDefaultUnitInstance(IProcessingContext context, IDefaultUnitInstanceDefinition definition, IUnitType unit) => null;
}
