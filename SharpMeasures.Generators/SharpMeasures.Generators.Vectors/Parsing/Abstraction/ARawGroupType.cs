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

    public TDefinition Definition { get; }

    public IEnumerable<RawDerivedQuantityDefinition> Derivations { get; }
    public IEnumerable<RawConvertibleQuantityDefinition> Conversions { get; }

    public IEnumerable<RawIncludeUnitsDefinition> UnitInstanceInclusions { get; }
    public IEnumerable<RawExcludeUnitsDefinition> UnitInstanceExclusions { get; }

    protected ARawGroupType(DefinedType type, TDefinition definition, IEnumerable<RawDerivedQuantityDefinition> derivations, IEnumerable<RawConvertibleQuantityDefinition> conversions, IEnumerable<RawIncludeUnitsDefinition> unitInstanceInclusions, IEnumerable<RawExcludeUnitsDefinition> unitInstanceExclusions)
    {
        Type = type;

        Definition = definition;

        Derivations = derivations.AsEquatable();
        Conversions = conversions.AsEquatable();

        UnitInstanceInclusions = unitInstanceInclusions.AsEquatable();
        UnitInstanceExclusions = unitInstanceExclusions.AsEquatable();
    }
}
