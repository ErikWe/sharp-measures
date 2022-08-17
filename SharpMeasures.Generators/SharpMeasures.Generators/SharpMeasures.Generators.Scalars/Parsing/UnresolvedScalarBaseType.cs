namespace SharpMeasures.Generators.Scalars.Parsing;

using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;
using SharpMeasures.Generators.Quantities.Parsing.UnitList;
using SharpMeasures.Generators.Scalars.Parsing.Abstraction;
using SharpMeasures.Generators.Scalars.Parsing.ConvertibleScalar;
using SharpMeasures.Generators.Scalars.Parsing.ScalarConstant;
using SharpMeasures.Generators.Scalars.Parsing.SharpMeasuresScalar;
using SharpMeasures.Generators.Raw.Quantities;
using SharpMeasures.Generators.Raw.Scalars;

using System.Collections.Generic;

internal record class UnresolvedScalarBaseType : AUnresolvedScalarType<RawSharpMeasuresScalarDefinition>, IRawScalarBaseType
{
    IRawScalarBase IRawScalarBaseType.Definition => Definition;
    IRawQuantityBase IRawQuantityBaseType.Definition => Definition;

    public UnresolvedScalarBaseType(DefinedType type, MinimalLocation typeLocation, RawSharpMeasuresScalarDefinition definition,
        IReadOnlyList<RawDerivedQuantityDefinition> derivations, IReadOnlyList<UnresolvedScalarConstantDefinition> constants,
        IReadOnlyList<UnresolvedConvertibleScalarDefinition> conversions, IReadOnlyList<RawUnitListDefinition> baseInclusions,
        IReadOnlyList<RawUnitListDefinition> baseExclusions, IReadOnlyList<RawUnitListDefinition> unitInclusions,
        IReadOnlyList<RawUnitListDefinition> unitExclusions)
        : base(type, typeLocation, definition, derivations, constants, conversions, baseInclusions, baseExclusions, unitInclusions, unitExclusions) { }
}
