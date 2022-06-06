namespace SharpMeasures.Generators.Attributes.Parsing.Quantities;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;

using System.Collections.Generic;

public record class IncludeUnits : AItemListDefinition<string, IncludeUnitsLocations>
{
    public IReadOnlyList<string> IncludedUnits => Items;

    public IncludeUnits(IReadOnlyList<string> includedUnits, IncludeUnitsLocations locations) : base(includedUnits, locations) { }
}
