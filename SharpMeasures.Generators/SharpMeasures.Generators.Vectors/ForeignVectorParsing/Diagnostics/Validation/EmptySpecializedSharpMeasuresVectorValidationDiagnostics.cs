namespace SharpMeasures.Generators.Vectors.ForeignVectorParsing.Diagnostics.Validation;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Quantities.Parsing.DefaultUnitInstance;
using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Vectors.Parsing.SpecializedSharpMeasuresVector;

internal sealed class EmptySpecializedSharpMeasuresVectorValidationDiagnostics : ISpecializedSharpMeasuresVectorValidationDiagnostics
{
    public static EmptySpecializedSharpMeasuresVectorValidationDiagnostics Instance { get; } = new();

    private EmptySpecializedSharpMeasuresVectorValidationDiagnostics() { }

    Diagnostic? ISpecializedSharpMeasuresVectorValidationDiagnostics.TypeAlreadyUnit(ISpecializedSharpMeasuresVectorValidationContext context, SpecializedSharpMeasuresVectorDefinition definition) => null;
    Diagnostic? ISpecializedSharpMeasuresVectorValidationDiagnostics.TypeAlreadyScalar(ISpecializedSharpMeasuresVectorValidationContext context, SpecializedSharpMeasuresVectorDefinition definition) => null;
    Diagnostic? ISpecializedSharpMeasuresVectorValidationDiagnostics.TypeAlreadyVectorBase(ISpecializedSharpMeasuresVectorValidationContext context, SpecializedSharpMeasuresVectorDefinition definition) => null;
    Diagnostic? ISpecializedSharpMeasuresVectorValidationDiagnostics.OriginalNotVector(ISpecializedSharpMeasuresVectorValidationContext context, SpecializedSharpMeasuresVectorDefinition definition) => null;
    Diagnostic? ISpecializedSharpMeasuresVectorValidationDiagnostics.RootVectorNotResolved(ISpecializedSharpMeasuresVectorValidationContext context, SpecializedSharpMeasuresVectorDefinition definition) => null;
    Diagnostic? ISpecializedSharpMeasuresVectorValidationDiagnostics.VectorNameAndDimensionConflict(ISpecializedSharpMeasuresVectorValidationContext context, SpecializedSharpMeasuresVectorDefinition definition, int interpretedDimension, int inheritedDimension) => null;
    Diagnostic? ISpecializedSharpMeasuresVectorValidationDiagnostics.TypeNotScalar(ISpecializedSharpMeasuresVectorValidationContext context, SpecializedSharpMeasuresVectorDefinition definition) => null;
    Diagnostic? ISpecializedSharpMeasuresVectorValidationDiagnostics.DifferenceNotVector(ISpecializedSharpMeasuresVectorValidationContext context, SpecializedSharpMeasuresVectorDefinition definition) => null;
    Diagnostic? ISpecializedSharpMeasuresVectorValidationDiagnostics.DifferenceVectorInvalidDimension(ISpecializedSharpMeasuresVectorValidationContext context, SpecializedSharpMeasuresVectorDefinition definition, int expectedDimension, int actualDimension) => null;
    Diagnostic? ISpecializedSharpMeasuresVectorValidationDiagnostics.DifferenceVectorGroupLacksMatchingDimension(ISpecializedSharpMeasuresVectorValidationContext context, SpecializedSharpMeasuresVectorDefinition definition, int dimension) => null;
    Diagnostic? IDefaultUnitInstanceValidationDiagnostics.UnrecognizedDefaultUnitInstance(IProcessingContext context, IDefaultUnitInstanceDefinition definition, IUnitType unit) => null;
}
