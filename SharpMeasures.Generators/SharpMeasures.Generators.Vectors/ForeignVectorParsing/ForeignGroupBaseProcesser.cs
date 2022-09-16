namespace SharpMeasures.Generators.Vectors.ForeignVectorParsing;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;
using SharpMeasures.Generators.Vectors.ForeignVectorParsing.Diagnostics;
using SharpMeasures.Generators.Vectors.Parsing;
using SharpMeasures.Generators.Vectors.Parsing.ConvertibleVector;
using SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVectorGroup;

using System.Collections.Generic;

internal class ForeignGroupBaseProcesser : AForeignGroupProcesser<RawGroupBaseType, RawSharpMeasuresVectorGroupDefinition, GroupBaseType, SharpMeasuresVectorGroupDefinition>
{
    protected override GroupBaseType ProduceResult(DefinedType type, MinimalLocation typeLocation, SharpMeasuresVectorGroupDefinition definition, IReadOnlyList<DerivedQuantityDefinition> derivations,
        IReadOnlyList<ConvertibleVectorDefinition> conversions, IReadOnlyList<IncludeUnitsDefinition> unitInstanceInclusions, IReadOnlyList<ExcludeUnitsDefinition> unitInstanceExclusions)
    {
        return new(type, typeLocation, definition, derivations, conversions, unitInstanceInclusions, unitInstanceExclusions);
    }

    protected override IOptionalWithDiagnostics<SharpMeasuresVectorGroupDefinition> ProcessGroup(DefinedType type, RawSharpMeasuresVectorGroupDefinition rawDefinition)
    {
        var processingContext = new SimpleProcessingContext(type);

        return ProcessingFilter.Create(SharpMeasuresVectorGroupProcesser).Filter(processingContext, rawDefinition);
    }

    private static SharpMeasuresVectorGroupProcesser SharpMeasuresVectorGroupProcesser { get; } = new(EmptySharpMeasuresVectorGroupProcessingDiagnostics.Instance);
}
