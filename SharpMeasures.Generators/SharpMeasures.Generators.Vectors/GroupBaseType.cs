namespace SharpMeasures.Generators.Vectors;

using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;
using SharpMeasures.Generators.Vectors.Abstraction;
using SharpMeasures.Generators.Vectors.Parsing.ConvertibleVector;
using SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVectorGroup;

using System.Collections.Generic;

internal record class GroupBaseType : AVectorGroupType<SharpMeasuresVectorGroupDefinition>, IVectorGroupBaseType
{
    IQuantityBase IQuantityBaseType.Definition => Definition;
    IVectorGroupBase IVectorGroupBaseType.Definition => Definition;

    public GroupBaseType(DefinedType type, MinimalLocation typeLocation, SharpMeasuresVectorGroupDefinition definition, IReadOnlyList<DerivedQuantityDefinition> derivations,
        IReadOnlyList<ConvertibleVectorDefinition> conversions, IReadOnlyList<IncludeUnitsDefinition> unitInclusions, IReadOnlyList<ExcludeUnitsDefinition> unitExclusions)
        : base(type, typeLocation, definition, derivations, conversions, unitInclusions, unitExclusions) { }
}
