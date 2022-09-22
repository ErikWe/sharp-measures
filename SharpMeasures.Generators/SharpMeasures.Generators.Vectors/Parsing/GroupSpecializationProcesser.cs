namespace SharpMeasures.Generators.Vectors.Parsing;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;
using SharpMeasures.Generators.Vectors.Parsing.Abstraction;
using SharpMeasures.Generators.Vectors.Parsing.ConvertibleVector;
using SharpMeasures.Generators.Vectors.Parsing.Diagnostics.Processing;
using SharpMeasures.Generators.Vectors.Parsing.SpecializedSharpMeasuresVectorGroup;

using System.Collections.Generic;

internal sealed class GroupSpecializationProcesser : AGroupProcesser<RawGroupSpecializationType, RawSpecializedSharpMeasuresVectorGroupDefinition, GroupSpecializationType, SpecializedSharpMeasuresVectorGroupDefinition>
{
    protected override GroupSpecializationType ProduceResult(DefinedType type, MinimalLocation typeLocation, SpecializedSharpMeasuresVectorGroupDefinition definition, IReadOnlyList<DerivedQuantityDefinition> derivations,
        IReadOnlyList<ConvertibleVectorDefinition> conversions, IReadOnlyList<IncludeUnitsDefinition> unitInstanceInclusions, IReadOnlyList<ExcludeUnitsDefinition> unitInstanceExclusions)
    {
        return new(type, typeLocation, definition, derivations, conversions, unitInstanceInclusions, unitInstanceExclusions);
    }

    protected override NamedType? GetOriginalQuantity(SpecializedSharpMeasuresVectorGroupDefinition group) => group.OriginalQuantity;
    protected override bool ConversionFromOriginalQuantitySpecified(SpecializedSharpMeasuresVectorGroupDefinition group) => group.Locations.ExplicitlySetForwardsCastOperatorBehaviour;
    protected override bool ConversionToOriginalQuantitySpecified(SpecializedSharpMeasuresVectorGroupDefinition group) => group.Locations.ExplicitlySetBackwardsCastOperatorBehaviour;

    protected override IOptionalWithDiagnostics<SpecializedSharpMeasuresVectorGroupDefinition> ProcessVector(DefinedType type, RawSpecializedSharpMeasuresVectorGroupDefinition rawDefinition)
    {
        var processingContext = new SimpleProcessingContext(type);

        return ProcessingFilter.Create(SpecializedSharpMeasuresVectorGroupProcesser).Filter(processingContext, rawDefinition);
    }

    private static SpecializedSharpMeasuresVectorGroupProcesser SpecializedSharpMeasuresVectorGroupProcesser { get; } = new(SpecializedSharpMeasuresVectorGroupProcessingDiagnostics.Instance);
}
