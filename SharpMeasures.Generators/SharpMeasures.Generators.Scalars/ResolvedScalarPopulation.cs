namespace SharpMeasures.Generators.Scalars;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Quantities;

using System.Collections.Generic;

internal sealed record class ResolvedScalarPopulation : IResolvedScalarPopulation
{
    public IReadOnlyDictionary<NamedType, IResolvedScalarType> Scalars { get; }

    IReadOnlyDictionary<NamedType, IResolvedQuantityType> IResolvedQuantityPopulation.Quantities => Scalars.Transform(static (scalar) => scalar as IResolvedQuantityType);

    private ResolvedScalarPopulation(IReadOnlyDictionary<NamedType, IResolvedScalarType> scalars)
    {
        Scalars = scalars.AsReadOnlyEquatable();
    }

    public static ResolvedScalarPopulation Build(IReadOnlyList<IResolvedScalarType> scalarBases, IReadOnlyList<IResolvedScalarType> scalarSpecializations)
    {
        Dictionary<NamedType, IResolvedScalarType> scalars = new(scalarBases.Count + scalarSpecializations.Count);

        foreach (var scalar in scalarBases)
        {
            scalars.TryAdd(scalar.Type.AsNamedType(), scalar);
        }

        foreach (var scalar in scalarSpecializations)
        {
            scalars.TryAdd(scalar.Type.AsNamedType(), scalar);
        }

        return new(scalars);
    }
}
