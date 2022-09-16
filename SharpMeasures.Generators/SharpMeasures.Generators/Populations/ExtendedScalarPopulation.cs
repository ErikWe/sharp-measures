namespace SharpMeasures.Generators.Populations;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Scalars;

using System.Collections.Generic;
using System.Linq;

internal class ExtendedScalarPopulation : IScalarPopulation
{
    public IReadOnlyDictionary<NamedType, IScalarBaseType> ScalarBases => scalarBases;
    public IReadOnlyDictionary<NamedType, IScalarType> Scalars => scalars;

    private ReadOnlyEquatableDictionary<NamedType, IScalarBaseType> scalarBases { get; }
    private ReadOnlyEquatableDictionary<NamedType, IScalarType> scalars { get; }

    IReadOnlyDictionary<NamedType, IQuantityBaseType> IQuantityPopulation.QuantityBases => ScalarBases.Transform(static (scalarBase) => scalarBase as IQuantityBaseType);
    IReadOnlyDictionary<NamedType, IQuantityType> IQuantityPopulation.Quantities => Scalars.Transform(static (scalar) => scalar as IQuantityType);

    private ExtendedScalarPopulation(IReadOnlyDictionary<NamedType, IScalarBaseType> scalarBases, IReadOnlyDictionary<NamedType, IScalarType> scalars)
    {
        this.scalarBases = scalarBases.AsReadOnlyEquatable();
        this.scalars = scalars.AsReadOnlyEquatable();
    }

    public static ExtendedScalarPopulation Build(IScalarPopulation originalPopulation, IReadOnlyList<IScalarBaseType> additionalScalarBases, IReadOnlyList<IScalarSpecializationType> additionalScalarSpecializations)
    {
        Dictionary<NamedType, IScalarBaseType> scalarBasePopulation = new(originalPopulation.ScalarBases.Count + additionalScalarBases.Count);
        Dictionary<NamedType, IScalarSpecializationType> additionalScalarSpecializationPopulation = new(additionalScalarSpecializations.Count);

        foreach (var keyValue in originalPopulation.ScalarBases)
        {
            scalarBasePopulation.TryAdd(keyValue.Key, keyValue.Value);
        }

        foreach (var scalarBase in additionalScalarBases)
        {
            scalarBasePopulation.TryAdd(scalarBase.Type.AsNamedType(), scalarBase);
        }

        foreach (var scalarSpecialization in additionalScalarSpecializations)
        {
            additionalScalarSpecializationPopulation.TryAdd(scalarSpecialization.Type.AsNamedType(), scalarSpecialization);
        }

        Dictionary<NamedType, IScalarType> scalarPopulation = new(scalarBasePopulation.Count + additionalScalarSpecializationPopulation.Count);

        foreach (var keyValue in originalPopulation.Scalars)
        {
            scalarPopulation.Add(keyValue.Key, keyValue.Value);
        }

        foreach (var keyValue in scalarBasePopulation)
        {
            scalarPopulation.TryAdd(keyValue.Key, keyValue.Value);
        }

        foreach (var keyValue in additionalScalarSpecializationPopulation)
        {
            scalarPopulation.TryAdd(keyValue.Key, keyValue.Value);
        }

        var unassignedSpecializations = additionalScalarSpecializationPopulation.Values.ToList();

        iterativelySetBaseScalarForSpecializations();

        return new(scalarBasePopulation, scalarPopulation);

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
