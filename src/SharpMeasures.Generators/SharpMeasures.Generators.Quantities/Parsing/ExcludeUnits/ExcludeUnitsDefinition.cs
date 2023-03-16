namespace SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;

using System.Collections.Generic;

public sealed record class ExcludeUnitsDefinition : AItemListDefinition<string, ExcludeUnitsLocations>, IUnitInstanceList
{
    public IReadOnlyList<string> UnitInstances => Items;

    IUnitInstanceListLocations IUnitInstanceList.Locations => Locations;

    public ExcludeUnitsDefinition(IReadOnlyList<string> unitInstances, ExcludeUnitsLocations locations, IReadOnlyList<int> locationMap) : base(unitInstances, locations, locationMap) { }
}
