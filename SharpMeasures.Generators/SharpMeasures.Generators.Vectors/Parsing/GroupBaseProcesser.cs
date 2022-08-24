namespace SharpMeasures.Generators.Vectors.Parsing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;
using SharpMeasures.Generators.Quantities.Parsing.UnitList;
using SharpMeasures.Generators.Vectors.Parsing.Abstraction;
using SharpMeasures.Generators.Vectors.Parsing.ConvertibleVector;
using SharpMeasures.Generators.Vectors.Parsing.Diagnostics.Processing;
using SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVectorGroup;

using System.Collections.Generic;

internal class GroupBaseProcesser : AVectorGroupProcesser<SharpMeasuresVectorGroupDefinition, GroupBaseType>
{
    protected override GroupBaseType FinalizeProcess(DefinedType type, MinimalLocation typeLocation, SharpMeasuresVectorGroupDefinition definition, IReadOnlyList<DerivedQuantityDefinition> derivations,
        IReadOnlyList<ConvertibleVectorDefinition> conversions, IReadOnlyList<UnitListDefinition> unitInclusions, IReadOnlyList<UnitListDefinition> unitExclusions)
    {
        return new(type, typeLocation, definition, derivations, conversions, unitInclusions, unitExclusions);
    }

    protected override IOptionalWithDiagnostics<SharpMeasuresVectorGroupDefinition> ParseAndProcessVector(INamedTypeSymbol typeSymbol)
    {
        if (SharpMeasuresVectorGroupParser.Parser.ParseFirstOccurrence(typeSymbol) is not RawSharpMeasuresVectorGroupDefinition rawVector)
        {
            return OptionalWithDiagnostics.EmptyWithoutDiagnostics<SharpMeasuresVectorGroupDefinition>();
        }

        var processingContext = new SimpleProcessingContext(typeSymbol.AsDefinedType());

        return SharpMeasuresVectorGroupProcesser.Process(processingContext, rawVector);
    }

    private static SharpMeasuresVectorGroupProcesser SharpMeasuresVectorGroupProcesser { get; } = new(SharpMeasuresVectorGroupProcessingDiagnostics.Instance);
}
