namespace SharpMeasures.Generators.Attributes.Parsing.ItemLists;

public interface IItemListParsingData : IAttributeParsingData
{
    new public abstract IItemListLocations Locations { get; }
}