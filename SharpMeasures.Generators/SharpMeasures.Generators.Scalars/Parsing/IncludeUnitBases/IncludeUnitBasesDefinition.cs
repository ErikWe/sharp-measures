namespace SharpMeasures.Generators.Scalars.Parsing.IncludeUnitBases;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Utility;

using System.Collections.Generic;

public record class IncludeUnitBasesDefinition : AItemListDefinition<string, IncludeUnitBasesLocations>, IUnitInstanceInclusionList
{
    public IReadOnlyList<string> UnitInstances => Items;

    public InclusionStackingMode StackingMode { get; }

    IUnitInstanceListLocations IUnitInstanceList.Locations => Locations;
    IUnitInstanceInclusionListLocations IUnitInstanceInclusionList.Locations => Locations;

    public IncludeUnitBasesDefinition(IReadOnlyList<string> unitInstances, InclusionStackingMode stackingMode, IncludeUnitBasesLocations locations, IReadOnlyList<int> locationMap) : base(unitInstances, locations, locationMap)
    {
        StackingMode = stackingMode;
    }
}
