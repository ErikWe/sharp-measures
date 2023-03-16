namespace SharpMeasures.Generators.Units.SourceBuilding.Common;

internal readonly record struct DataModel
{
    public DefinedType Unit { get; }
    public NamedType Quantity { get; }

    public bool BiasTerm { get; }
    public string QuantityParameterName { get; }

    public SourceBuildingContext SourceBuildingContext { get; }

    public DataModel(DefinedType unit, NamedType quantity, bool biasTerm, string quantityParameterName, SourceBuildingContext sourceBuildingContext)
    {
        Unit = unit;
        Quantity = quantity;

        BiasTerm = biasTerm;
        QuantityParameterName = quantityParameterName;

        SourceBuildingContext = sourceBuildingContext;
    }
}
