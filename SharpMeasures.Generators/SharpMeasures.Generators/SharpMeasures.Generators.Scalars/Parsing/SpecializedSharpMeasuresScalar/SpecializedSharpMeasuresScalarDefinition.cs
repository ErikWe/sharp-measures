namespace SharpMeasures.Generators.Scalars.Parsing.SpecializedSharpMeasuresScalar;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Unresolved.Quantities;
using SharpMeasures.Generators.Unresolved.Scalars;
using SharpMeasures.Generators.Unresolved.Units.UnitInstances;
using SharpMeasures.Generators.Unresolved.Vectors;

using System.Collections.Generic;

internal record class SpecializedSharpMeasuresScalarDefinition : AAttributeDefinition<SpecializedSharpMeasuresScalarLocations>, ISpecializedScalar
{
    public IUnresolvedScalarType OriginalScalar { get; }
    IUnresolvedQuantityType ISpecializedQuantity.OriginalQuantity => OriginalScalar;

    public bool InheritDerivations { get; }
    public bool InheritConstants { get; }
    public bool InheritConvertibleScalars { get; }
    bool ISpecializedQuantity.InheritConvertibleQuantities => InheritConvertibleScalars;
    public bool InheritBases { get; }
    public bool InheritUnits { get; }

    public IReadOnlyList<IUnresolvedVectorType> Vectors => vectors;

    public bool? ImplementSum { get; }
    public bool? ImplementDifference { get; }

    public IUnresolvedScalarType? Difference { get; }
    IUnresolvedQuantityType? IQuantity.Difference => Difference;

    public IUnresolvedUnitInstance? DefaultUnit { get; }
    public string? DefaultUnitSymbol { get; }

    public IUnresolvedScalarType? Reciprocal { get; }
    public IUnresolvedScalarType? Square { get; }
    public IUnresolvedScalarType? Cube { get; }
    public IUnresolvedScalarType? SquareRoot { get; }
    public IUnresolvedScalarType? CubeRoot { get; }

    public bool? GenerateDocumentation { get; }

    private ReadOnlyEquatableList<IUnresolvedVectorType> vectors { get; }

    public SpecializedSharpMeasuresScalarDefinition(IUnresolvedScalarType originalScalar, bool inheritDerivations, bool inheritConstants, bool inheritConvertibleScalars,
        bool inheritBases, bool inheritUnits, IReadOnlyList<IUnresolvedVectorType> vectors, bool? implementSum, bool? implementDifference, IUnresolvedScalarType? difference,
        IUnresolvedUnitInstance? defaultUnit, string? defaultUnitSymbol, IUnresolvedScalarType? reciprocal, IUnresolvedScalarType? square, IUnresolvedScalarType? cube,
        IUnresolvedScalarType? squareRoot, IUnresolvedScalarType? cubeRoot, bool? generateDocumentation, SpecializedSharpMeasuresScalarLocations locations)
        : base(locations)
    {
        OriginalScalar = originalScalar;

        InheritDerivations = inheritDerivations;
        InheritConstants = inheritConstants;
        InheritConvertibleScalars = inheritConvertibleScalars;
        InheritBases = inheritBases;
        InheritUnits = inheritUnits;

        this.vectors = vectors.AsReadOnlyEquatable();

        ImplementSum = implementSum;
        ImplementDifference = implementDifference;

        Difference = difference;

        DefaultUnit = defaultUnit;
        DefaultUnitSymbol = defaultUnitSymbol;

        Reciprocal = reciprocal;
        Square = square;
        Cube = cube;
        SquareRoot = squareRoot;
        CubeRoot = cubeRoot;

        GenerateDocumentation = generateDocumentation;
    }
}
