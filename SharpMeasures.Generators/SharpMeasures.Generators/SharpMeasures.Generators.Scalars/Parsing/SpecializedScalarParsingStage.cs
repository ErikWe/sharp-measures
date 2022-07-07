namespace SharpMeasures.Generators.Scalars.Parsing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.ConvertibleQuantity;
using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Scalars.Diagnostics;
using SharpMeasures.Generators.Scalars.Parsing.Abstractions;
using SharpMeasures.Generators.Scalars.Parsing.ExcludeBases;
using SharpMeasures.Generators.Scalars.Parsing.IncludeBases;
using SharpMeasures.Generators.Scalars.Parsing.ScalarConstant;
using SharpMeasures.Generators.Scalars.Parsing.SpecializedSharpMeasuresScalar;

using System.Collections.Generic;
using System.Threading;

internal class SpecializedScalarParsingStage : AScalarParsingStage<SpecializedSharpMeasuresScalarAttribute, RawSpecializedScalarType, SpecializedScalarType,
        RawSpecializedSharpMeasuresScalarDefinition, SpecializedSharpMeasuresScalarDefinition>
{
    protected override IAttributeParser<RawSpecializedSharpMeasuresScalarDefinition> ScalarParser => SpecializedSharpMeasuresScalarParser.Parser;
    protected override IScalarConstantDiagnostics ScalarConstantDiagnostics(RawSpecializedScalarType scalar, SpecializedSharpMeasuresScalarDefinition definition)
        => new ScalarConstantDiagnostics();

    protected override RawSpecializedScalarType ConstructRawResult(DefinedType type, MinimalLocation typeLocation, RawSpecializedSharpMeasuresScalarDefinition scalarDefinition,
        IEnumerable<RawDerivedQuantityDefinition> derivations, IEnumerable<RawScalarConstantDefinition> constants, IEnumerable<RawIncludeBasesDefinition> includeBases,
        IEnumerable<RawExcludeBasesDefinition> excludeBases, IEnumerable<RawIncludeUnitsDefinition> includeUnits, IEnumerable<RawExcludeUnitsDefinition> excludeUnits,
        IEnumerable<RawConvertibleQuantityDefinition> convertibleQuantities)
    {
        return new(type, typeLocation, scalarDefinition, derivations, constants, includeBases, excludeBases, includeUnits, excludeUnits, convertibleQuantities);
    }

    protected override SpecializedScalarType ConstructProcessedResult(DefinedType type, MinimalLocation typeLocation, SpecializedSharpMeasuresScalarDefinition scalarDefinition,
        IReadOnlyList<DerivedQuantityDefinition> derivations, IReadOnlyList<ScalarConstantDefinition> constants, IReadOnlyList<IncludeBasesDefinition> includeBases,
        IReadOnlyList<ExcludeBasesDefinition> excludeBases, IReadOnlyList<IncludeUnitsDefinition> includeUnits, IReadOnlyList<ExcludeUnitsDefinition> excludeUnits,
        IReadOnlyList<ConvertibleQuantityDefinition> convertibleQuantities)
    {
        return new(type, typeLocation, scalarDefinition, derivations, constants, includeBases, excludeBases, includeUnits, excludeUnits, convertibleQuantities);
    }

    protected override IOptionalWithDiagnostics<SpecializedSharpMeasuresScalarDefinition> ProcessScalarDefinition(RawSpecializedScalarType definition)
    {
        ProcessingContext context = new(definition.Type);

        return BaseProcessers.SharpMeasuresScalarProcesser.Process(context, definition.ScalarDefinition);
    }

    private readonly record struct ProcessingContext : IProcessingContext
    {
        public DefinedType Type { get; }

        public ProcessingContext(DefinedType type)
        {
            Type = type;
        }
    }

    private static class BaseProcessers
    {
        public static SpecializedSharpMeasuresScalarProcesser SharpMeasuresScalarProcesser { get; } = new(SpecializedSharpMeasuresScalarDiagnostics.Instance);
    }
}
