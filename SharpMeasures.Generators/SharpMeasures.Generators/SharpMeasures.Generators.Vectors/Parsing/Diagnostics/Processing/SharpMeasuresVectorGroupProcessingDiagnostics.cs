namespace SharpMeasures.Generators.Vectors.Parsing.Diagnostics.Processing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVectorGroup;

internal class SharpMeasuresVectorGroupProcessingDiagnostics : ISharpMeasuresVectorGroupProcessingDiagnostics
{
    public static SharpMeasuresVectorGroupProcessingDiagnostics Instance { get; } = new();

    private SharpMeasuresVectorGroupProcessingDiagnostics() { }

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

    public Diagnostic NullDefaultUnit(IProcessingContext context, RawSharpMeasuresVectorGroupDefinition definition)
    {
        return DiagnosticConstruction.NullUnrecognizedUnitName(definition.Locations.DefaultUnitName?.AsRoslynLocation(), definition.Unit!.Value.Name);
    }

    public Diagnostic EmptyDefaultUnit(IProcessingContext context, RawSharpMeasuresVectorGroupDefinition definition) => NullDefaultUnit(context, definition);

    public Diagnostic SetDefaultSymbolButNotUnit(IProcessingContext context, RawSharpMeasuresVectorGroupDefinition definition)
    {
        return DiagnosticConstruction.DefineQuantityDefaultUnit(definition.Locations.DefaultUnitSymbol?.AsRoslynLocation());
    }

    public Diagnostic SetDefaultUnitButNotSymbol(IProcessingContext context, RawSharpMeasuresVectorGroupDefinition definition)
    {
        return DiagnosticConstruction.DefineQuantityDefaultSymbol(definition.Locations.DefaultUnitName?.AsRoslynLocation());
    }
}
