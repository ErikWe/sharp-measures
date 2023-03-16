namespace SharpMeasures.Generators.Units.Parsing.UnitInstanceAlias;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Units.Parsing.Abstractions;

using System.Collections.Generic;

internal static class UnitInstanceAliasProperties
{
    public static IReadOnlyList<IAttributeProperty<RawUnitInstanceAliasDefinition>> AllProperties => new IAttributeProperty<RawUnitInstanceAliasDefinition>[]
    {
        CommonProperties.Name<RawUnitInstanceAliasDefinition, UnitInstanceAliasLocations>(nameof(UnitInstanceAliasAttribute.Name)),
        CommonProperties.PluralForm<RawUnitInstanceAliasDefinition, UnitInstanceAliasLocations>(nameof(UnitInstanceAliasAttribute.PluralForm)),
        CommonProperties.PluralFormRegexSubstitution<RawUnitInstanceAliasDefinition, UnitInstanceAliasLocations>(nameof(UnitInstanceAliasAttribute.PluralFormRegexSubstitution)),
        CommonProperties.OriginalUnitInstance<RawUnitInstanceAliasDefinition, UnitInstanceAliasLocations>(nameof(UnitInstanceAliasAttribute.AliasOf)),
    };
}
