namespace SharpMeasures.Generators.Scalars.Parsing;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Unresolved.Quantities;
using SharpMeasures.Generators.Unresolved.Scalars;

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

internal class UnresolvedScalarPopulation : IUnresolvedScalarPopulation
{
    public static UnresolvedScalarPopulation Build(ImmutableArray<IUnresolvedBaseScalarType> baseScalars, ImmutableArray<IUnresolvedSpecializedScalarType> specializedScalars)
    {
        var scalars = (baseScalars as IEnumerable<IUnresolvedScalarType>).Concat(specializedScalars).ToDictionary(static (scalar) => scalar.Type.AsNamedType());
        var specializationsByScalarTypeBuilder
            = scalars.Keys.ToDictionary(static (type) => type,static (_) => new Dictionary<NamedType, IUnresolvedSpecializedScalarType>());

        foreach (var specializedScalar in specializedScalars)
        {
            if (specializationsByScalarTypeBuilder.TryGetValue(specializedScalar.Definition.OriginalScalar, out var originalScalarSpecializations) is false)
            {
                continue;
            }

            originalScalarSpecializations.Add(specializedScalar.Type.AsNamedType(), specializedScalar);
        }

        var specializationsByScalarType = specializationsByScalarTypeBuilder.Transform(static (dictionary) => new UnresolvedSpecializedScalarPopulation(dictionary));
        var baseScalarByScalarType = baseScalars.ToDictionary(static (scalar) => scalar.Type.AsNamedType());

        foreach (var baseScalar in baseScalars)
        {
            recursivelySetBaseScalarForSpecializations(baseScalar, baseScalar);
        }

        return new(scalars, baseScalarByScalarType, specializationsByScalarType);

        void recursivelySetBaseScalarForSpecializations(IUnresolvedBaseScalarType baseScalar, IUnresolvedScalarType scalar)
        {
            if (specializationsByScalarTypeBuilder.TryGetValue(scalar.Type.AsNamedType(), out var specializations) is false)
            {
                return;
            }

            foreach (var specialization in specializations.Values)
            {
                baseScalarByScalarType.Add(specialization.Type.AsNamedType(), baseScalar);
                recursivelySetBaseScalarForSpecializations(baseScalar, specialization);
            }
        }
    }

    public IReadOnlyDictionary<NamedType, IUnresolvedScalarType> Scalars => scalars;
    public IReadOnlyDictionary<NamedType, IUnresolvedBaseScalarType> BaseScalarByScalarType => baseScalarByScalarType;
    public IReadOnlyDictionary<NamedType, UnresolvedSpecializedScalarPopulation> SpecializationsByScalarType => specializationsByScalarType;

    IReadOnlyDictionary<NamedType, IUnresolvedQuantityType> IUnresolvedQuantityPopulation.Quantities
        => Scalars.Transform(static (scalar) => scalar as IUnresolvedQuantityType);

    private ReadOnlyEquatableDictionary<NamedType, IUnresolvedScalarType> scalars { get; }
    private ReadOnlyEquatableDictionary<NamedType, IUnresolvedBaseScalarType> baseScalarByScalarType { get; }
    private ReadOnlyEquatableDictionary<NamedType, UnresolvedSpecializedScalarPopulation> specializationsByScalarType { get; }

    private UnresolvedScalarPopulation(IReadOnlyDictionary<NamedType, IUnresolvedScalarType> scalars,
        IReadOnlyDictionary<NamedType, IUnresolvedBaseScalarType> baseScalarByScalarType,
        IReadOnlyDictionary<NamedType, UnresolvedSpecializedScalarPopulation> specializationsByScalarType)
    {
        this.scalars = scalars.AsReadOnlyEquatable();
        this.baseScalarByScalarType = baseScalarByScalarType.AsReadOnlyEquatable();
        this.specializationsByScalarType = specializationsByScalarType.AsReadOnlyEquatable();
    }
}
