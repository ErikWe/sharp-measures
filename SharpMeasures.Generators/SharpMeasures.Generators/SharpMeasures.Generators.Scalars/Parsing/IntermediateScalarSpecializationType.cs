namespace SharpMeasures.Generators.Scalars.Parsing;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;
using SharpMeasures.Generators.Scalars.Parsing.Abstraction;
using SharpMeasures.Generators.Scalars.Parsing.ConvertibleScalar;
using SharpMeasures.Generators.Scalars.Parsing.ScalarConstant;
using SharpMeasures.Generators.Raw.Units.UnitInstances;

using System.Collections.Generic;

internal record class IntermediateScalarSpecializationType : IIntermediateScalarSpecializationType
{
    public DefinedType Type { get; }
    public MinimalLocation TypeLocation { get; }

    public IScalarSpecialization Definition { get; }

    public IReadOnlyList<DerivedQuantityDefinition> Derivations => derivations;
    public IReadOnlyList<ScalarConstantDefinition> Constants => constants;
    public IReadOnlyList<ConvertibleScalarDefinition> Conversions => conversions;

    public IReadOnlyList<IRawUnitInstance> BaseInclusions => baseInclusions;
    public IReadOnlyList<IRawUnitInstance> BaseExclusions => baseExclusions;

    public IReadOnlyList<IRawUnitInstance> UnitInclusions => unitInclusions;
    public IReadOnlyList<IRawUnitInstance> UnitExclusions => unitExclusions;

    IReadOnlyList<IDerivedQuantity> IIntermediateScalarSpecializationType.Derivations => Derivations;
    IReadOnlyList<IScalarConstant> IIntermediateScalarSpecializationType.Constants => Constants;
    IReadOnlyList<IConvertibleScalar> IIntermediateScalarSpecializationType.Conversions => Conversions;

    private ReadOnlyEquatableList<DerivedQuantityDefinition> derivations { get; }
    private ReadOnlyEquatableList<ScalarConstantDefinition> constants { get; }
    private ReadOnlyEquatableList<ConvertibleScalarDefinition> conversions { get; }

    private ReadOnlyEquatableList<IRawUnitInstance> baseInclusions { get; }
    private ReadOnlyEquatableList<IRawUnitInstance> baseExclusions { get; }

    private ReadOnlyEquatableList<IRawUnitInstance> unitInclusions { get; }
    private ReadOnlyEquatableList<IRawUnitInstance> unitExclusions { get; }

    public IntermediateScalarSpecializationType(DefinedType type, MinimalLocation typeLocation, IScalarSpecialization definition, IReadOnlyList<DerivedQuantityDefinition> derivations,
        IReadOnlyList<ScalarConstantDefinition> constants, IReadOnlyList<ConvertibleScalarDefinition> conversions, IReadOnlyList<IRawUnitInstance> baseInclusions,
        IReadOnlyList<IRawUnitInstance> baseExclusions, IReadOnlyList<IRawUnitInstance> unitInclusions, IReadOnlyList<IRawUnitInstance> unitExclusions)
    {
        Type = type;
        TypeLocation = typeLocation;

        Definition = definition;

        this.derivations = derivations.AsReadOnlyEquatable();
        this.constants = constants.AsReadOnlyEquatable();
        this.conversions = conversions.AsReadOnlyEquatable();

        this.baseInclusions = baseInclusions.AsReadOnlyEquatable();
        this.baseExclusions = baseExclusions.AsReadOnlyEquatable();

        this.unitInclusions = unitInclusions.AsReadOnlyEquatable();
        this.unitExclusions = unitExclusions.AsReadOnlyEquatable();
    }
}
