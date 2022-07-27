namespace SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVectorGroup;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Unresolved.Quantities;
using SharpMeasures.Generators.Unresolved.Scalars;
using SharpMeasures.Generators.Unresolved.Units;
using SharpMeasures.Generators.Unresolved.Units.UnitInstances;
using SharpMeasures.Generators.Unresolved.Vectors;

internal record class SharpMeasuresVectorGroupDefinition : AAttributeDefinition<SharpMeasuresVectorGroupLocations>, IVectorGroup
{
    public IUnresolvedUnitType Unit { get; }
    public IUnresolvedScalarType? Scalar { get; }

    public bool ImplementSum { get; }
    public bool ImplementDifference { get; }

    public IUnresolvedVectorGroupType Difference { get; }
    IUnresolvedQuantityType IQuantity.Difference => Difference;
    IUnresolvedVectorGroupType IVectorGroup.Difference => Difference;

    public IUnresolvedUnitInstance? DefaultUnit { get; }
    public string? DefaultUnitSymbol { get; }

    public bool? GenerateDocumentation { get; }

    public SharpMeasuresVectorGroupDefinition(IUnresolvedUnitType unit, IUnresolvedScalarType? scalar, bool implementSum, bool implementDifference,
        IUnresolvedVectorGroupType difference, IUnresolvedUnitInstance? defaultUnit, string? defaultUnitSymbol, bool? generateDocumentation,
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
