namespace SharpMeasures.Generators.Scalars.Parsing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;
using SharpMeasures.Generators.Scalars.Parsing.Abstraction;
using SharpMeasures.Generators.Scalars.Parsing.ConvertibleScalar;
using SharpMeasures.Generators.Scalars.Parsing.Diagnostics.Processing;
using SharpMeasures.Generators.Scalars.Parsing.ExcludeBases;
using SharpMeasures.Generators.Scalars.Parsing.IncludeBases;
using SharpMeasures.Generators.Scalars.Parsing.ScalarConstant;
using SharpMeasures.Generators.Scalars.Parsing.SharpMeasuresScalar;

using System.Collections.Generic;

internal class ScalarBaseProcesser : AScalarProcesser<SharpMeasuresScalarDefinition, ScalarBaseType>
{
    protected override ScalarBaseType FinalizeProcess(DefinedType type, MinimalLocation typeLocation, SharpMeasuresScalarDefinition definition, IReadOnlyList<DerivedQuantityDefinition> derivations,
        IReadOnlyList<ScalarConstantDefinition> constants, IReadOnlyList<ConvertibleScalarDefinition> conversions, IReadOnlyList<IncludeBasesDefinition> baseInclusions,
        IReadOnlyList<ExcludeBasesDefinition> baseExclusions, IReadOnlyList<IncludeUnitsDefinition> unitInclusions, IReadOnlyList<ExcludeUnitsDefinition> unitExclusions)
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

        return ProcessingFilter.Create(SharpMeasuresScalarProcesser).Filter(processingContext, rawScalar);
    }

    private static SharpMeasuresScalarProcesser SharpMeasuresScalarProcesser { get; } = new(SharpMeasuresScalarProcessingDiagnostics.Instance);
}
