﻿namespace SharpMeasures.Generators.Scalars.Parsing.SpecializedSharpMeasuresScalar;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Quantities;

internal sealed record class SpecializedSharpMeasuresScalarDefinition : AAttributeDefinition<SpecializedSharpMeasuresScalarLocations>, IScalarSpecialization, IDefaultUnitInstanceDefinition
{
    public NamedType OriginalQuantity { get; }

    public bool InheritDerivations { get; }
    public bool InheritConstants { get; }
    public bool InheritConversions { get; }
    public bool InheritBases { get; }
    public bool InheritUnits { get; }

    public ConversionOperatorBehaviour ForwardsCastOperatorBehaviour { get; }
    public ConversionOperatorBehaviour BackwardsCastOperatorBehaviour { get; }

    public NamedType? Vector { get; }

    public bool? ImplementSum { get; }
    public bool? ImplementDifference { get; }
    public NamedType? Difference { get; }

    public string? DefaultUnitInstanceName { get; }
    public string? DefaultUnitInstanceSymbol { get; }

    public bool? GenerateDocumentation { get; }

    ISharpMeasuresObjectLocations ISharpMeasuresObject.Locations => Locations;
    IQuantityLocations IQuantity.Locations => Locations;
    IQuantitySpecializationLocations IQuantitySpecialization.Locations => Locations;
    IScalarLocations IScalar.Locations => Locations;
    IScalarSpecializationLocations IScalarSpecialization.Locations => Locations;
    IDefaultUnitInstanceLocations IDefaultUnitInstanceDefinition.DefaultUnitInstanceLocations => Locations;

    public SpecializedSharpMeasuresScalarDefinition(NamedType originalScalar, bool inheritDerivations, bool inheritConstants, bool inheritConversions, bool inheritBases, bool inheritUnits, ConversionOperatorBehaviour forwardsCastOperatorBehaviour, ConversionOperatorBehaviour backwardsCastOperatorBehaviour, NamedType? vector, bool? implementSum,
        bool? implementDifference, NamedType? difference, string? defaultUnitInstanceName, string? defaultUnitInstanceSymbol, bool? generateDocumentation, SpecializedSharpMeasuresScalarLocations locations) : base(locations)
    {
        OriginalQuantity = originalScalar;

        InheritDerivations = inheritDerivations;
        InheritConstants = inheritConstants;
        InheritConversions = inheritConversions;
        InheritBases = inheritBases;
        InheritUnits = inheritUnits;

        ForwardsCastOperatorBehaviour = forwardsCastOperatorBehaviour;
        BackwardsCastOperatorBehaviour = backwardsCastOperatorBehaviour;

        Vector = vector;

        ImplementSum = implementSum;
        ImplementDifference = implementDifference;
        Difference = difference;

        DefaultUnitInstanceName = defaultUnitInstanceName;
        DefaultUnitInstanceSymbol = defaultUnitInstanceSymbol;

        GenerateDocumentation = generateDocumentation;
    }
}
