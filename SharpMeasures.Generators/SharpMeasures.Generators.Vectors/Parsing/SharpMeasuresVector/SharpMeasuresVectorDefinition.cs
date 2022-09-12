namespace SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVector;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Quantities;

internal record class SharpMeasuresVectorDefinition : AAttributeDefinition<SharpMeasuresVectorLocations>, IVectorBase, IDefaultUnitInstanceDefinition
{
    public NamedType Unit { get; }
    public NamedType? Scalar { get; }

    public int Dimension { get; }

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
    IVectorLocations IVector.Locations => Locations;
    IVectorBaseLocations IVectorBase.Locations => Locations;
    IDefaultUnitInstanceLocations IDefaultUnitInstanceDefinition.DefaultUnitInstanceLocations => Locations;

    public SharpMeasuresVectorDefinition(NamedType unit, NamedType? scalar, int dimension, bool implementSum, bool implementDifference, NamedType? difference, string? defaultUnitInstanceName, string? defaultUnitInstanceSymbol,
        bool? generateDocumentation, SharpMeasuresVectorLocations locations)
        : base(locations)
    {
        Unit = unit;
        Scalar = scalar;

        Dimension = dimension;

        ImplementSum = implementSum;
        ImplementDifference = implementDifference;

        Difference = difference;

        DefaultUnitInstanceName = defaultUnitInstanceName;
        DefaultUnitInstanceSymbol = defaultUnitInstanceSymbol;

        GenerateDocumentation = generateDocumentation;
    }
}
