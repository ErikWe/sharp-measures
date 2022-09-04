namespace SharpMeasures.Generators.Scalars;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Scalars.Parsing.Abstraction;

using System.Collections.Generic;
using System.Linq;

internal class ScalarPopulation : IScalarPopulationWithData
{
    public IReadOnlyDictionary<NamedType, IScalarBaseType> ScalarBases => scalarBases;
    public IReadOnlyDictionary<NamedType, IScalarType> Scalars => scalars;

    public IReadOnlyDictionary<NamedType, IScalarType> DuplicatelyDefinedScalars => duplicatelyDefinedScalars;

    private ReadOnlyEquatableDictionary<NamedType, IScalarBaseType> scalarBases { get; }
    private ReadOnlyEquatableDictionary<NamedType, IScalarType> scalars { get; }

    private ReadOnlyEquatableDictionary<NamedType, IScalarType> duplicatelyDefinedScalars { get; }

    IReadOnlyDictionary<NamedType, IQuantityBaseType> IQuantityPopulation.QuantityBases => ScalarBases.Transform(static (scalarBase) => scalarBase as IQuantityBaseType);
    IReadOnlyDictionary<NamedType, IQuantityType> IQuantityPopulation.Quantities => Scalars.Transform(static (scalar) => scalar as IQuantityType);

    private ScalarPopulation(IReadOnlyDictionary<NamedType, IScalarBaseType> scalarBases, IReadOnlyDictionary<NamedType, IScalarType> scalars, IReadOnlyDictionary<NamedType, IScalarType> duplicatelyDefinedScalars)
    {
        this.scalarBases = scalarBases.AsReadOnlyEquatable();
        this.scalars = scalars.AsReadOnlyEquatable();

        this.duplicatelyDefinedScalars = duplicatelyDefinedScalars.AsReadOnlyEquatable();
    }

    public static ScalarPopulation Build(IReadOnlyList<IScalarBaseType> scalarBases, IReadOnlyList<IScalarSpecializationType> scalarSpecializations)
    {
        Dictionary<NamedType, IScalarBaseType> scalarBasePopulation = new(scalarBases.Count);
        Dictionary<NamedType, IScalarType> scalarPopulation = new(scalarBases.Count + scalarSpecializations.Count);

        Dictionary<NamedType, IScalarType> duplicatePopulation = new();

        foreach (var scalar in (scalarBases as IEnumerable<IScalarType>).Concat(scalarSpecializations))
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

        return new(scalarBasePopulation, scalarPopulation, duplicatePopulation);

        void iterativelySetBaseScalarForSpecializations()
        {
            int startLength = unassignedSpecializations.Count;

            for (int i = 0; i < unassignedSpecializations.Count; i++)
            {
                if (scalarBasePopulation.TryGetValue(unassignedSpecializations[i].Definition.OriginalQuantity, out var scalarBase))
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
}
