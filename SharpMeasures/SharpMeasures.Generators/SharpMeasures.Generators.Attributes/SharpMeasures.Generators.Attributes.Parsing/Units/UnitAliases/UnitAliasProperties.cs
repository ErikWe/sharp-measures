namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using SharpMeasures.Generators.Units;

using System.Collections.Generic;

internal static class UnitAliasProperties
{
    public static IReadOnlyList<IAttributeProperty<RawUnitAliasDefinition>> AllProperties => new IAttributeProperty<RawUnitAliasDefinition>[]
    {
        CommonProperties.Name<RawUnitAliasDefinition, UnitAliasParsingData, UnitAliasLocations>(nameof(UnitAliasAttribute.Name)),
        CommonProperties.Plural<RawUnitAliasDefinition, UnitAliasParsingData, UnitAliasLocations>(nameof(UnitAliasAttribute.Plural)),
        CommonProperties.DependantOn<RawUnitAliasDefinition, UnitAliasParsingData, UnitAliasLocations>(nameof(UnitAliasAttribute.AliasOf)),
    };
}
