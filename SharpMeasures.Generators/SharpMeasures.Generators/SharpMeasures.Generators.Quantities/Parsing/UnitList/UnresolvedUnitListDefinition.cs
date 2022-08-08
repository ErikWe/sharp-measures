namespace SharpMeasures.Generators.Quantities.Parsing.UnitList;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;
using SharpMeasures.Generators.Unresolved.Quantities;

using System.Collections.Generic;

public record class UnresolvedUnitListDefinition : AItemListDefinition<string, UnitListLocations>, IUnresolvedUnitList
{
    public IReadOnlyList<string> Units => Items;

    public UnresolvedUnitListDefinition(IReadOnlyList<string> units, UnitListLocations locations, IReadOnlyList<int> locationMap) : base(units, locations, locationMap) { }
}
