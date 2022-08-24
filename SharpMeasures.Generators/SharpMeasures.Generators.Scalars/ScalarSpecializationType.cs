namespace SharpMeasures.Generators.Scalars;

using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;
using SharpMeasures.Generators.Quantities.Parsing.UnitList;
using SharpMeasures.Generators.Scalars.Abstraction;
using SharpMeasures.Generators.Scalars.Parsing.ConvertibleScalar;
using SharpMeasures.Generators.Scalars.Parsing.ScalarConstant;
using SharpMeasures.Generators.Scalars.Parsing.SpecializedSharpMeasuresScalar;

using System.Collections.Generic;

internal record class ScalarSpecializationType : AScalarType<SpecializedSharpMeasuresScalarDefinition>, IScalarSpecializationType
{
    IQuantitySpecialization IQuantitySpecializationType.Definition => Definition;
    IScalarSpecialization IScalarSpecializationType.Definition => Definition;

    public ScalarSpecializationType(DefinedType type, MinimalLocation typeLocation, SpecializedSharpMeasuresScalarDefinition definition, IReadOnlyList<DerivedQuantityDefinition> derivations,
        IReadOnlyList<ScalarConstantDefinition> constants, IReadOnlyList<ConvertibleScalarDefinition> conversions, IReadOnlyList<UnitListDefinition> baseInclusions,
        IReadOnlyList<UnitListDefinition> baseExclusions, IReadOnlyList<UnitListDefinition> unitInclusions, IReadOnlyList<UnitListDefinition> unitExclusions)
        : base(type, typeLocation, definition, derivations, constants, conversions, baseInclusions, baseExclusions, unitInclusions, unitExclusions) { }
}
