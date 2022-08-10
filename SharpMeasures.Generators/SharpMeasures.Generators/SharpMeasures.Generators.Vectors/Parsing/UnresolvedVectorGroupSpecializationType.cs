namespace SharpMeasures.Generators.Vectors.Parsing;

using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;
using SharpMeasures.Generators.Quantities.Parsing.UnitList;
using SharpMeasures.Generators.Unresolved.Quantities;
using SharpMeasures.Generators.Unresolved.Vectors;
using SharpMeasures.Generators.Vectors.Parsing.Abstraction;
using SharpMeasures.Generators.Vectors.Parsing.ConvertibleVector;
using SharpMeasures.Generators.Vectors.Parsing.SpecializedSharpMeasuresVectorGroup;

using System.Collections.Generic;

internal record class UnresolvedVectorGroupSpecializationType : AUnresolvedVectorGroupType<UnresolvedSpecializedSharpMeasuresVectorGroupDefinition>,
    IUnresolvedVectorGroupSpecializationType
{
    IUnresolvedQuantitySpecialization IUnresolvedQuantitySpecializationType.Definition => Definition;
    IUnresolvedVectorGroupSpecialization IUnresolvedVectorGroupSpecializationType.Definition => Definition;

    public UnresolvedVectorGroupSpecializationType(DefinedType type, MinimalLocation typeLocation, UnresolvedSpecializedSharpMeasuresVectorGroupDefinition definition,
        IReadOnlyList<UnresolvedDerivedQuantityDefinition> derivations, IReadOnlyList<UnresolvedConvertibleVectorDefinition> conversions, IReadOnlyList<UnresolvedUnitListDefinition> unitInclusions,
        IReadOnlyList<UnresolvedUnitListDefinition> unitExclusions)
        : base(type, typeLocation, definition, derivations, conversions, unitInclusions, unitExclusions) { }
}
