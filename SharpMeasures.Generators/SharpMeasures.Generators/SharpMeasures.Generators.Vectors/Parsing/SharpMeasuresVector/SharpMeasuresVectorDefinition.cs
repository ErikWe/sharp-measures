namespace SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVector;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Raw.Quantities;
using SharpMeasures.Generators.Raw.Scalars;
using SharpMeasures.Generators.Raw.Units;
using SharpMeasures.Generators.Raw.Units.UnitInstances;
using SharpMeasures.Generators.Raw.Vectors.Groups;

internal record class SharpMeasuresVectorDefinition : AAttributeDefinition<SharpMeasuresVectorLocations>, IVector
{
    public IRawUnitType Unit { get; }
    public IRawScalarType? Scalar { get; }

    public int Dimension { get; }

    public bool ImplementSum { get; }
    public bool ImplementDifference { get; }

    public IRawVectorGroupType Difference { get; }
    IRawQuantityType IQuantity.Difference => Difference;

    public IRawUnitInstance? DefaultUnit { get; }
    public string? DefaultUnitSymbol { get; }

    public bool? GenerateDocumentation { get; }

    public SharpMeasuresVectorDefinition(IRawUnitType unit, IRawScalarType? scalar, int dimension, bool implementSum, bool implementDifference,
        IRawVectorGroupType difference, IRawUnitInstance? defaultUnit, string? defaultUnitSymbol, bool? generateDocumentation, SharpMeasuresVectorLocations locations)
        : base(locations)
    {
        Unit = unit;
        Scalar = scalar;

        Dimension = dimension;

        ImplementSum = implementSum;
        ImplementDifference = implementDifference;

        Difference = difference;

        DefaultUnit = defaultUnit;
        DefaultUnitSymbol = defaultUnitSymbol;

        GenerateDocumentation = generateDocumentation;
    }
}
