namespace SharpMeasures.Generators.Scalars.Parsing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;
using SharpMeasures.Generators.Quantities.Parsing.UnitList;
using SharpMeasures.Generators.Scalars.Parsing.Abstraction;
using SharpMeasures.Generators.Scalars.Parsing.ConvertibleScalar;
using SharpMeasures.Generators.Scalars.Parsing.Diagnostics.Processing;
using SharpMeasures.Generators.Scalars.Parsing.ScalarConstant;
using SharpMeasures.Generators.Scalars.Parsing.SpecializedSharpMeasuresScalar;

using System.Collections.Generic;

internal class ScalarSpecializationProcesser : AScalarProcesser<SpecializedSharpMeasuresScalarDefinition, ScalarSpecializationType>
{
    protected override ScalarSpecializationType FinalizeProcess(DefinedType type, MinimalLocation typeLocation, SpecializedSharpMeasuresScalarDefinition definition,
        IReadOnlyList<DerivedQuantityDefinition> derivations, IReadOnlyList<ScalarConstantDefinition> constants, IReadOnlyList<ConvertibleScalarDefinition> conversions,
        IReadOnlyList<UnitListDefinition> baseInclusions, IReadOnlyList<UnitListDefinition> baseExclusions, IReadOnlyList<UnitListDefinition> unitInclusions,
        IReadOnlyList<UnitListDefinition> unitExclusions)
    {
        return new(type, typeLocation, definition, derivations, constants, conversions, baseInclusions, baseExclusions, unitInclusions, unitExclusions);
    }

    protected override NamedType? GetUnit(SpecializedSharpMeasuresScalarDefinition scalar) => null;

    protected override IOptionalWithDiagnostics<SpecializedSharpMeasuresScalarDefinition> ParseAndProcessScalar(INamedTypeSymbol typeSymbol)
    {
        if (SpecializedSharpMeasuresScalarParser.Parser.ParseFirstOccurrence(typeSymbol) is not RawSpecializedSharpMeasuresScalarDefinition rawScalar)
        {
            return OptionalWithDiagnostics.EmptyWithoutDiagnostics<SpecializedSharpMeasuresScalarDefinition>();
        }

        var processingContext = new SimpleProcessingContext(typeSymbol.AsDefinedType());

        return SpecializedSharpMeasuresScalarProcesser.Process(processingContext, rawScalar);
    }

    private static SpecializedSharpMeasuresScalarProcesser SpecializedSharpMeasuresScalarProcesser { get; } = new(SpecializedSharpMeasuresScalarProcessingDiagnostics.Instance);
}
