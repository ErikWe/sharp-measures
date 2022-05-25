namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using SharpMeasures.Generators.Units;

using System.Collections.Generic;

internal static class UnitAliasProperties
{
    public static IReadOnlyList<IAttributeProperty<UnitAliasDefinition>> AllProperties => new IAttributeProperty<UnitAliasDefinition>[]
    {
        CommonProperties.Name<UnitAliasDefinition, UnitAliasParsingData, UnitAliasLocations>(nameof(UnitAliasAttribute.Name)),
        CommonProperties.Plural<UnitAliasDefinition, UnitAliasParsingData, UnitAliasLocations>(nameof(UnitAliasAttribute.Plural)),
        CommonProperties.DependantOn<UnitAliasDefinition, UnitAliasParsingData, UnitAliasLocations>(nameof(UnitAliasAttribute.AliasOf)),
    };
}
