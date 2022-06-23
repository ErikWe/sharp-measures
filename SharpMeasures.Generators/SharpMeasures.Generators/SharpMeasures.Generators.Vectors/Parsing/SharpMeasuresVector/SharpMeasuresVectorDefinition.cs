namespace SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVector;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Vectors.Parsing.Abstractions;

internal record class SharpMeasuresVectorDefinition : AAttributeDefinition<SharpMeasuresVectorLocations>, IVectorDefinition
{
    public NamedType Unit { get; }
    public NamedType? Scalar { get; }

    public int Dimension { get; }

    public bool ImplementSum { get; }
    public bool ImplementDifference { get; }

    public NamedType Difference { get; }

    public string? DefaultUnitName { get; }
    public string? DefaultUnitSymbol { get; }

    public bool GenerateDocumentation { get; }

    public SharpMeasuresVectorDefinition(NamedType unit, NamedType? scalar, int dimension, bool implementSum, bool implementDifference, NamedType difference,
        string? defaultUnitName, string? defaultUnitSymbol, bool generateDocumentation, SharpMeasuresVectorLocations locations)
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
