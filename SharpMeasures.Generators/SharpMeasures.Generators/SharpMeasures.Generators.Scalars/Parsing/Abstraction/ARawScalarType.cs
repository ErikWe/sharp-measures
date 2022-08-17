namespace SharpMeasures.Generators.Scalars.Parsing.Abstraction;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Quantities.Parsing.ConvertibleQuantity;
using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;
using SharpMeasures.Generators.Quantities.Parsing.UnitList;
using SharpMeasures.Generators.Scalars.Parsing.ScalarConstant;

using System.Collections.Generic;

internal abstract record class ARawScalarType
{
    public DefinedType Type { get; }
    public MinimalLocation TypeLocation { get; }

    public IEnumerable<UnprocessedDerivedQuantityDefinition> Derivations => derivations;
    public IEnumerable<RawScalarConstantDefinition> Constants => constants;
    public IEnumerable<UnprocessedConvertibleQuantityDefinition> Conversions => conversions;

    public IEnumerable<UnprocessedUnitListDefinition> BaseInclusions => baseInclusions;
    public IEnumerable<UnprocessedUnitListDefinition> BaseExclusions => baseExclusions;

    public IEnumerable<UnprocessedUnitListDefinition> UnitInclusions => unitInclusions;
    public IEnumerable<UnprocessedUnitListDefinition> UnitExclusions => unitExclusions;

    private EquatableEnumerable<UnprocessedDerivedQuantityDefinition> derivations { get; }
    private EquatableEnumerable<RawScalarConstantDefinition> constants { get; }
    private EquatableEnumerable<UnprocessedConvertibleQuantityDefinition> conversions { get; }

    private EquatableEnumerable<UnprocessedUnitListDefinition> baseInclusions { get; }
    private EquatableEnumerable<UnprocessedUnitListDefinition> baseExclusions { get; }

    private EquatableEnumerable<UnprocessedUnitListDefinition> unitInclusions { get; }
    private EquatableEnumerable<UnprocessedUnitListDefinition> unitExclusions { get; }

    protected ARawScalarType(DefinedType type, MinimalLocation typeLocation, IEnumerable<UnprocessedDerivedQuantityDefinition> derivations,
        IEnumerable<RawScalarConstantDefinition> constants, IEnumerable<UnprocessedConvertibleQuantityDefinition> conversions, IEnumerable<UnprocessedUnitListDefinition> baseInclusions,
        IEnumerable<UnprocessedUnitListDefinition> baseExclusions, IEnumerable<UnprocessedUnitListDefinition> unitInclusions, IEnumerable<UnprocessedUnitListDefinition> unitExclusions)
    {
        Type = type;
        TypeLocation = typeLocation;

        this.derivations = derivations.AsEquatable();
        this.constants = constants.AsEquatable();
        this.conversions = conversions.AsEquatable();

        this.baseInclusions = baseInclusions.AsEquatable();
        this.baseExclusions = baseExclusions.AsEquatable();

        this.unitInclusions = unitInclusions.AsEquatable();
        this.unitExclusions = unitExclusions.AsEquatable();
    }
}
