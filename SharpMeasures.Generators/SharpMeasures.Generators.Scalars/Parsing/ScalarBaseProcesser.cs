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
using SharpMeasures.Generators.Scalars.Parsing.SharpMeasuresScalar;

using System.Collections.Generic;

internal class ScalarBaseProcesser : AScalarProcesser<SharpMeasuresScalarDefinition, ScalarBaseType>
{
    protected override ScalarBaseType FinalizeProcess(DefinedType type, MinimalLocation typeLocation, SharpMeasuresScalarDefinition definition, IReadOnlyList<DerivedQuantityDefinition> derivations,
        IReadOnlyList<ScalarConstantDefinition> constants, IReadOnlyList<ConvertibleScalarDefinition> conversions, IReadOnlyList<UnitListDefinition> baseInclusions,
        IReadOnlyList<UnitListDefinition> baseExclusions, IReadOnlyList<UnitListDefinition> unitInclusions, IReadOnlyList<UnitListDefinition> unitExclusions)
    {
        return new(type, typeLocation, definition, derivations, constants, conversions, baseInclusions, baseExclusions, unitInclusions, unitExclusions);
    }

    protected override NamedType? GetUnit(SharpMeasuresScalarDefinition scalar) => scalar.Unit;

    protected override IOptionalWithDiagnostics<SharpMeasuresScalarDefinition> ParseAndProcessScalar(INamedTypeSymbol typeSymbol)
    {
        if (SharpMeasuresScalarParser.Parser.ParseFirstOccurrence(typeSymbol) is not RawSharpMeasuresScalarDefinition rawScalar)
        {
            return OptionalWithDiagnostics.EmptyWithoutDiagnostics<SharpMeasuresScalarDefinition>();
        }

        var processingContext = new SimpleProcessingContext(typeSymbol.AsDefinedType());

        return SharpMeasuresScalarProcesser.Process(processingContext, rawScalar);
    }

    private static SharpMeasuresScalarProcesser SharpMeasuresScalarProcesser { get; } = new(SharpMeasuresScalarProcessingDiagnostics.Instance);
}
