namespace SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVector;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Unresolved.Quantities;
using SharpMeasures.Generators.Unresolved.Scalars;
using SharpMeasures.Generators.Unresolved.Units;
using SharpMeasures.Generators.Unresolved.Units.UnitInstances;
using SharpMeasures.Generators.Unresolved.Vectors;

internal record class SharpMeasuresVectorDefinition : AAttributeDefinition<SharpMeasuresVectorLocations>, IIndividualVector
{
    public IUnresolvedUnitType Unit { get; }
    public IUnresolvedScalarType? Scalar { get; }

    public int Dimension { get; }

    public bool ImplementSum { get; }
    public bool ImplementDifference { get; }

    public IUnresolvedVectorGroupType Difference { get; }
    IUnresolvedQuantityType IQuantity.Difference => Difference;

    public IUnresolvedUnitInstance? DefaultUnit { get; }
    public string? DefaultUnitSymbol { get; }

    public bool? GenerateDocumentation { get; }

    public SharpMeasuresVectorDefinition(IUnresolvedUnitType unit, IUnresolvedScalarType? scalar, int dimension, bool implementSum, bool implementDifference,
        IUnresolvedVectorGroupType difference, IUnresolvedUnitInstance? defaultUnit, string? defaultUnitSymbol, bool? generateDocumentation, SharpMeasuresVectorLocations locations)
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
