﻿namespace SharpMeasures.Generators.Vectors.ForeignVectorParsing;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;
using SharpMeasures.Generators.Vectors.ForeignVectorParsing.Diagnostics;
using SharpMeasures.Generators.Vectors.Parsing;
using SharpMeasures.Generators.Vectors.Parsing.ConvertibleVector;
using SharpMeasures.Generators.Vectors.Parsing.SpecializedSharpMeasuresVectorGroup;

using System.Collections.Generic;

internal class ForeignGroupSpecializationProcesser : AForeignGroupProcesser<RawGroupSpecializationType, RawSpecializedSharpMeasuresVectorGroupDefinition, GroupSpecializationType, SpecializedSharpMeasuresVectorGroupDefinition>
{
    protected override GroupSpecializationType ProduceResult(DefinedType type, MinimalLocation typeLocation, SpecializedSharpMeasuresVectorGroupDefinition definition, IReadOnlyList<DerivedQuantityDefinition> derivations,
        IReadOnlyList<ConvertibleVectorDefinition> conversions, IReadOnlyList<IncludeUnitsDefinition> unitInstanceInclusions, IReadOnlyList<ExcludeUnitsDefinition> unitInstanceExclusions)
    {
        return new(type, typeLocation, definition, derivations, conversions, unitInstanceInclusions, unitInstanceExclusions);
    }

    protected override IOptionalWithDiagnostics<SpecializedSharpMeasuresVectorGroupDefinition> ProcessGroup(DefinedType type, RawSpecializedSharpMeasuresVectorGroupDefinition rawDefinition)
    {
        var processingContext = new SimpleProcessingContext(type);

        return ProcessingFilter.Create(SpecializedSharpMeasuresVectorGroupProcesser).Filter(processingContext, rawDefinition);
    }

    private static SpecializedSharpMeasuresVectorGroupProcesser SpecializedSharpMeasuresVectorGroupProcesser { get; } = new(EmptySpecializedSharpMeasuresVectorGroupProcessingDiagnostics.Instance);
}