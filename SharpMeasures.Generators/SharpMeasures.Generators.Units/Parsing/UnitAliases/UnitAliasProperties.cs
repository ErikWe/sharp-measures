namespace SharpMeasures.Generators.Units.Parsing.UnitAlias;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Units.Parsing.Abstractions;

using System.Collections.Generic;

internal static class UnitAliasProperties
{
    public static IReadOnlyList<IAttributeProperty<RawUnitAliasDefinition>> AllProperties => new IAttributeProperty<RawUnitAliasDefinition>[]
    {
        CommonProperties.Name<RawUnitAliasDefinition, UnitAliasLocations>(nameof(UnitAliasAttribute.Name)),
        CommonProperties.Plural<RawUnitAliasDefinition, UnitAliasLocations>(nameof(UnitAliasAttribute.Plural)),
        CommonProperties.DependantOn<RawUnitAliasDefinition, UnitAliasLocations>(nameof(UnitAliasAttribute.AliasOf)),
    };
}
