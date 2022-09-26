namespace SharpMeasures.Generators.Vectors.Parsing;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;
using SharpMeasures.Generators.Vectors.Parsing.Abstraction;
using SharpMeasures.Generators.Vectors.Parsing.ConvertibleVector;
using SharpMeasures.Generators.Vectors.Parsing.SpecializedSharpMeasuresVector;
using SharpMeasures.Generators.Vectors.Parsing.VectorConstant;

using System.Collections.Generic;

internal sealed class VectorSpecializationProcesser : AVectorProcesser<RawVectorSpecializationType, RawSpecializedSharpMeasuresVectorDefinition, VectorSpecializationType, SpecializedSharpMeasuresVectorDefinition>
{
    public VectorSpecializationProcesser(IVectorProcessingDiagnosticsStrategy diagnosticsStrategy) : base(diagnosticsStrategy) { }

    protected override VectorSpecializationType ProduceResult(DefinedType type, SpecializedSharpMeasuresVectorDefinition definition, IReadOnlyList<DerivedQuantityDefinition> derivations, IReadOnlyList<VectorConstantDefinition> constants,
        IReadOnlyList<ConvertibleVectorDefinition> conversions, IReadOnlyList<IncludeUnitsDefinition> unitInstanceInclusions, IReadOnlyList<ExcludeUnitsDefinition> unitInstanceExclusions)
    {
        return new(type, definition, derivations, constants, conversions, unitInstanceInclusions, unitInstanceExclusions);
    }

    protected override NamedType? GetUnit(SpecializedSharpMeasuresVectorDefinition scalar) => null;
    protected override NamedType? GetOriginalQuantity(SpecializedSharpMeasuresVectorDefinition vector) => vector.OriginalQuantity;
    protected override bool ConversionFromOriginalQuantitySpecified(SpecializedSharpMeasuresVectorDefinition vector) => vector.Locations.ExplicitlySetForwardsCastOperatorBehaviour;
    protected override bool ConversionToOriginalQuantitySpecified(SpecializedSharpMeasuresVectorDefinition vector) => vector.Locations.ExplicitlySetBackwardsCastOperatorBehaviour;

    protected override IOptionalWithDiagnostics<SpecializedSharpMeasuresVectorDefinition> ProcessVector(DefinedType type, RawSpecializedSharpMeasuresVectorDefinition rawDefinition)
    {
        var processingContext = new SimpleProcessingContext(type);

        return ProcessingFilter.Create(SpecializedSharpMeasuresVectorProcesser).Filter(processingContext, rawDefinition);
    }

    private SpecializedSharpMeasuresVectorProcesser SpecializedSharpMeasuresVectorProcesser => new(DiagnosticsStrategy.SpecializedSharpMeasuresVectorDiagnostics);
}
