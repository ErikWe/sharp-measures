namespace SharpMeasures.Generators.Units.ForeignUnitParsing.Diagnostics;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Units.Parsing.SharpMeasuresUnit;

internal class EmptySharpMeasuresUnitProcessingDiagnostics : ISharpMeasuresUnitProcessingDiagnostics
{
    public static EmptySharpMeasuresUnitProcessingDiagnostics Instance { get; } = new();

    Diagnostic? ISharpMeasuresUnitProcessingDiagnostics.NullQuantity(IProcessingContext context, RawSharpMeasuresUnitDefinition definition) => null;
}
