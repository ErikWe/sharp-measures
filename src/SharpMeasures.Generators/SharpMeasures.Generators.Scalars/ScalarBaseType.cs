﻿namespace SharpMeasures.Generators.Scalars;

using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Quantities.Parsing.QuantityOperation;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;
using SharpMeasures.Generators.Scalars.Abstraction;
using SharpMeasures.Generators.Scalars.Parsing.ConvertibleScalar;
using SharpMeasures.Generators.Scalars.Parsing.ExcludeUnitBases;
using SharpMeasures.Generators.Scalars.Parsing.IncludeUnitBases;
using SharpMeasures.Generators.Scalars.Parsing.ScalarConstant;
using SharpMeasures.Generators.Scalars.Parsing.SharpMeasuresScalar;

using System.Collections.Generic;
using SharpMeasures.Generators.Quantities.Parsing.QuantityProcess;

internal sealed record class ScalarBaseType : AScalarType<SharpMeasuresScalarDefinition>, IScalarBaseType
{
    IQuantityBase IQuantityBaseType.Definition => Definition;
    IScalarBase IScalarBaseType.Definition => Definition;

    public ScalarBaseType(DefinedType type, SharpMeasuresScalarDefinition definition, IReadOnlyList<QuantityOperationDefinition> operations, IReadOnlyList<QuantityProcessDefinition> processes, IReadOnlyList<ScalarConstantDefinition> constants, IReadOnlyList<ConvertibleScalarDefinition> conversions,
        IReadOnlyList<IncludeUnitBasesDefinition> baseInclusions, IReadOnlyList<ExcludeUnitBasesDefinition> baseExclusions, IReadOnlyList<IncludeUnitsDefinition> unitInstanceInclusions, IReadOnlyList<ExcludeUnitsDefinition> unitInstanceExclusions)
        : base(type, definition, operations, processes, constants, conversions, baseInclusions, baseExclusions, unitInstanceInclusions, unitInstanceExclusions) { }
}
