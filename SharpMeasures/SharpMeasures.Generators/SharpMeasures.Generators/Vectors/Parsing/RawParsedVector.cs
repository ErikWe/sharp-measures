namespace SharpMeasures.Generators.Vectors.Parsing;

using SharpMeasures.Generators.Attributes.Parsing.Vectors;
using SharpMeasures.Generators.Attributes.Parsing.Quantities;

using System;
using System.Collections.Generic;
using System.Linq;

internal record class RawParsedVector
{
    public DefinedType VectorType { get; }
    public MinimalLocation VectorLocation { get; }
    public GeneratedVectorDefinition VectorDefinition { get; }

    public IEnumerable<RawIncludeUnitsDefinition> IncludeUnitsDefinitions { get; }
    public IEnumerable<RawExcludeUnitsDefinition> ExcludeUnitsDefinitions { get; }

    public IEnumerable<RawDimensionalEquivalenceDefinition> DimensionalEquivalenceDefinitions { get; }

    public RawParsedVector(DefinedType vectorType, MinimalLocation vectorLocation, GeneratedVectorDefinition vectorDefinition,
        IEnumerable<RawIncludeUnitsDefinition> includeUnitsDefinitions, IEnumerable<RawExcludeUnitsDefinition> excludeUnitsDefinitions,
        IEnumerable<RawDimensionalEquivalenceDefinition> dimensionalEquivalenceDefinitions)
    {
        VectorType = vectorType;
        VectorLocation = vectorLocation;
        VectorDefinition = vectorDefinition;

        IncludeUnitsDefinitions = includeUnitsDefinitions;
        ExcludeUnitsDefinitions = excludeUnitsDefinitions;

        DimensionalEquivalenceDefinitions = dimensionalEquivalenceDefinitions;
    }

    public virtual bool Equals(RawParsedVector other)
    {
        if (other is null)
        {
            return false;
        }

        return VectorType == other.VectorType && VectorLocation == other.VectorLocation && VectorDefinition == other.VectorDefinition
            && IncludeUnitsDefinitions.SequenceEqual(other.IncludeUnitsDefinitions) && ExcludeUnitsDefinitions.SequenceEqual(other.ExcludeUnitsDefinitions)
            && DimensionalEquivalenceDefinitions.SequenceEqual(other.DimensionalEquivalenceDefinitions);
    }

    public override int GetHashCode()
    {
        return (VectorType, VectorLocation, VectorDefinition).GetHashCode() ^ IncludeUnitsDefinitions.GetSequenceHashCode() ^ ExcludeUnitsDefinitions.GetSequenceHashCode()
            ^ DimensionalEquivalenceDefinitions.GetSequenceHashCode();
    }
}
