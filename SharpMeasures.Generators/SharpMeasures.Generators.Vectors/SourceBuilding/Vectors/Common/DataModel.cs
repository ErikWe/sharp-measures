namespace SharpMeasures.Generators.Vectors.SourceBuilding.Vectors.Common;

internal readonly record struct DataModel
{
    public DefinedType Vector { get; }
    public int Dimension { get; }

    public NamedType Unit { get; }
    public NamedType UnitQuantity { get; }
    public string UnitParameterName { get; }

    public NamedType? Scalar { get; }

    public string? DefaultUnitInstanceName { get; }
    public string? DefaultUnitInstanceSymbol { get; }

    public VectorSourceBuildingContext SourceBuildingContext { get; }

    public DataModel(DefinedType vector, int dimension, NamedType unit, NamedType unitQuantity, string unitParameterName, NamedType? scalar, string? defaultUnitInstanceName, string? defaultUnitInstanceSymbol, VectorSourceBuildingContext sourceBuildingContext)
    {
        Vector = vector;
        Dimension = dimension;

        Unit = unit;
        UnitQuantity = unitQuantity;
        UnitParameterName = unitParameterName;

        Scalar = scalar;

        DefaultUnitInstanceName = defaultUnitInstanceName;
        DefaultUnitInstanceSymbol = defaultUnitInstanceSymbol;

        SourceBuildingContext = sourceBuildingContext;
    }
}
