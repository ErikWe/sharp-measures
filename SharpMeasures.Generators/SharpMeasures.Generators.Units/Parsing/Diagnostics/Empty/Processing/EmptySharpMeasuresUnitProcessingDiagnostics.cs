namespace SharpMeasures.Generators.Units.Parsing.Diagnostics.Empty.Processing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Units.Parsing.SharpMeasuresUnit;

internal sealed class EmptySharpMeasuresUnitProcessingDiagnostics : ISharpMeasuresUnitProcessingDiagnostics
{
    public static EmptySharpMeasuresUnitProcessingDiagnostics Instance { get; } = new();

    private EmptySharpMeasuresUnitProcessingDiagnostics() { }

    Diagnostic? ISharpMeasuresUnitProcessingDiagnostics.NullQuantity(IProcessingContext context, RawSharpMeasuresUnitDefinition definition) => null;
}
