namespace SharpMeasures.Generators.Scalars.Parsing.Abstraction;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Quantities.Parsing.Contexts.Processing;
using SharpMeasures.Generators.Quantities.Parsing.ConvertibleQuantity;
using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;
using SharpMeasures.Generators.Quantities.Parsing.Diagnostics.Processing;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;
using SharpMeasures.Generators.Scalars.Parsing.Contexts.Processing;
using SharpMeasures.Generators.Scalars.Parsing.ConvertibleScalar;
using SharpMeasures.Generators.Scalars.Parsing.Diagnostics;
using SharpMeasures.Generators.Scalars.Parsing.Diagnostics.Processing;
using SharpMeasures.Generators.Scalars.Parsing.ExcludeBases;
using SharpMeasures.Generators.Scalars.Parsing.IncludeBases;
using SharpMeasures.Generators.Scalars.Parsing.ScalarConstant;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

internal abstract class AScalarProcesser<TDefinition, TProduct>
    where TDefinition : IScalar
{
    public IOptionalWithDiagnostics<TProduct> ParseAndProcess((TypeDeclarationSyntax Declaration, INamedTypeSymbol TypeSymbol) input, CancellationToken _) => ParseAndProcess(input.Declaration, input.TypeSymbol);
    public IOptionalWithDiagnostics<TProduct> ParseAndProcess(TypeDeclarationSyntax declaration, INamedTypeSymbol typeSymbol)
    {
        var scalar = ParseAndProcessScalar(typeSymbol);

        if (scalar.LacksResult)
        {
            return scalar.AsEmptyOptional<TProduct>();
        }

        var unit = GetUnit(scalar.Result);

        var derivations = ParseAndProcessDerivations(typeSymbol);
        var constants = ParseAndProcessConstants(typeSymbol, unit);
        var conversions = ParseAndProcessConversions(typeSymbol);

        var includeBases = ParseAndProcessIncludeBases(typeSymbol);
        var excludeBases = ParseAndProcessExcludeBases(typeSymbol);

        var includeUnits = ParseAndProcessIncludeUnits(typeSymbol);
        var excludeUnits = ParseAndProcessExcludeUnits(typeSymbol);

        var allDiagnostics = scalar.Diagnostics.Concat(derivations).Concat(constants).Concat(conversions).Concat(includeBases).Concat(excludeBases).Concat(includeUnits).Concat(excludeUnits);

        if (includeBases.HasResult && includeBases.Result.Count > 0 && excludeBases.HasResult && excludeBases.Result.Count > 0)
        {
            allDiagnostics = allDiagnostics.Concat(new[] { ScalarTypeDiagnostics.ContradictoryAttributes<IncludeBasesAttribute, ExcludeBasesAttribute>(declaration.Identifier.GetLocation().Minimize()) });
            excludeBases = ResultWithDiagnostics.Construct(Array.Empty<ExcludeBasesDefinition>() as IReadOnlyList<ExcludeBasesDefinition>);
        }

        if (includeUnits.HasResult && includeUnits.Result.Count > 0 && excludeUnits.HasResult && excludeUnits.Result.Count > 0)
        {
            allDiagnostics = allDiagnostics.Concat(new[] { ScalarTypeDiagnostics.ContradictoryAttributes<IncludeUnitsAttribute, ExcludeUnitsAttribute>(declaration.Identifier.GetLocation().Minimize()) });
            excludeUnits = ResultWithDiagnostics.Construct(Array.Empty<ExcludeUnitsDefinition>() as IReadOnlyList<ExcludeUnitsDefinition>);
        }

        TProduct product = FinalizeProcess(typeSymbol.AsDefinedType(), declaration.GetLocation().Minimize(), scalar.Result, derivations.Result, constants.Result,
            conversions.Result, includeBases.Result, excludeBases.Result, includeUnits.Result, excludeUnits.Result);

        return OptionalWithDiagnostics.Result(product, allDiagnostics);
    }

    protected abstract TProduct FinalizeProcess(DefinedType type, MinimalLocation typeLocation, TDefinition definition, IReadOnlyList<DerivedQuantityDefinition> derivations,
        IReadOnlyList<ScalarConstantDefinition> constants, IReadOnlyList<ConvertibleScalarDefinition> conversions, IReadOnlyList<IncludeBasesDefinition> baseInclusions,
        IReadOnlyList<ExcludeBasesDefinition> baseExclusions, IReadOnlyList<IncludeUnitsDefinition> unitInclusions, IReadOnlyList<ExcludeUnitsDefinition> unitExclusions);

    protected abstract IOptionalWithDiagnostics<TDefinition> ParseAndProcessScalar(INamedTypeSymbol typeSymbol);

    protected abstract NamedType? GetUnit(TDefinition scalar);

    private static IResultWithDiagnostics<IReadOnlyList<DerivedQuantityDefinition>> ParseAndProcessDerivations(INamedTypeSymbol typeSymbol)
    {
        var rawDerivations = DerivedQuantityParser.Parser.ParseAllOccurrences(typeSymbol);

        var processingContext = new SimpleProcessingContext(typeSymbol.AsDefinedType());

        return ProcessingFilter.Create(DerivedQuantityProcesser).Filter(processingContext, rawDerivations);
    }

    private static IResultWithDiagnostics<IReadOnlyList<ScalarConstantDefinition>> ParseAndProcessConstants(INamedTypeSymbol typeSymbol, NamedType? unit)
    {
        var rawConstants = ScalarConstantParser.Parser.ParseAllOccurrences(typeSymbol);

        var scalarConstantProcesser = unit is null
            ? ScalarConstantProcesserForUnknownUnit
            : ScalarConstantProcesser(unit.Value);

        QuantityConstantProcessingContext processingContext = new(typeSymbol.AsDefinedType());

        return ProcessingFilter.Create(scalarConstantProcesser).Filter(processingContext, rawConstants);
    }

    private static IResultWithDiagnostics<IReadOnlyList<ConvertibleScalarDefinition>> ParseAndProcessConversions(INamedTypeSymbol typeSymbol)
    {
        var rawConvertibles = ConvertibleQuantityParser.Parser.ParseAllOccurrences(typeSymbol);

        ConvertibleQuantityProcessingContext processingContext = new(typeSymbol.AsDefinedType());

        return ProcessingFilter.Create(ConvertibleScalarProcesser).Filter(processingContext, rawConvertibles);
    }

    private static IResultWithDiagnostics<IReadOnlyList<IncludeBasesDefinition>> ParseAndProcessIncludeBases(INamedTypeSymbol typeSymbol)
    {
        var rawIncludeBases = IncludeBasesParser.Parser.ParseAllOccurrences(typeSymbol);

        IncludeBasesProcessingContext processingContext = new(typeSymbol.AsDefinedType());

        return ProcessingFilter.Create(IncludeBasesProcesser).Filter(processingContext, rawIncludeBases);
    }

    private static IResultWithDiagnostics<IReadOnlyList<ExcludeBasesDefinition>> ParseAndProcessExcludeBases(INamedTypeSymbol typeSymbol)
    {
        var rawExcludeBases = ExcludeBasesParser.Parser.ParseAllOccurrences(typeSymbol);

        ExcludeBasesProcessingContext processingContext = new(typeSymbol.AsDefinedType());

        return ProcessingFilter.Create(ExcludeBasesProcesser).Filter(processingContext, rawExcludeBases);
    }

    private static IResultWithDiagnostics<IReadOnlyList<IncludeUnitsDefinition>> ParseAndProcessIncludeUnits(INamedTypeSymbol typeSymbol)
    {
        var rawIncludeUnits = IncludeUnitsParser.Parser.ParseAllOccurrences(typeSymbol);

        IncludeUnitsProcessingContext processingContext = new(typeSymbol.AsDefinedType());

        return ProcessingFilter.Create(IncludeUnitsProcesser).Filter(processingContext, rawIncludeUnits);
    }

    private static IResultWithDiagnostics<IReadOnlyList<ExcludeUnitsDefinition>> ParseAndProcessExcludeUnits(INamedTypeSymbol typeSymbol)
    {
        var rawExcludeUnits = ExcludeUnitsParser.Parser.ParseAllOccurrences(typeSymbol);

        ExcludeUnitsProcessingContext processingContext = new(typeSymbol.AsDefinedType());

        return ProcessingFilter.Create(ExcludeUnitsProcesser).Filter(processingContext, rawExcludeUnits);
    }

    private static DerivedQuantityProcesser DerivedQuantityProcesser { get; } = new(DerivedQuantityProcessingDiagnostics.Instance);
    private static ScalarConstantProcesser ScalarConstantProcesser(NamedType unit) => new(new ScalarConstantProcessingDiagnostics(unit));
    private static ScalarConstantProcesser ScalarConstantProcesserForUnknownUnit { get; } = new(new ScalarConstantProcessingDiagnostics());
    private static ConvertibleScalarProcesser ConvertibleScalarProcesser { get; } = new(ConvertibleScalarProcessingDiagnostics.Instance);

    private static IncludeBasesProcesser IncludeBasesProcesser { get; } = new(IncludeBasesProcessingDiagnostics.Instance);
    private static ExcludeBasesProcesser ExcludeBasesProcesser { get; } = new(ExcludeBasesProcessingDiagnostics.Instance);
    private static IncludeUnitsProcesser IncludeUnitsProcesser { get; } = new(IncludeUnitsProcessingDiagnostics.Instance);
    private static ExcludeUnitsProcesser ExcludeUnitsProcesser { get; } = new(ExcludeUnitsProcessingDiagnostics.Instance);
}
