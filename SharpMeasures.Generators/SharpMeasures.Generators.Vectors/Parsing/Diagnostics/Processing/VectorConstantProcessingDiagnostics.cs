namespace SharpMeasures.Generators.Vectors.Parsing.Diagnostics.Processing;

using SharpMeasures.Generators.Quantities.Parsing.Diagnostics.Processing;
using SharpMeasures.Generators.Vectors.Parsing.VectorConstant;

internal class VectorConstantProcessingDiagnostics : QuantityConstantProcessingDiagnostics<RawVectorConstantDefinition, VectorConstantLocations>
{
    public VectorConstantProcessingDiagnostics(NamedType unit) : base(unit) { }

    public VectorConstantProcessingDiagnostics() : base() { }
}
