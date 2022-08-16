namespace SharpMeasures.Generators.Vectors.Parsing;

using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;
using SharpMeasures.Generators.Quantities.Parsing.UnitList;
using SharpMeasures.Generators.Raw.Quantities;
using SharpMeasures.Generators.Raw.Vectors.Groups;
using SharpMeasures.Generators.Vectors.Parsing.Abstraction;
using SharpMeasures.Generators.Vectors.Parsing.ConvertibleVector;
using SharpMeasures.Generators.Vectors.Parsing.SpecializedSharpMeasuresVectorGroup;

using System.Collections.Generic;

internal record class UnresolvedVectorGroupSpecializationType : AUnresolvedVectorGroupType<UnresolvedSpecializedSharpMeasuresVectorGroupDefinition>,
    IRawVectorGroupSpecializationType
{
    IRawQuantitySpecialization IRawQuantitySpecializationType.Definition => Definition;
    IRawVectorGroupSpecialization IRawVectorGroupSpecializationType.Definition => Definition;

    public UnresolvedVectorGroupSpecializationType(DefinedType type, MinimalLocation typeLocation, UnresolvedSpecializedSharpMeasuresVectorGroupDefinition definition,
        IReadOnlyList<UnresolvedDerivedQuantityDefinition> derivations, IReadOnlyList<UnresolvedConvertibleVectorDefinition> conversions, IReadOnlyList<UnresolvedUnitListDefinition> unitInclusions,
        IReadOnlyList<UnresolvedUnitListDefinition> unitExclusions)
        : base(type, typeLocation, definition, derivations, conversions, unitInclusions, unitExclusions) { }
}
