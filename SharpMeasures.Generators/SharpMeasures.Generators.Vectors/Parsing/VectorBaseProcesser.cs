namespace SharpMeasures.Generators.Vectors.Parsing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;
using SharpMeasures.Generators.Vectors.Parsing.Abstraction;
using SharpMeasures.Generators.Vectors.Parsing.ConvertibleVector;
using SharpMeasures.Generators.Vectors.Parsing.Diagnostics.Processing;
using SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVector;
using SharpMeasures.Generators.Vectors.Parsing.VectorConstant;

using System.Collections.Generic;

internal class VectorBaseProcesser : AVectorProcesser<SharpMeasuresVectorDefinition, VectorBaseType>
{
    protected override VectorBaseType FinalizeProcess(DefinedType type, MinimalLocation typeLocation, SharpMeasuresVectorDefinition definition, IReadOnlyList<DerivedQuantityDefinition> derivations,
        IReadOnlyList<VectorConstantDefinition> constants, IReadOnlyList<ConvertibleVectorDefinition> conversions, IReadOnlyList<IncludeUnitsDefinition> unitInclusions,
        IReadOnlyList<ExcludeUnitsDefinition> unitExclusions)
    {
        return new(type, typeLocation, definition, derivations, constants, conversions, unitInclusions, unitExclusions);
    }

    protected override NamedType? GetUnit(SharpMeasuresVectorDefinition vector) => vector.Unit;

    protected override IOptionalWithDiagnostics<SharpMeasuresVectorDefinition> ParseAndProcessVector(INamedTypeSymbol typeSymbol)
    {
        if (SharpMeasuresVectorParser.Parser.ParseFirstOccurrence(typeSymbol) is not RawSharpMeasuresVectorDefinition rawVector)
        {
            return OptionalWithDiagnostics.EmptyWithoutDiagnostics<SharpMeasuresVectorDefinition>();
        }

        var processingContext = new SimpleProcessingContext(typeSymbol.AsDefinedType());

        return ProcessingFilter.Create(SharpMeasuresVectorProcesser).Filter(processingContext, rawVector);
    }

    private static SharpMeasuresVectorProcesser SharpMeasuresVectorProcesser { get; } = new(SharpMeasuresVectorProcessingDiagnostics.Instance);
}
