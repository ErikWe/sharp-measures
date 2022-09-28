namespace SharpMeasures.Generators.Vectors.Parsing.Diagnostics.Empty.Validation;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Vectors.Parsing.SpecializedSharpMeasuresVectorGroup;

internal sealed class EmptySpecializedSharpMeasuresVectorGroupValidationDiagnostics : ISpecializedSharpMeasuresVectorGroupValidationDiagnostics
{
    public static EmptySpecializedSharpMeasuresVectorGroupValidationDiagnostics Instance { get; } = new();

    private EmptySpecializedSharpMeasuresVectorGroupValidationDiagnostics() { }

    public Diagnostic? TypeAlreadyUnit(ISpecializedSharpMeasuresVectorGroupValidationContext context, SpecializedSharpMeasuresVectorGroupDefinition definition) => null;
    public Diagnostic? TypeAlreadyScalar(ISpecializedSharpMeasuresVectorGroupValidationContext context, SpecializedSharpMeasuresVectorGroupDefinition definition) => null;
    public Diagnostic? TypeAlreadyVector(ISpecializedSharpMeasuresVectorGroupValidationContext context, SpecializedSharpMeasuresVectorGroupDefinition definition) => null;
    public Diagnostic? TypeAlreadyVectorGroupBase(ISpecializedSharpMeasuresVectorGroupValidationContext context, SpecializedSharpMeasuresVectorGroupDefinition definition) => null;
    public Diagnostic? OriginalNotVectorGroup(ISpecializedSharpMeasuresVectorGroupValidationContext context, SpecializedSharpMeasuresVectorGroupDefinition definition) => null;
    public Diagnostic? RootVectorGroupNotResolved(ISpecializedSharpMeasuresVectorGroupValidationContext context, SpecializedSharpMeasuresVectorGroupDefinition definition) => null;
    public Diagnostic? TypeNotScalar(ISpecializedSharpMeasuresVectorGroupValidationContext context, SpecializedSharpMeasuresVectorGroupDefinition definition) => null;
    public Diagnostic? DifferenceNotVectorGroup(ISpecializedSharpMeasuresVectorGroupValidationContext context, SpecializedSharpMeasuresVectorGroupDefinition definition) => null;
    public Diagnostic? UnrecognizedDefaultUnitInstance(IProcessingContext context, IDefaultUnitInstanceDefinition definition, IUnitType unit) => null;
}
