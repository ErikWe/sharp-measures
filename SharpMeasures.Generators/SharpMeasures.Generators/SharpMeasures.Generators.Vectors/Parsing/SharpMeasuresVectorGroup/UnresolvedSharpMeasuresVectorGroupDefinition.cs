namespace SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVectorGroup;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Raw.Quantities;
using SharpMeasures.Generators.Raw.Vectors.Groups;

internal record class UnresolvedSharpMeasuresVectorGroupDefinition : AAttributeDefinition<SharpMeasuresVectorGroupLocations>, IRawVectorGroupBase
{
    public NamedType Unit { get; }
    public NamedType? Scalar { get; }

    public bool ImplementSum { get; }
    bool? IRawQuantity.ImplementSum => ImplementSum;
    public bool ImplementDifference { get; }
    bool? IRawQuantity.ImplementDifference => ImplementDifference;

    public NamedType Difference { get; }
    NamedType? IRawQuantity.Difference => Difference;

    public string? DefaultUnitName { get; }
    public string? DefaultUnitSymbol { get; }

    public bool? GenerateDocumentation { get; }

    public UnresolvedSharpMeasuresVectorGroupDefinition(NamedType unit, NamedType? scalar, bool implementSum, bool implementDifference, NamedType difference,
        string? defaultUnitName, string? defaultUnitSymbol, bool? generateDocumentation, SharpMeasuresVectorGroupLocations locations)
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
