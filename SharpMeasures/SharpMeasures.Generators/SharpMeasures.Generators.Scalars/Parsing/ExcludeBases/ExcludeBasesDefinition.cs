namespace SharpMeasures.Generators.Scalars.Parsing.ExcludeBases;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Attributes.Parsing.ItemLists;

using System.Collections.Generic;

internal record class ExcludeBasesDefinition : AItemListDefinition<string, ExcludeBasesLocations>
{
    public ReadOnlyEquatableList<string> ExcludedBases => Items;

    public ExcludeBasesDefinition(IReadOnlyList<string> excludedBases, ExcludeBasesLocations locations) : base(excludedBases, locations) { }
}
