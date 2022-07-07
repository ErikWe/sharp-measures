namespace SharpMeasures.Generators.Scalars.Parsing.IncludeBases;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;

using System.Collections.Generic;

internal record class IncludeBasesDefinition : AItemListDefinition<string, IncludeBasesLocations>, IIncludeBases
{
    public IReadOnlyList<string> IncludedBases => Items;

    public IncludeBasesDefinition(IReadOnlyList<string> includedBases, IncludeBasesLocations locations) : base(includedBases, locations) { }
}
