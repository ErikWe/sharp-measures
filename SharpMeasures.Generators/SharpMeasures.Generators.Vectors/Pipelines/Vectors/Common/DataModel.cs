namespace SharpMeasures.Generators.Vectors.Pipelines.Vectors.Common;

using SharpMeasures.Generators.Vectors.Documentation;

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

    public IVectorDocumentationStrategy Documentation { get; }

    public DataModel(DefinedType vector, int dimension, NamedType unit, NamedType unitQuantity, string unitParameterName, NamedType? scalar, string? defaultUnitInstanceName, string? defaultUnitInstanceSymbol, IVectorDocumentationStrategy documentation)
    {
        Vector = vector;
        Dimension = dimension;

        Unit = unit;
        UnitQuantity = unitQuantity;
        UnitParameterName = unitParameterName;

        Scalar = scalar;

        DefaultUnitInstanceName = defaultUnitInstanceName;
        DefaultUnitInstanceSymbol = defaultUnitInstanceSymbol;

        Documentation = documentation;
    }
}
