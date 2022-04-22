namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using SharpMeasures.Generators.Attributes.Parsing.Utility;
using SharpMeasures.Generators.Units;

using System.Collections.Generic;

public readonly record struct ScaledUnitAttributeParameters(string Name, string Plural, string From, double Scale)
    : IUnitAttributeParameters, IDerivedUnitAttributeParameters
{
    public static ParameterParser<ScaledUnitAttributeParameters, ScaledUnitAttribute> Parser { get; }
        = new(Properties.AllProperties, Defaults);

    string IDerivedUnitAttributeParameters.DerivedFrom => From;

    private static ScaledUnitAttributeParameters Defaults => new
    (
        Name: string.Empty,
        Plural: string.Empty,
        From: string.Empty,
        Scale: 0
    );

    private static class Properties
    {
        public static List<AttributeProperty<ScaledUnitAttributeParameters>> AllProperties => new()
        {
            Name,
            Plural,
            From,
            Scale
        };

        private static AttributeProperty<ScaledUnitAttributeParameters> Name { get; } = new
        (
            name: nameof(ScaledUnitAttribute.Name),
            setter: static (parameters, obj) => obj is string name ? parameters with { Name = name } : parameters
        );

        private static AttributeProperty<ScaledUnitAttributeParameters> Plural { get; } = new
        (
            name: nameof(ScaledUnitAttribute.Plural),
            setter: static (parameters, obj) => obj is string plural ? parameters with { Plural = plural } : parameters
        );

        private static AttributeProperty<ScaledUnitAttributeParameters> From { get; } = new
        (
            name: nameof(ScaledUnitAttribute.From),
            setter: static (parameters, obj) => obj is string from ? parameters with { From = from } : parameters
        );

        private static AttributeProperty<ScaledUnitAttributeParameters> Scale { get; } = new
        (
            name: nameof(ScaledUnitAttribute.Scale),
            setter: static (parameters, obj) => obj is double scale ? parameters with { Scale = scale } : parameters
        );
    }
}