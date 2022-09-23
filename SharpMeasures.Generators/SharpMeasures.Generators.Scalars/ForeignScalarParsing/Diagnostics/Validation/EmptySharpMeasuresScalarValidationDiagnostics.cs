namespace SharpMeasures.Generators.Scalars.ForeignScalarParsing.Diagnostics.Validation;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Quantities.Parsing.DefaultUnitInstance;
using SharpMeasures.Generators.Scalars.Parsing.SharpMeasuresScalar;
using SharpMeasures.Generators.Units;

internal sealed class EmptySharpMeasuresScalarValidationDiagnostics : ISharpMeasuresScalarValidationDiagnostics
{
    public static EmptySharpMeasuresScalarValidationDiagnostics Instance { get; } = new();

    private EmptySharpMeasuresScalarValidationDiagnostics() { }

    Diagnostic? ISharpMeasuresScalarValidationDiagnostics.TypeAlreadyUnit(ISharpMeasuresScalarValidationContext context, SharpMeasuresScalarDefinition definition) => null;
    Diagnostic? ISharpMeasuresScalarValidationDiagnostics.TypeNotUnit(ISharpMeasuresScalarValidationContext context, SharpMeasuresScalarDefinition definition) => null;
    Diagnostic? ISharpMeasuresScalarValidationDiagnostics.UnitNotIncludingBiasTerm(ISharpMeasuresScalarValidationContext context, SharpMeasuresScalarDefinition definition) => null;
    Diagnostic? ISharpMeasuresScalarValidationDiagnostics.TypeNotVector(ISharpMeasuresScalarValidationContext context, SharpMeasuresScalarDefinition definition) => null;
    Diagnostic? ISharpMeasuresScalarValidationDiagnostics.DifferenceNotScalar(ISharpMeasuresScalarValidationContext context, SharpMeasuresScalarDefinition definition) => null;
    Diagnostic? IDefaultUnitInstanceValidationDiagnostics.UnrecognizedDefaultUnitInstance(IProcessingContext context, IDefaultUnitInstanceDefinition definition, IUnitType unit) => null;
}
