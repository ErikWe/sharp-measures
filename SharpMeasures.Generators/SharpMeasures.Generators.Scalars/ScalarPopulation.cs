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

    public IReadOnlyDictionary<NamedType, IScalarBaseType> DuplicatelyDefinedScalarBases => duplicatelyDefinedScalarBases;
    public IReadOnlyDictionary<NamedType, IScalarSpecializationType> DuplicatelyDefinedScalarSpecializations => duplicatelyDefinedScalarSpecializations;
    public IReadOnlyDictionary<NamedType, IScalarSpecializationType> ScalarSpecializationsAlreadyDefinedAsScalarBases => scalarSpecializationsAlreadyDefinedAsScalarBases;

    private ReadOnlyEquatableDictionary<NamedType, IScalarBaseType> scalarBases { get; }
    private ReadOnlyEquatableDictionary<NamedType, IScalarType> scalars { get; }

    private ReadOnlyEquatableDictionary<NamedType, IScalarBaseType> duplicatelyDefinedScalarBases { get; }
    private ReadOnlyEquatableDictionary<NamedType, IScalarSpecializationType> duplicatelyDefinedScalarSpecializations { get; }
    private ReadOnlyEquatableDictionary<NamedType, IScalarSpecializationType> scalarSpecializationsAlreadyDefinedAsScalarBases { get; }

    IReadOnlyDictionary<NamedType, IQuantityBaseType> IQuantityPopulation.QuantityBases => ScalarBases.Transform(static (scalarBase) => scalarBase as IQuantityBaseType);
    IReadOnlyDictionary<NamedType, IQuantityType> IQuantityPopulation.Quantities => Scalars.Transform(static (scalar) => scalar as IQuantityType);

    private ScalarPopulation(IReadOnlyDictionary<NamedType, IScalarBaseType> scalarBases, IReadOnlyDictionary<NamedType, IScalarType> scalars, IReadOnlyDictionary<NamedType, IScalarBaseType> duplicatelyDefinedScalarBases,
        IReadOnlyDictionary<NamedType, IScalarSpecializationType> duplicatelyDefinedScalarSpecializations, IReadOnlyDictionary<NamedType, IScalarSpecializationType> scalarSpecializationsAlreadyDefinedAsScalarBases)
    {
        this.scalarBases = scalarBases.AsReadOnlyEquatable();
        this.scalars = scalars.AsReadOnlyEquatable();

        this.duplicatelyDefinedScalarBases = duplicatelyDefinedScalarBases.AsReadOnlyEquatable();
        this.duplicatelyDefinedScalarSpecializations = duplicatelyDefinedScalarSpecializations.AsReadOnlyEquatable();
        this.scalarSpecializationsAlreadyDefinedAsScalarBases = scalarSpecializationsAlreadyDefinedAsScalarBases.AsReadOnlyEquatable();
    }

    public static ScalarPopulation Build(IReadOnlyList<IScalarBaseType> scalarBases, IReadOnlyList<IScalarSpecializationType> scalarSpecializations)
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

        var unassignedSpecializations = scalarSpecializationPopulation.Values.ToList();

        iterativelySetBaseScalarForSpecializations();

        return new(scalarBasePopulation, scalarPopulation, duplicateScalarBases, duplicateScalarSpecializations, scalarSpecializationsAlreadyDefinedAsBases);

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
