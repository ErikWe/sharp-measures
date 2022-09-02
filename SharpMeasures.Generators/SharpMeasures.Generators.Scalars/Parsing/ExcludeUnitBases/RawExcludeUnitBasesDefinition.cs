namespace SharpMeasures.Generators.Scalars.Parsing.ExcludeBases;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;

using System.Collections.Generic;

internal record class RawExcludeUnitBasesDefinition : ARawItemListDefinition<string?, RawExcludeUnitBasesDefinition, ExcludeUnitBasesLocations>
{
    public static RawExcludeUnitBasesDefinition Empty => new();

    public IReadOnlyList<string?> UnitInstances => Items;

    protected override RawExcludeUnitBasesDefinition Definition => this;

    private RawExcludeUnitBasesDefinition() : base(ExcludeUnitBasesLocations.Empty) { }
}
