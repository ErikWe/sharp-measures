namespace SharpMeasures.Generators.Quantities.Parsing.UnitList;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;

using System.Collections.Generic;

public record class UnitListDefinition : AItemListDefinition<string, UnitListLocations>, IUnitList
{
    public IReadOnlyList<string> Units => Items;

    public UnitListDefinition(IReadOnlyList<string> units, UnitListLocations locations, IReadOnlyList<int> locationMap) : base(units, locations, locationMap) { }
}
