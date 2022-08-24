namespace SharpMeasures.Generators.Quantities.Parsing.UnitList;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;

using System.Collections.Generic;

public record class RawUnitListDefinition : ARawItemListDefinition<string?, RawUnitListDefinition, UnitListLocations>
{
    public static RawUnitListDefinition Empty => new();

    public IReadOnlyList<string?> Units => Items;

    protected override RawUnitListDefinition Definition => this;

    private RawUnitListDefinition() : base(UnitListLocations.Empty) { }
}
