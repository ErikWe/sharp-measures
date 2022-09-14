namespace SharpMeasures.Generators.Vectors.Parsing.Abstraction;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Quantities.Parsing.ConvertibleQuantity;
using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;

using System.Collections.Generic;

internal record class ARawGroupType<TDefinition>
{
    public DefinedType Type { get; }
    public MinimalLocation TypeLocation { get; }

    public TDefinition Definition { get; }

    public IEnumerable<RawDerivedQuantityDefinition> Derivations => derivations;
    public IEnumerable<RawConvertibleQuantityDefinition> Conversions => conversions;

    public IEnumerable<RawIncludeUnitsDefinition> UnitInstanceInclusions => unitInstanceInclusions;
    public IEnumerable<RawExcludeUnitsDefinition> UnitInstanceExclusions => unitInstanceExclusions;

    private EquatableEnumerable<RawDerivedQuantityDefinition> derivations { get; }
    private EquatableEnumerable<RawConvertibleQuantityDefinition> conversions { get; }

    private EquatableEnumerable<RawIncludeUnitsDefinition> unitInstanceInclusions { get; }
    private EquatableEnumerable<RawExcludeUnitsDefinition> unitInstanceExclusions { get; }

    protected ARawGroupType(DefinedType type, MinimalLocation typeLocation, TDefinition definition, IEnumerable<RawDerivedQuantityDefinition> derivations,
        IEnumerable<RawConvertibleQuantityDefinition> conversions, IEnumerable<RawIncludeUnitsDefinition> unitInstanceInclusions, IEnumerable<RawExcludeUnitsDefinition> unitInstanceExclusions)
    {
        Type = type;
        TypeLocation = typeLocation;

        Definition = definition;

        this.derivations = derivations.AsEquatable();
        this.conversions = conversions.AsEquatable();

        this.unitInstanceInclusions = unitInstanceInclusions.AsEquatable();
        this.unitInstanceExclusions = unitInstanceExclusions.AsEquatable();
    }
}
