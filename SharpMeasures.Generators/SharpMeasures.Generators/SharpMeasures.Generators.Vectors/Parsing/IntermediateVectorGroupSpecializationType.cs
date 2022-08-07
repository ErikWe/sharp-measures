namespace SharpMeasures.Generators.Vectors.Parsing;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;
using SharpMeasures.Generators.Unresolved.Units.UnitInstances;
using SharpMeasures.Generators.Vectors.Parsing.Abstraction;
using SharpMeasures.Generators.Vectors.Parsing.ConvertibleVector;

using System.Collections.Generic;

internal record class IntermediateVectorGroupSpecializationType : IIntermediateVectorGroupSpecializationType
{
    public DefinedType Type { get; }
    public MinimalLocation TypeLocation { get; }

    public IVectorGroupSpecialization Definition { get; }

    public IReadOnlyDictionary<int, IRegisteredVectorGroupMember> RegisteredMembersByDimension => registeredMembersByDimension;

    public IReadOnlyList<DerivedQuantityDefinition> Derivations => derivations;
    public IReadOnlyList<ConvertibleVectorDefinition> Conversions => conversions;

    public IReadOnlyList<IUnresolvedUnitInstance> UnitInclusions => unitInclusions;
    public IReadOnlyList<IUnresolvedUnitInstance> UnitExclusions => unitExclusions;

    IReadOnlyList<IDerivedQuantity> IIntermediateVectorGroupSpecializationType.Derivations => Derivations;
    IReadOnlyList<IConvertibleVector> IIntermediateVectorGroupSpecializationType.Conversions => Conversions;

    private ReadOnlyEquatableDictionary<int, IRegisteredVectorGroupMember> registeredMembersByDimension { get; }

    private ReadOnlyEquatableList<DerivedQuantityDefinition> derivations { get; }
    private ReadOnlyEquatableList<ConvertibleVectorDefinition> conversions { get; }

    private ReadOnlyEquatableList<IUnresolvedUnitInstance> unitInclusions { get; }
    private ReadOnlyEquatableList<IUnresolvedUnitInstance> unitExclusions { get; }

    public IntermediateVectorGroupSpecializationType(DefinedType type, MinimalLocation typeLocation, IVectorGroupSpecialization definition,
        IReadOnlyDictionary<int, IRegisteredVectorGroupMember> registeredMembersByDimension, IReadOnlyList<DerivedQuantityDefinition> derivations,
        IReadOnlyList<ConvertibleVectorDefinition> conversions, IReadOnlyList<IUnresolvedUnitInstance> unitInclusions, IReadOnlyList<IUnresolvedUnitInstance> unitExclusions)
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
