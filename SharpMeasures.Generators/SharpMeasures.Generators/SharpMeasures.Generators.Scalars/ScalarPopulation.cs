namespace SharpMeasures.Generators.Scalars;

using SharpMeasures.Equatables;
using SharpMeasures.Generators;
using SharpMeasures.Generators.Quantities;

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

internal class ScalarPopulation : IScalarPopulation
{
    public static ScalarPopulation Build(ImmutableArray<IBaseScalarType> baseScalars, ImmutableArray<ISpecializedScalarType> specializedScalars)
    {
        var scalars = (baseScalars as IEnumerable<IScalarType>).Concat(specializedScalars).ToDictionary(static (scalar) => scalar.Type.AsNamedType());
        var specializationsByScalarTypeBuilder
            = scalars.Keys.ToDictionary(static (type) => type, static (_) => new Dictionary<NamedType, ISpecializedScalarType>());

        foreach (var specializedScalar in specializedScalars)
        {
            if (specializationsByScalarTypeBuilder.TryGetValue(specializedScalar.Definition.OriginalScalar.Type.AsNamedType(), out var originalScalarSpecializations) is false)
            {
                continue;
            }

            originalScalarSpecializations.Add(specializedScalar.Type.AsNamedType(), specializedScalar);
        }

        var specializationsByScalarType = specializationsByScalarTypeBuilder.Transform(static (dictionary) => new SpecializedScalarPopulation(dictionary));
        var baseScalarByScalarType = baseScalars.ToDictionary(static (scalar) => scalar.Type.AsNamedType());

        foreach (var baseScalar in baseScalars)
        {
            recursivelySetBaseScalarForSpecializations(baseScalar, baseScalar);
        }

        return new(scalars, baseScalarByScalarType, specializationsByScalarType);

        void recursivelySetBaseScalarForSpecializations(IBaseScalarType baseScalar, IScalarType scalar)
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

    public IReadOnlyDictionary<NamedType, IScalarType> Scalars => scalars;
    public IReadOnlyDictionary<NamedType, IBaseScalarType> BaseScalarByScalarType => baseScalarByScalarType;
    public IReadOnlyDictionary<NamedType, SpecializedScalarPopulation> SpecializationsByScalarType => specializationsByScalarType;

    private ReadOnlyEquatableDictionary<NamedType, IScalarType> scalars { get; }
    private ReadOnlyEquatableDictionary<NamedType, IBaseScalarType> baseScalarByScalarType { get; }
    private ReadOnlyEquatableDictionary<NamedType, SpecializedScalarPopulation> specializationsByScalarType { get; }

    IReadOnlyDictionary<NamedType, IQuantityType> IQuantityPopulation.Quantities => Scalars.Transform(static (scalar) => scalar as IQuantityType);

    private ScalarPopulation(IReadOnlyDictionary<NamedType, IScalarType> scalars, IReadOnlyDictionary<NamedType, IBaseScalarType> baseScalarByScalarType,
        IReadOnlyDictionary<NamedType, SpecializedScalarPopulation> specializationsByScalarType)
    {
        this.scalars = scalars.AsReadOnlyEquatable();
        this.baseScalarByScalarType = baseScalarByScalarType.AsReadOnlyEquatable();
        this.specializationsByScalarType = specializationsByScalarType.AsReadOnlyEquatable();
    }
}
