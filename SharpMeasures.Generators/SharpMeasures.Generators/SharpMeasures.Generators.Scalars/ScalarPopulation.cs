namespace SharpMeasures.Generators.Scalars;

using Microsoft.CodeAnalysis;

using SharpMeasures.Equatables;
using SharpMeasures.Generators;

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;

internal class ScalarPopulation : IScalarPopulation
{
    public static IncrementalValueProvider<ScalarPopulation> Build(IncrementalValuesProvider<IBaseScalarType> baseScalars,
        IncrementalValuesProvider<ISpecializedScalarType> specializedScalars)
    {
        return baseScalars.Collect().Combine(specializedScalars.Collect()).Select(createPopulation);

        static ScalarPopulation createPopulation((ImmutableArray<IBaseScalarType> Base, ImmutableArray<ISpecializedScalarType> Specialized) scalars, CancellationToken _)
        {
            return new Builder(scalars.Base, scalars.Specialized).Resolve();
        }
    }

    public IReadOnlyDictionary<NamedType, IScalarType> Scalars => scalars;
    public IReadOnlyDictionary<NamedType, SpecializedScalarPopulation> Specializations => specializations;
    public IReadOnlyDictionary<NamedType, ISpecializedScalarType> UnresolvedSpecializedScalars => unresolvedSpecializedScalars;

    private ReadOnlyEquatableDictionary<NamedType, IScalarType> scalars { get; }
    private ReadOnlyEquatableDictionary<NamedType, SpecializedScalarPopulation> specializations { get; }

    private ReadOnlyEquatableDictionary<NamedType, ISpecializedScalarType> unresolvedSpecializedScalars { get; }

    private ScalarPopulation(IReadOnlyDictionary<NamedType, IScalarType> scalars, IReadOnlyDictionary<NamedType, SpecializedScalarPopulation> specializations,
        IReadOnlyDictionary<NamedType, ISpecializedScalarType> unresolvedSpecializedScalars)
    {
        this.scalars = scalars.AsReadOnlyEquatable();
        this.specializations = specializations.AsReadOnlyEquatable();

        this.unresolvedSpecializedScalars = unresolvedSpecializedScalars.AsReadOnlyEquatable();
    }

    private class Builder
    {
        private Dictionary<NamedType, IScalarType> AllScalars { get; }
        private Dictionary<NamedType, Dictionary<NamedType, ISpecializedScalarType>> Specializations { get; } = new();

        private List<ISpecializedScalarType> UnresolvedSpecializedScalars { get; }

        public Builder(ImmutableArray<IBaseScalarType> baseScalars, ImmutableArray<ISpecializedScalarType> specializedScalars)
        {
            AllScalars = baseScalars.ToDictionary(static (scalar) => scalar.Type.AsNamedType(), static (scalar) => scalar as IScalarType);

            foreach (var baseScalar in baseScalars)
            {
                Specializations.Add(baseScalar.Type.AsNamedType(), new Dictionary<NamedType, ISpecializedScalarType>());
            }

            UnresolvedSpecializedScalars = specializedScalars.ToList();
        }

        public ScalarPopulation Resolve()
        {
            ResolveSpecializedScalars();

            var specializationsDictionary = Specializations.ToDictionary(static (x) => x.Key, static (x) => new SpecializedScalarPopulation(x.Value));

            var unresolvedSpecializedScalarsDictionary = UnresolvedSpecializedScalars.ToDictionary(static (scalar) => scalar.Type.AsNamedType());

            return new(AllScalars, specializationsDictionary, unresolvedSpecializedScalarsDictionary);
        }

        private void ResolveSpecializedScalars()
        {
            while (true)
            {
                var startLength = UnresolvedSpecializedScalars.Count;

                for (var i = 0; i < UnresolvedSpecializedScalars.Count; i++)
                {
                    if (AllScalars.TryGetValue(UnresolvedSpecializedScalars[i].ScalarDefinition.OriginalScalar, out var parentScalar))
                    {
                        AllScalars.Add(UnresolvedSpecializedScalars[i].Type.AsNamedType(), UnresolvedSpecializedScalars[i]);
                        Specializations.Add(UnresolvedSpecializedScalars[i].Type.AsNamedType(), new Dictionary<NamedType, ISpecializedScalarType>());
                        AddPotentiallyRecursiveSpecialization(parentScalar, UnresolvedSpecializedScalars[i]);

                        UnresolvedSpecializedScalars.RemoveAt(i);
                        i -= 1;
                    }
                }

                if (UnresolvedSpecializedScalars.Count == startLength)
                {
                    break;
                }
            }
        }

        private void AddPotentiallyRecursiveSpecialization(IScalarType parentScalar, ISpecializedScalarType specializedScalar)
        {
            if (Specializations.TryGetValue(parentScalar.Type.AsNamedType(), out var specializationsOfOriginalScalar))
            {
                specializationsOfOriginalScalar.Add(specializedScalar.Type.AsNamedType(), specializedScalar);
            }

            if (parentScalar is ISpecializedScalarType originalScalarAsSpecialized
                && AllScalars.TryGetValue(originalScalarAsSpecialized.ScalarDefinition.OriginalScalar, out var grandParentScalar))
            {
                AddPotentiallyRecursiveSpecialization(grandParentScalar, specializedScalar);
            }
        }
    }
}
