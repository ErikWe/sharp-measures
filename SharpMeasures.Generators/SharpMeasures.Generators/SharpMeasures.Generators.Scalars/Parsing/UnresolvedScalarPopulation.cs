namespace SharpMeasures.Generators.Scalars.Parsing;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Scalars.Parsing.Abstraction;
using SharpMeasures.Generators.Raw.Quantities;
using SharpMeasures.Generators.Raw.Scalars;

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

internal class UnresolvedScalarPopulation : IUnresolvedScalarPopulationWithData
{
    public static UnresolvedScalarPopulation Build(ImmutableArray<IRawScalarBaseType> scalarBases, ImmutableArray<IRawScalarSpecializationType> scalarSpecializations)
    {
        Dictionary<NamedType, IRawScalarType> scalarPopulation = new(scalarBases.Length + scalarSpecializations.Length);
        Dictionary<NamedType, IRawScalarBaseType> scalarBasePopulation = new(scalarBases.Length);

        Dictionary<NamedType, IRawScalarType> duplicatePopulation = new();

        foreach (var scalar in (scalarBases as IEnumerable<IRawScalarType>).Concat(scalarSpecializations))
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

    public IReadOnlyDictionary<NamedType, IRawScalarType> Scalars => scalars;
    public IReadOnlyDictionary<NamedType, IRawScalarBaseType> ScalarBases => scalarBases;

    public IReadOnlyDictionary<NamedType, IRawScalarType> DuplicatelyDefined => duplicatelyDefined;

    IReadOnlyDictionary<NamedType, IRawQuantityType> IRawQuantityPopulation.Quantities
        => Scalars.Transform(static (quantity) => quantity as IRawQuantityType);

    IReadOnlyDictionary<NamedType, IRawQuantityBaseType> IRawQuantityPopulation.QuantityBases
        => ScalarBases.Transform(static (quantity) => quantity as IRawQuantityBaseType);

    private ReadOnlyEquatableDictionary<NamedType, IRawScalarType> scalars { get; }
    private ReadOnlyEquatableDictionary<NamedType, IRawScalarBaseType> scalarBases { get; }

    private ReadOnlyEquatableDictionary<NamedType, IRawScalarType> duplicatelyDefined { get; }

    private UnresolvedScalarPopulation(IReadOnlyDictionary<NamedType, IRawScalarType> scalars, IReadOnlyDictionary<NamedType, IRawScalarBaseType> baseScalarByScalarType,
        IReadOnlyDictionary<NamedType, IRawScalarType> duplicatelyDefined)
    {
        this.scalars = scalars.AsReadOnlyEquatable();
        this.scalarBases = baseScalarByScalarType.AsReadOnlyEquatable();

        this.duplicatelyDefined = duplicatelyDefined.AsReadOnlyEquatable();
    }
}
