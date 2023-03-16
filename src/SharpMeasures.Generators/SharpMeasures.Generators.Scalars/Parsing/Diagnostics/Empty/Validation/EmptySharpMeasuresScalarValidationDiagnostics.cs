namespace SharpMeasures.Generators.Scalars.Parsing.Diagnostics.Empty.Validation;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Scalars.Parsing.SharpMeasuresScalar;
using SharpMeasures.Generators.Units;

internal sealed class EmptySharpMeasuresScalarValidationDiagnostics : ISharpMeasuresScalarValidationDiagnostics
{
    public static EmptySharpMeasuresScalarValidationDiagnostics Instance { get; } = new();

    private EmptySharpMeasuresScalarValidationDiagnostics() { }

    public Diagnostic? TypeAlreadyUnit(ISharpMeasuresScalarValidationContext context, SharpMeasuresScalarDefinition definition) => null;
    public Diagnostic? TypeNotUnit(ISharpMeasuresScalarValidationContext context, SharpMeasuresScalarDefinition definition) => null;
    public Diagnostic? UnitNotIncludingBiasTerm(ISharpMeasuresScalarValidationContext context, SharpMeasuresScalarDefinition definition) => null;
    public Diagnostic? TypeNotVector(ISharpMeasuresScalarValidationContext context, SharpMeasuresScalarDefinition definition) => null;
    public Diagnostic? DifferenceNotScalar(ISharpMeasuresScalarValidationContext context, SharpMeasuresScalarDefinition definition) => null;
    public Diagnostic? UnrecognizedDefaultUnitInstance(IProcessingContext context, IDefaultUnitInstanceDefinition definition, IUnitType unit) => null;
}
