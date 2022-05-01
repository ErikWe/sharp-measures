namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Units;

using System;
using System.Collections.ObjectModel;

internal static class DerivedUnitProperties
{
    public static ReadOnlyCollection<AttributeProperty<DerivedUnitParameters>> AllProperties => Array.AsReadOnly(new[]
    {
        Name,
        Plural,
        Signature,
        Units
    });

    public static AttributeProperty<DerivedUnitParameters> Name { get; } = new
    (
        name: nameof(DerivedUnitAttribute.Name),
        setter: static (parameters, obj) => obj is string name ? parameters with { Name = name } : parameters
    );

    public static AttributeProperty<DerivedUnitParameters> Plural { get; } = new
    (
        name: nameof(DerivedUnitAttribute.Plural),
        setter: static (parameters, obj) => obj is string plural ? parameters with { Plural = plural } : parameters
    );

    public static AttributeProperty<DerivedUnitParameters> Signature { get; } = new
    (
        name: nameof(DerivedUnitAttribute.Signature),
        setter: SetSignature
    );

    public static AttributeProperty<DerivedUnitParameters> Units { get; } = new
    (
        name: nameof(DerivedUnitAttribute.Units),
        setter: static (parameters, obj) => obj is string[] units
            ? parameters with { Units = Array.AsReadOnly(units) }
            : parameters
    );

    private static DerivedUnitParameters SetSignature(DerivedUnitParameters parameters, object? obj)
    {
        if (obj is not INamedTypeSymbol[] signature)
        {
            return parameters;
        }

        if (signature.Length == 0)
        {
            return parameters with { ParsingData = parameters.ParsingData with { EmptySignature = true } };
        }

        return parameters with { Signature = Array.AsReadOnly(signature) };
    }
}
