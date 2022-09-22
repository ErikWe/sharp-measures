namespace SharpMeasures.Generators.Vectors.ForeignVectorParsing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;
using SharpMeasures.Generators.Vectors.ForeignVectorParsing.Diagnostics.Processing;
using SharpMeasures.Generators.Vectors.Parsing;
using SharpMeasures.Generators.Vectors.Parsing.ConvertibleVector;
using SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVector;
using SharpMeasures.Generators.Vectors.Parsing.VectorConstant;

using System.Collections.Generic;

internal sealed class ForeignVectorBaseProcesser : AForeignVectorProcesser<RawVectorBaseType, RawSharpMeasuresVectorDefinition, VectorBaseType, SharpMeasuresVectorDefinition>
{
    protected override VectorBaseType ProduceResult(DefinedType type, MinimalLocation typeLocation, SharpMeasuresVectorDefinition definition, IReadOnlyList<DerivedQuantityDefinition> derivations,
        IReadOnlyList<VectorConstantDefinition> constants, IReadOnlyList<ConvertibleVectorDefinition> conversions, IReadOnlyList<IncludeUnitsDefinition> unitInstanceInclusions,
        IReadOnlyList<ExcludeUnitsDefinition> unitInstanceExclusions)
    {
        return new(type, typeLocation, definition, derivations, constants, conversions, unitInstanceInclusions, unitInstanceExclusions);
    }

    protected override NamedType? GetOriginalQuantity(SharpMeasuresVectorDefinition vector) => null;
    protected override bool ConversionFromOriginalQuantitySpecified(SharpMeasuresVectorDefinition vector) => false;
    protected override bool ConversionToOriginalQuantitySpecified(SharpMeasuresVectorDefinition vector) => false;

    protected override Optional<SharpMeasuresVectorDefinition> ProcessVector(DefinedType type, RawSharpMeasuresVectorDefinition rawDefinition)
    {
        var processingContext = new SimpleProcessingContext(type);

        var vector = ProcessingFilter.Create(SharpMeasuresVectorProcesser).Filter(processingContext, rawDefinition);

        if (vector.LacksResult)
        {
            return new Optional<SharpMeasuresVectorDefinition>();
        }

        return vector.Result;
    }

    private static SharpMeasuresVectorProcesser SharpMeasuresVectorProcesser { get; } = new(EmptySharpMeasuresVectorProcessingDiagnostics.Instance);
}
