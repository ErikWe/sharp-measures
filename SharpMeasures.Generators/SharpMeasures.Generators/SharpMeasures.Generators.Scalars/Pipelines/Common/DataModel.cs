namespace SharpMeasures.Generators.Scalars.Pipelines.Common;

using SharpMeasures.Generators.Scalars.Documentation;
using SharpMeasures.Generators.Unresolved.Units.UnitInstances;

internal readonly record struct DataModel
{
    public DefinedType Scalar { get; }

    public NamedType Unit { get; }
    public NamedType UnitQuantity { get; }
    public string UnitParameterName { get; }

    public bool UseUnitBias { get; }

    public IUnresolvedUnitInstance? DefaultUnit { get; }
    public string? DefaultUnitSymbol { get; }

    public IDocumentationStrategy Documentation { get; }

    public DataModel(DefinedType scalar, NamedType unit, NamedType unitQuantity, string unitParameterName, bool useUnitBias, IUnresolvedUnitInstance? defaultUnit,
        string? defaultUnitSymbol, IDocumentationStrategy documentation)
    {
        Scalar = scalar;

        Unit = unit;
        UnitQuantity = unitQuantity;
        UnitParameterName = unitParameterName;

        UseUnitBias = useUnitBias;

        DefaultUnit = defaultUnit;
        DefaultUnitSymbol = defaultUnitSymbol;

        Documentation = documentation;
    }
}
