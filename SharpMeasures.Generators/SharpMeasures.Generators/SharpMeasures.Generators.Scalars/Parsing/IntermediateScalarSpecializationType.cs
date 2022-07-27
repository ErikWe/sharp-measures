namespace SharpMeasures.Generators.Scalars.Parsing;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Scalars.Parsing.Abstraction;
using SharpMeasures.Generators.Unresolved.Units.UnitInstances;

using System.Collections.Generic;

internal record class IntermediateScalarSpecializationType : IIntermediateScalarSpecializationType
{
    public DefinedType Type { get; }
    public MinimalLocation TypeLocation { get; }

    public IScalarSpecialization Definition { get; }

    public IReadOnlyList<IDerivedQuantity> Derivations => derivations;
    public IReadOnlyList<IScalarConstant> Constants => constants;
    public IReadOnlyList<IConvertibleScalar> Conversions => conversions;

    public IReadOnlyList<IUnresolvedUnitInstance> BaseInclusions => baseInclusions;
    public IReadOnlyList<IUnresolvedUnitInstance> BaseExclusions => baseExclusions;

    public IReadOnlyList<IUnresolvedUnitInstance> UnitInclusions => unitInclusions;
    public IReadOnlyList<IUnresolvedUnitInstance> UnitExclusions => unitExclusions;

    private ReadOnlyEquatableList<IDerivedQuantity> derivations { get; }
    private ReadOnlyEquatableList<IScalarConstant> constants { get; }
    private ReadOnlyEquatableList<IConvertibleScalar> conversions { get; }

    private ReadOnlyEquatableList<IUnresolvedUnitInstance> baseInclusions { get; }
    private ReadOnlyEquatableList<IUnresolvedUnitInstance> baseExclusions { get; }

    private ReadOnlyEquatableList<IUnresolvedUnitInstance> unitInclusions { get; }
    private ReadOnlyEquatableList<IUnresolvedUnitInstance> unitExclusions { get; }

    public IntermediateScalarSpecializationType(DefinedType type, MinimalLocation typeLocation, IScalarSpecialization definition, IReadOnlyList<IDerivedQuantity> derivations,
        IReadOnlyList<IScalarConstant> constants, IReadOnlyList<IConvertibleScalar> conversions, IReadOnlyList<IUnresolvedUnitInstance> baseInclusions,
        IReadOnlyList<IUnresolvedUnitInstance> baseExclusions, IReadOnlyList<IUnresolvedUnitInstance> unitInclusions, IReadOnlyList<IUnresolvedUnitInstance> unitExclusions)
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
