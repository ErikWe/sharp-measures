namespace SharpMeasures.Generators.Vectors.Populations;

using SharpMeasures.Equatables;

using System.Collections.Generic;

internal class VectorPopulationErrors
{
    public static IBuilder StartBuilder() => new Builder();

    public ReadOnlyEquatableHashSet<NamedType> NonUniquelyDefinedTypes { get; }
    public ReadOnlyEquatableHashSet<NamedType> UnresolvedTypes { get; }
    public ReadOnlyEquatableHashSet<NamedType> ResizedVectorsWithDuplicateDimension { get; }

    private VectorPopulationErrors(HashSet<NamedType> nonUniquelyDefinedTypes, HashSet<NamedType> unresolvedTypes,
        HashSet<NamedType> resizedVectorsWithDuplicateDimension)
    {
        NonUniquelyDefinedTypes = nonUniquelyDefinedTypes.AsReadOnlyEquatable();
        UnresolvedTypes = unresolvedTypes.AsReadOnlyEquatable();
        ResizedVectorsWithDuplicateDimension = resizedVectorsWithDuplicateDimension.AsReadOnlyEquatable();
    }

    public interface IBuilder
    {
        public abstract ISet<NamedType> NonUniquelyDefinedTypes { get; }
        public abstract ISet<NamedType> UnresolvedTypes { get; }
        public abstract ISet<NamedType> ResizedVectorsWithDuplicateDimension { get; }

        public abstract VectorPopulationErrors Finalize();
    }

    private class Builder : IBuilder
    {
        public HashSet<NamedType> NonUniquelyDefinedTypes { get; } = new();
        public HashSet<NamedType> UnresolvedTypes { get; } = new();
        public HashSet<NamedType> ResizedVectorsWithDuplicateDimension { get; } = new();

        public VectorPopulationErrors Finalize()
        {
            return new(NonUniquelyDefinedTypes, UnresolvedTypes, ResizedVectorsWithDuplicateDimension);
        }

        ISet<NamedType> IBuilder.NonUniquelyDefinedTypes => NonUniquelyDefinedTypes;
        ISet<NamedType> IBuilder.UnresolvedTypes => UnresolvedTypes;
        ISet<NamedType> IBuilder.ResizedVectorsWithDuplicateDimension => ResizedVectorsWithDuplicateDimension;
    }
}
