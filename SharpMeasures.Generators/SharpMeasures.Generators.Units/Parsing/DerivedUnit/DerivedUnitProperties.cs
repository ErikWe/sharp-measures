namespace SharpMeasures.Generators.Units.Parsing.DerivedUnit;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Units.Parsing.Abstractions;

using System.Collections.Generic;

internal static class DerivedUnitProperties
{
    public static IReadOnlyList<IAttributeProperty<RawDerivedUnitDefinition>> AllProperties => new IAttributeProperty<RawDerivedUnitDefinition>[]
    {
        CommonProperties.Name<RawDerivedUnitDefinition, DerivedUnitLocations>(nameof(DerivedUnitAttribute.Name)),
        CommonProperties.Plural<RawDerivedUnitDefinition, DerivedUnitLocations>(nameof(DerivedUnitAttribute.Plural)),
        DerivationID,
        Units
    };

    private static DerivedUnitProperty<string> DerivationID { get; } = new
    (
        name: nameof(DerivedUnitAttribute.DerivationID),
        setter: static (definition, derivationID) => definition with { DerivationID = derivationID },
        locator: static (locations, derivationIDLocation) => locations with { DerivationID = derivationIDLocation }
    );

    private static DerivedUnitProperty<string?[]> Units { get; } = new
    (
        name: nameof(DerivedUnitAttribute.Units),
        setter: static (definition, units) => definition with { Units = units },
        locator: static (locations, collectionLocation, elementLocations) => locations with
        {
            UnitsCollection = collectionLocation,
            UnitsElements = elementLocations
        }
    );
}
