namespace SharpMeasures.Generators.Vectors.Parsing;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Unresolved.Units.UnitInstances;
using SharpMeasures.Generators.Vectors.Parsing.Abstraction;

using System.Collections.Generic;

internal record class IntermediateVectorGroupSpecializationType : IIntermediateVectorGroupSpecializationType
{
    public DefinedType Type { get; }
    public MinimalLocation TypeLocation { get; }

    public IVectorGroupSpecialization Definition { get; }

    public IReadOnlyDictionary<int, IRegisteredVectorGroupMember> RegisteredMembersByDimension => registeredMembersByDimension;

    public IReadOnlyList<IDerivedQuantity> Derivations => derivations;
    public IReadOnlyList<IConvertibleVector> Conversions => conversions;

    public IReadOnlyList<IUnresolvedUnitInstance> UnitInclusions => unitInclusions;
    public IReadOnlyList<IUnresolvedUnitInstance> UnitExclusions => unitExclusions;

    private ReadOnlyEquatableDictionary<int, IRegisteredVectorGroupMember> registeredMembersByDimension { get; }

    private ReadOnlyEquatableList<IDerivedQuantity> derivations { get; }
    private ReadOnlyEquatableList<IConvertibleVector> conversions { get; }

    private ReadOnlyEquatableList<IUnresolvedUnitInstance> unitInclusions { get; }
    private ReadOnlyEquatableList<IUnresolvedUnitInstance> unitExclusions { get; }

    public IntermediateVectorGroupSpecializationType(DefinedType type, MinimalLocation typeLocation, IVectorGroupSpecialization definition,
        IReadOnlyDictionary<int, IRegisteredVectorGroupMember> registeredMembersByDimension, IReadOnlyList<IDerivedQuantity> derivations,
        IReadOnlyList<IConvertibleVector> conversions, IReadOnlyList<IUnresolvedUnitInstance> unitInclusions, IReadOnlyList<IUnresolvedUnitInstance> unitExclusions)
    {
        Type = type;
        TypeLocation = typeLocation;

        Definition = definition;

        this.registeredMembersByDimension = registeredMembersByDimension.AsReadOnlyEquatable();

        this.derivations = derivations.AsReadOnlyEquatable();
        this.conversions = conversions.AsReadOnlyEquatable();

        this.unitInclusions = unitInclusions.AsReadOnlyEquatable();
        this.unitExclusions = unitExclusions.AsReadOnlyEquatable();
    }
}
