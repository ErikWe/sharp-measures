namespace SharpMeasures.Generators.Vectors.Parsing.Contexts.Resolution;

using SharpMeasures.Generators.Unresolved.Units;
using SharpMeasures.Generators.Vectors.Parsing.VectorConstant;

internal record class VectorConstantResolutionContext : IVectorConstantResolutionContext
{
    public DefinedType Type { get; }

    public IUnresolvedUnitType Unit { get; }

    public int Dimension { get; }

    public VectorConstantResolutionContext(DefinedType type, IUnresolvedUnitType unit, int dimension)
    {
        Type = type;

        Unit = unit;

        Dimension = dimension;
    }
}
