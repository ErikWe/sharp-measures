namespace SharpMeasures.Generators.Scalars.Parsing;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Providers;
using SharpMeasures.Generators.Quantities.Parsing.Contexts.Processing;
using SharpMeasures.Generators.Quantities.Parsing.ConvertibleQuantity;
using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;
using SharpMeasures.Generators.Quantities.Parsing.Diagnostics.Processing;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.UnitList;
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Scalars.Parsing.ConvertibleScalar;
using SharpMeasures.Generators.Scalars.Parsing.Diagnostics;
using SharpMeasures.Generators.Scalars.Parsing.Diagnostics.Processing;
using SharpMeasures.Generators.Scalars.Parsing.ExcludeBases;
using SharpMeasures.Generators.Scalars.Parsing.IncludeBases;
using SharpMeasures.Generators.Scalars.Parsing.ScalarConstant;
using SharpMeasures.Generators.Scalars.Parsing.SharpMeasuresScalar;
using SharpMeasures.Generators.Scalars.Parsing.SpecializedSharpMeasuresScalar;
using SharpMeasures.Generators.Unresolved.Scalars;

using System.Collections.Immutable;
using System.Linq;
using System.Threading;

public static class ScalarParser
{
    public static (IncrementalValueProvider<IUnresolvedScalarPopulation>, ScalarResolver) Attach(IncrementalGeneratorInitializationContext context)
    {
        var baseScalarSymbols = AttachSymbolProvider<SharpMeasuresScalarAttribute>(context, BaseScalarTypeDiagnostics.Instance);
        var specializedScalarSymbols = AttachSymbolProvider<SpecializedSharpMeasuresScalarAttribute>(context, SpecializedScalarTypeDiagnostics.Instance);

        var parsedBaseScalars = baseScalarSymbols.Select(ParseBaseAttributes).ReportDiagnostics(context).Select(ProcessParsedData).ReportDiagnostics(context);
        var parsedSpecializedScalars = specializedScalarSymbols.Select(ParseSpecializedAttributes).ReportDiagnostics(context).Select(ProcessParsedData)
            .ReportDiagnostics(context);

        var baseScalarInterfaces = parsedBaseScalars.Select(ExtractInterface).Collect();
        var specializedScalarInterfaces = parsedSpecializedScalars.Select(ExtractInterface).Collect();
        
        var population = baseScalarInterfaces.Combine(specializedScalarInterfaces).Select(CreatePopulation);

        return (population, new ScalarResolver(parsedBaseScalars, parsedSpecializedScalars));
    }

    private static IncrementalValuesProvider<IntermediateResult> AttachSymbolProvider<TAttribute>(IncrementalGeneratorInitializationContext context,
        IPartialDeclarationProviderDiagnostics diagnostics)
    {
        var declarations = MarkedTypeDeclarationCandidateProvider.Construct().Attach<TAttribute>(context.SyntaxProvider);
        var partialDeclarations = PartialDeclarationProvider.Construct<TypeDeclarationSyntax>().AttachAndReport(context, declarations, diagnostics);
        return DeclarationSymbolProvider.ConstructForValueType(IntermediateResult.Construct).Attach(partialDeclarations, context.CompilationProvider);
    }

    private static IOptionalWithDiagnostics<RawBaseScalarType> ParseBaseAttributes(IntermediateResult input, CancellationToken _)
    {
        if (SharpMeasuresScalarParser.Parser.ParseFirstOccurrence(input.TypeSymbol) is not RawSharpMeasuresScalarDefinition scalar)
        {
            return OptionalWithDiagnostics.EmptyWithoutDiagnostics<RawBaseScalarType>();
        }

        var definedType = input.TypeSymbol.AsDefinedType();
        var typeLocation = input.Declaration.Identifier.GetLocation().Minimize();

        var derivations = DerivedQuantityParser.Parser.ParseAllOccurrences(input.TypeSymbol);
        var constants = ScalarConstantParser.Parser.ParseAllOccurrences(input.TypeSymbol);
        var convertibles = ConvertibleQuantityParser.Parser.ParseAllOccurrences(input.TypeSymbol);

        var baseInclusions = IncludeBasesParser.Parser.ParseAllOccurrences(input.TypeSymbol);
        var baseExclusions = ExcludeBasesParser.Parser.ParseAllOccurrences(input.TypeSymbol);

        var unitInclusions = IncludeUnitsParser.Parser.ParseAllOccurrences(input.TypeSymbol);
        var unitExclusions = ExcludeUnitsParser.Parser.ParseAllOccurrences(input.TypeSymbol);

        RawBaseScalarType rawScalarType = new(definedType, typeLocation, scalar, derivations, constants, convertibles, baseInclusions, baseExclusions, unitInclusions,
            unitExclusions);

        return OptionalWithDiagnostics.Result(rawScalarType);
    }

    private static IOptionalWithDiagnostics<RawSpecializedScalarType> ParseSpecializedAttributes(IntermediateResult input, CancellationToken _)
    {
        if (SpecializedSharpMeasuresScalarParser.Parser.ParseFirstOccurrence(input.TypeSymbol) is not RawSpecializedSharpMeasuresScalarDefinition scalar)
        {
            return OptionalWithDiagnostics.EmptyWithoutDiagnostics<RawSpecializedScalarType>();
        }

        var definedType = input.TypeSymbol.AsDefinedType();
        var typeLocation = input.Declaration.Identifier.GetLocation().Minimize();

        var derivations = DerivedQuantityParser.Parser.ParseAllOccurrences(input.TypeSymbol);
        var constants = ScalarConstantParser.Parser.ParseAllOccurrences(input.TypeSymbol);
        var convertibles = ConvertibleQuantityParser.Parser.ParseAllOccurrences(input.TypeSymbol);

        var baseInclusions = IncludeBasesParser.Parser.ParseAllOccurrences(input.TypeSymbol);
        var baseExclusions = ExcludeBasesParser.Parser.ParseAllOccurrences(input.TypeSymbol);

        var unitInclusions = IncludeUnitsParser.Parser.ParseAllOccurrences(input.TypeSymbol);
        var unitExclusions = ExcludeUnitsParser.Parser.ParseAllOccurrences(input.TypeSymbol);

        RawSpecializedScalarType rawScalarType = new(definedType, typeLocation, scalar, derivations, constants, convertibles, baseInclusions, baseExclusions,
            unitInclusions, unitExclusions);

        return OptionalWithDiagnostics.Result(rawScalarType);
    }

    private static IOptionalWithDiagnostics<UnresolvedBaseScalarType> ProcessParsedData(RawBaseScalarType rawScalarType, CancellationToken _)
    {
        IProcessingContext processingContext = new SimpleProcessingContext(rawScalarType.Type);

        var scalar = Processers.SharpMeasuresScalarProcesser.Process(processingContext, rawScalarType.Definition);
        var allDiagnostics = scalar.Diagnostics;

        if (scalar.LacksResult)
        {
            return OptionalWithDiagnostics.Empty<UnresolvedBaseScalarType>(allDiagnostics);
        }

        DerivedQuantityProcessingContext derivedQuantityProcessingContext = new(rawScalarType.Type);
        QuantityConstantProcessingContext quantityConstantProcessingContext = new(rawScalarType.Type);
        ConvertibleQuantityProcessingContext convertibleQuantityProcessingContext = new(rawScalarType.Type);
        UnitListProcessingContext unitListProcessingContext = new(rawScalarType.Type);

        var derivations = ProcessingFilter.Create(Processers.DerivedQuantityProcesser).Filter(derivedQuantityProcessingContext, rawScalarType.Derivations);
        var constants = ProcessingFilter.Create(Processers.ScalarConstantProcesser(scalar.Result.Unit)).Filter(quantityConstantProcessingContext, rawScalarType.Constants);
        var convertibleScalars = ProcessingFilter.Create(Processers.ConvertibleScalarProcesser).Filter(convertibleQuantityProcessingContext, rawScalarType.ConvertibleScalars);

        var includeBases = ProcessingFilter.Create(Processers.UnitListProcesser).Filter(unitListProcessingContext, rawScalarType.BaseInclusions);
        unitListProcessingContext.ListedItems.Clear();

        var excludeBases = ProcessingFilter.Create(Processers.UnitListProcesser).Filter(unitListProcessingContext, rawScalarType.BaseExclusions);
        unitListProcessingContext.ListedItems.Clear();

        var includeUnits = ProcessingFilter.Create(Processers.UnitListProcesser).Filter(unitListProcessingContext, rawScalarType.UnitInclusions);
        unitListProcessingContext.ListedItems.Clear();

        var excludeUnits = ProcessingFilter.Create(Processers.UnitListProcesser).Filter(unitListProcessingContext, rawScalarType.UnitExclusions);

        allDiagnostics = allDiagnostics.Concat(derivations.Diagnostics).Concat(constants.Diagnostics).Concat(convertibleScalars.Diagnostics)
            .Concat(includeBases.Diagnostics).Concat(excludeBases.Diagnostics).Concat(includeUnits.Diagnostics).Concat(excludeUnits.Diagnostics);

        UnresolvedBaseScalarType scalarType = new(rawScalarType.Type, rawScalarType.TypeLocation, scalar.Result, derivations.Result, constants.Result,
            convertibleScalars.Result, includeBases.Result, excludeBases.Result, includeUnits.Result, excludeUnits.Result);

        return OptionalWithDiagnostics.Result(scalarType, allDiagnostics);
    }

    private static IOptionalWithDiagnostics<UnresolvedSpecializedScalarType> ProcessParsedData(RawSpecializedScalarType rawScalarType, CancellationToken _)
    {
        IProcessingContext processingContext = new SimpleProcessingContext(rawScalarType.Type);

        var scalar = Processers.SpecializedSharpMeasuresScalarProcesser.Process(processingContext, rawScalarType.Definition);
        var allDiagnostics = scalar.Diagnostics;

        if (scalar.LacksResult)
        {
            return OptionalWithDiagnostics.Empty<UnresolvedSpecializedScalarType>(allDiagnostics);
        }

        DerivedQuantityProcessingContext derivedQuantityProcessingContext = new(rawScalarType.Type);
        QuantityConstantProcessingContext quantityConstantProcessingContext = new(rawScalarType.Type);
        ConvertibleQuantityProcessingContext convertibleQuantityProcessingContext = new(rawScalarType.Type);
        UnitListProcessingContext unitListProcessingContext = new(rawScalarType.Type);

        var derivations = ProcessingFilter.Create(Processers.DerivedQuantityProcesser).Filter(derivedQuantityProcessingContext, rawScalarType.Derivations);
        var constants = ProcessingFilter.Create(Processers.ScalarConstantProcesserForUnknownUnit).Filter(quantityConstantProcessingContext, rawScalarType.Constants);
        var convertibleScalars = ProcessingFilter.Create(Processers.ConvertibleScalarProcesser).Filter(convertibleQuantityProcessingContext, rawScalarType.ConvertibleScalars);

        var includeBases = ProcessingFilter.Create(Processers.UnitListProcesser).Filter(unitListProcessingContext, rawScalarType.BaseInclusions);
        unitListProcessingContext.ListedItems.Clear();

        var excludeBases = ProcessingFilter.Create(Processers.UnitListProcesser).Filter(unitListProcessingContext, rawScalarType.BaseExclusions);
        unitListProcessingContext.ListedItems.Clear();

        var includeUnits = ProcessingFilter.Create(Processers.UnitListProcesser).Filter(unitListProcessingContext, rawScalarType.UnitInclusions);
        unitListProcessingContext.ListedItems.Clear();

        var excludeUnits = ProcessingFilter.Create(Processers.UnitListProcesser).Filter(unitListProcessingContext, rawScalarType.UnitExclusions);

        allDiagnostics = allDiagnostics.Concat(derivations.Diagnostics).Concat(constants.Diagnostics).Concat(convertibleScalars.Diagnostics)
            .Concat(includeBases.Diagnostics).Concat(excludeBases.Diagnostics).Concat(includeUnits.Diagnostics).Concat(excludeUnits.Diagnostics);

        UnresolvedSpecializedScalarType scalarType = new(rawScalarType.Type, rawScalarType.TypeLocation, scalar.Result, derivations.Result, constants.Result,
            convertibleScalars.Result, includeBases.Result, excludeBases.Result, includeUnits.Result, excludeUnits.Result);

        return OptionalWithDiagnostics.Result(scalarType, allDiagnostics);
    }

    private static IUnresolvedBaseScalarType ExtractInterface(UnresolvedBaseScalarType scalarType, CancellationToken _) => scalarType;
    private static IUnresolvedSpecializedScalarType ExtractInterface(UnresolvedSpecializedScalarType scalarType, CancellationToken _) => scalarType;

    private static IUnresolvedScalarPopulation CreatePopulation
        ((ImmutableArray<IUnresolvedBaseScalarType> Bases, ImmutableArray<IUnresolvedSpecializedScalarType> Specialized) scalars, CancellationToken _)
    {
        return UnresolvedScalarPopulation.Build(scalars.Bases, scalars.Specialized);
    }

    private static class Processers
    {
        public static SharpMeasuresScalarProcesser SharpMeasuresScalarProcesser { get; } = new(SharpMeasuresScalarProcessingDiagnostics.Instance);
        public static SpecializedSharpMeasuresScalarProcesser SpecializedSharpMeasuresScalarProcesser { get; }
            = new(SpecializedSharpMeasuresScalarProcessingDiagnostics.Instance);

        public static DerivedQuantityProcesser DerivedQuantityProcesser { get; } = new(DerivedQuantityProcessingDiagnostics.Instance);
        public static ScalarConstantProcesser ScalarConstantProcesser(NamedType unit) => new(new ScalarConstantProcessingDiagnostics(unit));
        public static ScalarConstantProcesser ScalarConstantProcesserForUnknownUnit { get; } = new(new ScalarConstantProcessingDiagnostics());
        public static ConvertibleScalarProcesser ConvertibleScalarProcesser { get; } = new(ConvertibleScalarProcessingDiagnostics.Instance);

        public static UnitListProcesser UnitListProcesser { get; } = new(UnitListProcessingDiagnostics.Instance);
    }

    private readonly record struct IntermediateResult(TypeDeclarationSyntax Declaration, INamedTypeSymbol TypeSymbol)
    {
        public static DeclarationSymbolProvider.DOutputTransform<TypeDeclarationSyntax, IntermediateResult> Construct => (declaration, symbol) => new(declaration, symbol);
    }
}
