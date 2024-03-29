﻿namespace SharpMeasures.Generators.Scalars.Parsing.Diagnostics.Validation;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Quantities.Parsing.Diagnostics.Validation;
using SharpMeasures.Generators.Scalars.Parsing.SpecializedSharpMeasuresScalar;
using SharpMeasures.Generators.Units;

internal sealed class SpecializedSharpMeasuresScalarValidationDiagnostics : ISpecializedSharpMeasuresScalarValidationDiagnostics
{
    public static SpecializedSharpMeasuresScalarValidationDiagnostics Instance { get; } = new();

    private SpecializedSharpMeasuresScalarValidationDiagnostics() { }

    public Diagnostic TypeAlreadyUnit(ISpecializedSharpMeasuresScalarValidationContext context, SpecializedSharpMeasuresScalarDefinition definition)
    {
        return DiagnosticConstruction.ScalarTypeAlreadyDefinedAsUnit(definition.Locations.AttributeName.AsRoslynLocation(), context.Type.Name);
    }

    public Diagnostic TypeAlreadyScalarBase(ISpecializedSharpMeasuresScalarValidationContext context, SpecializedSharpMeasuresScalarDefinition definition)
    {
        return DiagnosticConstruction.ScalarTypeAlreadyDefinedAsScalar(definition.Locations.AttributeName.AsRoslynLocation(), context.Type.Name);
    }

    public Diagnostic OriginalNotScalar(ISpecializedSharpMeasuresScalarValidationContext context, SpecializedSharpMeasuresScalarDefinition definition)
    {
        return DiagnosticConstruction.TypeNotScalar(definition.Locations.OriginalQuantity?.AsRoslynLocation(), definition.OriginalQuantity.Name);
    }

    public Diagnostic RootScalarNotResolved(ISpecializedSharpMeasuresScalarValidationContext context, SpecializedSharpMeasuresScalarDefinition definition)
    {
        return DiagnosticConstruction.QuantityGroupMissingRoot<ScalarQuantityAttribute>(definition.Locations.AttributeName.AsRoslynLocation());
    }

    public Diagnostic TypeNotVector(ISpecializedSharpMeasuresScalarValidationContext context, SpecializedSharpMeasuresScalarDefinition definition)
    {
        return DiagnosticConstruction.TypeNotVector(definition.Locations.Vector?.AsRoslynLocation(), definition.Vector!.Value.Name);
    }

    public Diagnostic DifferenceNotScalar(ISpecializedSharpMeasuresScalarValidationContext context, SpecializedSharpMeasuresScalarDefinition definition)
    {
        return DiagnosticConstruction.TypeNotScalar(definition.Locations.Difference?.AsRoslynLocation(), definition.Difference!.Value.Name);
    }

    public Diagnostic UnrecognizedDefaultUnitInstance(IProcessingContext context, IDefaultUnitInstanceDefinition definition, IUnitType unit)
    {
        return DefaultUnitInstanceValidationDiagnostics.Instance.UnrecognizedDefaultUnitInstance(context, definition, unit);
    }
}
