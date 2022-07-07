namespace SharpMeasures.Generators.Vectors.Parsing;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Quantities.Parsing.ConvertibleQuantity;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;
using SharpMeasures.Generators.Vectors.Parsing.Abstractions;
using SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVector;
using SharpMeasures.Generators.Vectors.Parsing.VectorConstant;

using System.Collections.Generic;

internal record class RawParsedBaseVector : IRawParsedVector<SharpMeasuresVectorDefinition>
{
    public DefinedType VectorType { get; }
    public MinimalLocation VectorLocation { get; }
    public SharpMeasuresVectorDefinition VectorDefinition { get; }

    public EquatableEnumerable<RawIncludeUnitsDefinition> IncludeUnits { get; }
    public EquatableEnumerable<RawExcludeUnitsDefinition> ExcludeUnits { get; }

    public EquatableEnumerable<RawVectorConstantDefinition> VectorConstants { get; }
    public EquatableEnumerable<RawConvertibleQuantityDefinition> DimensionalEquivalences { get; }

    IEnumerable<RawIncludeUnitsDefinition> IRawParsedVector<SharpMeasuresVectorDefinition>.IncludeUnits => IncludeUnits;
    IEnumerable<RawExcludeUnitsDefinition> IRawParsedVector<SharpMeasuresVectorDefinition>.ExcludeUnits => ExcludeUnits;
    IEnumerable<RawVectorConstantDefinition> IRawParsedVector<SharpMeasuresVectorDefinition>.VectorConstants => VectorConstants;
    IEnumerable<RawConvertibleQuantityDefinition> IRawParsedVector<SharpMeasuresVectorDefinition>.DimensionalEquivalences => DimensionalEquivalences;

    public RawParsedBaseVector(DefinedType vectorType, MinimalLocation vectorLocation, SharpMeasuresVectorDefinition vectorDefinition,
        IEnumerable<RawIncludeUnitsDefinition> includeUnits, IEnumerable<RawExcludeUnitsDefinition> excludeUnits, IEnumerable<RawVectorConstantDefinition> vectorConstants,
        IEnumerable<RawConvertibleQuantityDefinition> dimensionalEquivalences)
    {
        VectorType = vectorType;
        VectorLocation = vectorLocation;
        VectorDefinition = vectorDefinition;

        IncludeUnits = includeUnits.AsEquatable();
        ExcludeUnits = excludeUnits.AsEquatable();

        VectorConstants = vectorConstants.AsEquatable();
        DimensionalEquivalences = dimensionalEquivalences.AsEquatable();
    }
}
