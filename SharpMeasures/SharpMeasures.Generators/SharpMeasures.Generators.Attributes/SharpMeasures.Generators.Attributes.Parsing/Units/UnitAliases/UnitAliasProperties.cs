namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using SharpMeasures.Generators.Units;

using System.Collections.Generic;

internal static class UnitAliasProperties
{
    public static IReadOnlyList<IAttributeProperty<RawUnitAlias>> AllProperties => new IAttributeProperty<RawUnitAlias>[]
    {
        CommonProperties.Name<RawUnitAlias, UnitAliasParsingData, UnitAliasLocations>(nameof(UnitAliasAttribute.Name)),
        CommonProperties.Plural<RawUnitAlias, UnitAliasParsingData, UnitAliasLocations>(nameof(UnitAliasAttribute.Plural)),
        CommonProperties.DependantOn<RawUnitAlias, UnitAliasParsingData, UnitAliasLocations>(nameof(UnitAliasAttribute.AliasOf)),
    };
}
