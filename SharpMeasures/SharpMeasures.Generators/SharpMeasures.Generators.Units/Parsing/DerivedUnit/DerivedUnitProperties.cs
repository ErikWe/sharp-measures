﻿namespace SharpMeasures.Generators.Units.Parsing.DerivedUnit;

using Microsoft.CodeAnalysis;

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
        Signature,
        Units
    };

    private static DerivedUnitProperty<INamedTypeSymbol[]> Signature { get; } = new
    (
        name: nameof(DerivedUnitAttribute.Signature),
        setter: static (definition, signature) => definition with { Signature = signature.AsNamedTypes().AsReadOnlyEquatable() },
        locator: static (locations, collectionLocation, elementLocations) => locations with
        {
            SignatureCollection = collectionLocation,
            SignatureElements = elementLocations
        }
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