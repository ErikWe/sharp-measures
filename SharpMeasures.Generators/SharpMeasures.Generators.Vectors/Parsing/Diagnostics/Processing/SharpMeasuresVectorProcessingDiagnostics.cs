namespace SharpMeasures.Generators.Vectors.Parsing.Diagnostics.Processing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Quantities.Parsing.Diagnostics.Processing;
using SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVector;

internal class SharpMeasuresVectorProcessingDiagnostics : ISharpMeasuresVectorProcessingDiagnostics
{
    public static SharpMeasuresVectorProcessingDiagnostics Instance { get; } = new();

    private SharpMeasuresVectorProcessingDiagnostics() { }

    public Diagnostic NullUnit(IProcessingContext context, RawSharpMeasuresVectorDefinition definition)
    {
        return DiagnosticConstruction.NullTypeNotUnit(definition.Locations.Unit?.AsRoslynLocation());
    }

    public Diagnostic NullScalar(IProcessingContext context, RawSharpMeasuresVectorDefinition definition)
    {
        return DiagnosticConstruction.NullTypeNotScalar(definition.Locations.Scalar?.AsRoslynLocation());
    }

    public Diagnostic InvalidDimension(IProcessingContext context, RawSharpMeasuresVectorDefinition definition)
    {
        return DiagnosticConstruction.InvalidVectorDimension(definition.Locations.Dimension?.AsRoslynLocation(), definition.Dimension!.Value);
    }

    public Diagnostic InvalidInterpretedDimension(IProcessingContext context, RawSharpMeasuresVectorDefinition definition, int dimension)
    {
        return DiagnosticConstruction.InvalidInterpretedVectorDimension(definition.Locations.AttributeName.AsRoslynLocation(), context.Type.Name, dimension);
    }

    public Diagnostic MissingDimension(IProcessingContext context, RawSharpMeasuresVectorDefinition definition)
    {
        return DiagnosticConstruction.MissingVectorDimension(definition.Locations.AttributeName.AsRoslynLocation(), context.Type.Name);
    }

    public Diagnostic VectorNameAndDimensionConflict(IProcessingContext context, RawSharpMeasuresVectorDefinition definition, int interpretedDimension)
    {
        return DiagnosticConstruction.VectorNameAndDimensionConflict(definition.Locations.AttributeName.AsRoslynLocation(), context.Type.Name, interpretedDimension, definition.Dimension!.Value);
    }

    public Diagnostic NullDifferenceQuantity(IProcessingContext context, RawSharpMeasuresVectorDefinition definition)
    {
        return DiagnosticConstruction.NullTypeNotVector(definition.Locations.Difference?.AsRoslynLocation());
    }

    public Diagnostic DifferenceDisabledButQuantitySpecified(IProcessingContext context, RawSharpMeasuresVectorDefinition definition)
    {
        return DiagnosticConstruction.DifferenceDisabledButQuantitySpecified(definition.Locations.Difference?.AsRoslynLocation(), context.Type.Name);
    }

    public Diagnostic NullDefaultUnitInstanceName(IProcessingContext context, IDefaultUnitInstanceDefinition definition)
    {
        return DefaultUnitInstanceProcessingDiagnostics.Instance.NullDefaultUnitInstanceName(context, definition);
    }

    public Diagnostic EmptyDefaultUnitInstanceName(IProcessingContext context, IDefaultUnitInstanceDefinition definition)
    {
        return DefaultUnitInstanceProcessingDiagnostics.Instance.EmptyDefaultUnitInstanceName(context, definition);
    }

    public Diagnostic SetDefaultUnitInstanceSymbolButNotName(IProcessingContext context, IDefaultUnitInstanceDefinition definition)
    {
        return DefaultUnitInstanceProcessingDiagnostics.Instance.SetDefaultUnitInstanceSymbolButNotName(context, definition);
    }

    public Diagnostic SetDefaultUnitInstanceNameButNotSymbol(IProcessingContext context, IDefaultUnitInstanceDefinition definition)
    {
        return DefaultUnitInstanceProcessingDiagnostics.Instance.SetDefaultUnitInstanceNameButNotSymbol(context, definition);
    }
}
