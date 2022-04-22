namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using SharpMeasures.Generators.Attributes.Parsing.Utility;
using SharpMeasures.Generators.Units;

using System.Collections.Generic;

public readonly record struct UnitAliasAttributeParameters(string Name, string Plural, string AliasOf)
    : IUnitAttributeParameters, IDerivedUnitAttributeParameters
{
    public static ParameterParser<UnitAliasAttributeParameters, UnitAliasAttribute> Parser { get; }
        = new(Properties.AllProperties, Defaults);

    string IDerivedUnitAttributeParameters.DerivedFrom => AliasOf;

    private static UnitAliasAttributeParameters Defaults => new
    (
        Name: string.Empty,
        Plural: string.Empty,
        AliasOf: string.Empty
    );

    private static class Properties
    {
        public static List<AttributeProperty<UnitAliasAttributeParameters>> AllProperties => new()
        {
            Name,
            Plural,
            AliasOf
        };

        private static AttributeProperty<UnitAliasAttributeParameters> Name { get; } = new
        (
            name: nameof(UnitAliasAttribute.Name),
            setter: static (parameters, obj) => obj is string name ? parameters with { Name = name } : parameters
        );

        private static AttributeProperty<UnitAliasAttributeParameters> Plural { get; } = new
        (
            name: nameof(UnitAliasAttribute.Plural),
            setter: static (parameters, obj) => obj is string plural ? parameters with { Plural = plural } : parameters
        );

        private static AttributeProperty<UnitAliasAttributeParameters> AliasOf { get; } = new
        (
            name: nameof(UnitAliasAttribute.AliasOf),
            setter: static (parameters, obj) => obj is string aliasOf ? parameters with { AliasOf = aliasOf } : parameters
        );
    }
}