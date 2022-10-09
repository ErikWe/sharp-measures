namespace SharpMeasures.Generators.Scalars.SourceBuilding.Common;

internal readonly record struct DataModel
{
    public DefinedType Scalar { get; }

    public NamedType Unit { get; }
    public NamedType UnitQuantity { get; }
    public string UnitParameterName { get; }

    public bool UseUnitBias { get; }

    public string? DefaultUnitInstanceName { get; }
    public string? DefaultUnitInstanceSymbol { get; }

    public SourceBuildingContext SourceBuildingContext { get; }

    public DataModel(DefinedType scalar, NamedType unit, NamedType unitQuantity, string unitParameterName, bool useUnitBias, string? defaultUnitInstanceName, string? defaultUnitInstanceSymbol, SourceBuildingContext sourceBuildingContext)
    {
        Scalar = scalar;

        Unit = unit;
        UnitQuantity = unitQuantity;
        UnitParameterName = unitParameterName;

        UseUnitBias = useUnitBias;

        DefaultUnitInstanceName = defaultUnitInstanceName;
        DefaultUnitInstanceSymbol = defaultUnitInstanceSymbol;

        SourceBuildingContext = sourceBuildingContext;
    }
}
