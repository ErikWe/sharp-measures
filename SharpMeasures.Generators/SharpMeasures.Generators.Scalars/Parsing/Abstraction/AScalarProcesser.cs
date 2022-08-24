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
using SharpMeasures.Generators.Quantities.Parsing.UnitList;
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

        var includeBases = ParseAndProcessUnitList(typeSymbol, IncludeBasesParser.Parser);
        var excludeBases = ParseAndProcessUnitList(typeSymbol, ExcludeBasesParser.Parser);

        var includeUnits = ParseAndProcessUnitList(typeSymbol, IncludeUnitsParser.Parser);
        var excludeUnits = ParseAndProcessUnitList(typeSymbol, ExcludeUnitsParser.Parser);

        var allDiagnostics = scalar.Diagnostics.Concat(derivations).Concat(constants).Concat(conversions).Concat(includeBases).Concat(excludeBases).Concat(includeUnits).Concat(excludeUnits);

        if (includeBases.HasResult && includeBases.Result.Count > 0 && excludeBases.HasResult && excludeBases.Result.Count > 0)
        {
            allDiagnostics = allDiagnostics.Concat(new[] { ScalarTypeDiagnostics.ContradictoryAttributes<IncludeBasesAttribute, ExcludeBasesAttribute>(declaration.GetLocation().Minimize()) });
            excludeBases = ResultWithDiagnostics.Construct(Array.Empty<UnitListDefinition>() as IReadOnlyList<UnitListDefinition>);
        }

        if (includeUnits.HasResult && includeUnits.Result.Count > 0 && excludeUnits.HasResult && excludeUnits.Result.Count > 0)
        {
            allDiagnostics = allDiagnostics.Concat(new[] { ScalarTypeDiagnostics.ContradictoryAttributes<IncludeUnitsAttribute, ExcludeUnitsAttribute>(declaration.GetLocation().Minimize()) });
            excludeUnits = ResultWithDiagnostics.Construct(Array.Empty<UnitListDefinition>() as IReadOnlyList<UnitListDefinition>);
        }

        TProduct product = FinalizeProcess(typeSymbol.AsDefinedType(), declaration.GetLocation().Minimize(), scalar.Result, derivations.Result, constants.Result,
            conversions.Result, includeBases.Result, excludeBases.Result, includeUnits.Result, excludeUnits.Result);

        return OptionalWithDiagnostics.Result(product, allDiagnostics);
    }

    protected abstract TProduct FinalizeProcess(DefinedType type, MinimalLocation typeLocation, TDefinition definition, IReadOnlyList<DerivedQuantityDefinition> derivations,
        IReadOnlyList<ScalarConstantDefinition> constants, IReadOnlyList<ConvertibleScalarDefinition> conversions, IReadOnlyList<UnitListDefinition> baseInclusions,
        IReadOnlyList<UnitListDefinition> baseExclusions, IReadOnlyList<UnitListDefinition> unitInclusions, IReadOnlyList<UnitListDefinition> unitExclusions);

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

    private static IResultWithDiagnostics<IReadOnlyList<UnitListDefinition>> ParseAndProcessUnitList(INamedTypeSymbol typeSymbol, IAttributeParser<RawUnitListDefinition> parser)
    {
        var rawLists = parser.ParseAllOccurrences(typeSymbol);

        UnitListProcessingContext processingContext = new(typeSymbol.AsDefinedType());

        return ProcessingFilter.Create(UnitListProcesser).Filter(processingContext, rawLists);
    }

    private static DerivedQuantityProcesser DerivedQuantityProcesser { get; } = new(DerivedQuantityProcessingDiagnostics.Instance);
    private static ScalarConstantProcesser ScalarConstantProcesser(NamedType unit) => new(new ScalarConstantProcessingDiagnostics(unit));
    private static ScalarConstantProcesser ScalarConstantProcesserForUnknownUnit { get; } = new(new ScalarConstantProcessingDiagnostics());
    private static ConvertibleScalarProcesser ConvertibleScalarProcesser { get; } = new(ConvertibleScalarProcessingDiagnostics.Instance);

    private static UnitListProcesser UnitListProcesser { get; } = new(UnitListProcessingDiagnostics.Instance);
}
