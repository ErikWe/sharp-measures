namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;

using System.Collections.Generic;

public record class IncludeBasesDefinition : AItemListDefinition<string, IncludeBasesLocations>
{
    public IReadOnlyList<string> IncludedBases => Items;

    public IncludeBasesDefinition(IReadOnlyList<string> includedBases, IncludeBasesLocations locations) : base(includedBases, locations) { }
}
