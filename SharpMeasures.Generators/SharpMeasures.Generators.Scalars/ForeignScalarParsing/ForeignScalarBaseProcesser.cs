namespace SharpMeasures.Generators.Scalars.ForeignScalarParsing;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;
using SharpMeasures.Generators.Scalars.ForeignScalarParsing.Diagnostics;
using SharpMeasures.Generators.Scalars.Parsing;
using SharpMeasures.Generators.Scalars.Parsing.ConvertibleScalar;
using SharpMeasures.Generators.Scalars.Parsing.ExcludeUnitBases;
using SharpMeasures.Generators.Scalars.Parsing.IncludeUnitBases;
using SharpMeasures.Generators.Scalars.Parsing.ScalarConstant;
using SharpMeasures.Generators.Scalars.Parsing.SharpMeasuresScalar;

using System.Collections.Generic;

internal class ForeignScalarBaseProcesser : AForeignScalarProcesser<RawScalarBaseType, RawSharpMeasuresScalarDefinition, ScalarBaseType, SharpMeasuresScalarDefinition>
{
    protected override ScalarBaseType ProduceResult(DefinedType type, MinimalLocation typeLocation, SharpMeasuresScalarDefinition definition, IReadOnlyList<DerivedQuantityDefinition> derivations,
        IReadOnlyList<ScalarConstantDefinition> constants, IReadOnlyList<ConvertibleScalarDefinition> conversions, IReadOnlyList<IncludeUnitBasesDefinition> baseInclusions,
        IReadOnlyList<ExcludeUnitBasesDefinition> baseExclusions, IReadOnlyList<IncludeUnitsDefinition> unitInstanceInclusions, IReadOnlyList<ExcludeUnitsDefinition> unitInstanceExclusions)
    {
        return new(type, typeLocation, definition, derivations, constants, conversions, baseInclusions, baseExclusions, unitInstanceInclusions, unitInstanceExclusions);
    }

    protected override IOptionalWithDiagnostics<SharpMeasuresScalarDefinition> ProcessScalar(DefinedType type, RawSharpMeasuresScalarDefinition rawDefinition)
    {
        var processingContext = new SimpleProcessingContext(type);

        return ProcessingFilter.Create(SharpMeasuresScalarProcesser).Filter(processingContext, rawDefinition);
    }

    private static SharpMeasuresScalarProcesser SharpMeasuresScalarProcesser { get; } = new(EmptySharpMeasuresScalarProcessingDiagnostics.Instance);
}
