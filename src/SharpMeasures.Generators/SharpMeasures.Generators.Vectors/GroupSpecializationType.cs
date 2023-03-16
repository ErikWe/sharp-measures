namespace SharpMeasures.Generators.Vectors;

using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Quantities.Parsing.QuantityOperation;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;
using SharpMeasures.Generators.Vectors.Abstraction;
using SharpMeasures.Generators.Vectors.Parsing.ConvertibleVector;
using SharpMeasures.Generators.Vectors.Parsing.SpecializedSharpMeasuresVectorGroup;

using System.Collections.Generic;
using SharpMeasures.Generators.Vectors.Parsing.VectorOperation;

internal sealed record class GroupSpecializationType : AGroupType<SpecializedSharpMeasuresVectorGroupDefinition>, IVectorGroupSpecializationType
{
    IQuantitySpecialization IQuantitySpecializationType.Definition => Definition;
    IVectorGroupSpecialization IVectorGroupSpecializationType.Definition => Definition;

    public GroupSpecializationType(DefinedType type, SpecializedSharpMeasuresVectorGroupDefinition definition, IReadOnlyList<QuantityOperationDefinition> operations, IReadOnlyList<VectorOperationDefinition> vectorOperations, IReadOnlyList<ConvertibleVectorDefinition> conversions, IReadOnlyList<IncludeUnitsDefinition> unitInstanceInclusions, IReadOnlyList<ExcludeUnitsDefinition> unitInstanceExclusions)
        : base(type, definition, operations, vectorOperations, conversions, unitInstanceInclusions, unitInstanceExclusions) { }
}
