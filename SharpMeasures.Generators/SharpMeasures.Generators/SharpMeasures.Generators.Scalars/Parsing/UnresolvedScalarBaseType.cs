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

internal record class UnresolvedScalarBaseType : AUnresolvedScalarType<UnresolvedSharpMeasuresScalarDefinition>, IRawScalarBaseType
{
    IRawScalarBase IRawScalarBaseType.Definition => Definition;
    IRawQuantityBase IRawQuantityBaseType.Definition => Definition;

    public UnresolvedScalarBaseType(DefinedType type, MinimalLocation typeLocation, UnresolvedSharpMeasuresScalarDefinition definition,
        IReadOnlyList<UnresolvedDerivedQuantityDefinition> derivations, IReadOnlyList<UnresolvedScalarConstantDefinition> constants,
        IReadOnlyList<UnresolvedConvertibleScalarDefinition> conversions, IReadOnlyList<UnresolvedUnitListDefinition> baseInclusions,
        IReadOnlyList<UnresolvedUnitListDefinition> baseExclusions, IReadOnlyList<UnresolvedUnitListDefinition> unitInclusions,
        IReadOnlyList<UnresolvedUnitListDefinition> unitExclusions)
        : base(type, typeLocation, definition, derivations, constants, conversions, baseInclusions, baseExclusions, unitInclusions, unitExclusions) { }
}
