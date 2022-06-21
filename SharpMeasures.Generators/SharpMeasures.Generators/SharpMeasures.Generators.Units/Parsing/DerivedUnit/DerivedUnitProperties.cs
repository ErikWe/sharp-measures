namespace SharpMeasures.Generators.Units.Parsing.DerivedUnit;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Units.Parsing.Abstractions;

using System.Collections.Generic;

internal static class DerivedUnitProperties
{
    public static IReadOnlyList<IAttributeProperty<RawDerivedUnitDefinition>> AllProperties => new IAttributeProperty<RawDerivedUnitDefinition>[]
    {
        CommonProperties.Name<RawDerivedUnitDefinition, DerivedUnitParsingData, DerivedUnitLocations>(nameof(DerivedUnitAttribute.Name)),
        CommonProperties.Plural<RawDerivedUnitDefinition, DerivedUnitParsingData, DerivedUnitLocations>(nameof(DerivedUnitAttribute.Plural)),
        SignatureID,
        Units
    };

    private static DerivedUnitProperty<string> SignatureID { get; } = new
    (
        name: nameof(DerivedUnitAttribute.SignatureID),
        setter: static (definition, signatureID) => definition with { SignatureID = signatureID },
        locator: static (locations, signatureIDLocation) => locations with { SignatureID = signatureIDLocation }
    );

    private static DerivedUnitProperty<string?[]> Units { get; } = new
    (
        name: nameof(DerivedUnitAttribute.Units),
        setter: static (definition, units) => definition with { Units = units.AsReadOnlyEquatable() },
        locator: static (locations, collectionLocation, elementLocations) => locations with
        {
            UnitsCollection = collectionLocation,
            UnitsElements = elementLocations
        }
    );
}
