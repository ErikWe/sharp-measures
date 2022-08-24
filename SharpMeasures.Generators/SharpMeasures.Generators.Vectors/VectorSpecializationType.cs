namespace SharpMeasures.Generators.Vectors;

using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;
using SharpMeasures.Generators.Quantities.Parsing.UnitList;
using SharpMeasures.Generators.Vectors.Abstraction;
using SharpMeasures.Generators.Vectors.Parsing.ConvertibleVector;
using SharpMeasures.Generators.Vectors.Parsing.SpecializedSharpMeasuresVector;
using SharpMeasures.Generators.Vectors.Parsing.VectorConstant;

using System.Collections.Generic;

internal record class VectorSpecializationType : AVectorType<SpecializedSharpMeasuresVectorDefinition>, IVectorSpecializationType
{
    IQuantitySpecialization IQuantitySpecializationType.Definition => Definition;
    IVectorSpecialization IVectorSpecializationType.Definition => Definition;

    public VectorSpecializationType(DefinedType type, MinimalLocation typeLocation, SpecializedSharpMeasuresVectorDefinition definition, IReadOnlyList<DerivedQuantityDefinition> derivations,
        IReadOnlyList<VectorConstantDefinition> constants, IReadOnlyList<ConvertibleVectorDefinition> conversions, IReadOnlyList<UnitListDefinition> unitInclusions, IReadOnlyList<UnitListDefinition> unitExclusions)
        : base(type, typeLocation, definition, derivations, constants, conversions, unitInclusions, unitExclusions) { }
}
