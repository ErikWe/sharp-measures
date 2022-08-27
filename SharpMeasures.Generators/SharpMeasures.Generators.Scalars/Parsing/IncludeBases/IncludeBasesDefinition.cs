namespace SharpMeasures.Generators.Scalars.Parsing.IncludeBases;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Utility;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

public record class IncludeBasesDefinition : AItemListDefinition<string, IncludeBasesLocations>, IUnitInclusionList
{
    public IReadOnlyList<string> IncludedBases => Items;

    public InclusionStackingMode StackingMode { get; }

    [SuppressMessage("Design", "CA1033", Justification = "Available under another name")]
    IReadOnlyList<string> IUnitList.Units => IncludedBases;

    public IncludeBasesDefinition(IReadOnlyList<string> includedBases, InclusionStackingMode stackingMode, IncludeBasesLocations locations, IReadOnlyList<int> locationMap) : base(includedBases, locations, locationMap)
    {
        StackingMode = stackingMode;
    }
}
