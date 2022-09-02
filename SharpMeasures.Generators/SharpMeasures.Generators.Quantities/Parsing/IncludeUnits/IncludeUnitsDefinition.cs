namespace SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;
using SharpMeasures.Generators.Utility;

using System.Collections.Generic;

public record class IncludeUnitsDefinition : AItemListDefinition<string, IncludeUnitsLocations>, IUnitInstanceInclusionList
{
    public IReadOnlyList<string> UnitInstances => Items;

    public InclusionStackingMode StackingMode { get; }

    IUnitInstanceListLocations IUnitInstanceList.Locations => Locations;
    IUnitInstanceInclusionListLocations IUnitInstanceInclusionList.Locations => Locations;

    public IncludeUnitsDefinition(IReadOnlyList<string> unitInstances, InclusionStackingMode stackingMode, IncludeUnitsLocations locations, IReadOnlyList<int> locationMap) : base(unitInstances, locations, locationMap)
    {
        StackingMode = stackingMode;
    }
}
