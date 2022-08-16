﻿namespace SharpMeasures.Generators.Vectors.Parsing;

using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;
using SharpMeasures.Generators.Quantities.Parsing.UnitList;
using SharpMeasures.Generators.Raw.Quantities;
using SharpMeasures.Generators.Raw.Vectors;
using SharpMeasures.Generators.Raw.Vectors.Groups;
using SharpMeasures.Generators.Vectors.Parsing.Abstraction;
using SharpMeasures.Generators.Vectors.Parsing.ConvertibleVector;
using SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVector;
using SharpMeasures.Generators.Vectors.Parsing.VectorConstant;

using System.Collections.Generic;

internal record class UnresolvedIndividualVectorBaseType : AUnresolvedIndividualVectorType<UnresolvedSharpMeasuresVectorDefinition>, IRawVectorBaseType
{
    IRawQuantityBase IRawQuantityBaseType.Definition => Definition;
    IRawVectorGroupBase IRawVectorGroupBaseType.Definition => Definition;
    IRawVectorBase IRawVectorBaseType.Definition => Definition;

    public UnresolvedIndividualVectorBaseType(DefinedType type, MinimalLocation typeLocation, UnresolvedSharpMeasuresVectorDefinition definition,
        IReadOnlyList<UnresolvedDerivedQuantityDefinition> derivations, IReadOnlyList<UnresolvedVectorConstantDefinition> constants,
        IReadOnlyList<UnresolvedConvertibleVectorDefinition> conversions, IReadOnlyList<UnresolvedUnitListDefinition> unitInclusions,
        IReadOnlyList<UnresolvedUnitListDefinition> unitExclusions)
        : base(type, typeLocation, definition, derivations, constants, conversions, unitInclusions, unitExclusions) { }
}
