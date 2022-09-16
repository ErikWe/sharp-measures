namespace SharpMeasures.Generators.Scalars.Parsing.ExcludeUnitBases;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;
using SharpMeasures.Generators.Quantities;

using System.Collections.Generic;

internal record class ExcludeUnitBasesDefinition : AItemListDefinition<string, ExcludeUnitBasesLocations>, IUnitInstanceList
{
    public IReadOnlyList<string> UnitInstances => Items;

    IUnitInstanceListLocations IUnitInstanceList.Locations => Locations;

    public ExcludeUnitBasesDefinition(IReadOnlyList<string> unitInstances, ExcludeUnitBasesLocations locations, IReadOnlyList<int> locationMap) : base(unitInstances, locations, locationMap) { }
}
