namespace SharpMeasures.Generators.Vectors.Diagnostics;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVector;
using SharpMeasures.Generators.Vectors.Refinement.SharpMeasuresVector;

internal class SharpMeasuresVectorDiagnostics : ISharpMeasuresVectorProcessingDiagnostics, ISharpMeasuresVectorRefinementDiagnostics
{
    public static SharpMeasuresVectorDiagnostics Instance { get; } = new();

    private SharpMeasuresVectorDiagnostics() { }

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
        return DiagnosticConstruction.InvalidVectorDimension(definition.Locations.Dimension?.AsRoslynLocation(), definition.Dimension);
    }

    public Diagnostic InvalidInterpretedDimension(IProcessingContext context, RawSharpMeasuresVectorDefinition definition, int dimension)
    {
        return DiagnosticConstruction.InvalidInterpretedVectorDimension(definition.Locations.AttributeName.AsRoslynLocation(), context.Type.Name, dimension);
    }

    public Diagnostic MissingDimension(IProcessingContext context, RawSharpMeasuresVectorDefinition definition)
    {
        return DiagnosticConstruction.MissingVectorDimension(definition.Locations.AttributeName.AsRoslynLocation(), context.Type.Name);
    }

    public Diagnostic NullDifferenceQuantity(IProcessingContext context, RawSharpMeasuresVectorDefinition definition)
    {
        return DiagnosticConstruction.NullTypeNotVector(definition.Locations.Difference?.AsRoslynLocation());
    }

    public Diagnostic DifferenceDisabledButQuantitySpecified(IProcessingContext context, RawSharpMeasuresVectorDefinition definition)
    {
        return DiagnosticConstruction.DifferenceDisabledButQuantitySpecified(definition.Locations.Difference?.AsRoslynLocation(), context.Type.Name);
    }

    public Diagnostic NullDefaultUnit(IProcessingContext context, RawSharpMeasuresVectorDefinition definition)
    {
        return DiagnosticConstruction.NullUnrecognizedUnitName(definition.Locations.DefaultUnitName?.AsRoslynLocation(), definition.Unit!.Value.Name);
    }

    public Diagnostic EmptyDefaultUnit(IProcessingContext context, RawSharpMeasuresVectorDefinition definition) => NullDefaultUnit(context, definition);

    public Diagnostic NullDefaultSymbol(IProcessingContext context, RawSharpMeasuresVectorDefinition definition)
    {
        return DiagnosticConstruction.DefineQuantityDefaultSymbol(definition.Locations.DefaultUnitSymbol?.AsRoslynLocation());
    }

    public Diagnostic EmptyDefaultSymbol(IProcessingContext context, RawSharpMeasuresVectorDefinition definition) => NullDefaultUnit(context, definition);

    public Diagnostic SetDefaultSymbolButNotUnit(IProcessingContext context, RawSharpMeasuresVectorDefinition definition)
    {
        return DiagnosticConstruction.DefineQuantityDefaultUnit(definition.Locations.DefaultUnitSymbol?.AsRoslynLocation());
    }

    public Diagnostic SetDefaultUnitButNotSymbol(IProcessingContext context, RawSharpMeasuresVectorDefinition definition)
    {
        return DiagnosticConstruction.DefineQuantityDefaultSymbol(definition.Locations.DefaultUnitName?.AsRoslynLocation());
    }

    public Diagnostic TypeAlreadyUnit(ISharpMeasuresVectorRefinementContext context, SharpMeasuresVectorDefinition definition)
    {
        return DiagnosticConstruction.VectorTypeAlreadyDefinedAsUnit(definition.Locations.AttributeName.AsRoslynLocation(), context.Type.Name);
    }

    public Diagnostic TypeAlreadyScalar(ISharpMeasuresVectorRefinementContext context, SharpMeasuresVectorDefinition definition)
    {
        return DiagnosticConstruction.VectorTypeAlreadyDefinedAsScalar(definition.Locations.AttributeName.AsRoslynLocation(), context.Type.Name);
    }

    public Diagnostic TypeAlreadyVector(ISharpMeasuresVectorRefinementContext context, SharpMeasuresVectorDefinition definition)
    {
        return DiagnosticConstruction.VectorTypeAlreadyDefinedAsVector(definition.Locations.AttributeName.AsRoslynLocation(), context.Type.Name);
    }

    public Diagnostic TypeNotUnit(ISharpMeasuresVectorRefinementContext context, SharpMeasuresVectorDefinition definition)
    {
        return DiagnosticConstruction.TypeNotUnit(definition.Locations.Unit?.AsRoslynLocation(), definition.Unit.Name);
    }

    public Diagnostic TypeNotScalar(ISharpMeasuresVectorRefinementContext context, SharpMeasuresVectorDefinition definition)
    {
        return DiagnosticConstruction.TypeNotScalar(definition.Locations.Scalar?.AsRoslynLocation(), definition.Scalar!.Value.Name);
    }

    public Diagnostic UnrecognizedDefaultUnit(ISharpMeasuresVectorRefinementContext context, SharpMeasuresVectorDefinition definition)
    {
        return DiagnosticConstruction.UnrecognizedUnitName(definition.Locations.DefaultUnitName?.AsRoslynLocation(), definition.DefaultUnitName!, definition.Unit.Name);
    }

    public Diagnostic DifferenceNotVector(ISharpMeasuresVectorRefinementContext context, SharpMeasuresVectorDefinition definition)
    {
        return DiagnosticConstruction.TypeNotVector(definition.Locations.Difference?.AsRoslynLocation(), definition.Difference.Name);
    }
}
