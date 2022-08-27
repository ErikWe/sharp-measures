namespace SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;
using SharpMeasures.Generators.Utility;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

public record class IncludeUnitsDefinition : AItemListDefinition<string, IncludeUnitsLocations>, IUnitInclusionList
{
    public IReadOnlyList<string> IncludedUnits => Items;

    public InclusionStackingMode StackingMode { get; }

    [SuppressMessage("Design", "CA1033", Justification = "Available under another name")]
    IReadOnlyList<string> IUnitList.Units => IncludedUnits;

    public IncludeUnitsDefinition(IReadOnlyList<string> includedUnits, InclusionStackingMode stackingMode, IncludeUnitsLocations locations, IReadOnlyList<int> locationMap) : base(includedUnits, locations, locationMap)
    {
        StackingMode = stackingMode;
    }
}
