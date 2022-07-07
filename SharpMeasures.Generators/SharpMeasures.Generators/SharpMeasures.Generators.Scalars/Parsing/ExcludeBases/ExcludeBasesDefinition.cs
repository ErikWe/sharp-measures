namespace SharpMeasures.Generators.Scalars.Parsing.ExcludeBases;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;

using System.Collections.Generic;

internal record class ExcludeBasesDefinition : AItemListDefinition<string, ExcludeBasesLocations>, IExcludeBases
{
    public IReadOnlyList<string> ExcludedBases => Items;

    public ExcludeBasesDefinition(IReadOnlyList<string> excludedBases, ExcludeBasesLocations locations) : base(excludedBases, locations) { }
}
