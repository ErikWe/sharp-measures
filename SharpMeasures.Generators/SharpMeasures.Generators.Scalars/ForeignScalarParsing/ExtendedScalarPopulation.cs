namespace SharpMeasures.Generators.Scalars.ForeignScalarParsing;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Scalars;

using System.Collections.Generic;

internal sealed record class ExtendedScalarPopulation : IScalarPopulation
{
    public IReadOnlyDictionary<NamedType, IScalarBaseType> ScalarBases { get; }
    public IReadOnlyDictionary<NamedType, IScalarType> Scalars { get; }

    IReadOnlyDictionary<NamedType, IQuantityBaseType> IQuantityPopulation.QuantityBases => ScalarBases.Transform(static (scalarBase) => scalarBase as IQuantityBaseType);
    IReadOnlyDictionary<NamedType, IQuantityType> IQuantityPopulation.Quantities => Scalars.Transform(static (scalar) => scalar as IQuantityType);

    private ExtendedScalarPopulation(IReadOnlyDictionary<NamedType, IScalarBaseType> scalarBases, IReadOnlyDictionary<NamedType, IScalarType> scalars)
    {
        ScalarBases = scalarBases.AsReadOnlyEquatable();
        Scalars = scalars.AsReadOnlyEquatable();
    }

    public static ExtendedScalarPopulation Build(IScalarPopulation originalPopulation, ForeignScalarProcessingResult foreignPopulation)
    {
        Dictionary<NamedType, IScalarBaseType> scalarBasePopulation = new(originalPopulation.ScalarBases.Count + foreignPopulation.ScalarBases.Count);
        Dictionary<NamedType, IScalarSpecializationType> additionalScalarSpecializationPopulation = new(foreignPopulation.ScalarSpecializations.Count);

        foreach (var keyValue in originalPopulation.ScalarBases)
        {
            scalarBasePopulation.TryAdd(keyValue.Key, keyValue.Value);
        }

        foreach (var scalarBase in foreignPopulation.ScalarBases)
        {
            scalarBasePopulation.TryAdd(scalarBase.Type.AsNamedType(), scalarBase);
        }

        foreach (var scalarSpecialization in foreignPopulation.ScalarSpecializations)
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

        List<IScalarSpecializationType> unassignedSpecializations = new(additionalScalarSpecializationPopulation.Count);

        foreach (var additionalScalarSpecialization in additionalScalarSpecializationPopulation)
        {
            unassignedSpecializations.Add(additionalScalarSpecialization.Value);
        }

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
