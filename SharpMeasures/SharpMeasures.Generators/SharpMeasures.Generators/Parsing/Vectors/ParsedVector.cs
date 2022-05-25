namespace SharpMeasures.Generators.Parsing.Vectors;

using SharpMeasures.Generators.Attributes.Parsing.Vectors;
using SharpMeasures.Generators.Attributes.Parsing.Quantities;

using System;
using System.Collections.Generic;
using System.Linq;

internal record class ParsedVector
{
    public DefinedType VectorType { get; }
    public MinimalLocation VectorLocation { get; }
    public GeneratedVectorDefinition VectorDefinition { get; }

    public IEnumerable<IncludeUnitsDefinition> IncludeUnitsDefinitions { get; }
    public IEnumerable<ExcludeUnitsDefinition> ExcludeUnitsDefinitions { get; }

    public IEnumerable<DimensionalEquivalenceDefinition> DimensionalEquivalenceDefinitions { get; }

    public ParsedVector(DefinedType vectorType, MinimalLocation vectorLocation, GeneratedVectorDefinition vectorDefinition,
        IEnumerable<IncludeUnitsDefinition> includeUnitsDefinitions, IEnumerable<ExcludeUnitsDefinition> excludeUnitsDefinitions,
        IEnumerable<DimensionalEquivalenceDefinition> dimensionalEquivalenceDefinitions)
    {
        VectorType = vectorType;
        VectorLocation = vectorLocation;
        VectorDefinition = vectorDefinition;

        IncludeUnitsDefinitions = includeUnitsDefinitions;
        ExcludeUnitsDefinitions = excludeUnitsDefinitions;

        DimensionalEquivalenceDefinitions = dimensionalEquivalenceDefinitions;
    }

    public virtual bool Equals(ParsedVector other)
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
