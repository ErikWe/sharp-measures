namespace SharpMeasures.Generators.Scalars.Parsing.Diagnostics.Processing;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Scalars.Parsing.ScalarConstant;
using SharpMeasures.Generators.Quantities.Parsing.Diagnostics.Processing;

internal class ScalarConstantProcessingDiagnostics : QuantityConstantProcessingDiagnostics<RawScalarConstantDefinition, ScalarConstantLocations>
{
    public ScalarConstantProcessingDiagnostics(NamedType unit) : base(unit) { }

    public ScalarConstantProcessingDiagnostics() : base() { }
}
