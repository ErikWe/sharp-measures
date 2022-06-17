namespace SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Attributes.Parsing.ItemLists;

using System.Collections.Generic;

public record class ExcludeUnitsDefinition : AItemListDefinition<string, ExcludeUnitsLocations>
{
    public ReadOnlyEquatableList<string> ExcludedUnits => Items;

    public ExcludeUnitsDefinition(IReadOnlyList<string> excludedUnits, ExcludeUnitsLocations locations) : base(excludedUnits, locations) { }
}
