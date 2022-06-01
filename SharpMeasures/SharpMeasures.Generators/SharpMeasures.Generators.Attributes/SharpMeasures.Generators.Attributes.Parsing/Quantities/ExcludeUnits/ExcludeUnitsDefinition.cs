namespace SharpMeasures.Generators.Attributes.Parsing.Quantities;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;

using System.Collections.Generic;

public record class ExcludeUnitsDefinition : AItemListDefinition<string, ExcludeUnitsLocations>
{
    public IReadOnlyList<string> ExcludedUnits => Items;

    public ExcludeUnitsDefinition(IReadOnlyList<string> excludedUnits, ExcludeUnitsLocations locations) : base(excludedUnits, locations) { }
}
