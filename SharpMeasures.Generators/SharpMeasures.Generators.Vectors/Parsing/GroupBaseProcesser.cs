namespace SharpMeasures.Generators.Vectors.Parsing;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;
using SharpMeasures.Generators.Vectors.Parsing.Abstraction;
using SharpMeasures.Generators.Vectors.Parsing.ConvertibleVector;
using SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVectorGroup;

using System.Collections.Generic;

internal sealed class GroupBaseProcesser : AGroupProcesser<RawGroupBaseType, RawSharpMeasuresVectorGroupDefinition, GroupBaseType, SharpMeasuresVectorGroupDefinition>
{
    public GroupBaseProcesser(IVectorProcessingDiagnosticsStrategy diagnosticsStrategy) : base(diagnosticsStrategy) { }

    protected override GroupBaseType ProduceResult(DefinedType type, SharpMeasuresVectorGroupDefinition definition, IReadOnlyList<DerivedQuantityDefinition> derivations, IReadOnlyList<ConvertibleVectorDefinition> conversions, IReadOnlyList<IncludeUnitsDefinition> unitInstanceInclusions, IReadOnlyList<ExcludeUnitsDefinition> unitInstanceExclusions)
    {
        return new(type, definition, derivations, conversions, unitInstanceInclusions, unitInstanceExclusions);
    }

    protected override NamedType? GetOriginalQuantity(SharpMeasuresVectorGroupDefinition group) => null;
    protected override bool ConversionFromOriginalQuantitySpecified(SharpMeasuresVectorGroupDefinition group) => false;
    protected override bool ConversionToOriginalQuantitySpecified(SharpMeasuresVectorGroupDefinition group) => false;

    protected override IOptionalWithDiagnostics<SharpMeasuresVectorGroupDefinition> ProcessVector(DefinedType type, RawSharpMeasuresVectorGroupDefinition rawDefinition)
    {
        var processingContext = new SimpleProcessingContext(type);

        return ProcessingFilter.Create(SharpMeasuresVectorGroupProcesser).Filter(processingContext, rawDefinition);
    }

    private SharpMeasuresVectorGroupProcesser SharpMeasuresVectorGroupProcesser => new(DiagnosticsStrategy.SharpMeasuresVectorGroupDiagnostics);
}
