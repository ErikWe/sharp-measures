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
using SharpMeasures.Generators.Vectors.Parsing.SpecializedSharpMeasuresVector;
using SharpMeasures.Generators.Vectors.Parsing.VectorConstant;

using System.Collections.Generic;

internal class VectorSpecializationProcesser : AVectorProcesser<SpecializedSharpMeasuresVectorDefinition, VectorSpecializationType>
{
    protected override VectorSpecializationType FinalizeProcess(DefinedType type, MinimalLocation typeLocation, SpecializedSharpMeasuresVectorDefinition definition,
        IReadOnlyList<DerivedQuantityDefinition> derivations, IReadOnlyList<VectorConstantDefinition> constants, IReadOnlyList<ConvertibleVectorDefinition> conversions,
        IReadOnlyList<IncludeUnitsDefinition> unitInclusions, IReadOnlyList<ExcludeUnitsDefinition> unitExclusions)
    {
        return new(type, typeLocation, definition, derivations, constants, conversions, unitInclusions, unitExclusions);
    }

    protected override NamedType? GetUnit(SpecializedSharpMeasuresVectorDefinition scalar) => null;

    protected override IOptionalWithDiagnostics<SpecializedSharpMeasuresVectorDefinition> ParseAndProcessVector(INamedTypeSymbol typeSymbol)
    {
        if (SpecializedSharpMeasuresVectorParser.Parser.ParseFirstOccurrence(typeSymbol) is not RawSpecializedSharpMeasuresVectorDefinition rawScalar)
        {
            return OptionalWithDiagnostics.EmptyWithoutDiagnostics<SpecializedSharpMeasuresVectorDefinition>();
        }

        var processingContext = new SimpleProcessingContext(typeSymbol.AsDefinedType());

        return SpecializedSharpMeasuresVectorProcesser.Process(processingContext, rawScalar);
    }

    private static SpecializedSharpMeasuresVectorProcesser SpecializedSharpMeasuresVectorProcesser { get; } = new(SpecializedSharpMeasuresVectorProcessingDiagnostics.Instance);
}
