namespace SharpMeasures.Generators.Quantities.Parsing.UnitList;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;
using SharpMeasures.Generators.Raw.Quantities;

using System.Collections.Generic;

public record class RawUnitListDefinition : AItemListDefinition<string, UnitListLocations>, IRawUnitList
{
    public IReadOnlyList<string> Units => Items;

    public RawUnitListDefinition(IReadOnlyList<string> units, UnitListLocations locations, IReadOnlyList<int> locationMap) : base(units, locations, locationMap) { }
}
