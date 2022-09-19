namespace SharpMeasures.Generators.Vectors.ForeignVectorParsing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;
using SharpMeasures.Generators.Vectors.ForeignVectorParsing.Diagnostics.Processing;
using SharpMeasures.Generators.Vectors.Parsing;
using SharpMeasures.Generators.Vectors.Parsing.ConvertibleVector;
using SharpMeasures.Generators.Vectors.Parsing.SpecializedSharpMeasuresVector;
using SharpMeasures.Generators.Vectors.Parsing.VectorConstant;

using System.Collections.Generic;

internal sealed class ForeignVectorSpecializationProcesser : AForeignVectorProcesser<RawVectorSpecializationType, RawSpecializedSharpMeasuresVectorDefinition, VectorSpecializationType, SpecializedSharpMeasuresVectorDefinition>
{
    protected override VectorSpecializationType ProduceResult(DefinedType type, MinimalLocation typeLocation, SpecializedSharpMeasuresVectorDefinition definition, IReadOnlyList<DerivedQuantityDefinition> derivations,
        IReadOnlyList<VectorConstantDefinition> constants, IReadOnlyList<ConvertibleVectorDefinition> conversions, IReadOnlyList<IncludeUnitsDefinition> unitInstanceInclusions, IReadOnlyList<ExcludeUnitsDefinition> unitInstanceExclusions)
    {
        return new(type, typeLocation, definition, derivations, constants, conversions, unitInstanceInclusions, unitInstanceExclusions);
    }

    protected override Optional<SpecializedSharpMeasuresVectorDefinition> ProcessVector(DefinedType type, RawSpecializedSharpMeasuresVectorDefinition rawDefinition)
    {
        var processingContext = new SimpleProcessingContext(type);

        var vector = ProcessingFilter.Create(SpecializedSharpMeasuresVectorProcesser).Filter(processingContext, rawDefinition);

        if (vector.LacksResult)
        {
            return new Optional<SpecializedSharpMeasuresVectorDefinition>();
        }

        return vector.Result;
    }

    private static SpecializedSharpMeasuresVectorProcesser SpecializedSharpMeasuresVectorProcesser { get; } = new(EmptySpecializedSharpMeasuresVectorProcessingDiagnostics.Instance);
}
