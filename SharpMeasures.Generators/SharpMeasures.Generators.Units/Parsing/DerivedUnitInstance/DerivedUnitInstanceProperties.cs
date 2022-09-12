namespace SharpMeasures.Generators.Units.Parsing.DerivedUnitInstance;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Units.Parsing.Abstractions;

using System.Collections.Generic;

internal static class DerivedUnitInstanceProperties
{
    public static IReadOnlyList<IAttributeProperty<RawDerivedUnitInstanceDefinition>> AllProperties => new IAttributeProperty<RawDerivedUnitInstanceDefinition>[]
    {
        CommonProperties.Name<RawDerivedUnitInstanceDefinition, DerivedUnitInstanceLocations>(nameof(DerivedUnitInstanceAttribute.Name)),
        CommonProperties.PluralForm<RawDerivedUnitInstanceDefinition, DerivedUnitInstanceLocations>(nameof(DerivedUnitInstanceAttribute.PluralForm)),
        CommonProperties.PluralFormRegexSubstitution<RawDerivedUnitInstanceDefinition, DerivedUnitInstanceLocations>(nameof(DerivedUnitInstanceAttribute.PluralFormRegexSubstitution)),
        DerivationID,
        Units
    };

    private static DerivedUnitInstanceProperty<string> DerivationID { get; } = new
    (
        name: nameof(DerivedUnitInstanceAttribute.DerivationID),
        setter: static (definition, derivationID) => definition with { DerivationID = derivationID },
        locator: static (locations, derivationIDLocation) => locations with { DerivationID = derivationIDLocation }
    );

    private static DerivedUnitInstanceProperty<string?[]> Units { get; } = new
    (
        name: nameof(DerivedUnitInstanceAttribute.Units),
        setter: static (definition, units) => definition with { Units = units },
        locator: static (locations, collectionLocation, elementLocations) => locations with
        {
            UnitsCollection = collectionLocation,
            UnitsElements = elementLocations
        }
    );
}
