﻿namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;

using System.Collections.Generic;

public record class ExcludeBases : AItemListDefinition<string, ExcludeBasesLocations>
{
    public IReadOnlyList<string> ExcludedBases => Items;

    public ExcludeBases(IReadOnlyList<string> excludedBases, ExcludeBasesLocations locations) : base(excludedBases, locations) { }
}
