namespace SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;

using System.Collections.Generic;

public sealed record class RawExcludeUnitsDefinition : ARawItemListDefinition<string?, RawExcludeUnitsDefinition, ExcludeUnitsLocations>
{
    public static RawExcludeUnitsDefinition Empty => new();

    public IReadOnlyList<string?> UnitInstances => Items;

    protected override RawExcludeUnitsDefinition Definition => this;

    private RawExcludeUnitsDefinition() : base(ExcludeUnitsLocations.Empty) { }
}
