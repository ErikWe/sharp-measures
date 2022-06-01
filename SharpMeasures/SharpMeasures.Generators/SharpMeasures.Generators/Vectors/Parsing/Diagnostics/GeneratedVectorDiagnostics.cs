﻿namespace SharpMeasures.Generators.Vectors.Parsing.Diagnostics;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Attributes.Parsing.Vectors;
using SharpMeasures.Generators.Diagnostics;

internal class GeneratedVectorDiagnostics : IGeneratedVectorDiagnostics
{
    public static GeneratedVectorDiagnostics Instance { get; } = new();

    private GeneratedVectorDiagnostics() { }

    public Diagnostic NullUnit(IProcessingContext context, RawGeneratedVectorDefinition definition)
    {
        return DiagnosticConstruction.TypeNotUnit_Null(definition.Locations.Unit?.AsRoslynLocation());
    }

    public Diagnostic NullScalar(IProcessingContext context, RawGeneratedVectorDefinition definition)
    {
        return DiagnosticConstruction.TypeNotScalar_Null(definition.Locations.Scalar?.AsRoslynLocation());
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
        return DiagnosticConstruction.TypeNotUnit_Null(definition.Locations.DefaultUnitName?.AsRoslynLocation());
    }

    public Diagnostic EmptyDefaultUnit(IProcessingContext context, RawGeneratedVectorDefinition definition) => NullDefaultUnit(context, definition);

    public Diagnostic SetDefaultSymbolButNotUnit(IProcessingContext context, RawGeneratedVectorDefinition definition)
    {
        return DiagnosticConstruction.DefineQuantityUnitAndSymbol_MissingSymbol(definition.Locations.DefaultUnitName?.AsRoslynLocation());
    }
}
