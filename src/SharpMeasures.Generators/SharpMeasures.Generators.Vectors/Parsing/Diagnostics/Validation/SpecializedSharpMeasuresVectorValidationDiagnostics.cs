﻿namespace SharpMeasures.Generators.Vectors.Parsing.Diagnostics.Validation;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Quantities.Parsing.Diagnostics.Validation;
using SharpMeasures.Generators.Vectors.Parsing.SpecializedSharpMeasuresVector;

internal sealed class SpecializedSharpMeasuresVectorValidationDiagnostics : ISpecializedSharpMeasuresVectorValidationDiagnostics
{
    public static SpecializedSharpMeasuresVectorValidationDiagnostics Instance { get; } = new();

    private SpecializedSharpMeasuresVectorValidationDiagnostics() { }

    public Diagnostic TypeAlreadyUnit(ISpecializedSharpMeasuresVectorValidationContext context, SpecializedSharpMeasuresVectorDefinition definition)
    {
        return DiagnosticConstruction.VectorTypeAlreadyDefinedAsUnit(definition.Locations.AttributeName.AsRoslynLocation(), context.Type.Name);
    }

    public Diagnostic TypeAlreadyScalar(ISpecializedSharpMeasuresVectorValidationContext context, SpecializedSharpMeasuresVectorDefinition definition)
    {
        return DiagnosticConstruction.VectorTypeAlreadyDefinedAsScalar(definition.Locations.AttributeName.AsRoslynLocation(), context.Type.Name);
    }

    public Diagnostic TypeAlreadyVectorBase(ISpecializedSharpMeasuresVectorValidationContext context, SpecializedSharpMeasuresVectorDefinition definition)
    {
        return DiagnosticConstruction.VectorTypeAlreadyDefinedAsVector(definition.Locations.AttributeName.AsRoslynLocation(), context.Type.Name);
    }

    public Diagnostic OriginalNotVector(ISpecializedSharpMeasuresVectorValidationContext context, SpecializedSharpMeasuresVectorDefinition definition)
    {
        return DiagnosticConstruction.TypeNotVector(definition.Locations.OriginalQuantity?.AsRoslynLocation(), definition.OriginalQuantity.Name);
    }

    public Diagnostic RootVectorNotResolved(ISpecializedSharpMeasuresVectorValidationContext context, SpecializedSharpMeasuresVectorDefinition definition)
    {
        return DiagnosticConstruction.QuantityGroupMissingRoot<VectorQuantityAttribute>(definition.Locations.AttributeName.AsRoslynLocation());
    }

    public Diagnostic VectorNameAndDimensionConflict(ISpecializedSharpMeasuresVectorValidationContext context, SpecializedSharpMeasuresVectorDefinition definition, int interpretedDimension, int inheritedDimension)
    {
        return DiagnosticConstruction.VectorNameAndDimensionConflict(definition.Locations.AttributeName.AsRoslynLocation(), context.Type.Name, interpretedDimension, inheritedDimension);
    }

    public Diagnostic TypeNotScalar(ISpecializedSharpMeasuresVectorValidationContext context, SpecializedSharpMeasuresVectorDefinition definition)
    {
        return DiagnosticConstruction.TypeNotScalar(definition.Locations.Scalar?.AsRoslynLocation(), definition.Scalar!.Value.Name);
    }

    public Diagnostic DifferenceNotVector(ISpecializedSharpMeasuresVectorValidationContext context, SpecializedSharpMeasuresVectorDefinition definition)
    {
        return DiagnosticConstruction.TypeNotVector(definition.Locations.Difference?.AsRoslynLocation(), definition.Difference!.Value.Name);
    }

    public Diagnostic DifferenceVectorInvalidDimension(ISpecializedSharpMeasuresVectorValidationContext context, SpecializedSharpMeasuresVectorDefinition definition, int expectedDimension, int actualDimension)
    {
        return DiagnosticConstruction.VectorUnexpectedDimension(definition.Locations.Difference?.AsRoslynLocation(), definition.Difference!.Value.Name, expectedDimension, actualDimension);
    }

    public Diagnostic DifferenceVectorGroupLacksMatchingDimension(ISpecializedSharpMeasuresVectorValidationContext context, SpecializedSharpMeasuresVectorDefinition definition, int dimension)
    {
        return DiagnosticConstruction.VectorGroupsLacksMemberOfDimension(definition.Locations.Difference?.AsRoslynLocation(), definition.Difference!.Value.Name, dimension);
    }

    public Diagnostic UnrecognizedDefaultUnitInstance(IProcessingContext context, IDefaultUnitInstanceDefinition definition, IUnitType unit)
    {
        return DefaultUnitInstanceValidationDiagnostics.Instance.UnrecognizedDefaultUnitInstance(context, definition, unit);
    }
}
