namespace SharpMeasures.Generators.Scalars.Parsing.SpecializedSharpMeasuresScalar;

using SharpMeasures.Generators.Scalars.Parsing.Abstraction;
using SharpMeasures.Generators.Scalars.Parsing.SharpMeasuresScalar;
using SharpMeasures.Generators.Raw.Scalars;
using SharpMeasures.Generators.Raw.Units;
using SharpMeasures.Generators.Raw.Units.UnitInstances;
using SharpMeasures.Generators.Raw.Vectors.Groups;

internal record class SpecializedSharpMeasuresScalarDefinition : SharpMeasuresScalarDefinition, IScalarSpecialization
{
    public IRawScalarType OriginalScalar { get; }

    public bool InheritDerivations { get; }
    public bool InheritConstants { get; }
    public bool InheritConversions { get; }
    public bool InheritBases { get; }
    public bool InheritUnits { get; }

    public SpecializedSharpMeasuresScalarDefinition(IRawScalarType originalScalar, bool inheritDerivations, bool inheritConstants, bool inheritConversions,
        bool inheritBases, bool inheritUnits, IRawUnitType unit, IRawVectorGroupType? vectorGroup, bool useUnitBias, bool implementSum,
        bool implementDifference, IRawScalarType difference, IRawUnitInstance? defaultUnit, string? defaultUnitSymbol, IRawScalarType? reciprocal,
        IRawScalarType? square, IRawScalarType? cube, IRawScalarType? squareRoot, IRawScalarType? cubeRoot, bool? generateDocumentation,
        SharpMeasuresScalarLocations locations)
        : base(unit, vectorGroup, useUnitBias, implementSum, implementDifference, difference, defaultUnit, defaultUnitSymbol, reciprocal, square, cube, squareRoot, cubeRoot,
            generateDocumentation, locations)
    {
        OriginalScalar = originalScalar;

        InheritDerivations = inheritDerivations;
        InheritConstants = inheritConstants;
        InheritConversions = inheritConversions;
        InheritBases = inheritBases;
        InheritUnits = inheritUnits;
    }
}
