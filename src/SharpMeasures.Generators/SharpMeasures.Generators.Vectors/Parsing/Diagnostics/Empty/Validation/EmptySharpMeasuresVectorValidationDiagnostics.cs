namespace SharpMeasures.Generators.Vectors.Parsing.Diagnostics.Empty.Validation;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVector;

internal sealed class EmptySharpMeasuresVectorValidationDiagnostics : ISharpMeasuresVectorValidationDiagnostics
{
    public static EmptySharpMeasuresVectorValidationDiagnostics Instance { get; } = new();

    private EmptySharpMeasuresVectorValidationDiagnostics() { }

    public Diagnostic? TypeAlreadyUnit(ISharpMeasuresVectorValidationContext context, SharpMeasuresVectorDefinition definition) => null;
    public Diagnostic? TypeAlreadyScalar(ISharpMeasuresVectorValidationContext context, SharpMeasuresVectorDefinition definition) => null;
    public Diagnostic? TypeNotUnit(ISharpMeasuresVectorValidationContext context, SharpMeasuresVectorDefinition definition) => null;
    public Diagnostic? TypeNotScalar(ISharpMeasuresVectorValidationContext context, SharpMeasuresVectorDefinition definition) => null;
    public Diagnostic? DifferenceNotVector(ISharpMeasuresVectorValidationContext context, SharpMeasuresVectorDefinition definition) => null;
    public Diagnostic? DifferenceVectorInvalidDimension(ISharpMeasuresVectorValidationContext context, SharpMeasuresVectorDefinition definition, int dimension) => null;
    public Diagnostic? DifferenceVectorGroupLacksMatchingDimension(ISharpMeasuresVectorValidationContext context, SharpMeasuresVectorDefinition definition) => null;
    public Diagnostic? UnrecognizedDefaultUnitInstance(IProcessingContext context, IDefaultUnitInstanceDefinition definition, IUnitType unit) => null;
}
