namespace SharpMeasures.Generators.Scalars.Parsing.ExcludeBases;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;
using SharpMeasures.Generators.Quantities;

using System.Collections.Generic;

internal record class ExcludeBasesDefinition : AItemListDefinition<string, ExcludeBasesLocations>, IUnitList
{
    public IReadOnlyList<string> ExcludedBases => Items;

    IReadOnlyList<string> IUnitList.Units => ExcludedBases;

    public ExcludeBasesDefinition(IReadOnlyList<string> excludedBases, ExcludeBasesLocations locations, IReadOnlyList<int> locationMap) : base(excludedBases, locations, locationMap) { }
}
