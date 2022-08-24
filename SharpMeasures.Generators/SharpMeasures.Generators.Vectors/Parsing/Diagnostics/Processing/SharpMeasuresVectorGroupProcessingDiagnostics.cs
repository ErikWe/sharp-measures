namespace SharpMeasures.Generators.Vectors.Parsing.Diagnostics.Processing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.DefaultUnit;
using SharpMeasures.Generators.Quantities.Parsing.Diagnostics.Processing;
using SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVectorGroup;

internal class SharpMeasuresVectorGroupProcessingDiagnostics : ISharpMeasuresVectorGroupProcessingDiagnostics
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

    public Diagnostic NullDefaultUnit(IProcessingContext context, IDefaultUnitDefinition definition)
    {
        return DefaultUnitProcessingDiagnostics.Instance.NullDefaultUnit(context, definition);
    }

    public Diagnostic EmptyDefaultUnit(IProcessingContext context, IDefaultUnitDefinition definition)
    {
        return DefaultUnitProcessingDiagnostics.Instance.EmptyDefaultUnit(context, definition);
    }

    public Diagnostic SetDefaultSymbolButNotUnit(IProcessingContext context, IDefaultUnitDefinition definition)
    {
        return DefaultUnitProcessingDiagnostics.Instance.SetDefaultSymbolButNotUnit(context, definition);
    }

    public Diagnostic SetDefaultUnitButNotSymbol(IProcessingContext context, IDefaultUnitDefinition definition)
    {
        return DefaultUnitProcessingDiagnostics.Instance.SetDefaultUnitButNotSymbol(context, definition);
    }
}
