namespace SharpMeasures.Generators.Scalars;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Quantities;

using System.Collections.Generic;
using System.Linq;

internal sealed record class ResolvedScalarPopulation : IResolvedScalarPopulation
{
    public IReadOnlyDictionary<NamedType, IResolvedScalarType> Scalars { get; }

    IReadOnlyDictionary<NamedType, IResolvedQuantityType> IResolvedQuantityPopulation.Quantities => Scalars.Transform(static (scalar) => scalar as IResolvedQuantityType);

    private ResolvedScalarPopulation(IReadOnlyDictionary<NamedType, IResolvedScalarType> scalars)
    {
        Scalars = scalars.AsReadOnlyEquatable();
    }

    public static ResolvedScalarPopulation Build(IReadOnlyList<IResolvedScalarType> scalarBases, IReadOnlyList<IResolvedScalarType> scalarSpecializations) => new(scalarBases.Concat(scalarSpecializations).ToDictionary(static (scalar) => scalar.Type.AsNamedType()));
}
