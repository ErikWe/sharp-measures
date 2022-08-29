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
using SharpMeasures.Generators.Vectors.Parsing.SpecializedSharpMeasuresVectorGroup;

using System.Collections.Generic;

internal class GroupSpecializationProcesser : AVectorGroupProcesser<SpecializedSharpMeasuresVectorGroupDefinition, GroupSpecializationType>
{
    protected override GroupSpecializationType FinalizeProcess(DefinedType type, MinimalLocation typeLocation, SpecializedSharpMeasuresVectorGroupDefinition definition,
        IReadOnlyList<DerivedQuantityDefinition> derivations, IReadOnlyList<ConvertibleVectorDefinition> conversions, IReadOnlyList<IncludeUnitsDefinition> unitInclusions,
        IReadOnlyList<ExcludeUnitsDefinition> unitExclusions)
    {
        return new(type, typeLocation, definition, derivations, conversions, unitInclusions, unitExclusions);
    }

    protected override IOptionalWithDiagnostics<SpecializedSharpMeasuresVectorGroupDefinition> ParseAndProcessVector(INamedTypeSymbol typeSymbol)
    {
        if (SpecializedSharpMeasuresVectorGroupParser.Parser.ParseFirstOccurrence(typeSymbol) is not RawSpecializedSharpMeasuresVectorGroupDefinition rawVector)
        {
            return OptionalWithDiagnostics.EmptyWithoutDiagnostics<SpecializedSharpMeasuresVectorGroupDefinition>();
        }

        var processingContext = new SimpleProcessingContext(typeSymbol.AsDefinedType());

        return ProcessingFilter.Create(SpecializedSharpMeasuresVectorGroupProcesser).Filter(processingContext, rawVector);
    }

    private static SpecializedSharpMeasuresVectorGroupProcesser SpecializedSharpMeasuresVectorGroupProcesser { get; } = new(SpecializedSharpMeasuresVectorGroupProcessingDiagnostics.Instance);
}
