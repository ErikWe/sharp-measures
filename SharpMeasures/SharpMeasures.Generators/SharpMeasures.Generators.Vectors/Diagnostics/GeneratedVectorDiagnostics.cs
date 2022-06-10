namespace SharpMeasures.Generators.Vectors.Diagnostics;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Vectors.Parsing.GeneratedVector;
using SharpMeasures.Generators.Vectors.Refinement;

internal class GeneratedVectorDiagnostics : IGeneratedVectorProcessingDiagnostics, IGeneratedVectorRefinementDiagnostics
{
    public static GeneratedVectorDiagnostics Instance { get; } = new();

    private GeneratedVectorDiagnostics() { }

    public Diagnostic NullUnit(IProcessingContext context, RawGeneratedVectorDefinition definition)
    {
        return DiagnosticConstruction.NullTypeNotUnit(definition.Locations.Unit?.AsRoslynLocation());
    }

    public Diagnostic NullScalar(IProcessingContext context, RawGeneratedVectorDefinition definition)
    {
        return DiagnosticConstruction.NullTypeNotScalar(definition.Locations.Scalar?.AsRoslynLocation());
    }

    public Diagnostic InvalidDimension(IProcessingContext context, RawGeneratedVectorDefinition definition)
    {
        return DiagnosticConstruction.InvalidVectorDimension(definition.Locations.Dimension?.AsRoslynLocation(), definition.Dimension);
    }

    public Diagnostic MissingDimension(IProcessingContext context, RawGeneratedVectorDefinition definition)
    {
        return DiagnosticConstruction.MissingVectorDimension(definition.Locations.Attribute.AsRoslynLocation(), context.Type.Name);
    }

    public Diagnostic NullDefaultUnit(IProcessingContext context, RawGeneratedVectorDefinition definition)
    {
        return DiagnosticConstruction.NullUnrecognizedUnitName(definition.Locations.DefaultUnitName?.AsRoslynLocation(), definition.Unit!.Value.Name);
    }

    public Diagnostic EmptyDefaultUnit(IProcessingContext context, RawGeneratedVectorDefinition definition) => NullDefaultUnit(context, definition);

    public Diagnostic NullDefaultSymbol(IProcessingContext context, RawGeneratedVectorDefinition definition)
    {
        return DiagnosticConstruction.DefineQuantityDefaultSymbol(definition.Locations.DefaultUnitSymbol?.AsRoslynLocation());
    }

    public Diagnostic EmptyDefaultSymbol(IProcessingContext context, RawGeneratedVectorDefinition definition) => NullDefaultUnit(context, definition);

    public Diagnostic SetDefaultSymbolButNotUnit(IProcessingContext context, RawGeneratedVectorDefinition definition)
    {
        return DiagnosticConstruction.DefineQuantityDefaultUnit(definition.Locations.DefaultUnitSymbol?.AsRoslynLocation());
    }

    public Diagnostic SetDefaultUnitButNotSymbol(IProcessingContext context, RawGeneratedVectorDefinition definition)
    {
        return DiagnosticConstruction.DefineQuantityDefaultSymbol(definition.Locations.DefaultUnitName?.AsRoslynLocation());
    }

    public Diagnostic TypeAlreadyUnit(IGeneratedVectorRefinementContext context, GeneratedVectorDefinition definition)
    {
        return DiagnosticConstruction.VectorTypeAlreadyDefinedAsUnit(definition.Locations.Attribute.AsRoslynLocation(), context.Type.Name);
    }

    public Diagnostic TypeAlreadyScalar(IGeneratedVectorRefinementContext context, GeneratedVectorDefinition definition)
    {
        return DiagnosticConstruction.VectorTypeAlreadyDefinedAsScalar(definition.Locations.Attribute.AsRoslynLocation(), context.Type.Name);
    }

    public Diagnostic TypeNotUnit(IGeneratedVectorRefinementContext context, GeneratedVectorDefinition definition)
    {
        return DiagnosticConstruction.TypeNotUnit(definition.Locations.Unit?.AsRoslynLocation(), definition.Unit.Name);
    }

    public Diagnostic TypeNotScalar(IGeneratedVectorRefinementContext context, GeneratedVectorDefinition definition)
    {
        return DiagnosticConstruction.TypeNotScalar(definition.Locations.Scalar?.AsRoslynLocation(), definition.Scalar!.Value.Name);
    }

    public Diagnostic UnrecognizedUnit(IGeneratedVectorRefinementContext context, GeneratedVectorDefinition definition)
    {
        return DiagnosticConstruction.UnrecognizedUnitName(definition.Locations.DefaultUnitName?.AsRoslynLocation(), definition.DefaultUnitName!, definition.Unit.Name);
    }
}
