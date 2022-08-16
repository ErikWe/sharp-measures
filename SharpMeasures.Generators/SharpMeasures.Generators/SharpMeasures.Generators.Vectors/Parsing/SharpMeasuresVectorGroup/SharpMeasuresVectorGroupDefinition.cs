namespace SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVectorGroup;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Raw.Quantities;
using SharpMeasures.Generators.Raw.Scalars;
using SharpMeasures.Generators.Raw.Units;
using SharpMeasures.Generators.Raw.Units.UnitInstances;
using SharpMeasures.Generators.Raw.Vectors.Groups;
using SharpMeasures.Generators.Vectors.Groups;

internal record class SharpMeasuresVectorGroupDefinition : AAttributeDefinition<SharpMeasuresVectorGroupLocations>, IVectorGroup
{
    public IRawUnitType Unit { get; }
    public IRawScalarType? Scalar { get; }

    public bool ImplementSum { get; }
    public bool ImplementDifference { get; }

    public IRawVectorGroupType Difference { get; }
    IRawQuantityType IQuantity.Difference => Difference;
    IRawVectorGroupType IVectorGroup.Difference => Difference;

    public IRawUnitInstance? DefaultUnit { get; }
    public string? DefaultUnitSymbol { get; }

    public bool? GenerateDocumentation { get; }

    public SharpMeasuresVectorGroupDefinition(IRawUnitType unit, IRawScalarType? scalar, bool implementSum, bool implementDifference,
        IRawVectorGroupType difference, IRawUnitInstance? defaultUnit, string? defaultUnitSymbol, bool? generateDocumentation,
        SharpMeasuresVectorGroupLocations locations)
        : base(locations)
    {
        Unit = unit;
        Scalar = scalar;

        ImplementSum = implementSum;
        ImplementDifference = implementDifference;

        Difference = difference;

        DefaultUnit = defaultUnit;
        DefaultUnitSymbol = defaultUnitSymbol;

        GenerateDocumentation = generateDocumentation;
    }
}
