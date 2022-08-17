namespace SharpMeasures.Generators.Vectors.Parsing.Abstraction;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;
using SharpMeasures.Generators.Quantities.Parsing.UnitList;
using SharpMeasures.Generators.Raw.Quantities;
using SharpMeasures.Generators.Raw.Vectors;
using SharpMeasures.Generators.Raw.Vectors.Groups;
using SharpMeasures.Generators.Vectors.Parsing.ConvertibleVector;

using System.Collections.Generic;

internal abstract record class AUnresolvedVectorGroupType<TDefinition> : IRawVectorGroupType
    where TDefinition : IRawVectorGroup
{
    public DefinedType Type { get; }
    public MinimalLocation TypeLocation { get; }

    public TDefinition Definition { get; }
    ISharpMeasuresObject ISharpMeasuresObjectType.Definition => Definition;
    IRawQuantity IRawQuantityType.Definition => Definition;
    IRawVectorGroup IRawVectorGroupType.Definition => Definition;

    public IReadOnlyList<RawDerivedQuantityDefinition> Derivations => derivations;
    public IReadOnlyList<UnresolvedConvertibleVectorDefinition> Conversions => conversions;

    public IReadOnlyList<RawUnitListDefinition> UnitInclusions => unitInclusions;
    public IReadOnlyList<RawUnitListDefinition> UnitExclusions => unitExclusions;

    private ReadOnlyEquatableList<RawDerivedQuantityDefinition> derivations { get; }
    private ReadOnlyEquatableList<UnresolvedConvertibleVectorDefinition> conversions { get; }

    private ReadOnlyEquatableList<RawUnitListDefinition> unitInclusions { get; }
    private ReadOnlyEquatableList<RawUnitListDefinition> unitExclusions { get; }

    IReadOnlyList<IRawDerivedQuantity> IRawQuantityType.Derivations => Derivations;
    IReadOnlyList<IRawConvertibleVectorGroup> IRawVectorGroupType.Conversions => Conversions;
    IReadOnlyList<IRawConvertibleQuantity> IRawQuantityType.Conversions => Conversions;
    IReadOnlyList<IRawUnitList> IRawQuantityType.UnitInclusions => UnitInclusions;
    IReadOnlyList<IRawUnitList> IRawQuantityType.UnitExclusions => UnitExclusions;

    protected AUnresolvedVectorGroupType(DefinedType type, MinimalLocation typeLocation, TDefinition definition, IReadOnlyList<RawDerivedQuantityDefinition> derivations,
        IReadOnlyList<UnresolvedConvertibleVectorDefinition> conversions, IReadOnlyList<RawUnitListDefinition> unitInclusions, IReadOnlyList<RawUnitListDefinition> unitExclusions)
    {
        Type = type;
        TypeLocation = typeLocation;

        Definition = definition;

        this.derivations = derivations.AsReadOnlyEquatable();
        this.conversions = conversions.AsReadOnlyEquatable();

        this.unitInclusions = unitInclusions.AsReadOnlyEquatable();
        this.unitExclusions = unitExclusions.AsReadOnlyEquatable();
    }
}
