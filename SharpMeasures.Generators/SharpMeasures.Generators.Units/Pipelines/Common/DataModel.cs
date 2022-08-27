namespace SharpMeasures.Generators.Units.Pipelines.Common;

using SharpMeasures.Generators.Units.Documentation;

internal readonly record struct DataModel
{
    public DefinedType Unit { get; }
    public NamedType Quantity { get; }

    public bool BiasTerm { get; }
    public string QuantityParameterName { get; }

    public IDocumentationStrategy Documentation { get; }

    public DataModel(DefinedType unit, NamedType quantity, bool biasTerm, string quantityParameterName, IDocumentationStrategy documentation)
    {
        Unit = unit;
        Quantity = quantity;

        BiasTerm = biasTerm;
        QuantityParameterName = quantityParameterName;

        Documentation = documentation;
    }
}
