namespace SharpMeasures.Generators.Vectors.Populations;

using SharpMeasures.Generators;

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading;
using System.Linq;

internal static class VectorPopulationBuilder
{
    public static (VectorPopulation, VectorPopulationErrors) Build
        ((ImmutableArray<BaseVector> Base, ImmutableArray<ResizedVector> Resized) vectors, CancellationToken _)
    {
        Builder builder = new(allVectors());

        return (builder.Population, builder.Errors);

        IEnumerable<IVector> allVectors()
        {
            foreach (var baseVector in vectors.Base)
            {
                yield return baseVector;
            }

            foreach (var resizedVector in vectors.Base)
            {
                yield return resizedVector;
            }
        }
    }

    private class Builder
    {
        public VectorPopulation Population { get; }
        public VectorPopulationErrors Errors { get; }

        private Dictionary<NamedType, BaseVector> BaseVectors { get; } = new();
        private Dictionary<NamedType, ResizedVector> ResizedVectors { get; } = new();

        private Dictionary<NamedType, ResizedGroup.IBuilder> ResizedGroupBuilders { get; } = new();
        private Dictionary<NamedType, ResizedGroup.IBuilder> IntendedResizedGroupBuilderForRejectedVectors { get; } = new();

        private VectorPopulationErrors.IBuilder ErrorBuilder { get; } = VectorPopulationErrors.StartBuilder();

        public Builder(IEnumerable<IVector> vectors)
        {
            ResolveVectors(vectors);

            var builtGroups = ResizedGroupBuilders.ToDictionary(static (x) => x.Key, static (x) => x.Value.Build());

            Population = new(new BaseVectorPopulation(BaseVectors), new ResizedVectorPopulation(ResizedVectors),
                new ResizedGroupPopulation(builtGroups));

            Errors = ErrorBuilder.Finalize();
        }

        private void ResolveVectors(IEnumerable<IVector> vectors)
        {
            List<IVector> unresolvedVectors = new(vectors);

            while (true)
            {
                var startLength = unresolvedVectors.Count;

                for (var i = 0; i < unresolvedVectors.Count; i++)
                {
                    bool resolved = unresolvedVectors[i] switch
                    {
                        BaseVector baseVector => ResolveBaseVector(baseVector),
                        ResizedVector resizedVector => ResolveResizedVector(resizedVector),
                        _ => true
                    };

                    if (resolved)
                    {
                        unresolvedVectors.RemoveAt(i);
                        i -= 1;
                    }
                }

                if (unresolvedVectors.Count == startLength)
                {
                    break;
                }
            }

            foreach (var unresolvedVector in unresolvedVectors)
            {
                ErrorBuilder.UnresolvedTypes.Add(unresolvedVector.VectorType);
            }
        }

        private bool ResolveBaseVector(BaseVector vector)
        {
            if (BaseVectors.ContainsKey(vector.VectorType) || ResizedGroupBuilders.ContainsKey(vector.VectorType))
            {
                ErrorBuilder.NonUniquelyDefinedTypes.Add(vector.VectorType);
                return false;
            }

            BaseVectors.Add(vector.VectorType, vector);
            ResizedGroupBuilders.Add(vector.VectorType, ResizedGroup.StartBuilder(vector));
            return true;
        }

        private bool ResolveResizedVector(ResizedVector vector)
        {
            if (ResizedVectors.ContainsKey(vector.VectorType))
            {
                ErrorBuilder.NonUniquelyDefinedTypes.Add(vector.VectorType);
                return false;
            }

            if (TryGetGroupBuilderOfLinkedVector(vector, out var builder) is false)
            {
                return false;
            }

            if (builder.HasVectorOfDimension(vector.Dimension))
            {
                ErrorBuilder.ResizedVectorsWithDuplicateDimension.Add(vector.VectorType);
                IntendedResizedGroupBuilderForRejectedVectors.Add(vector.VectorType, builder);
                return false;
            }

            ResizedVectors.Add(vector.VectorType, vector);
            builder.AddResizedVector(vector);
            ResizedGroupBuilders.Add(vector.VectorType, builder);
            return true;
        }

        private bool TryGetGroupBuilderOfLinkedVector(ILinkedVector vector, out ResizedGroup.IBuilder builder)
        {
            if (ResizedGroupBuilders.TryGetValue(vector.LinkedTo, out builder))
            {
                return true;
            }

            if (IntendedResizedGroupBuilderForRejectedVectors.TryGetValue(vector.LinkedTo, out builder))
            {
                return true;
            }

            return false;
        }
    }
}
