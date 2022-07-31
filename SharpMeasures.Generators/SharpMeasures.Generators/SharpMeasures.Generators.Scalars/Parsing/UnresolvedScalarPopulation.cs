namespace SharpMeasures.Generators.Scalars.Parsing;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Scalars.Parsing.Abstraction;
using SharpMeasures.Generators.Unresolved.Quantities;
using SharpMeasures.Generators.Unresolved.Scalars;

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

internal class UnresolvedScalarPopulation : IUnresolvedScalarPopulationWithData
{
    public static UnresolvedScalarPopulation Build(ImmutableArray<IUnresolvedScalarBaseType> scalarBases, ImmutableArray<IUnresolvedScalarSpecializationType> scalarSpecializations)
    {
        Dictionary<NamedType, IUnresolvedScalarType> scalarPopulation = new(scalarBases.Length + scalarSpecializations.Length);
        Dictionary<NamedType, IUnresolvedScalarBaseType> scalarBasePopulation = new(scalarBases.Length);

        Dictionary<NamedType, IUnresolvedScalarType> duplicatePopulation = new();

        foreach (var scalar in (scalarBases as IEnumerable<IUnresolvedScalarType>).Concat(scalarSpecializations))
        {
            if (scalarPopulation.TryAdd(scalar.Type.AsNamedType(), scalar))
            {
                continue;
            }

            duplicatePopulation.TryAdd(scalar.Type.AsNamedType(), scalar);
        }

        foreach (var scalarBase in scalarBases)
        {
            scalarBasePopulation.TryAdd(scalarBase.Type.AsNamedType(), scalarBase);
        }

        var unassignedSpecializations = scalarSpecializations.ToList();
        
        iterativelySetBaseScalarForSpecializations();

        return new(scalarPopulation, scalarBasePopulation, duplicatePopulation);

        void iterativelySetBaseScalarForSpecializations()
        {
            int startLength = unassignedSpecializations.Count;

            for (int i = 0; i < unassignedSpecializations.Count; i++)
            {
                if (scalarBasePopulation.TryGetValue(unassignedSpecializations[i].Definition.OriginalScalar, out var scalarBase))
                {
                    scalarBasePopulation.TryAdd(unassignedSpecializations[i].Type.AsNamedType(), scalarBase);

                    unassignedSpecializations.RemoveAt(i);
                    i -= 1;
                }
            }

            if (startLength != unassignedSpecializations.Count)
            {
                iterativelySetBaseScalarForSpecializations();
            }
        }
    }

    public IReadOnlyDictionary<NamedType, IUnresolvedScalarType> Scalars => scalars;
    public IReadOnlyDictionary<NamedType, IUnresolvedScalarBaseType> ScalarBases => scalarBases;

    public IReadOnlyDictionary<NamedType, IUnresolvedScalarType> DuplicatelyDefined => duplicatelyDefined;

    IReadOnlyDictionary<NamedType, IUnresolvedQuantityType> IUnresolvedQuantityPopulation.Quantities
        => Scalars.Transform(static (quantity) => quantity as IUnresolvedQuantityType);

    IReadOnlyDictionary<NamedType, IUnresolvedQuantityBaseType> IUnresolvedQuantityPopulation.QuantityBases
        => ScalarBases.Transform(static (quantity) => quantity as IUnresolvedQuantityBaseType);

    private ReadOnlyEquatableDictionary<NamedType, IUnresolvedScalarType> scalars { get; }
    private ReadOnlyEquatableDictionary<NamedType, IUnresolvedScalarBaseType> scalarBases { get; }

    private ReadOnlyEquatableDictionary<NamedType, IUnresolvedScalarType> duplicatelyDefined { get; }

    private UnresolvedScalarPopulation(IReadOnlyDictionary<NamedType, IUnresolvedScalarType> scalars, IReadOnlyDictionary<NamedType, IUnresolvedScalarBaseType> baseScalarByScalarType,
        IReadOnlyDictionary<NamedType, IUnresolvedScalarType> duplicatelyDefined)
    {
        this.scalars = scalars.AsReadOnlyEquatable();
        this.scalarBases = baseScalarByScalarType.AsReadOnlyEquatable();

        this.duplicatelyDefined = duplicatelyDefined.AsReadOnlyEquatable();
    }
}
