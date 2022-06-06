namespace SharpMeasures.Generators.Attributes.Parsing.Quantities;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;

using System.Collections.Generic;

public record class ExcludeUnits : AItemListDefinition<string, ExcludeUnitsLocations>
{
    public IReadOnlyList<string> ExcludedUnits => Items;

    public ExcludeUnits(IReadOnlyList<string> excludedUnits, ExcludeUnitsLocations locations) : base(excludedUnits, locations) { }
}
