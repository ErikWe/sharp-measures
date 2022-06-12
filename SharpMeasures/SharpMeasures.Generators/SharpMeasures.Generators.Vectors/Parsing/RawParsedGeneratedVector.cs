namespace SharpMeasures.Generators.Vectors.Parsing;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Quantities.Parsing.DimensionalEquivalence;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;
using SharpMeasures.Generators.Vectors.Parsing.Abstractions;
using SharpMeasures.Generators.Vectors.Parsing.GeneratedVector;
using SharpMeasures.Generators.Vectors.Parsing.VectorConstant;

using System.Collections.Generic;

internal record class RawParsedGeneratedVector : IRawParsedVector<GeneratedVectorDefinition>
{
    public DefinedType VectorType { get; }
    public MinimalLocation VectorLocation { get; }
    public GeneratedVectorDefinition VectorDefinition { get; }

    public EquatableEnumerable<RawIncludeUnitsDefinition> IncludeUnits { get; }
    public EquatableEnumerable<RawExcludeUnitsDefinition> ExcludeUnits { get; }

    public EquatableEnumerable<RawVectorConstantDefinition> VectorConstants { get; }
    public EquatableEnumerable<RawDimensionalEquivalenceDefinition> DimensionalEquivalences { get; }

    IEnumerable<RawIncludeUnitsDefinition> IRawParsedVector<GeneratedVectorDefinition>.IncludeUnits => IncludeUnits;
    IEnumerable<RawExcludeUnitsDefinition> IRawParsedVector<GeneratedVectorDefinition>.ExcludeUnits => ExcludeUnits;
    IEnumerable<RawVectorConstantDefinition> IRawParsedVector<GeneratedVectorDefinition>.VectorConstants => VectorConstants;
    IEnumerable<RawDimensionalEquivalenceDefinition> IRawParsedVector<GeneratedVectorDefinition>.DimensionalEquivalences => DimensionalEquivalences;

    public RawParsedGeneratedVector(DefinedType vectorType, MinimalLocation vectorLocation, GeneratedVectorDefinition vectorDefinition,
        IEnumerable<RawIncludeUnitsDefinition> includeUnits, IEnumerable<RawExcludeUnitsDefinition> excludeUnits, IEnumerable<RawVectorConstantDefinition> vectorConstants,
        IEnumerable<RawDimensionalEquivalenceDefinition> dimensionalEquivalences)
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
