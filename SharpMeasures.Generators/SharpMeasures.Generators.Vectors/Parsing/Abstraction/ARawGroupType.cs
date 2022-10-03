namespace SharpMeasures.Generators.Vectors.Parsing.Abstraction;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Quantities.Parsing.ConvertibleQuantity;
using SharpMeasures.Generators.Quantities.Parsing.QuantityOperation;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;

using System.Collections.Generic;
using SharpMeasures.Generators.Vectors.Parsing.VectorOperation;

internal record class ARawGroupType<TDefinition>
{
    public DefinedType Type { get; }

    public TDefinition Definition { get; }

    public IEnumerable<RawQuantityOperationDefinition> Operations { get; }
    public IEnumerable<RawVectorOperationDefinition> VectorOperations { get; }
    public IEnumerable<RawConvertibleQuantityDefinition> Conversions { get; }

    public IEnumerable<RawIncludeUnitsDefinition> UnitInstanceInclusions { get; }
    public IEnumerable<RawExcludeUnitsDefinition> UnitInstanceExclusions { get; }

    protected ARawGroupType(DefinedType type, TDefinition definition, IEnumerable<RawQuantityOperationDefinition> operations, IEnumerable<RawVectorOperationDefinition> vectorOperations, IEnumerable<RawConvertibleQuantityDefinition> conversions, IEnumerable<RawIncludeUnitsDefinition> unitInstanceInclusions, IEnumerable<RawExcludeUnitsDefinition> unitInstanceExclusions)
    {
        Type = type;

        Definition = definition;

        Operations = operations.AsEquatable();
        VectorOperations = vectorOperations.AsEquatable();
        Conversions = conversions.AsEquatable();

        UnitInstanceInclusions = unitInstanceInclusions.AsEquatable();
        UnitInstanceExclusions = unitInstanceExclusions.AsEquatable();
    }
}
