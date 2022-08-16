namespace SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVector;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Raw.Quantities;
using SharpMeasures.Generators.Raw.Vectors;

internal record class UnresolvedSharpMeasuresVectorDefinition : AAttributeDefinition<SharpMeasuresVectorLocations>, IRawVectorBase
{
    public NamedType Unit { get; }
    public NamedType? Scalar { get; }

    public int Dimension { get; }

    public bool ImplementSum { get; }
    bool? IRawQuantity.ImplementSum => ImplementSum;
    public bool ImplementDifference { get; }
    bool? IRawQuantity.ImplementDifference => ImplementDifference;

    public NamedType Difference { get; }
    NamedType? IRawQuantity.Difference => Difference;

    public string? DefaultUnitName { get; }
    public string? DefaultUnitSymbol { get; }

    public bool? GenerateDocumentation { get; }

    public UnresolvedSharpMeasuresVectorDefinition(NamedType unit, NamedType? scalar, int dimension, bool implementSum, bool implementDifference, NamedType difference,
        string? defaultUnitName, string? defaultUnitSymbol, bool? generateDocumentation, SharpMeasuresVectorLocations locations)
        : base(locations)
    {
        Unit = unit;
        Scalar = scalar;

        Dimension = dimension;

        ImplementSum = implementSum;
        ImplementDifference = implementDifference;
        
        Difference = difference;

        DefaultUnitName = defaultUnitName;
        DefaultUnitSymbol = defaultUnitSymbol;

        GenerateDocumentation = generateDocumentation;
    }
}
