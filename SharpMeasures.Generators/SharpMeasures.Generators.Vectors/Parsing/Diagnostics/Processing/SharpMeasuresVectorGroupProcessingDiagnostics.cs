namespace SharpMeasures.Generators.Vectors.Parsing.Diagnostics.Processing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Quantities.Parsing.Diagnostics.Processing;
using SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVectorGroup;

internal sealed class SharpMeasuresVectorGroupProcessingDiagnostics : ISharpMeasuresVectorGroupProcessingDiagnostics
{
    public static SharpMeasuresVectorGroupProcessingDiagnostics Instance { get; } = new();

    private SharpMeasuresVectorGroupProcessingDiagnostics() { }

    public Diagnostic NameSuggestsDimension(IProcessingContext context, RawSharpMeasuresVectorGroupDefinition definition, int interpretedDImension)
    {
        return DiagnosticConstruction.VectorGroupNameSuggestsDimension(definition.Locations.AttributeName.AsRoslynLocation(), context.Type.Name, interpretedDImension);
    }

    public Diagnostic NullUnit(IProcessingContext context, RawSharpMeasuresVectorGroupDefinition definition)
    {
        return DiagnosticConstruction.NullTypeNotUnit(definition.Locations.Unit?.AsRoslynLocation());
    }

    public Diagnostic NullScalar(IProcessingContext context, RawSharpMeasuresVectorGroupDefinition definition)
    {
        return DiagnosticConstruction.NullTypeNotScalar(definition.Locations.Scalar?.AsRoslynLocation());
    }

    public Diagnostic NullDifferenceQuantity(IProcessingContext context, RawSharpMeasuresVectorGroupDefinition definition)
    {
        return DiagnosticConstruction.NullTypeNotVectorGroup(definition.Locations.Difference?.AsRoslynLocation());
    }

    public Diagnostic DifferenceDisabledButQuantitySpecified(IProcessingContext context, RawSharpMeasuresVectorGroupDefinition definition)
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
