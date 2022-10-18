namespace SharpMeasures.Generators.Vectors.Parsing.SpecializedSharpMeasuresVectorGroup;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Quantities;

internal sealed record class SpecializedSharpMeasuresVectorGroupDefinition : AAttributeDefinition<SpecializedSharpMeasuresVectorGroupLocations>, IVectorGroupSpecialization, IDefaultUnitInstanceDefinition
{
    public NamedType OriginalQuantity { get; }

    public bool InheritOperations { get; }
    public bool InheritConversions { get; }
    public bool InheritUnits { get; }

    public ConversionOperatorBehaviour ForwardsCastOperatorBehaviour { get; }
    public ConversionOperatorBehaviour BackwardsCastOperatorBehaviour { get; }

    public NamedType? Scalar { get; }

    public bool? ImplementSum { get; }
    public bool? ImplementDifference { get; }
    public NamedType? Difference { get; }

    public string? DefaultUnitInstanceName { get; }
    public string? DefaultUnitInstanceSymbol { get; }

    ISharpMeasuresObjectLocations ISharpMeasuresObject.Locations => Locations;
    IQuantityLocations IQuantity.Locations => Locations;
    IQuantitySpecializationLocations IQuantitySpecialization.Locations => Locations;
    IVectorGroupLocations IVectorGroup.Locations => Locations;
    IVectorGroupSpecializationLocations IVectorGroupSpecialization.Locations => Locations;
    IDefaultUnitInstanceLocations IDefaultUnitInstanceDefinition.DefaultUnitInstanceLocations => Locations;

    public SpecializedSharpMeasuresVectorGroupDefinition(NamedType originalQuantity, bool inheritOperations, bool inheritConversions, bool inheritUnits, ConversionOperatorBehaviour forwardsCastOperatorBehaviour, ConversionOperatorBehaviour backwardsCastOperatorBehaviour,
        NamedType? scalar, bool? implementSum, bool? implementDifference, NamedType? difference, string? defaultUnitInstanceName, string? defaultUnitInstanceSymbol, SpecializedSharpMeasuresVectorGroupLocations locations) : base(locations)
    {
        OriginalQuantity = originalQuantity;

        InheritOperations = inheritOperations;
        InheritConversions = inheritConversions;
        InheritUnits = inheritUnits;

        ForwardsCastOperatorBehaviour = forwardsCastOperatorBehaviour;
        BackwardsCastOperatorBehaviour = backwardsCastOperatorBehaviour;

        Scalar = scalar;

        ImplementSum = implementSum;
        ImplementDifference = implementDifference;
        Difference = difference;

        DefaultUnitInstanceName = defaultUnitInstanceName;
        DefaultUnitInstanceSymbol = defaultUnitInstanceSymbol;
    }
}
