namespace SharpMeasures.Generators.Scalars.Parsing.SpecializedSharpMeasuresScalar;

using SharpMeasures.Generators.Scalars.Parsing.Abstraction;
using SharpMeasures.Generators.Scalars.Parsing.SharpMeasuresScalar;
using SharpMeasures.Generators.Unresolved.Scalars;
using SharpMeasures.Generators.Unresolved.Units;
using SharpMeasures.Generators.Unresolved.Units.UnitInstances;
using SharpMeasures.Generators.Unresolved.Vectors;

internal record class SpecializedSharpMeasuresScalarDefinition : SharpMeasuresScalarDefinition, IScalarSpecialization
{
    public IUnresolvedScalarType OriginalScalar { get; }

    public bool InheritDerivations { get; }
    public bool InheritConstants { get; }
    public bool InheritConversions { get; }
    public bool InheritBases { get; }
    public bool InheritUnits { get; }

    public SpecializedSharpMeasuresScalarDefinition(IUnresolvedScalarType originalScalar, bool inheritDerivations, bool inheritConstants, bool inheritConversions,
        bool inheritBases, bool inheritUnits, IUnresolvedUnitType unit, IUnresolvedVectorGroupType? vectorGroup, bool useUnitBias, bool implementSum,
        bool implementDifference, IUnresolvedScalarType difference, IUnresolvedUnitInstance? defaultUnit, string? defaultUnitSymbol, IUnresolvedScalarType? reciprocal,
        IUnresolvedScalarType? square, IUnresolvedScalarType? cube, IUnresolvedScalarType? squareRoot, IUnresolvedScalarType? cubeRoot, bool? generateDocumentation,
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
