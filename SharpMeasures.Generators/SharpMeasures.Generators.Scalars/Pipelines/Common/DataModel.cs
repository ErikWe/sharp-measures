namespace SharpMeasures.Generators.Scalars.Pipelines.Common;

using SharpMeasures.Generators.Scalars.Documentation;

internal readonly record struct DataModel
{
    public DefinedType Scalar { get; }

    public NamedType Unit { get; }
    public NamedType UnitQuantity { get; }
    public string UnitParameterName { get; }

    public bool UseUnitBias { get; }

    public string? DefaultUnitInstanceName { get; }
    public string? DefaultUnitInstanceSymbol { get; }

    public IDocumentationStrategy Documentation { get; }

    public DataModel(DefinedType scalar, NamedType unit, NamedType unitQuantity, string unitParameterName, bool useUnitBias, string? defaultUnitInstanceName, string? defaultUnitInstanceSymbol, IDocumentationStrategy documentation)
    {
        Scalar = scalar;

        Unit = unit;
        UnitQuantity = unitQuantity;
        UnitParameterName = unitParameterName;

        UseUnitBias = useUnitBias;

        DefaultUnitInstanceName = defaultUnitInstanceName;
        DefaultUnitInstanceSymbol = defaultUnitInstanceSymbol;

        Documentation = documentation;
    }
}
