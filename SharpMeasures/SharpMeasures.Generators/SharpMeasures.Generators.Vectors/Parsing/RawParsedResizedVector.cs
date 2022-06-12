namespace SharpMeasures.Generators.Vectors.Parsing;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Quantities.Parsing.DimensionalEquivalence;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;
using SharpMeasures.Generators.Vectors.Parsing.Abstractions;
using SharpMeasures.Generators.Vectors.Parsing.ResizedVector;
using SharpMeasures.Generators.Vectors.Parsing.VectorConstant;

using System.Collections.Generic;

internal class RawParsedResizedVector : IRawParsedVector<ResizedVectorDefinition>
{
    public DefinedType VectorType { get; }
    public MinimalLocation VectorLocation { get; }
    public ResizedVectorDefinition VectorDefinition { get; }

    public EquatableEnumerable<RawIncludeUnitsDefinition> IncludeUnits { get; }
    public EquatableEnumerable<RawExcludeUnitsDefinition> ExcludeUnits { get; }

    public EquatableEnumerable<RawVectorConstantDefinition> VectorConstants { get; }
    public EquatableEnumerable<RawDimensionalEquivalenceDefinition> DimensionalEquivalences { get; }

    IEnumerable<RawIncludeUnitsDefinition> IRawParsedVector<ResizedVectorDefinition>.IncludeUnits => IncludeUnits;
    IEnumerable<RawExcludeUnitsDefinition> IRawParsedVector<ResizedVectorDefinition>.ExcludeUnits => ExcludeUnits;
    IEnumerable<RawVectorConstantDefinition> IRawParsedVector<ResizedVectorDefinition>.VectorConstants => VectorConstants;
    IEnumerable<RawDimensionalEquivalenceDefinition> IRawParsedVector<ResizedVectorDefinition>.DimensionalEquivalences => DimensionalEquivalences;

    public RawParsedResizedVector(DefinedType vectorType, MinimalLocation vectorLocation, ResizedVectorDefinition vectorDefinition,
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
