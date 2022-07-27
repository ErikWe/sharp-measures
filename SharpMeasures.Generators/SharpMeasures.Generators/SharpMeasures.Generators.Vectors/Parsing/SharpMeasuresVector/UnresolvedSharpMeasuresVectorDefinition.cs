namespace SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVector;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Unresolved.Quantities;
using SharpMeasures.Generators.Unresolved.Vectors;

internal record class UnresolvedSharpMeasuresVectorDefinition : AAttributeDefinition<SharpMeasuresVectorLocations>, IUnresolvedIndividualVectorBase
{
    public NamedType Unit { get; }
    public NamedType? Scalar { get; }

    public int Dimension { get; }

    public bool ImplementSum { get; }
    bool? IUnresolvedQuantity.ImplementSum => ImplementSum;
    public bool ImplementDifference { get; }
    bool? IUnresolvedQuantity.ImplementDifference => ImplementDifference;

    public NamedType Difference { get; }
    NamedType? IUnresolvedQuantity.Difference => Difference;

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
