namespace SharpMeasures.Generators.Scalars.Parsing;

using SharpMeasures.Generators.Attributes.Parsing.Scalars;
using SharpMeasures.Generators.Attributes.Parsing.Quantities;

using System;
using System.Collections.Generic;
using System.Linq;

internal record class RawParsedScalar
{
    public DefinedType ScalarType { get; }
    public MinimalLocation ScalarLocation { get; }
    public GeneratedScalarDefinition ScalarDefinition { get; }

    public IEnumerable<RawIncludeBasesDefinition> IncludeBasesDefinitions { get; }
    public IEnumerable<RawExcludeBasesDefinition> ExcludeBasesDefinitions { get; }

    public IEnumerable<RawIncludeUnitsDefinition> IncludeUnitsDefinitions { get; }
    public IEnumerable<RawExcludeUnitsDefinition> ExcludeUnitsDefinitions { get; }

    public IEnumerable<RawScalarConstantDefinition> ScalarConstantDefinitions { get; }

    public IEnumerable<RawDimensionalEquivalenceDefinition> DimensionalEquivalenceDefinitions { get; }

    public RawParsedScalar(DefinedType scalarType, MinimalLocation scalarLocation, GeneratedScalarDefinition scalarDefinition,
        IEnumerable<RawIncludeBasesDefinition> includeBasesDefinitions, IEnumerable<RawExcludeBasesDefinition> excludeBasesDefinitions,
        IEnumerable<RawIncludeUnitsDefinition> includeUnitsDefinitions, IEnumerable<RawExcludeUnitsDefinition> excludeUnitsDefinitions,
        IEnumerable<RawScalarConstantDefinition> scalarConstantDefinitions, IEnumerable<RawDimensionalEquivalenceDefinition> dimensionalEquivalenceDefinitions)
    {
        ScalarType = scalarType;
        ScalarLocation = scalarLocation;
        ScalarDefinition = scalarDefinition;

        IncludeBasesDefinitions = includeBasesDefinitions;
        ExcludeBasesDefinitions = excludeBasesDefinitions;

        IncludeUnitsDefinitions = includeUnitsDefinitions;
        ExcludeUnitsDefinitions = excludeUnitsDefinitions;

        ScalarConstantDefinitions = scalarConstantDefinitions;

        DimensionalEquivalenceDefinitions = dimensionalEquivalenceDefinitions;
    }

    public virtual bool Equals(RawParsedScalar other)
    {
        if (other is null)
        {
            return false;
        }

        return ScalarType == other.ScalarType && ScalarLocation == other.ScalarLocation && ScalarDefinition == other.ScalarDefinition
            && IncludeBasesDefinitions.SequenceEqual(other.IncludeBasesDefinitions) && ExcludeBasesDefinitions.SequenceEqual(other.ExcludeBasesDefinitions)
            && IncludeUnitsDefinitions.SequenceEqual(other.IncludeUnitsDefinitions) && ExcludeUnitsDefinitions.SequenceEqual(other.ExcludeUnitsDefinitions)
            && ScalarConstantDefinitions.SequenceEqual(other.ScalarConstantDefinitions)
            && DimensionalEquivalenceDefinitions.SequenceEqual(other.DimensionalEquivalenceDefinitions);
    }

    public override int GetHashCode()
    {
        return (ScalarType, ScalarLocation, ScalarDefinition).GetHashCode() ^ ExcludeBasesDefinitions.GetSequenceHashCode()
            ^ IncludeBasesDefinitions.GetSequenceHashCode() ^ ExcludeUnitsDefinitions.GetSequenceHashCode() ^ IncludeUnitsDefinitions.GetSequenceHashCode()
            ^ ScalarConstantDefinitions.GetSequenceHashCode() ^ DimensionalEquivalenceDefinitions.GetSequenceHashCode();
    }
}
