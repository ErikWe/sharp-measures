namespace SharpMeasures.Generators.Parsing.Scalars;

using SharpMeasures.Generators.Attributes.Parsing.Scalars;
using SharpMeasures.Generators.Attributes.Parsing.Quantities;

using System;
using System.Collections.Generic;
using System.Linq;

internal record class ParsedScalar
{
    public DefinedType ScalarType { get; }
    public MinimalLocation ScalarLocation { get; }
    public GeneratedScalarDefinition ScalarDefinition { get; }

    public IEnumerable<SquarableScalarDefinition> SquarableDefinitions { get; }
    public IEnumerable<CubableScalarDefinition> CubableDefinitions { get; }

    public IEnumerable<SquareRootableScalarDefinition> SquareRootableDefinitions { get; }
    public IEnumerable<CubeRootableScalarDefinition> CubeRootableDefinitions { get; }

    public IEnumerable<InvertibleScalarDefinition> InvertibleDefinitions { get; }

    public IEnumerable<IncludeBasesDefinition> IncludeBasesDefinitions { get; }
    public IEnumerable<ExcludeBasesDefinition> ExcludeBasesDefinitions { get; }

    public IEnumerable<IncludeUnitsDefinition> IncludeUnitsDefinitions { get; }
    public IEnumerable<ExcludeUnitsDefinition> ExcludeUnitsDefinitions { get; }

    public IEnumerable<ScalarConstantDefinition> ScalarConstantDefinitions { get; }

    public IEnumerable<DimensionalEquivalenceDefinition> DimensionalEquivalenceDefinitions { get; }

    public ParsedScalar(DefinedType scalarType, MinimalLocation scalarLocation, GeneratedScalarDefinition scalarDefinition,
        IEnumerable<SquarableScalarDefinition> squarableDefinitions, IEnumerable<CubableScalarDefinition> cubableDefinitions,
        IEnumerable<SquareRootableScalarDefinition> squareRootableDefinitions, IEnumerable<CubeRootableScalarDefinition> cubeRootableDefinitions,
        IEnumerable<InvertibleScalarDefinition> invertibleDefinitions, IEnumerable<IncludeBasesDefinition> includeBasesDefinitions,
        IEnumerable<ExcludeBasesDefinition> excludeBasesDefinitions, IEnumerable<IncludeUnitsDefinition> includeUnitsDefinitions,
        IEnumerable<ExcludeUnitsDefinition> excludeUnitsDefinitions, IEnumerable<ScalarConstantDefinition> scalarConstantDefinitions,
        IEnumerable<DimensionalEquivalenceDefinition> dimensionalEquivalenceDefinitions)
    {
        ScalarType = scalarType;
        ScalarLocation = scalarLocation;
        ScalarDefinition = scalarDefinition;

        SquarableDefinitions = squarableDefinitions;
        CubableDefinitions = cubableDefinitions;

        SquareRootableDefinitions = squareRootableDefinitions;
        CubeRootableDefinitions = cubeRootableDefinitions;

        InvertibleDefinitions = invertibleDefinitions;

        IncludeBasesDefinitions = includeBasesDefinitions;
        ExcludeBasesDefinitions = excludeBasesDefinitions;

        IncludeUnitsDefinitions = includeUnitsDefinitions;
        ExcludeUnitsDefinitions = excludeUnitsDefinitions;

        ScalarConstantDefinitions = scalarConstantDefinitions;

        DimensionalEquivalenceDefinitions = dimensionalEquivalenceDefinitions;
    }

    public virtual bool Equals(ParsedScalar other)
    {
        if (other is null)
        {
            return false;
        }

        return ScalarType == other.ScalarType && ScalarLocation == other.ScalarLocation && ScalarDefinition == other.ScalarDefinition
            && SquarableDefinitions.SequenceEqual(other.SquarableDefinitions) && CubableDefinitions.SequenceEqual(other.CubableDefinitions)
            && SquareRootableDefinitions.SequenceEqual(other.SquareRootableDefinitions) && CubeRootableDefinitions.SequenceEqual(other.CubeRootableDefinitions)
            && InvertibleDefinitions.SequenceEqual(other.InvertibleDefinitions) && IncludeBasesDefinitions.SequenceEqual(other.IncludeBasesDefinitions)
            && ExcludeBasesDefinitions.SequenceEqual(other.ExcludeBasesDefinitions) && IncludeUnitsDefinitions.SequenceEqual(other.IncludeUnitsDefinitions)
            && ExcludeUnitsDefinitions.SequenceEqual(other.ExcludeUnitsDefinitions) && ScalarConstantDefinitions.SequenceEqual(other.ScalarConstantDefinitions)
            && DimensionalEquivalenceDefinitions.SequenceEqual(other.DimensionalEquivalenceDefinitions);
    }

    public override int GetHashCode()
    {
        return (ScalarType, ScalarLocation, ScalarDefinition).GetHashCode() ^ SquarableDefinitions.GetSequenceHashCode() ^ CubableDefinitions.GetSequenceHashCode()
            ^ SquareRootableDefinitions.GetSequenceHashCode() ^ CubeRootableDefinitions.GetSequenceHashCode() ^ InvertibleDefinitions.GetSequenceHashCode()
            ^ ExcludeBasesDefinitions.GetSequenceHashCode() ^ IncludeBasesDefinitions.GetSequenceHashCode() ^ ExcludeUnitsDefinitions.GetSequenceHashCode()
            ^ IncludeUnitsDefinitions.GetSequenceHashCode() ^ ScalarConstantDefinitions.GetSequenceHashCode() ^ DimensionalEquivalenceDefinitions.GetSequenceHashCode();
    }
}
