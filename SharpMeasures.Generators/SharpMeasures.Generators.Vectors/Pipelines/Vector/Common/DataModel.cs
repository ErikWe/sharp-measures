namespace SharpMeasures.Generators.Vectors.Pipelines.Vector.Common;

using SharpMeasures.Generators.Vectors.Documentation;

internal readonly record struct DataModel
{
    public DefinedType Vector { get; }
    public int Dimension { get; }

    public NamedType? Scalar { get; }
    public NamedType? SquaredScalar { get; }

    public NamedType Unit { get; }
    public NamedType UnitQuantity { get; }
    public string UnitParameterName { get; }

    public string? DefaultUnitInstanceName { get; }
    public string? DefaultUnitInstanceSymbol { get; }

    public IVectorDocumentationStrategy Documentation { get; }

    public DataModel(DefinedType vector, int dimension, NamedType? scalar, NamedType? squaredScalar, NamedType unit, NamedType unitQuantity, string unitParameterName,
        string? defaultUnitInstanceName, string? defaultUnitInstanceSymbol, IVectorDocumentationStrategy documentation)
    {
        Vector = vector;
        Dimension = dimension;

        Scalar = scalar;
        SquaredScalar = squaredScalar;

        Unit = unit;
        UnitQuantity = unitQuantity;
        UnitParameterName = unitParameterName;

        DefaultUnitInstanceName = defaultUnitInstanceName;
        DefaultUnitInstanceSymbol = defaultUnitInstanceSymbol;

        Documentation = documentation;
    }
}
