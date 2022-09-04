namespace SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVectorGroup;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Quantities;

internal record class SharpMeasuresVectorGroupDefinition : AAttributeDefinition<SharpMeasuresVectorGroupLocations>, IVectorGroupBase, IDefaultUnitInstanceDefinition
{
    public NamedType Unit { get; }
    public NamedType? Scalar { get; }

    public bool ImplementSum { get; }
    public bool ImplementDifference { get; }

    public NamedType? Difference { get; }

    public string? DefaultUnitInstanceName { get; }
    public string? DefaultUnitInstanceSymbol { get; }

    public bool? GenerateDocumentation { get; }

    bool? IQuantity.ImplementSum => ImplementSum;
    bool? IQuantity.ImplementDifference => ImplementDifference;

    ISharpMeasuresObjectLocations ISharpMeasuresObject.Locations => Locations;
    IQuantityLocations IQuantity.Locations => Locations;
    IQuantityBaseLocations IQuantityBase.Locations => Locations;
    IVectorGroupLocations IVectorGroup.Locations => Locations;
    IVectorGroupBaseLocations IVectorGroupBase.Locations => Locations;
    IDefaultUnitInstanceLocations IDefaultUnitInstanceDefinition.DefaultUnitInstanceLocations => Locations;

    public SharpMeasuresVectorGroupDefinition(NamedType unit, NamedType? scalar, bool implementSum, bool implementDifference, NamedType? difference, string? defaultUnitInstanceName, string? defaultUnitInstanceSymbol,
        bool? generateDocumentation, SharpMeasuresVectorGroupLocations locations)
        : base(locations)
    {
        Unit = unit;
        Scalar = scalar;

        ImplementSum = implementSum;
        ImplementDifference = implementDifference;

        Difference = difference;

        DefaultUnitInstanceName = defaultUnitInstanceName;
        DefaultUnitInstanceSymbol = defaultUnitInstanceSymbol;

        GenerateDocumentation = generateDocumentation;
    }
}
