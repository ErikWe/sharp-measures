namespace SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;

using System.Collections.Generic;

public record class ExcludeUnitsDefinition : AItemListDefinition<string, ExcludeUnitsLocations>, IExcludeUnits
{
    public IReadOnlyList<string> ExcludedUnits => Items;

    public ExcludeUnitsDefinition(IReadOnlyList<string> excludedUnits, ExcludeUnitsLocations locations) : base(excludedUnits, locations) { }
}
