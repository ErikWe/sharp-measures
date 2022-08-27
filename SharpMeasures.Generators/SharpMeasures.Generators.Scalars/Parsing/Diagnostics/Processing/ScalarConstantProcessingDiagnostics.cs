namespace SharpMeasures.Generators.Scalars.Parsing.Diagnostics.Processing;

using SharpMeasures.Generators.Quantities.Parsing.Diagnostics.Processing;
using SharpMeasures.Generators.Scalars.Parsing.ScalarConstant;

internal class ScalarConstantProcessingDiagnostics : QuantityConstantProcessingDiagnostics<RawScalarConstantDefinition, ScalarConstantLocations>
{
    public ScalarConstantProcessingDiagnostics(NamedType unit) : base(unit) { }

    public ScalarConstantProcessingDiagnostics() : base() { }
}
