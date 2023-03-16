namespace SharpMeasures.Generators.Vectors.Parsing;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.QuantityOperation;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;
using SharpMeasures.Generators.Vectors.Parsing.Abstraction;
using SharpMeasures.Generators.Vectors.Parsing.ConvertibleVector;
using SharpMeasures.Generators.Vectors.Parsing.SpecializedSharpMeasuresVectorGroup;

using System.Collections.Generic;
using SharpMeasures.Generators.Vectors.Parsing.VectorOperation;

internal sealed class GroupSpecializationProcesser : AGroupProcesser<RawGroupSpecializationType, RawSpecializedSharpMeasuresVectorGroupDefinition, GroupSpecializationType, SpecializedSharpMeasuresVectorGroupDefinition>
{
    public GroupSpecializationProcesser(IVectorProcessingDiagnosticsStrategy diagnosticsStrategy) : base(diagnosticsStrategy) { }

    protected override GroupSpecializationType ProduceResult(DefinedType type, SpecializedSharpMeasuresVectorGroupDefinition definition, IReadOnlyList<QuantityOperationDefinition> operations, IReadOnlyList<VectorOperationDefinition> vectorOperations, IReadOnlyList<ConvertibleVectorDefinition> conversions, IReadOnlyList<IncludeUnitsDefinition> unitInstanceInclusions, IReadOnlyList<ExcludeUnitsDefinition> unitInstanceExclusions)
    {
        return new(type, definition, operations, vectorOperations, conversions, unitInstanceInclusions, unitInstanceExclusions);
    }

    protected override NamedType? GetOriginalQuantity(SpecializedSharpMeasuresVectorGroupDefinition group) => group.OriginalQuantity;
    protected override bool ConversionFromOriginalQuantitySpecified(SpecializedSharpMeasuresVectorGroupDefinition group) => group.Locations.ExplicitlySetForwardsCastOperatorBehaviour;
    protected override bool ConversionToOriginalQuantitySpecified(SpecializedSharpMeasuresVectorGroupDefinition group) => group.Locations.ExplicitlySetBackwardsCastOperatorBehaviour;

    protected override IOptionalWithDiagnostics<SpecializedSharpMeasuresVectorGroupDefinition> ProcessVector(DefinedType type, RawSpecializedSharpMeasuresVectorGroupDefinition rawDefinition)
    {
        var processingContext = new SimpleProcessingContext(type);

        return ProcessingFilter.Create(SpecializedSharpMeasuresVectorGroupProcesser).Filter(processingContext, rawDefinition);
    }

    private SpecializedSharpMeasuresVectorGroupProcesser SpecializedSharpMeasuresVectorGroupProcesser => new(DiagnosticsStrategy.SpecializedSharpMeasuresVectorGroupDiagnostics);
}
