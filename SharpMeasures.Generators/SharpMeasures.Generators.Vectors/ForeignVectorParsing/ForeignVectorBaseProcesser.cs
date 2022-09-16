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
using SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVector;

using System.Collections.Generic;

internal class ForeignVectorBaseProcesser : AForeignVectorProcesser<RawVectorBaseType, RawSharpMeasuresVectorDefinition, VectorBaseType, SharpMeasuresVectorDefinition>
{
    protected override VectorBaseType ProduceResult(DefinedType type, MinimalLocation typeLocation, SharpMeasuresVectorDefinition definition, IReadOnlyList<DerivedQuantityDefinition> derivations,
        IReadOnlyList<VectorConstantDefinition> constants, IReadOnlyList<ConvertibleVectorDefinition> conversions, IReadOnlyList<IncludeUnitsDefinition> unitInstanceInclusions,
        IReadOnlyList<ExcludeUnitsDefinition> unitInstanceExclusions)
    {
        return new(type, typeLocation, definition, derivations, constants, conversions, unitInstanceInclusions, unitInstanceExclusions);
    }

    protected override IOptionalWithDiagnostics<SharpMeasuresVectorDefinition> ProcessVector(DefinedType type, RawSharpMeasuresVectorDefinition rawDefinition)
    {
        var processingContext = new SimpleProcessingContext(type);

        return ProcessingFilter.Create(SharpMeasuresVectorProcesser).Filter(processingContext, rawDefinition);
    }

    private static SharpMeasuresVectorProcesser SharpMeasuresVectorProcesser { get; } = new(EmptySharpMeasuresVectorProcessingDiagnostics.Instance);
}
