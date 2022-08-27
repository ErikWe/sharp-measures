namespace SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVectorGroup;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Quantities.Parsing.DefaultUnit;
using SharpMeasures.Generators.Vectors;

internal record class SharpMeasuresVectorGroupDefinition : AAttributeDefinition<SharpMeasuresVectorGroupLocations>, IVectorGroupBase, IDefaultUnitDefinition
{
    public NamedType Unit { get; }
    public NamedType? Scalar { get; }

    public bool ImplementSum { get; }
    public bool ImplementDifference { get; }

    public NamedType? Difference { get; }

    public string? DefaultUnitName { get; }
    public string? DefaultUnitSymbol { get; }

    public bool? GenerateDocumentation { get; }

    bool? IQuantity.ImplementSum => ImplementSum;
    bool? IQuantity.ImplementDifference => ImplementDifference;

    IDefaultUnitLocations IDefaultUnitDefinition.DefaultUnitLocations => Locations;

    public SharpMeasuresVectorGroupDefinition(NamedType unit, NamedType? scalar, bool implementSum, bool implementDifference, NamedType? difference, string? defaultUnitName, string? defaultUnitSymbol,
        bool? generateDocumentation, SharpMeasuresVectorGroupLocations locations)
        : base(locations)
    {
        Unit = unit;
        Scalar = scalar;

        ImplementSum = implementSum;
        ImplementDifference = implementDifference;

        Difference = difference;

        DefaultUnitName = defaultUnitName;
        DefaultUnitSymbol = defaultUnitSymbol;

        GenerateDocumentation = generateDocumentation;
    }
}
