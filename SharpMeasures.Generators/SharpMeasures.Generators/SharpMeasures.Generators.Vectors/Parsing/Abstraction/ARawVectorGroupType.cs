namespace SharpMeasures.Generators.Vectors.Parsing.Abstraction;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Quantities.Parsing.ConvertibleQuantity;
using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;
using SharpMeasures.Generators.Quantities.Parsing.UnitList;
using SharpMeasures.Generators.Vectors.Parsing.RegisterVectorGroupMember;

using System.Collections.Generic;

internal abstract record class ARawVectorGroupType
{
    public DefinedType Type { get; }
    public MinimalLocation TypeLocation { get; }

    public IEnumerable<RawRegisterVectorGroupMemberDefinition> Members => members;

    public IEnumerable<RawDerivedQuantityDefinition> Derivations => derivations;
    public IEnumerable<RawConvertibleQuantityDefinition> Conversions => conversions;

    public IEnumerable<RawUnitListDefinition> UnitInclusions => unitInclusions;
    public IEnumerable<RawUnitListDefinition> UnitExclusions => unitExclusions;

    private EquatableEnumerable<RawRegisterVectorGroupMemberDefinition> members { get; }

    private EquatableEnumerable<RawDerivedQuantityDefinition> derivations { get; }
    private EquatableEnumerable<RawConvertibleQuantityDefinition> conversions { get; }

    private EquatableEnumerable<RawUnitListDefinition> unitInclusions { get; }
    private EquatableEnumerable<RawUnitListDefinition> unitExclusions { get; }

    protected ARawVectorGroupType(DefinedType type, MinimalLocation typeLocation, IEnumerable<RawRegisterVectorGroupMemberDefinition> members,
        IEnumerable<RawDerivedQuantityDefinition> derivations, IEnumerable<RawConvertibleQuantityDefinition> conversions,
        IEnumerable<RawUnitListDefinition> unitInclusions, IEnumerable<RawUnitListDefinition> unitExclusions)
    {
        Type = type;
        TypeLocation = typeLocation;

        this.members = members.AsEquatable();

        this.derivations = derivations.AsEquatable();
        this.conversions = conversions.AsEquatable();

        this.unitInclusions = unitInclusions.AsEquatable();
        this.unitExclusions = unitExclusions.AsEquatable();
    }
}
