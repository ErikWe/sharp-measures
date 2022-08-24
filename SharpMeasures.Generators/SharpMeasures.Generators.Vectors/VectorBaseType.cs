namespace SharpMeasures.Generators.Vectors;

using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;
using SharpMeasures.Generators.Quantities.Parsing.UnitList;
using SharpMeasures.Generators.Vectors.Abstraction;
using SharpMeasures.Generators.Vectors.Parsing.ConvertibleVector;
using SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVector;
using SharpMeasures.Generators.Vectors.Parsing.VectorConstant;

using System.Collections.Generic;

internal record class VectorBaseType : AVectorType<SharpMeasuresVectorDefinition>, IVectorBaseType
{
    IQuantityBase IQuantityBaseType.Definition => Definition;
    IVectorBase IVectorBaseType.Definition => Definition;

    public VectorBaseType(DefinedType type, MinimalLocation typeLocation, SharpMeasuresVectorDefinition definition, IReadOnlyList<DerivedQuantityDefinition> derivations,
        IReadOnlyList<VectorConstantDefinition> constants, IReadOnlyList<ConvertibleVectorDefinition> conversions, IReadOnlyList<UnitListDefinition> unitInclusions, IReadOnlyList<UnitListDefinition> unitExclusions)
        : base(type, typeLocation, definition, derivations, constants, conversions, unitInclusions, unitExclusions) { }
}
