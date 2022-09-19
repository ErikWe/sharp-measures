namespace SharpMeasures.Generators.Scalars.ForeignScalarParsing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;
using SharpMeasures.Generators.Scalars.ForeignScalarParsing.Diagnostics.Processing;
using SharpMeasures.Generators.Scalars.Parsing;
using SharpMeasures.Generators.Scalars.Parsing.ConvertibleScalar;
using SharpMeasures.Generators.Scalars.Parsing.ExcludeUnitBases;
using SharpMeasures.Generators.Scalars.Parsing.IncludeUnitBases;
using SharpMeasures.Generators.Scalars.Parsing.ScalarConstant;
using SharpMeasures.Generators.Scalars.Parsing.SpecializedSharpMeasuresScalar;

using System.Collections.Generic;

internal sealed class ForeignScalarSpecializationProcesser : AForeignScalarProcesser<RawScalarSpecializationType, RawSpecializedSharpMeasuresScalarDefinition, ScalarSpecializationType, SpecializedSharpMeasuresScalarDefinition>
{
    protected override ScalarSpecializationType ProduceResult(DefinedType type, MinimalLocation typeLocation, SpecializedSharpMeasuresScalarDefinition definition, IReadOnlyList<DerivedQuantityDefinition> derivations, IReadOnlyList<ScalarConstantDefinition> constants,
        IReadOnlyList<ConvertibleScalarDefinition> conversions, IReadOnlyList<IncludeUnitBasesDefinition> baseInclusions, IReadOnlyList<ExcludeUnitBasesDefinition> baseExclusions, IReadOnlyList<IncludeUnitsDefinition> unitInstanceInclusions, IReadOnlyList<ExcludeUnitsDefinition> unitInstanceExclusions)
        => new(type, typeLocation, definition, derivations, constants, conversions, baseInclusions, baseExclusions, unitInstanceInclusions, unitInstanceExclusions);

    protected override Optional<SpecializedSharpMeasuresScalarDefinition> ProcessScalar(DefinedType type, RawSpecializedSharpMeasuresScalarDefinition rawDefinition)
    {
        var processingContext = new SimpleProcessingContext(type);

        var scalar = ProcessingFilter.Create(SpecializedSharpMeasuresScalarProcesser).Filter(processingContext, rawDefinition);

        if (scalar.LacksResult)
        {
            return new Optional<SpecializedSharpMeasuresScalarDefinition>();
        }

        return scalar.Result;
    }

    private static SpecializedSharpMeasuresScalarProcesser SpecializedSharpMeasuresScalarProcesser { get; } = new(EmptySpecializedSharpMeasuresScalarProcessingDiagnostics.Instance);
}
