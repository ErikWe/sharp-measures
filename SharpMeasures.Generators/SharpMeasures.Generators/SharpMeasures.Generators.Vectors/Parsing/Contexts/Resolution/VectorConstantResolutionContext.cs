namespace SharpMeasures.Generators.Vectors.Parsing.Contexts.Resolution;

using SharpMeasures.Generators.Raw.Units;
using SharpMeasures.Generators.Vectors.Parsing.VectorConstant;

internal record class VectorConstantResolutionContext : IVectorConstantResolutionContext
{
    public DefinedType Type { get; }

    public IRawUnitType Unit { get; }

    public int Dimension { get; }

    public VectorConstantResolutionContext(DefinedType type, IRawUnitType unit, int dimension)
    {
        Type = type;

        Unit = unit;

        Dimension = dimension;
    }
}
