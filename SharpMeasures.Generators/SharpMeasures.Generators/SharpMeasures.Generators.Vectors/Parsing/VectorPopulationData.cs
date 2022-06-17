﻿namespace SharpMeasures.Generators.Vectors.Parsing;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Quantities.Utility;

using System.Collections.Generic;

internal class VectorPopulationData
{
    public ReadOnlyEquatableDictionary<ResizedVectorGroup, ReadOnlyEquatableDictionary<ResizedVectorGroup, ConversionOperationBehaviour>> DimensionalEquivalences { get; }
    public ReadOnlyEquatableDictionary<NamedType, ReadOnlyEquatableList<NamedType>> ExcessiveDimensionalEquivalences { get; }

    public ReadOnlyEquatableDictionary<NamedType, IVectorInterface> UnresolvedVectors { get; }
    public ReadOnlyEquatableDictionary<NamedType, ResizedVectorInterface> ResizedVectorsWithDuplicateDimension { get; }

    public VectorPopulationData
    (
        IReadOnlyDictionary<ResizedVectorGroup, ReadOnlyEquatableDictionary<ResizedVectorGroup, ConversionOperationBehaviour>> dimensionalEquivalences,
        IReadOnlyDictionary<NamedType, ReadOnlyEquatableList<NamedType>> excessiveDimensionalEquivalences,
        IReadOnlyDictionary<NamedType, IVectorInterface> unresolvedVectors,
        IReadOnlyDictionary<NamedType, ResizedVectorInterface> resizedVectorsWithDuplicateDimension)
    {
        DimensionalEquivalences = dimensionalEquivalences.AsReadOnlyEquatable();
        ExcessiveDimensionalEquivalences = excessiveDimensionalEquivalences.AsReadOnlyEquatable();

        UnresolvedVectors = unresolvedVectors.AsReadOnlyEquatable();
        ResizedVectorsWithDuplicateDimension = resizedVectorsWithDuplicateDimension.AsReadOnlyEquatable();
    }
}