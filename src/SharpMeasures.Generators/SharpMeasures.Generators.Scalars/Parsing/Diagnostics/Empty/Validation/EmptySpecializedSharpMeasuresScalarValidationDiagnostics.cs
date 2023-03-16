namespace SharpMeasures.Generators.Scalars.Parsing.Diagnostics.Empty.Validation;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Scalars.Parsing.SpecializedSharpMeasuresScalar;
using SharpMeasures.Generators.Units;

internal sealed class EmptySpecializedSharpMeasuresScalarValidationDiagnostics : ISpecializedSharpMeasuresScalarValidationDiagnostics
{
    public static EmptySpecializedSharpMeasuresScalarValidationDiagnostics Instance { get; } = new();

    private EmptySpecializedSharpMeasuresScalarValidationDiagnostics() { }

    public Diagnostic? TypeAlreadyUnit(ISpecializedSharpMeasuresScalarValidationContext context, SpecializedSharpMeasuresScalarDefinition definition) => null;
    public Diagnostic? TypeAlreadyScalarBase(ISpecializedSharpMeasuresScalarValidationContext context, SpecializedSharpMeasuresScalarDefinition definition) => null;
    public Diagnostic? OriginalNotScalar(ISpecializedSharpMeasuresScalarValidationContext context, SpecializedSharpMeasuresScalarDefinition definition) => null;
    public Diagnostic? RootScalarNotResolved(ISpecializedSharpMeasuresScalarValidationContext context, SpecializedSharpMeasuresScalarDefinition definition) => null;
    public Diagnostic? TypeNotVector(ISpecializedSharpMeasuresScalarValidationContext context, SpecializedSharpMeasuresScalarDefinition definition) => null;
    public Diagnostic? DifferenceNotScalar(ISpecializedSharpMeasuresScalarValidationContext context, SpecializedSharpMeasuresScalarDefinition definition) => null;
    public Diagnostic? UnrecognizedDefaultUnitInstance(IProcessingContext context, IDefaultUnitInstanceDefinition definition, IUnitType unit) => null;
}
