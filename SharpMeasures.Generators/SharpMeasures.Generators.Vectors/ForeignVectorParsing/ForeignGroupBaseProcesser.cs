namespace SharpMeasures.Generators.Vectors.ForeignVectorParsing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;
using SharpMeasures.Generators.Vectors.ForeignVectorParsing.Diagnostics.Processing;
using SharpMeasures.Generators.Vectors.Parsing;
using SharpMeasures.Generators.Vectors.Parsing.ConvertibleVector;
using SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVectorGroup;

using System.Collections.Generic;

internal sealed class ForeignGroupBaseProcesser : AForeignGroupProcesser<RawGroupBaseType, RawSharpMeasuresVectorGroupDefinition, GroupBaseType, SharpMeasuresVectorGroupDefinition>
{
    protected override GroupBaseType ProduceResult(DefinedType type, MinimalLocation typeLocation, SharpMeasuresVectorGroupDefinition definition, IReadOnlyList<DerivedQuantityDefinition> derivations,
        IReadOnlyList<ConvertibleVectorDefinition> conversions, IReadOnlyList<IncludeUnitsDefinition> unitInstanceInclusions, IReadOnlyList<ExcludeUnitsDefinition> unitInstanceExclusions)
    {
        return new(type, typeLocation, definition, derivations, conversions, unitInstanceInclusions, unitInstanceExclusions);
    }

    protected override NamedType? GetOriginalQuantity(SharpMeasuresVectorGroupDefinition group) => null;
    protected override bool ConversionFromOriginalQuantitySpecified(SharpMeasuresVectorGroupDefinition group) => false;
    protected override bool ConversionToOriginalQuantitySpecified(SharpMeasuresVectorGroupDefinition group) => false;

    protected override Optional<SharpMeasuresVectorGroupDefinition> ProcessGroup(DefinedType type, RawSharpMeasuresVectorGroupDefinition rawDefinition)
    {
        var processingContext = new SimpleProcessingContext(type);

        var group = ProcessingFilter.Create(SharpMeasuresVectorGroupProcesser).Filter(processingContext, rawDefinition);

        if (group.LacksResult)
        {
            return new Optional<SharpMeasuresVectorGroupDefinition>();
        }

        return group.Result;
    }

    private static SharpMeasuresVectorGroupProcesser SharpMeasuresVectorGroupProcesser { get; } = new(EmptySharpMeasuresVectorGroupProcessingDiagnostics.Instance);
}
