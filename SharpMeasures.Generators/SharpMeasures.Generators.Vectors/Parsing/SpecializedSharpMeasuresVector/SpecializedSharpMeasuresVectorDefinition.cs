﻿namespace SharpMeasures.Generators.Vectors.Parsing.SpecializedSharpMeasuresVector;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Quantities;

internal record class SpecializedSharpMeasuresVectorDefinition : AAttributeDefinition<SpecializedSharpMeasuresVectorLocations>, IVectorSpecialization, IDefaultUnitInstanceDefinition
{
    public NamedType OriginalQuantity { get; }

    public bool InheritDerivations { get; }
    public bool InheritConstants { get; }
    public bool InheritConversions { get; }
    public bool InheritUnits { get; }

    public NamedType? Scalar { get; }

    public bool? ImplementSum { get; }
    public bool? ImplementDifference { get; }
    public NamedType? Difference { get; }

    public string? DefaultUnitInstanceName { get; }
    public string? DefaultUnitInstanceSymbol { get; }

    public bool? GenerateDocumentation { get; }

    ISharpMeasuresObjectLocations ISharpMeasuresObject.Locations => Locations;
    IQuantityLocations IQuantity.Locations => Locations;
    IQuantitySpecializationLocations IQuantitySpecialization.Locations => Locations;
    IVectorLocations IVector.Locations => Locations;
    IVectorSpecializationLocations IVectorSpecialization.Locations => Locations;
    IDefaultUnitInstanceLocations IDefaultUnitInstanceDefinition.DefaultUnitInstanceLocations => Locations;

    public SpecializedSharpMeasuresVectorDefinition(NamedType originalQuantity, bool inheritDerivations, bool inheritConstants, bool inheritConversions, bool inheritUnits, NamedType? scalar, bool? implementSum,
        bool? implementDifference, NamedType? difference, string? defaultUnitInstanceName, string? defaultUnitInstanceSymbol, bool? generateDocumentation, SpecializedSharpMeasuresVectorLocations locations)
        : base(locations)
    {
        OriginalQuantity = originalQuantity;

        InheritDerivations = inheritDerivations;
        InheritConstants = inheritConstants;
        InheritConversions = inheritConversions;
        InheritUnits = inheritUnits;

        Scalar = scalar;

        ImplementSum = implementSum;
        ImplementDifference = implementDifference;
        Difference = difference;

        DefaultUnitInstanceName = defaultUnitInstanceName;
        DefaultUnitInstanceSymbol = defaultUnitInstanceSymbol;

        GenerateDocumentation = generateDocumentation;
    }
}