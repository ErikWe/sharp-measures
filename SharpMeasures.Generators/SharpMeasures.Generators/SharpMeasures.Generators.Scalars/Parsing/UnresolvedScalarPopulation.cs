namespace SharpMeasures.Generators.Scalars.Parsing;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Unresolved.Quantities;
using SharpMeasures.Generators.Unresolved.Scalars;

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

internal class UnresolvedScalarPopulation : IUnresolvedScalarPopulation
{
    public static UnresolvedScalarPopulation Build(ImmutableArray<IUnresolvedScalarBaseType> baseScalars, ImmutableArray<IUnresolvedScalarSpecializationType> specializedScalars)
    {
        var scalars = (baseScalars as IEnumerable<IUnresolvedScalarType>).Concat(specializedScalars).ToDictionary(static (scalar) => scalar.Type.AsNamedType());
        var baseScalarByScalarType = baseScalars.ToDictionary(static (baseScalar) => baseScalar.Type.AsNamedType());

        var unassignedSpecializedScalars = specializedScalars.ToList();

        iterativelySetBaseScalarForSpecializations();

        return new(scalars, baseScalarByScalarType);

        void iterativelySetBaseScalarForSpecializations()
        {
            int startLength = unassignedSpecializedScalars.Count;

            for (int i = 0; i < unassignedSpecializedScalars.Count; i++)
            {
                if (baseScalarByScalarType.TryGetValue(unassignedSpecializedScalars[i].Definition.OriginalScalar, out var baseScalar))
                {
                    unassignedSpecializedScalars.RemoveAt(i);

                    baseScalarByScalarType[unassignedSpecializedScalars[i].Type.AsNamedType()] = baseScalar;
                }
            }

            if (startLength != unassignedSpecializedScalars.Count)
            {
                iterativelySetBaseScalarForSpecializations();
            }
        }
    }

    public IReadOnlyDictionary<NamedType, IUnresolvedScalarType> Scalars => scalars;
    public IReadOnlyDictionary<NamedType, IUnresolvedScalarBaseType> ScalarBases => baseScalarByScalarType;

    IReadOnlyDictionary<NamedType, IUnresolvedQuantityType> IUnresolvedQuantityPopulation.Quantities
        => Scalars.Transform(static (quantity) => quantity as IUnresolvedQuantityType);

    IReadOnlyDictionary<NamedType, IUnresolvedQuantityBaseType> IUnresolvedQuantityPopulation.QuantityBases
        => ScalarBases.Transform(static (quantity) => quantity as IUnresolvedQuantityBaseType);

    private ReadOnlyEquatableDictionary<NamedType, IUnresolvedScalarType> scalars { get; }
    private ReadOnlyEquatableDictionary<NamedType, IUnresolvedScalarBaseType> baseScalarByScalarType { get; }

    private UnresolvedScalarPopulation(IReadOnlyDictionary<NamedType, IUnresolvedScalarType> scalars,
        IReadOnlyDictionary<NamedType, IUnresolvedScalarBaseType> baseScalarByScalarType)
    {
        this.scalars = scalars.AsReadOnlyEquatable();
        this.baseScalarByScalarType = baseScalarByScalarType.AsReadOnlyEquatable();
    }
}
