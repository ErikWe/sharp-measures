namespace SharpMeasures.Generators.Vectors.Parsing.Diagnostics.Empty.Validation;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVectorGroup;

internal sealed class EmptySharpMeasuresVectorGroupValidationDiagnostics : ISharpMeasuresVectorGroupValidationDiagnostics
{
    public static EmptySharpMeasuresVectorGroupValidationDiagnostics Instance { get; } = new();

    private EmptySharpMeasuresVectorGroupValidationDiagnostics() { }

    public Diagnostic? TypeAlreadyUnit(ISharpMeasuresVectorGroupValidationContext context, SharpMeasuresVectorGroupDefinition definition) => null;
    public Diagnostic? TypeAlreadyScalar(ISharpMeasuresVectorGroupValidationContext context, SharpMeasuresVectorGroupDefinition definition) => null;
    public Diagnostic? TypeAlreadyVector(ISharpMeasuresVectorGroupValidationContext context, SharpMeasuresVectorGroupDefinition definition) => null;
    public Diagnostic? TypeNotUnit(ISharpMeasuresVectorGroupValidationContext context, SharpMeasuresVectorGroupDefinition definition) => null;
    public Diagnostic? TypeNotScalar(ISharpMeasuresVectorGroupValidationContext context, SharpMeasuresVectorGroupDefinition definition) => null;
    public Diagnostic? DifferenceNotVectorGroup(ISharpMeasuresVectorGroupValidationContext context, SharpMeasuresVectorGroupDefinition definition) => null;
    public Diagnostic? UnrecognizedDefaultUnitInstance(IProcessingContext context, IDefaultUnitInstanceDefinition definition, IUnitType unit) => null;
}
