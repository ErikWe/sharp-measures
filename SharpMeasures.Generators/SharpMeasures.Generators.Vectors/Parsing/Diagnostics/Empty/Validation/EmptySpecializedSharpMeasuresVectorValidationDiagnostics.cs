namespace SharpMeasures.Generators.Vectors.Parsing.Diagnostics.Empty.Validation;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Vectors.Parsing.SpecializedSharpMeasuresVector;

internal sealed class EmptySpecializedSharpMeasuresVectorValidationDiagnostics : ISpecializedSharpMeasuresVectorValidationDiagnostics
{
    public static EmptySpecializedSharpMeasuresVectorValidationDiagnostics Instance { get; } = new();

    private EmptySpecializedSharpMeasuresVectorValidationDiagnostics() { }

    public Diagnostic? TypeAlreadyUnit(ISpecializedSharpMeasuresVectorValidationContext context, SpecializedSharpMeasuresVectorDefinition definition) => null;
    public Diagnostic? TypeAlreadyScalar(ISpecializedSharpMeasuresVectorValidationContext context, SpecializedSharpMeasuresVectorDefinition definition) => null;
    public Diagnostic? TypeAlreadyVectorBase(ISpecializedSharpMeasuresVectorValidationContext context, SpecializedSharpMeasuresVectorDefinition definition) => null;
    public Diagnostic? OriginalNotVector(ISpecializedSharpMeasuresVectorValidationContext context, SpecializedSharpMeasuresVectorDefinition definition) => null;
    public Diagnostic? RootVectorNotResolved(ISpecializedSharpMeasuresVectorValidationContext context, SpecializedSharpMeasuresVectorDefinition definition) => null;
    public Diagnostic? VectorNameAndDimensionConflict(ISpecializedSharpMeasuresVectorValidationContext context, SpecializedSharpMeasuresVectorDefinition definition, int interpretedDimension, int inheritedDimension) => null;
    public Diagnostic? TypeNotScalar(ISpecializedSharpMeasuresVectorValidationContext context, SpecializedSharpMeasuresVectorDefinition definition) => null;
    public Diagnostic? DifferenceNotVector(ISpecializedSharpMeasuresVectorValidationContext context, SpecializedSharpMeasuresVectorDefinition definition) => null;
    public Diagnostic? DifferenceVectorInvalidDimension(ISpecializedSharpMeasuresVectorValidationContext context, SpecializedSharpMeasuresVectorDefinition definition, int expectedDimension, int actualDimension) => null;
    public Diagnostic? DifferenceVectorGroupLacksMatchingDimension(ISpecializedSharpMeasuresVectorValidationContext context, SpecializedSharpMeasuresVectorDefinition definition, int dimension) => null;
    public Diagnostic? UnrecognizedDefaultUnitInstance(IProcessingContext context, IDefaultUnitInstanceDefinition definition, IUnitType unit) => null;
}
