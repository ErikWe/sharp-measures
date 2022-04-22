namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using SharpMeasures.Generators.Attributes.Parsing.Utility;
using SharpMeasures.Generators.Units;

using System.Collections.Generic;

public readonly record struct OffsetUnitAttributeParameters(string Name, string Plural, string From, double Offset)
    : IUnitAttributeParameters, IDerivedUnitAttributeParameters
{
    public static ParameterParser<OffsetUnitAttributeParameters, OffsetUnitAttribute> Parser { get; }
        = new(Properties.AllProperties, Defaults);

    string IDerivedUnitAttributeParameters.DerivedFrom => From;

    private static OffsetUnitAttributeParameters Defaults => new
    (
        Name: string.Empty,
        Plural: string.Empty,
        From: string.Empty,
        Offset: 0
    );

    private static class Properties
    {
        public static List<AttributeProperty<OffsetUnitAttributeParameters>> AllProperties => new()
        {
            Name,
            Plural,
            From,
            Offset
        };

        private static AttributeProperty<OffsetUnitAttributeParameters> Name { get; } = new
        (
            name: nameof(OffsetUnitAttribute.Name),
            setter: static (parameters, obj) => obj is string name ? parameters with { Name = name } : parameters
        );

        private static AttributeProperty<OffsetUnitAttributeParameters> Plural { get; } = new
        (
            name: nameof(OffsetUnitAttribute.Plural),
            setter: static (parameters, obj) => obj is string plural ? parameters with { Plural = plural } : parameters
        );

        private static AttributeProperty<OffsetUnitAttributeParameters> From { get; } = new
        (
            name: nameof(OffsetUnitAttribute.From),
            setter: static (parameters, obj) => obj is string from ? parameters with { From = from } : parameters
        );

        private static AttributeProperty<OffsetUnitAttributeParameters> Offset { get; } = new
        (
            name: nameof(OffsetUnitAttribute.Offset),
            setter: static (parameters, obj) => obj is double offset ? parameters with { Offset = offset } : parameters
        );
    }
}