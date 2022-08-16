namespace SharpMeasures.Generators.Vectors.Parsing;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;
using SharpMeasures.Generators.Raw.Units.UnitInstances;
using SharpMeasures.Generators.Raw.Vectors.Groups;
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

    public IReadOnlyDictionary<int, IRawVectorGroupMemberType> MembersByDimension => membersByDimension;

    public IReadOnlyList<IRawUnitInstance> UnitInclusions => unitInclusions;
    public IReadOnlyList<IRawUnitInstance> UnitExclusions => unitExclusions;

    IReadOnlyList<IDerivedQuantity> IIntermediateIndividualVectorSpecializationType.Derivations => Derivations;
    IReadOnlyList<IVectorConstant> IIntermediateIndividualVectorSpecializationType.Constants => Constants;
    IReadOnlyList<IConvertibleVector> IIntermediateIndividualVectorSpecializationType.Conversions => Conversions;

    private ReadOnlyEquatableList<DerivedQuantityDefinition> derivations { get; }
    private ReadOnlyEquatableList<VectorConstantDefinition> constants { get; }
    private ReadOnlyEquatableList<ConvertibleVectorDefinition> conversions { get; }

    private ReadOnlyEquatableDictionary<int, IRawVectorGroupMemberType> membersByDimension { get; }

    private ReadOnlyEquatableList<IRawUnitInstance> unitInclusions { get; }
    private ReadOnlyEquatableList<IRawUnitInstance> unitExclusions { get; }

    public IntermediateIndividualVectorSpecializationType(DefinedType type, MinimalLocation typeLocation, IIndividualVectorSpecialization definition, IReadOnlyList<DerivedQuantityDefinition> derivations,
        IReadOnlyList<VectorConstantDefinition> constants, IReadOnlyList<ConvertibleVectorDefinition> conversions, IReadOnlyDictionary<int, IRawVectorGroupMemberType> membersByDimension,
        IReadOnlyList<IRawUnitInstance> unitInclusions, IReadOnlyList<IRawUnitInstance> unitExclusions)
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
