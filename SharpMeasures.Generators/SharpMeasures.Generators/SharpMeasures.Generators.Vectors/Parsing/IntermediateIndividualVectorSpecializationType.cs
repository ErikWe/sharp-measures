namespace SharpMeasures.Generators.Vectors.Parsing;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;
using SharpMeasures.Generators.Unresolved.Units.UnitInstances;
using SharpMeasures.Generators.Unresolved.Vectors;
using SharpMeasures.Generators.Vectors.Parsing.Abstraction;
using SharpMeasures.Generators.Vectors.Parsing.ConvertibleVector;
using SharpMeasures.Generators.Vectors.Parsing.VectorConstant;

using System.Collections.Generic;

internal record class IntermediateIndividualVectorSpecializationType : IIntermediateIndividualVectorSpecializationType
{
    public DefinedType Type { get; }
    public MinimalLocation TypeLocation { get; }

    public IIndividualVectorSpecialization Definition { get; }

    public IReadOnlyList<DerivedQuantityDefinition> Derivations => derivations;
    public IReadOnlyList<VectorConstantDefinition> Constants => constants;
    public IReadOnlyList<ConvertibleVectorDefinition> Conversions => conversions;

    public IReadOnlyDictionary<int, IUnresolvedVectorGroupMemberType> MembersByDimension => membersByDimension;

    public IReadOnlyList<IUnresolvedUnitInstance> UnitInclusions => unitInclusions;
    public IReadOnlyList<IUnresolvedUnitInstance> UnitExclusions => unitExclusions;

    IReadOnlyList<IDerivedQuantity> IIntermediateIndividualVectorSpecializationType.Derivations => Derivations;
    IReadOnlyList<IVectorConstant> IIntermediateIndividualVectorSpecializationType.Constants => Constants;
    IReadOnlyList<IConvertibleVector> IIntermediateIndividualVectorSpecializationType.Conversions => Conversions;

    private ReadOnlyEquatableList<DerivedQuantityDefinition> derivations { get; }
    private ReadOnlyEquatableList<VectorConstantDefinition> constants { get; }
    private ReadOnlyEquatableList<ConvertibleVectorDefinition> conversions { get; }

    private ReadOnlyEquatableDictionary<int, IUnresolvedVectorGroupMemberType> membersByDimension { get; }

    private ReadOnlyEquatableList<IUnresolvedUnitInstance> unitInclusions { get; }
    private ReadOnlyEquatableList<IUnresolvedUnitInstance> unitExclusions { get; }

    public IntermediateIndividualVectorSpecializationType(DefinedType type, MinimalLocation typeLocation, IIndividualVectorSpecialization definition, IReadOnlyList<DerivedQuantityDefinition> derivations,
        IReadOnlyList<VectorConstantDefinition> constants, IReadOnlyList<ConvertibleVectorDefinition> conversions, IReadOnlyDictionary<int, IUnresolvedVectorGroupMemberType> membersByDimension,
        IReadOnlyList<IUnresolvedUnitInstance> unitInclusions, IReadOnlyList<IUnresolvedUnitInstance> unitExclusions)
    {
        Type = type;
        TypeLocation = typeLocation;

        Definition = definition;

        this.derivations = derivations.AsReadOnlyEquatable();
        this.constants = constants.AsReadOnlyEquatable();
        this.conversions = conversions.AsReadOnlyEquatable();

        this.membersByDimension = membersByDimension.AsReadOnlyEquatable();

        this.unitInclusions = unitInclusions.AsReadOnlyEquatable();
        this.unitExclusions = unitExclusions.AsReadOnlyEquatable();
    }
}
