namespace SharpMeasures.Generators.Vectors.ForeignVectorParsing.Diagnostics.Validation;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Quantities.Parsing.DefaultUnitInstance;
using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVectorGroup;

internal sealed class EmptySharpMeasuresVectorGroupValidationDiagnostics : ISharpMeasuresVectorGroupValidationDiagnostics
{
    public static EmptySharpMeasuresVectorGroupValidationDiagnostics Instance { get; } = new();

    private EmptySharpMeasuresVectorGroupValidationDiagnostics() { }

    Diagnostic? ISharpMeasuresVectorGroupValidationDiagnostics.TypeAlreadyUnit(ISharpMeasuresVectorGroupValidationContext context, SharpMeasuresVectorGroupDefinition definition) => null;
    Diagnostic? ISharpMeasuresVectorGroupValidationDiagnostics.TypeAlreadyScalar(ISharpMeasuresVectorGroupValidationContext context, SharpMeasuresVectorGroupDefinition definition) => null;
    Diagnostic? ISharpMeasuresVectorGroupValidationDiagnostics.TypeAlreadyVector(ISharpMeasuresVectorGroupValidationContext context, SharpMeasuresVectorGroupDefinition definition) => null;
    Diagnostic? ISharpMeasuresVectorGroupValidationDiagnostics.TypeNotUnit(ISharpMeasuresVectorGroupValidationContext context, SharpMeasuresVectorGroupDefinition definition) => null;
    Diagnostic? ISharpMeasuresVectorGroupValidationDiagnostics.TypeNotScalar(ISharpMeasuresVectorGroupValidationContext context, SharpMeasuresVectorGroupDefinition definition) => null;
    Diagnostic? ISharpMeasuresVectorGroupValidationDiagnostics.DifferenceNotVectorGroup(ISharpMeasuresVectorGroupValidationContext context, SharpMeasuresVectorGroupDefinition definition) => null;
    Diagnostic? IDefaultUnitInstanceValidationDiagnostics.UnrecognizedDefaultUnitInstance(IProcessingContext context, IDefaultUnitInstanceDefinition definition, IUnitType unit) => null;
}
