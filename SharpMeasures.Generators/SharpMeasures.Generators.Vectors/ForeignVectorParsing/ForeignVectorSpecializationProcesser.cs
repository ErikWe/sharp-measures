namespace SharpMeasures.Generators.Vectors.ForeignVectorParsing;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;
using SharpMeasures.Generators.Vectors.ForeignVectorParsing.Diagnostics;
using SharpMeasures.Generators.Vectors.Parsing;
using SharpMeasures.Generators.Vectors.Parsing.ConvertibleVector;
using SharpMeasures.Generators.Vectors.Parsing.VectorConstant;
using SharpMeasures.Generators.Vectors.Parsing.SpecializedSharpMeasuresVector;

using System.Collections.Generic;

internal class ForeignVectorSpecializationProcesser : AForeignVectorProcesser<RawVectorSpecializationType, RawSpecializedSharpMeasuresVectorDefinition, VectorSpecializationType, SpecializedSharpMeasuresVectorDefinition>
{
    protected override VectorSpecializationType ProduceResult(DefinedType type, MinimalLocation typeLocation, SpecializedSharpMeasuresVectorDefinition definition, IReadOnlyList<DerivedQuantityDefinition> derivations,
        IReadOnlyList<VectorConstantDefinition> constants, IReadOnlyList<ConvertibleVectorDefinition> conversions, IReadOnlyList<IncludeUnitsDefinition> unitInstanceInclusions, IReadOnlyList<ExcludeUnitsDefinition> unitInstanceExclusions)
    {
        return new(type, typeLocation, definition, derivations, constants, conversions, unitInstanceInclusions, unitInstanceExclusions);
    }

    protected override IOptionalWithDiagnostics<SpecializedSharpMeasuresVectorDefinition> ProcessVector(DefinedType type, RawSpecializedSharpMeasuresVectorDefinition rawDefinition)
    {
        var processingContext = new SimpleProcessingContext(type);

        return ProcessingFilter.Create(SpecializedSharpMeasuresVectorProcesser).Filter(processingContext, rawDefinition);
    }

    private static SpecializedSharpMeasuresVectorProcesser SpecializedSharpMeasuresVectorProcesser { get; } = new(EmptySpecializedSharpMeasuresVectorProcessingDiagnostics.Instance);
}
