namespace SharpMeasures.Generators.Vectors;

using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;
using SharpMeasures.Generators.Vectors.Abstraction;
using SharpMeasures.Generators.Vectors.Parsing.ConvertibleVector;
using SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVector;
using SharpMeasures.Generators.Vectors.Parsing.VectorConstant;

using System.Collections.Generic;

internal record class VectorBaseType : AVectorType<SharpMeasuresVectorDefinition>, IVectorBaseType
{
    IQuantityBase IQuantityBaseType.Definition => Definition;
    IVectorBase IVectorBaseType.Definition => Definition;

    public VectorBaseType(DefinedType type, MinimalLocation typeLocation, SharpMeasuresVectorDefinition definition, IReadOnlyList<DerivedQuantityDefinition> derivations, IReadOnlyList<VectorConstantDefinition> constants,
        IReadOnlyList<ConvertibleVectorDefinition> conversions, IReadOnlyList<IncludeUnitsDefinition> unitInstanceInclusions, IReadOnlyList<ExcludeUnitsDefinition> unitInstanceExclusions)
        : base(type, typeLocation, definition, derivations, constants, conversions, unitInstanceInclusions, unitInstanceExclusions) { }
}
