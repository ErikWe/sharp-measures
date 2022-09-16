namespace SharpMeasures.Generators.Scalars.Parsing.Abstraction;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.ConvertibleQuantity;
using SharpMeasures.Generators.Scalars.Parsing.ExcludeUnitBases;
using SharpMeasures.Generators.Scalars.Parsing.IncludeUnitBases;
using SharpMeasures.Generators.Scalars.Parsing.ScalarConstant;

using System.Collections.Generic;

internal record class ARawScalarType<TDefinition>
{
    public DefinedType Type { get; }
    public MinimalLocation TypeLocation { get; }

    public TDefinition Definition { get; }

    public IEnumerable<RawDerivedQuantityDefinition> Derivations => derivations;
    public IEnumerable<RawScalarConstantDefinition> Constants => constants;
    public IEnumerable<RawConvertibleQuantityDefinition> Conversions => conversions;

    public IEnumerable<RawIncludeUnitBasesDefinition> UnitBaseInstanceInclusions => unitBaseInstanceInclusions;
    public IEnumerable<RawExcludeUnitBasesDefinition> UnitBaseInstanceExclusions => unitBaseInstanceExclusions;

    public IEnumerable<RawIncludeUnitsDefinition> UnitInstanceInclusions => unitInstanceInclusions;
    public IEnumerable<RawExcludeUnitsDefinition> UnitInstanceExclusions => unitInstanceExclusions;

    private EquatableEnumerable<RawDerivedQuantityDefinition> derivations { get; }
    private EquatableEnumerable<RawScalarConstantDefinition> constants { get; }
    private EquatableEnumerable<RawConvertibleQuantityDefinition> conversions { get; }

    private EquatableEnumerable<RawIncludeUnitBasesDefinition> unitBaseInstanceInclusions { get; }
    private EquatableEnumerable<RawExcludeUnitBasesDefinition> unitBaseInstanceExclusions { get; }

    private EquatableEnumerable<RawIncludeUnitsDefinition> unitInstanceInclusions { get; }
    private EquatableEnumerable<RawExcludeUnitsDefinition> unitInstanceExclusions { get; }

    protected ARawScalarType(DefinedType type, MinimalLocation typeLocation, TDefinition definition, IEnumerable<RawDerivedQuantityDefinition> derivations, IEnumerable<RawScalarConstantDefinition> constants,
        IEnumerable<RawConvertibleQuantityDefinition> conversions, IEnumerable<RawIncludeUnitBasesDefinition> unitBaseInstanceInclusions, IEnumerable<RawExcludeUnitBasesDefinition> unitBaseInstanceExclusions,
        IEnumerable<RawIncludeUnitsDefinition> unitInstanceInclusions, IEnumerable<RawExcludeUnitsDefinition> unitInstanceExclusions)
    {
        Type = type;
        TypeLocation = typeLocation;

        Definition = definition;

        this.derivations = derivations.AsEquatable();
        this.constants = constants.AsEquatable();
        this.conversions = conversions.AsEquatable();

        this.unitBaseInstanceInclusions = unitBaseInstanceInclusions.AsEquatable();
        this.unitBaseInstanceExclusions = unitBaseInstanceExclusions.AsEquatable();

        this.unitInstanceInclusions = unitInstanceInclusions.AsEquatable();
        this.unitInstanceExclusions = unitInstanceExclusions.AsEquatable();
    }
}
