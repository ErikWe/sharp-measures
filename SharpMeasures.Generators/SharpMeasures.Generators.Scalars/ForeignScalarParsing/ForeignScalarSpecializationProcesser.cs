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
using SharpMeasures.Generators.Scalars.Parsing.SpecializedSharpMeasuresScalar;

using System.Collections.Generic;

internal class ForeignScalarSpecializationProcesser : AForeignScalarProcesser<RawScalarSpecializationType, RawSpecializedSharpMeasuresScalarDefinition, ScalarSpecializationType, SpecializedSharpMeasuresScalarDefinition>
{
    protected override ScalarSpecializationType ProduceResult(DefinedType type, MinimalLocation typeLocation, SpecializedSharpMeasuresScalarDefinition definition, IReadOnlyList<DerivedQuantityDefinition> derivations,
        IReadOnlyList<ScalarConstantDefinition> constants, IReadOnlyList<ConvertibleScalarDefinition> conversions, IReadOnlyList<IncludeUnitBasesDefinition> baseInclusions,
        IReadOnlyList<ExcludeUnitBasesDefinition> baseExclusions, IReadOnlyList<IncludeUnitsDefinition> unitInstanceInclusions, IReadOnlyList<ExcludeUnitsDefinition> unitInstanceExclusions)
    {
        return new(type, typeLocation, definition, derivations, constants, conversions, baseInclusions, baseExclusions, unitInstanceInclusions, unitInstanceExclusions);
    }

    protected override IOptionalWithDiagnostics<SpecializedSharpMeasuresScalarDefinition> ProcessScalar(DefinedType type, RawSpecializedSharpMeasuresScalarDefinition rawDefinition)
    {
        var processingContext = new SimpleProcessingContext(type);

        return ProcessingFilter.Create(SpecializedSharpMeasuresScalarProcesser).Filter(processingContext, rawDefinition);
    }

    private static SpecializedSharpMeasuresScalarProcesser SpecializedSharpMeasuresScalarProcesser { get; } = new(EmptySpecializedSharpMeasuresScalarProcessingDiagnostics.Instance);
}
