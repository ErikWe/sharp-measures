﻿namespace SharpMeasures.Generators.Scalars;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Scalars.Parsing.Abstraction;

using System.Collections.Generic;

internal sealed record class ScalarPopulation : IScalarPopulation
{
    public IReadOnlyDictionary<NamedType, IScalarBaseType> ScalarBases { get; }
    public IReadOnlyDictionary<NamedType, IScalarType> Scalars { get; }

    IReadOnlyDictionary<NamedType, IQuantityBaseType> IQuantityPopulation.QuantityBases => ScalarBases.Transform(static (scalarBase) => scalarBase as IQuantityBaseType);
    IReadOnlyDictionary<NamedType, IQuantityType> IQuantityPopulation.Quantities => Scalars.Transform(static (scalar) => scalar as IQuantityType);

    private ScalarPopulation(IReadOnlyDictionary<NamedType, IScalarBaseType> scalarBases, IReadOnlyDictionary<NamedType, IScalarType> scalars)
    {
        ScalarBases = scalarBases.AsReadOnlyEquatable();
        Scalars = scalars.AsReadOnlyEquatable();
    }

    public static (ScalarPopulation Population, ScalarProcessingData ProcessingData) Build(IReadOnlyList<IScalarBaseType> scalarBases, IReadOnlyList<IScalarSpecializationType> scalarSpecializations)
    {
        Dictionary<NamedType, IScalarBaseType> scalarBasePopulation = new(scalarBases.Count);
        Dictionary<NamedType, IScalarSpecializationType> scalarSpecializationPopulation = new(scalarSpecializations.Count);

        Dictionary<NamedType, IScalarBaseType> duplicateScalarBases = new();
        Dictionary<NamedType, IScalarSpecializationType> duplicateScalarSpecializations = new();
        Dictionary<NamedType, IScalarSpecializationType> scalarSpecializationsAlreadyDefinedAsBases = new();

        foreach (var scalarBase in scalarBases)
        {
            if (scalarBasePopulation.TryAdd(scalarBase.Type.AsNamedType(), scalarBase))
            {
                continue;
            }

            duplicateScalarBases.TryAdd(scalarBase.Type.AsNamedType(), scalarBase);
        }

        foreach (var scalarSpecialization in scalarSpecializations)
        {
            if (scalarBasePopulation.ContainsKey(scalarSpecialization.Type.AsNamedType()))
            {
                scalarSpecializationsAlreadyDefinedAsBases.TryAdd(scalarSpecialization.Type.AsNamedType(), scalarSpecialization);

                continue;
            }

            if (scalarSpecializationPopulation.TryAdd(scalarSpecialization.Type.AsNamedType(), scalarSpecialization))
            {
                continue;
            }

            duplicateScalarSpecializations.TryAdd(scalarSpecialization.Type.AsNamedType(), scalarSpecialization);
        }

        Dictionary<NamedType, IScalarType> scalarPopulation = new(scalarBasePopulation.Count + scalarSpecializationPopulation.Count);

        foreach (var keyValue in scalarBasePopulation)
        {
            scalarPopulation.Add(keyValue.Key, keyValue.Value);
        }

        foreach (var keyValue in scalarSpecializationPopulation)
        {
            scalarPopulation.Add(keyValue.Key, keyValue.Value);
        }

        IterativelySetScalarBaseForSpecializations(scalarBasePopulation, scalarSpecializationPopulation);

        return (new ScalarPopulation(scalarBasePopulation, scalarPopulation), new ScalarProcessingData(duplicateScalarBases, duplicateScalarSpecializations, scalarSpecializationsAlreadyDefinedAsBases));
    }

    public static ScalarPopulation BuildWithoutProcessingData(IReadOnlyList<IScalarBaseType> scalarBases, IReadOnlyList<IScalarSpecializationType> scalarSpecializations)
    {
        Dictionary<NamedType, IScalarBaseType> scalarBasePopulation = new(scalarBases.Count);
        Dictionary<NamedType, IScalarSpecializationType> scalarSpecializationPopulation = new(scalarBases.Count + scalarSpecializations.Count);

        foreach (var scalarBase in scalarBases)
        {
            scalarBasePopulation.TryAdd(scalarBase.Type.AsNamedType(), scalarBase);
        }

        foreach (var scalarSpecialization in scalarSpecializations)
        {
            scalarSpecializationPopulation.TryAdd(scalarSpecialization.Type.AsNamedType(), scalarSpecialization);
        }

        Dictionary<NamedType, IScalarType> scalarPopulation = new(scalarBasePopulation.Count + scalarSpecializationPopulation.Count);

        foreach (var keyValue in scalarBasePopulation)
        {
            scalarPopulation.Add(keyValue.Key, keyValue.Value);
        }

        foreach (var keyValue in scalarSpecializationPopulation)
        {
            scalarPopulation.Add(keyValue.Key, keyValue.Value);
        }

        IterativelySetScalarBaseForSpecializations(scalarBasePopulation, scalarSpecializationPopulation);

        return new(scalarBasePopulation, scalarPopulation);
    }

    private static void IterativelySetScalarBaseForSpecializations(Dictionary<NamedType, IScalarBaseType> scalarBasePopulation, IReadOnlyDictionary<NamedType, IScalarSpecializationType> scalarSpecializationPopulation)
    {
        List<IScalarSpecializationType> unassignedSpecializations = new(scalarSpecializationPopulation.Count);

        foreach (var scalarSpecialization in scalarSpecializationPopulation)
        {
            unassignedSpecializations.Add(scalarSpecialization.Value);
        }

        iterativelySetBaseScalarForSpecializations();

        void iterativelySetBaseScalarForSpecializations()
        {
            var startLength = unassignedSpecializations.Count;

            for (var i = 0; i < unassignedSpecializations.Count; i++)
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
