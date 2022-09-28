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
using SharpMeasures.Generators.Quantities.Parsing.ProcessedQuantity;

internal record class ARawScalarType<TDefinition>
{
    public DefinedType Type { get; }

    public TDefinition Definition { get; }

    public IEnumerable<RawDerivedQuantityDefinition> Derivations { get; }
    public IEnumerable<RawProcessedQuantityDefinition> Processes { get; }
    public IEnumerable<RawScalarConstantDefinition> Constants { get; }
    public IEnumerable<RawConvertibleQuantityDefinition> Conversions { get; }

    public IEnumerable<RawIncludeUnitBasesDefinition> UnitBaseInstanceInclusions { get; }
    public IEnumerable<RawExcludeUnitBasesDefinition> UnitBaseInstanceExclusions { get; }

    public IEnumerable<RawIncludeUnitsDefinition> UnitInstanceInclusions { get; }
    public IEnumerable<RawExcludeUnitsDefinition> UnitInstanceExclusions { get; }

    protected ARawScalarType(DefinedType type, TDefinition definition, IEnumerable<RawDerivedQuantityDefinition> derivations, IEnumerable<RawProcessedQuantityDefinition> processes, IEnumerable<RawScalarConstantDefinition> constants, IEnumerable<RawConvertibleQuantityDefinition> conversions,
        IEnumerable<RawIncludeUnitBasesDefinition> unitBaseInstanceInclusions, IEnumerable<RawExcludeUnitBasesDefinition> unitBaseInstanceExclusions, IEnumerable<RawIncludeUnitsDefinition> unitInstanceInclusions, IEnumerable<RawExcludeUnitsDefinition> unitInstanceExclusions)
    {
        Type = type;

        Definition = definition;

        Derivations = derivations.AsEquatable();
        Processes = processes.AsEquatable();
        Constants = constants.AsEquatable();
        Conversions = conversions.AsEquatable();

        UnitBaseInstanceInclusions = unitBaseInstanceInclusions.AsEquatable();
        UnitBaseInstanceExclusions = unitBaseInstanceExclusions.AsEquatable();

        UnitInstanceInclusions = unitInstanceInclusions.AsEquatable();
        UnitInstanceExclusions = unitInstanceExclusions.AsEquatable();
    }
}
