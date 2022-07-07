namespace SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;

using System.Collections.Generic;

public record class IncludeUnitsDefinition : AItemListDefinition<string, IncludeUnitsLocations>, IIncludeUnits
{
    public IReadOnlyList<string> IncludedUnits => Items;

    public IncludeUnitsDefinition(IReadOnlyList<string> includedUnits, IncludeUnitsLocations locations) : base(includedUnits, locations) { }
}
