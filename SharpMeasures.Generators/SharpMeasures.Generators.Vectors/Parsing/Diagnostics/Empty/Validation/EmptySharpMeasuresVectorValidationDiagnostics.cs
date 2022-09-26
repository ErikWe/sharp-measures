namespace SharpMeasures.Generators.Vectors.Parsing.Diagnostics.Empty.Validation;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Quantities.Parsing.DefaultUnitInstance;
using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVector;

internal sealed class EmptySharpMeasuresVectorValidationDiagnostics : ISharpMeasuresVectorValidationDiagnostics
{
    public static EmptySharpMeasuresVectorValidationDiagnostics Instance { get; } = new();

    private EmptySharpMeasuresVectorValidationDiagnostics() { }

    Diagnostic? ISharpMeasuresVectorValidationDiagnostics.TypeAlreadyUnit(ISharpMeasuresVectorValidationContext context, SharpMeasuresVectorDefinition definition) => null;
    Diagnostic? ISharpMeasuresVectorValidationDiagnostics.TypeAlreadyScalar(ISharpMeasuresVectorValidationContext context, SharpMeasuresVectorDefinition definition) => null;
    Diagnostic? ISharpMeasuresVectorValidationDiagnostics.TypeNotUnit(ISharpMeasuresVectorValidationContext context, SharpMeasuresVectorDefinition definition) => null;
    Diagnostic? ISharpMeasuresVectorValidationDiagnostics.TypeNotScalar(ISharpMeasuresVectorValidationContext context, SharpMeasuresVectorDefinition definition) => null;
    Diagnostic? ISharpMeasuresVectorValidationDiagnostics.DifferenceNotVector(ISharpMeasuresVectorValidationContext context, SharpMeasuresVectorDefinition definition) => null;
    Diagnostic? ISharpMeasuresVectorValidationDiagnostics.DifferenceVectorInvalidDimension(ISharpMeasuresVectorValidationContext context, SharpMeasuresVectorDefinition definition, int dimension) => null;
    Diagnostic? ISharpMeasuresVectorValidationDiagnostics.DifferenceVectorGroupLacksMatchingDimension(ISharpMeasuresVectorValidationContext context, SharpMeasuresVectorDefinition definition) => null;
    Diagnostic? IDefaultUnitInstanceValidationDiagnostics.UnrecognizedDefaultUnitInstance(IProcessingContext context, IDefaultUnitInstanceDefinition definition, IUnitType unit) => null;
}
