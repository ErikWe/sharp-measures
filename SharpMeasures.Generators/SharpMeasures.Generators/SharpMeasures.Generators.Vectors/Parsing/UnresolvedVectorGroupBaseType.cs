namespace SharpMeasures.Generators.Vectors.Parsing;

using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;
using SharpMeasures.Generators.Quantities.Parsing.UnitList;
using SharpMeasures.Generators.Raw.Quantities;
using SharpMeasures.Generators.Raw.Vectors.Groups;
using SharpMeasures.Generators.Vectors.Parsing.Abstraction;
using SharpMeasures.Generators.Vectors.Parsing.ConvertibleVector;
using SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVectorGroup;

using System.Collections.Generic;

internal record class UnresolvedVectorGroupBaseType : AUnresolvedVectorGroupType<UnresolvedSharpMeasuresVectorGroupDefinition>, IRawVectorGroupBaseType
{
    IRawVectorGroupBase IRawVectorGroupBaseType.Definition => Definition;
    IRawQuantityBase IRawQuantityBaseType.Definition => Definition;

    public UnresolvedVectorGroupBaseType(DefinedType type, MinimalLocation typeLocation, UnresolvedSharpMeasuresVectorGroupDefinition definition,
        IReadOnlyList<RawDerivedQuantityDefinition> derivations, IReadOnlyList<UnresolvedConvertibleVectorDefinition> conversions, IReadOnlyList<RawUnitListDefinition> unitInclusions,
        IReadOnlyList<RawUnitListDefinition> unitExclusions)
        : base(type, typeLocation, definition, derivations, conversions, unitInclusions, unitExclusions) { }
}
