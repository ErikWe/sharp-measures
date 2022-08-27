namespace SharpMeasures.Generators.Vectors.Parsing.Diagnostics.Validation;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.DefaultUnit;
using SharpMeasures.Generators.Quantities.Parsing.Diagnostics.Validation;
using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVector;

internal class SharpMeasuresVectorValidationDiagnostics : ISharpMeasuresVectorValidationDiagnostics
{
    public static SharpMeasuresVectorValidationDiagnostics Instance { get; } = new();

    private SharpMeasuresVectorValidationDiagnostics() { }

    public Diagnostic TypeAlreadyUnit(ISharpMeasuresVectorValidationContext context, SharpMeasuresVectorDefinition definition)
    {
        return DiagnosticConstruction.VectorTypeAlreadyDefinedAsUnit(definition.Locations.AttributeName.AsRoslynLocation(), context.Type.Name);
    }

    public Diagnostic TypeAlreadyScalar(ISharpMeasuresVectorValidationContext context, SharpMeasuresVectorDefinition definition)
    {
        return DiagnosticConstruction.VectorTypeAlreadyDefinedAsScalar(definition.Locations.AttributeName.AsRoslynLocation(), context.Type.Name);
    }

    public Diagnostic TypeNotUnit(ISharpMeasuresVectorValidationContext context, SharpMeasuresVectorDefinition definition)
    {
        return DiagnosticConstruction.TypeNotUnit(definition.Locations.Unit?.AsRoslynLocation(), definition.Unit.Name);
    }

    public Diagnostic TypeNotScalar(ISharpMeasuresVectorValidationContext context, SharpMeasuresVectorDefinition definition)
    {
        return DiagnosticConstruction.TypeNotScalar(definition.Locations.Scalar?.AsRoslynLocation(), definition.Scalar!.Value.Name);
    }

    public Diagnostic DifferenceNotVector(ISharpMeasuresVectorValidationContext context, SharpMeasuresVectorDefinition definition)
    {
        return DiagnosticConstruction.TypeNotVector(definition.Locations.Difference?.AsRoslynLocation(), definition.Difference!.Value.Name);
    }

    public Diagnostic DifferenceVectorInvalidDimension(ISharpMeasuresVectorValidationContext context, SharpMeasuresVectorDefinition definition, int dimension)
    {
        return DiagnosticConstruction.VectorUnexpectedDimension(definition.Locations.Difference?.AsRoslynLocation(), definition.Difference!.Value.Name, definition.Dimension, dimension);
    }

    public Diagnostic DifferenceVectorGroupLacksMatchingDimension(ISharpMeasuresVectorValidationContext context, SharpMeasuresVectorDefinition definition)
    {
        return DiagnosticConstruction.VectorGroupsLacksMemberOfDimension(definition.Locations.Difference?.AsRoslynLocation(), definition.Difference!.Value.Name, definition.Dimension);
    }

    public Diagnostic UnrecognizedDefaultUnit(IProcessingContext context, IDefaultUnitDefinition definition, IUnitType unit)
    {
        return DefaultUnitValidationDiagnostics.Instance.UnrecognizedDefaultUnit(context, definition, unit);
    }
}
