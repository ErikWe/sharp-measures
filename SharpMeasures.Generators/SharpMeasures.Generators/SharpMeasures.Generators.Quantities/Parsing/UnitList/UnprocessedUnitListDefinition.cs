namespace SharpMeasures.Generators.Quantities.Parsing.UnitList;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;

using System.Collections.Generic;

public record class UnprocessedUnitListDefinition : ARawItemListDefinition<string?, UnprocessedUnitListDefinition, UnitListLocations>
{
    public static UnprocessedUnitListDefinition Empty => new();

    public IReadOnlyList<string?> Units => Items;

    protected override UnprocessedUnitListDefinition Definition => this;

    private UnprocessedUnitListDefinition() : base(UnitListLocations.Empty) { }
}
