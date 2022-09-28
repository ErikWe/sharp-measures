namespace SharpMeasures.Generators.Vectors;

using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.ProcessedQuantity;
using SharpMeasures.Generators.Vectors.Abstraction;
using SharpMeasures.Generators.Vectors.Parsing.ConvertibleVector;
using SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVector;
using SharpMeasures.Generators.Vectors.Parsing.VectorConstant;

using System.Collections.Generic;

internal sealed record class VectorBaseType : AVectorType<SharpMeasuresVectorDefinition>, IVectorBaseType
{
    IQuantityBase IQuantityBaseType.Definition => Definition;
    IVectorBase IVectorBaseType.Definition => Definition;

    public VectorBaseType(DefinedType type, SharpMeasuresVectorDefinition definition, IReadOnlyList<DerivedQuantityDefinition> derivations, IReadOnlyList<ProcessedQuantityDefinition> processes, IReadOnlyList<VectorConstantDefinition> constants, IReadOnlyList<ConvertibleVectorDefinition> conversions, IReadOnlyList<IncludeUnitsDefinition> unitInstanceInclusions, IReadOnlyList<ExcludeUnitsDefinition> unitInstanceExclusions)
        : base(type, definition, derivations, processes, constants, conversions, unitInstanceInclusions, unitInstanceExclusions) { }
}
