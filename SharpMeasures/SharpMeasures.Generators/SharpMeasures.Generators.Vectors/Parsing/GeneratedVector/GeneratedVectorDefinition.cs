namespace SharpMeasures.Generators.Vectors.Parsing.GeneratedVector;

using SharpMeasures.Generators.Attributes.Parsing;

internal record class GeneratedVectorDefinition : AAttributeDefinition<GeneratedVectorLocations>
{
    public NamedType Unit { get; }
    public NamedType? Scalar { get; }

    public int Dimension { get; }

    public string? DefaultUnitName { get; }
    public string? DefaultUnitSymbol { get; }

    public bool GenerateDocumentation { get; }

    public GeneratedVectorDefinition(NamedType unit, NamedType? scalar, int dimension, string? defaultUnitName,
        string? defaultUnitSymbol, bool generateDocumentation, GeneratedVectorLocations locations)
        : base(locations)
    {
        Unit = unit;
        Scalar = scalar;

        Dimension = dimension;

        DefaultUnitName = defaultUnitName;
        DefaultUnitSymbol = defaultUnitSymbol;

        GenerateDocumentation = generateDocumentation;
    }
}
