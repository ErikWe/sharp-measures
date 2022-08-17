namespace SharpMeasures.Generators.Vectors.Parsing.Abstraction;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Quantities.Parsing.ConvertibleQuantity;
using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;
using SharpMeasures.Generators.Quantities.Parsing.UnitList;

using System.Collections.Generic;

internal abstract record class ARawVectorGroupType
{
    public DefinedType Type { get; }
    public MinimalLocation TypeLocation { get; }

    public IEnumerable<UnprocessedDerivedQuantityDefinition> Derivations => derivations;
    public IEnumerable<UnprocessedConvertibleQuantityDefinition> Conversions => conversions;

    public IEnumerable<UnprocessedUnitListDefinition> UnitInclusions => unitInclusions;
    public IEnumerable<UnprocessedUnitListDefinition> UnitExclusions => unitExclusions;

    private EquatableEnumerable<UnprocessedDerivedQuantityDefinition> derivations { get; }
    private EquatableEnumerable<UnprocessedConvertibleQuantityDefinition> conversions { get; }

    private EquatableEnumerable<UnprocessedUnitListDefinition> unitInclusions { get; }
    private EquatableEnumerable<UnprocessedUnitListDefinition> unitExclusions { get; }

    protected ARawVectorGroupType(DefinedType type, MinimalLocation typeLocation, IEnumerable<UnprocessedDerivedQuantityDefinition> derivations, IEnumerable<UnprocessedConvertibleQuantityDefinition> conversions,
        IEnumerable<UnprocessedUnitListDefinition> unitInclusions, IEnumerable<UnprocessedUnitListDefinition> unitExclusions)
    {
        Type = type;
        TypeLocation = typeLocation;

        this.derivations = derivations.AsEquatable();
        this.conversions = conversions.AsEquatable();

        this.unitInclusions = unitInclusions.AsEquatable();
        this.unitExclusions = unitExclusions.AsEquatable();
    }
}
