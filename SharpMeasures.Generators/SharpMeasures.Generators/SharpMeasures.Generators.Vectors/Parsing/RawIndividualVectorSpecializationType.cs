namespace SharpMeasures.Generators.Vectors.Parsing;

using SharpMeasures.Generators.Quantities.Parsing.ConvertibleQuantity;
using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;
using SharpMeasures.Generators.Quantities.Parsing.UnitList;
using SharpMeasures.Generators.Vectors.Parsing.Abstraction;
using SharpMeasures.Generators.Vectors.Parsing.SpecializedSharpMeasuresVector;
using SharpMeasures.Generators.Vectors.Parsing.VectorConstant;

using System.Collections.Generic;

internal record class RawIndividualVectorSpecializationType : ARawIndividualVectorType
{
    public RawSpecializedSharpMeasuresVectorDefinition Definition { get; }

    public RawIndividualVectorSpecializationType(DefinedType type, MinimalLocation typeLocation, RawSpecializedSharpMeasuresVectorDefinition definition,
        IEnumerable<UnprocessedDerivedQuantityDefinition> derivations, IEnumerable<RawVectorConstantDefinition> constants, IEnumerable<UnprocessedConvertibleQuantityDefinition> conversions,
        IEnumerable<UnprocessedUnitListDefinition> unitInclusions, IEnumerable<UnprocessedUnitListDefinition> unitExclusions)
        : base(type, typeLocation, derivations, constants, conversions, unitInclusions, unitExclusions)
    {
        Definition = definition;
    }
}
