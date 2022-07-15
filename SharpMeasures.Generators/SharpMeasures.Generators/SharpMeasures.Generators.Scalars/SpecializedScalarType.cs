namespace SharpMeasures.Generators.Scalars;

using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;
using SharpMeasures.Generators.Quantities.Parsing.UnitList;
using SharpMeasures.Generators.Scalars.Parsing.ConvertibleScalar;
using SharpMeasures.Generators.Scalars.Parsing.ScalarConstant;
using SharpMeasures.Generators.Scalars.Parsing.SpecializedSharpMeasuresScalar;

using System.Collections.Generic;

internal record class SpecializedScalarType : AScalarType<SpecializedSharpMeasuresScalarDefinition>, ISpecializedScalarType
{
    ISpecializedQuantity ISpecializedQuantityType.Definition => Definition;
    ISpecializedScalar ISpecializedScalarType.Definition => Definition;

    public SpecializedScalarType(DefinedType type, MinimalLocation typeLocation, SpecializedSharpMeasuresScalarDefinition definition,
        IReadOnlyList<DerivedQuantityDefinition> derivations, IReadOnlyList<ScalarConstantDefinition> constants, IReadOnlyList<ConvertibleScalarDefinition> convertibleScalars,
        IReadOnlyList<UnitListDefinition> includeBases, IReadOnlyList<UnitListDefinition> excludeBases, IReadOnlyList<UnitListDefinition> includeUnits,
        IReadOnlyList<UnitListDefinition> excludeUnits)
        : base(type, typeLocation, definition, derivations, constants, convertibleScalars, includeBases, excludeBases, includeUnits, excludeUnits) { }
}
