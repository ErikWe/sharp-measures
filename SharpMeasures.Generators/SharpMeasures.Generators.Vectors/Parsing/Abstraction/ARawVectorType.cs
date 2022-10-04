namespace SharpMeasures.Generators.Vectors.Parsing.Abstraction;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Quantities.Parsing.ConvertibleQuantity;
using SharpMeasures.Generators.Quantities.Parsing.QuantityOperation;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.QuantityProcess;
using SharpMeasures.Generators.Vectors.Parsing.VectorConstant;

using System.Collections.Generic;
using SharpMeasures.Generators.Vectors.Parsing.VectorOperation;

internal record class ARawVectorType<TDefinition>
{
    public DefinedType Type { get; }

    public TDefinition Definition { get; }

    public IEnumerable<RawQuantityOperationDefinition> Operations { get; }
    public IEnumerable<RawVectorOperationDefinition> VectorOperations { get; }
    public IEnumerable<RawQuantityProcessDefinition> Processes { get; }
    public IEnumerable<RawVectorConstantDefinition> Constants { get; }
    public IEnumerable<RawConvertibleQuantityDefinition> Conversions { get; }

    public IEnumerable<RawIncludeUnitsDefinition> UnitInstanceInclusions { get; }
    public IEnumerable<RawExcludeUnitsDefinition> UnitInstanceExclusions { get; }

    protected ARawVectorType(DefinedType type, TDefinition definition, IEnumerable<RawQuantityOperationDefinition> operations, IEnumerable<RawVectorOperationDefinition> vectorOperations, IEnumerable<RawQuantityProcessDefinition> processes, IEnumerable<RawVectorConstantDefinition> constants, IEnumerable<RawConvertibleQuantityDefinition> conversions, IEnumerable<RawIncludeUnitsDefinition> unitInstanceInclusions, IEnumerable<RawExcludeUnitsDefinition> unitInstanceExclusions)
    {
        Type = type;

        Definition = definition;

        Operations = operations.AsEquatable();
        VectorOperations = vectorOperations.AsEquatable();
        Processes = processes.AsEquatable();
        Constants = constants.AsEquatable();
        Conversions = conversions.AsEquatable();

        UnitInstanceInclusions = unitInstanceInclusions.AsEquatable();
        UnitInstanceExclusions = unitInstanceExclusions.AsEquatable();
    }
}
