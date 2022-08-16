namespace SharpMeasures.Generators.Vectors.Pipelines.IndividualVector.Common;

using SharpMeasures.Generators.Raw.Units.UnitInstances;
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

    public IRawUnitInstance? DefaultUnit { get; }
    public string? DefaultUnitSymbol { get; }

    public IIndividualVectorDocumentationStrategy Documentation { get; }

    public DataModel(DefinedType vector, int dimension, NamedType? scalar, NamedType? squaredScalar, NamedType unit, NamedType unitQuantity, string unitParameterName,
        IRawUnitInstance? defaultUnit, string? defaultUnitSymbol, IIndividualVectorDocumentationStrategy documentation)
    {
        Vector = vector;

        Dimension = dimension;

        Scalar = scalar;
        SquaredScalar = squaredScalar;

        Unit = unit;
        UnitQuantity = unitQuantity;
        UnitParameterName = unitParameterName;

        DefaultUnit = defaultUnit;
        DefaultUnitSymbol = defaultUnitSymbol;

        Documentation = documentation;
    }
}
