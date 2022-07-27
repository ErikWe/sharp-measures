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

    public IEnumerable<RawDerivedQuantityDefinition> Derivations => derivations;
    public IEnumerable<RawScalarConstantDefinition> Constants => constants;
    public IEnumerable<RawConvertibleQuantityDefinition> Conversions => conversions;

    public IEnumerable<RawUnitListDefinition> BaseInclusions => baseInclusions;
    public IEnumerable<RawUnitListDefinition> BaseExclusions => baseExclusions;

    public IEnumerable<RawUnitListDefinition> UnitInclusions => unitInclusions;
    public IEnumerable<RawUnitListDefinition> UnitExclusions => unitExclusions;

    private EquatableEnumerable<RawDerivedQuantityDefinition> derivations { get; }
    private EquatableEnumerable<RawScalarConstantDefinition> constants { get; }
    private EquatableEnumerable<RawConvertibleQuantityDefinition> conversions { get; }

    private EquatableEnumerable<RawUnitListDefinition> baseInclusions { get; }
    private EquatableEnumerable<RawUnitListDefinition> baseExclusions { get; }

    private EquatableEnumerable<RawUnitListDefinition> unitInclusions { get; }
    private EquatableEnumerable<RawUnitListDefinition> unitExclusions { get; }

    protected ARawScalarType(DefinedType type, MinimalLocation typeLocation, IEnumerable<RawDerivedQuantityDefinition> derivations,
        IEnumerable<RawScalarConstantDefinition> constants, IEnumerable<RawConvertibleQuantityDefinition> conversions, IEnumerable<RawUnitListDefinition> baseInclusions,
        IEnumerable<RawUnitListDefinition> baseExclusions, IEnumerable<RawUnitListDefinition> unitInclusions, IEnumerable<RawUnitListDefinition> unitExclusions)
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
