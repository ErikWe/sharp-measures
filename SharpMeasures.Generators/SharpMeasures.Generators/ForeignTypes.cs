namespace SharpMeasures.Generators;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Vectors;

using System.Collections.Generic;

internal record class ForeignTypes
{
    public IReadOnlyList<IUnitType> Units => units;

    public IReadOnlyList<IScalarBaseType> ScalarBases => scalarBases;
    public IReadOnlyList<IScalarSpecializationType> ScalarSpecializations => scalarSpecializations;

    public IReadOnlyList<IVectorGroupBaseType> GroupBases => groupBases;
    public IReadOnlyList<IVectorGroupSpecializationType> GroupSpecializations => groupSpecializations;
    public IReadOnlyList<IVectorGroupMemberType> GroupMembers => groupMembers;

    public IReadOnlyList<IVectorBaseType> VectorBases => vectorBases;
    public IReadOnlyList<IVectorSpecializationType> VectorSpecializations => vectorSpecializations;

    private ReadOnlyEquatableList<IUnitType> units { get; }

    private ReadOnlyEquatableList<IScalarBaseType> scalarBases { get; }
    private ReadOnlyEquatableList<IScalarSpecializationType> scalarSpecializations { get; }

    private ReadOnlyEquatableList<IVectorGroupBaseType> groupBases { get; }
    private ReadOnlyEquatableList<IVectorGroupSpecializationType> groupSpecializations { get; }
    private ReadOnlyEquatableList<IVectorGroupMemberType> groupMembers { get; }

    private ReadOnlyEquatableList<IVectorBaseType> vectorBases { get; }
    private ReadOnlyEquatableList<IVectorSpecializationType> vectorSpecializations { get; }

    public ForeignTypes(IReadOnlyList<IUnitType> units, IReadOnlyList<IScalarBaseType> scalarBases, IReadOnlyList<IScalarSpecializationType> scalarSpecializations, IReadOnlyList<IVectorGroupBaseType> groupBases,
        IReadOnlyList<IVectorGroupSpecializationType> groupSpecializations, IReadOnlyList<IVectorGroupMemberType> groupMembers, IReadOnlyList<IVectorBaseType> vectorBases, IReadOnlyList<IVectorSpecializationType> vectorSpecializations)
    {
        this.units = units.AsReadOnlyEquatable();

        this.scalarBases = scalarBases.AsReadOnlyEquatable();
        this.scalarSpecializations = scalarSpecializations.AsReadOnlyEquatable();

        this.groupBases = groupBases.AsReadOnlyEquatable();
        this.groupSpecializations = groupSpecializations.AsReadOnlyEquatable();
        this.groupMembers = groupMembers.AsReadOnlyEquatable();

        this.vectorBases = vectorBases.AsReadOnlyEquatable();
        this.vectorSpecializations = vectorSpecializations.AsReadOnlyEquatable();
    }
}
