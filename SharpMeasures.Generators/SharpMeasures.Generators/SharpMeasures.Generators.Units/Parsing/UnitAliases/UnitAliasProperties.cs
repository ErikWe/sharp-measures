namespace SharpMeasures.Generators.Units.Parsing.UnitAlias;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Units.Parsing.Abstractions;

using System.Collections.Generic;

internal static class UnitAliasProperties
{
    public static IReadOnlyList<IAttributeProperty<UnprocessedUnitAliasDefinition>> AllProperties => new IAttributeProperty<UnprocessedUnitAliasDefinition>[]
    {
        CommonProperties.Name<UnprocessedUnitAliasDefinition, UnitAliasLocations>(nameof(UnitAliasAttribute.Name)),
        CommonProperties.Plural<UnprocessedUnitAliasDefinition, UnitAliasLocations>(nameof(UnitAliasAttribute.Plural)),
        CommonProperties.DependantOn<UnprocessedUnitAliasDefinition, UnitAliasLocations>(nameof(UnitAliasAttribute.AliasOf)),
    };
}
