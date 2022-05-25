namespace SharpMeasures.Generators.Attributes.Parsing.ItemLists;

public abstract record class AItemListParsingData<TLocations> : AAttributeParsingData<TLocations>, IItemListParsingData
    where TLocations : AItemListLocations
{
    IItemListLocations IItemListParsingData.Locations => Locations;

    protected AItemListParsingData(TLocations locations) : base(locations) { }
}
