namespace SharpMeasures.Generators.Units.Parsing.UnitAlias;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Units.Parsing.Abstractions;

using System.Collections.Generic;

internal static class UnitAliasProperty
{
    public static IReadOnlyList<IAttributeProperty<RawUnitAliasDefinition>> AllProperties => new IAttributeProperty<RawUnitAliasDefinition>[]
    {
        CommonProperties.Name<RawUnitAliasDefinition, UnitAliasParsingData, UnitAliasLocations>(nameof(UnitAliasAttribute.Name)),
        CommonProperties.Plural<RawUnitAliasDefinition, UnitAliasParsingData, UnitAliasLocations>(nameof(UnitAliasAttribute.Plural)),
        CommonProperties.DependantOn<RawUnitAliasDefinition, UnitAliasParsingData, UnitAliasLocations>(nameof(UnitAliasAttribute.AliasOf)),
    };
}
