namespace SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

public record class ExcludeUnitsDefinition : AItemListDefinition<string, ExcludeUnitsLocations>, IUnitList
{
    public IReadOnlyList<string> ExcludedUnits => Items;

    [SuppressMessage("Design", "CA1033", Justification = "Available under another name")]
    IReadOnlyList<string> IUnitList.Units => ExcludedUnits;

    public ExcludeUnitsDefinition(IReadOnlyList<string> excludedUnits, ExcludeUnitsLocations locations, IReadOnlyList<int> locationMap) : base(excludedUnits, locations, locationMap) { }
}
