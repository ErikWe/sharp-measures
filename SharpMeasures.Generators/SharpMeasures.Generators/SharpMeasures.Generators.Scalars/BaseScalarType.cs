namespace SharpMeasures.Generators.Scalars;

using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;
using SharpMeasures.Generators.Quantities.Parsing.UnitList;
using SharpMeasures.Generators.Scalars.Parsing.ConvertibleScalar;
using SharpMeasures.Generators.Scalars.Parsing.ScalarConstant;
using SharpMeasures.Generators.Scalars.Parsing.SharpMeasuresScalar;

using System.Collections.Generic;

internal record class BaseScalarType : AScalarType<SharpMeasuresScalarDefinition>, IBaseScalarType
{
    IBaseScalar IBaseScalarType.Definition => Definition;

    public BaseScalarType(DefinedType type, MinimalLocation typeLocation, SharpMeasuresScalarDefinition definition, IReadOnlyList<DerivedQuantityDefinition> derivations,
        IReadOnlyList<ScalarConstantDefinition> constants, IReadOnlyList<ConvertibleScalarDefinition> convertibleScalars, IReadOnlyList<UnitListDefinition> includeBases,
        IReadOnlyList<UnitListDefinition> excludeBases, IReadOnlyList<UnitListDefinition> includeUnits, IReadOnlyList<UnitListDefinition> excludeUnits)
        : base(type, typeLocation, definition, derivations, constants, convertibleScalars, includeBases, excludeBases, includeUnits, excludeUnits) { }
}
