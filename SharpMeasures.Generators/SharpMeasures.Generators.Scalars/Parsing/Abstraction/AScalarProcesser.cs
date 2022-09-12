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
using SharpMeasures.Generators.Scalars.Parsing.IncludeUnitBases;
using SharpMeasures.Generators.Scalars.Parsing.ScalarConstant;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

internal abstract class AScalarProcesser<TDefinition, TProduct>
    where TDefinition : IScalar
{
    public IOptionalWithDiagnostics<TProduct> ParseAndProcess(Optional<(TypeDeclarationSyntax Declaration, INamedTypeSymbol TypeSymbol)> input, CancellationToken token)
    {
        if (token.IsCancellationRequested || input.HasValue is false)
        {
            return OptionalWithDiagnostics.Empty<TProduct>();
        }

        return ParseAndProcess(input.Value.Declaration, input.Value.TypeSymbol);
    }

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

        var includeUnitInstanceBases = ParseAndProcessIncludeUnitBases(typeSymbol);
        var excludeUnitInstanceBases = ParseAndProcessExcludeUnitBases(typeSymbol);

        var includeUnitInstances = ParseAndProcessIncludeUnits(typeSymbol);
        var excludeUnitInstances = ParseAndProcessExcludeUnits(typeSymbol);

        var allDiagnostics = scalar.Diagnostics.Concat(derivations).Concat(constants).Concat(conversions).Concat(includeUnitInstanceBases).Concat(excludeUnitInstanceBases).Concat(includeUnitInstances).Concat(excludeUnitInstances);

        if (includeUnitInstanceBases.HasResult && includeUnitInstanceBases.Result.Count > 0 && excludeUnitInstanceBases.HasResult && excludeUnitInstanceBases.Result.Count > 0)
        {
            allDiagnostics = allDiagnostics.Concat(new[] { ScalarTypeDiagnostics.ContradictoryAttributes<IncludeUnitBasesAttribute, ExcludeUnitBasesAttribute>(declaration.Identifier.GetLocation().Minimize()) });
            excludeUnitInstanceBases = ResultWithDiagnostics.Construct(Array.Empty<ExcludeUnitBasesDefinition>() as IReadOnlyList<ExcludeUnitBasesDefinition>);
        }

        if (includeUnitInstances.HasResult && includeUnitInstances.Result.Count > 0 && excludeUnitInstances.HasResult && excludeUnitInstances.Result.Count > 0)
        {
            allDiagnostics = allDiagnostics.Concat(new[] { ScalarTypeDiagnostics.ContradictoryAttributes<IncludeUnitsAttribute, ExcludeUnitsAttribute>(declaration.Identifier.GetLocation().Minimize()) });
            excludeUnitInstances = ResultWithDiagnostics.Construct(Array.Empty<ExcludeUnitsDefinition>() as IReadOnlyList<ExcludeUnitsDefinition>);
        }

        TProduct product = FinalizeProcess(typeSymbol.AsDefinedType(), declaration.GetLocation().Minimize(), scalar.Result, derivations.Result, constants.Result,
            conversions.Result, includeUnitInstanceBases.Result, excludeUnitInstanceBases.Result, includeUnitInstances.Result, excludeUnitInstances.Result);

        return OptionalWithDiagnostics.Result(product, allDiagnostics);
    }

    protected abstract TProduct FinalizeProcess(DefinedType type, MinimalLocation typeLocation, TDefinition definition, IReadOnlyList<DerivedQuantityDefinition> derivations,
        IReadOnlyList<ScalarConstantDefinition> constants, IReadOnlyList<ConvertibleScalarDefinition> conversions, IReadOnlyList<IncludeUnitBasesDefinition> baseInclusions,
        IReadOnlyList<ExcludeUnitBasesDefinition> baseExclusions, IReadOnlyList<IncludeUnitsDefinition> unitInclusions, IReadOnlyList<ExcludeUnitsDefinition> unitExclusions);

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

    private static IResultWithDiagnostics<IReadOnlyList<IncludeUnitBasesDefinition>> ParseAndProcessIncludeUnitBases(INamedTypeSymbol typeSymbol)
    {
        var rawIncludeUnitBases = IncludeUnitBasesParser.Parser.ParseAllOccurrences(typeSymbol);

        IncludeUnitBasesProcessingContext processingContext = new(typeSymbol.AsDefinedType());

        return ProcessingFilter.Create(IncludeUnitBasesProcesser).Filter(processingContext, rawIncludeUnitBases);
    }

    private static IResultWithDiagnostics<IReadOnlyList<ExcludeUnitBasesDefinition>> ParseAndProcessExcludeUnitBases(INamedTypeSymbol typeSymbol)
    {
        var rawExcludeUnitBases = ExcludeUnitBasesParser.Parser.ParseAllOccurrences(typeSymbol);

        ExcludUniteBasesProcessingContext processingContext = new(typeSymbol.AsDefinedType());

        return ProcessingFilter.Create(ExcludeUnitBasesProcesser).Filter(processingContext, rawExcludeUnitBases);
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

    private static IncludeUnitBasesProcesser IncludeUnitBasesProcesser { get; } = new(IncludeUnitBasesProcessingDiagnostics.Instance);
    private static ExcludeUnitBasesProcesser ExcludeUnitBasesProcesser { get; } = new(ExcludeUnitBasesProcessingDiagnostics.Instance);
    private static IncludeUnitsProcesser IncludeUnitsProcesser { get; } = new(IncludeUnitsProcessingDiagnostics.Instance);
    private static ExcludeUnitsProcesser ExcludeUnitsProcesser { get; } = new(ExcludeUnitsProcessingDiagnostics.Instance);
}
