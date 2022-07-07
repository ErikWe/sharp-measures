namespace SharpMeasures.Generators.Scalars.Parsing.Abstractions;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Attributes.Parsing.ItemLists;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Providers;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Quantities.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.ConvertibleQuantity;
using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;
using SharpMeasures.Generators.Scalars.Diagnostics;
using SharpMeasures.Generators.Scalars.Parsing.ExcludeBases;
using SharpMeasures.Generators.Scalars.Parsing.IncludeBases;
using SharpMeasures.Generators.Scalars.Parsing.ScalarConstant;

using System.Collections.Generic;
using System.Linq;
using System.Threading;

internal abstract class AScalarParsingStage<TAttribute, TRawScalarType, TProcessedScalarType, TRawScalarDefinition, TProcessedScalarDefinition>
    where TRawScalarType : IRawScalarType
{
    public IncrementalValuesProvider<TProcessedScalarType> Attach(IncrementalGeneratorInitializationContext context)
    {
        var declarations = MarkedTypeDeclarationCandidateProvider.Construct().Attach<TAttribute>(context.SyntaxProvider);
        var partialDeclarations = PartialDeclarationProvider.Construct<TypeDeclarationSyntax>().AttachAndReport(context, declarations, ScalarDiagnostics.Instance);
        var symbols = DeclarationSymbolProvider.ConstructForValueType(ConstructIntermediateResult).Attach(partialDeclarations, context.CompilationProvider);

        return symbols.Select(ParseAttributes).ReportDiagnostics(context).Select(ProcessParsedData).ReportDiagnostics(context);
    }

    private IOptionalWithDiagnostics<TRawScalarType> ParseAttributes(IntermediateResult input, CancellationToken _)
    {
        if (ScalarParser.ParseFirstOccurrence(input.TypeSymbol) is not TRawScalarDefinition scalar)
        {
            return OptionalWithDiagnostics.EmptyWithoutDiagnostics<TRawScalarType>();
        }

        var definedType = input.TypeSymbol.AsDefinedType();
        var typeLocation = input.Declaration.Identifier.GetLocation().Minimize();

        var derivations = DerivedQuantityParser.Parser.ParseAllOccurrences(input.TypeSymbol);

        var constants = ScalarConstantParser.Parser.ParseAllOccurrences(input.TypeSymbol);

        var includeBases = IncludeBasesParser.Parser.ParseAllOccurrences(input.TypeSymbol);
        var excludeBases = ExcludeBasesParser.Parser.ParseAllOccurrences(input.TypeSymbol);

        var includeUnits = IncludeUnitsParser.Parser.ParseAllOccurrences(input.TypeSymbol);
        var excludeUnits = ExcludeUnitsParser.Parser.ParseAllOccurrences(input.TypeSymbol);

        var convertibleQuantities = ConvertibleQuantityParser.Parser.ParseAllOccurrences(input.TypeSymbol);

        TRawScalarType result = ConstructRawResult(definedType, typeLocation, scalar, derivations, constants, includeBases, excludeBases, includeUnits, excludeUnits,
            convertibleQuantities);

        return OptionalWithDiagnostics.Result(result);
    }

    private IOptionalWithDiagnostics<TProcessedScalarType> ProcessParsedData(TRawScalarType rawScalarType, CancellationToken _)
    {
        var scalar = ProcessScalarDefinition(rawScalarType);

        if (scalar.LacksResult)
        {
            return OptionalWithDiagnostics.Empty<TProcessedScalarType>(scalar.Diagnostics);
        }

        DerivedQuantityProcessingContext derivedQuantityProcessingContext = new(rawScalarType.Type);

        var derivations = ProcessingFilter.Create(Processers.DerivedQuantityProcesser).Filter(derivedQuantityProcessingContext, rawScalarType.Derivations);

        ScalarConstantProcessingContext scalarConstantProcessingContext = new(rawScalarType.Type);

        var constants = ProcessingFilter.Create(new ScalarConstantProcesser(ScalarConstantDiagnostics(rawScalarType, scalar.Result)))
            .Filter(scalarConstantProcessingContext, rawScalarType.Constants);

        UnitListProcessingContext unitListProcessingContext = new(rawScalarType.Type);

        var includeBases = ProcessingFilter.Create(Processers.IncludeBasesProcesser).Filter(unitListProcessingContext, rawScalarType.IncludeBases);
        unitListProcessingContext.ListedItems.Clear();

        var excludeBases = ProcessingFilter.Create(Processers.ExcludeBasesProcesser).Filter(unitListProcessingContext, rawScalarType.ExcludeBases);
        unitListProcessingContext.ListedItems.Clear();

        var includeUnits = ProcessingFilter.Create(Processers.IncludeUnitsProcesser).Filter(unitListProcessingContext, rawScalarType.IncludeUnits);
        unitListProcessingContext.ListedItems.Clear();

        var excludeUnits = ProcessingFilter.Create(Processers.ExcludeUnitsProcesser).Filter(unitListProcessingContext, rawScalarType.ExcludeUnits);
        unitListProcessingContext.ListedItems.Clear();

        ConvertibleQuantityProcessingContext convertibleQuantityProcessingContext = new(rawScalarType.Type);

        var convertibleQuantities = ProcessingFilter.Create(Processers.ConvertibleQuantityProcesser)
            .Filter(convertibleQuantityProcessingContext, rawScalarType.ConvertibleQuantities);

        TProcessedScalarType product = ConstructProcessedResult(rawScalarType.Type, rawScalarType.TypeLocation, scalar.Result, derivations.Result, constants.Result,
            includeBases.Result, excludeBases.Result, includeUnits.Result, excludeUnits.Result, convertibleQuantities.Result);

        var allDiagnostics = scalar.Diagnostics.Concat(constants.Diagnostics).Concat(includeBases.Diagnostics).Concat(excludeBases.Diagnostics)
            .Concat(includeUnits.Diagnostics).Concat(excludeUnits.Diagnostics).Concat(convertibleQuantities.Diagnostics);

        return OptionalWithDiagnostics.Result(product, allDiagnostics);
    }

    protected abstract IAttributeParser<TRawScalarDefinition> ScalarParser { get; }
    protected abstract IScalarConstantDiagnostics ScalarConstantDiagnostics(TRawScalarType scalar, TProcessedScalarDefinition definition);

    protected abstract TRawScalarType ConstructRawResult(DefinedType type, MinimalLocation typeLocation, TRawScalarDefinition scalarDefinition,
        IEnumerable<RawDerivedQuantityDefinition> derivations, IEnumerable<RawScalarConstantDefinition> constants, IEnumerable<RawIncludeBasesDefinition> includeBases,
        IEnumerable<RawExcludeBasesDefinition> excludeBases, IEnumerable<RawIncludeUnitsDefinition> includeUnits, IEnumerable<RawExcludeUnitsDefinition> excludeUnits,
        IEnumerable<RawConvertibleQuantityDefinition> convertibleQuantities);

    protected abstract IOptionalWithDiagnostics<TProcessedScalarDefinition> ProcessScalarDefinition(TRawScalarType definition);

    protected abstract TProcessedScalarType ConstructProcessedResult(DefinedType type, MinimalLocation typeLocation, TProcessedScalarDefinition scalarDefinition,
        IReadOnlyList<DerivedQuantityDefinition> derivations, IReadOnlyList<ScalarConstantDefinition> constants, IReadOnlyList<IncludeBasesDefinition> includeBases,
        IReadOnlyList<ExcludeBasesDefinition> excludeBases, IReadOnlyList<IncludeUnitsDefinition> includeUnits, IReadOnlyList<ExcludeUnitsDefinition> excludeUnits,
        IReadOnlyList<ConvertibleQuantityDefinition> convertibleQuantities);

    protected readonly record struct IntermediateResult(TypeDeclarationSyntax Declaration, INamedTypeSymbol TypeSymbol);

    private static DeclarationSymbolProvider.DOutputTransform<TypeDeclarationSyntax, IntermediateResult> ConstructIntermediateResult
        => (declaration, symbol) => new IntermediateResult(declaration, symbol);

    private readonly record struct ProcessingContext : IProcessingContext
    {
        public DefinedType Type { get; }

        public ProcessingContext(DefinedType type)
        {
            Type = type;
        }
    }

    private readonly record struct DerivedQuantityProcessingContext : IDerivedQuantityProcessingContext
    {
        public DefinedType Type { get; }

        public HashSet<QuantityDerivationSignature> ListedDerivations { get; } = new();

        public DerivedQuantityProcessingContext(DefinedType type)
        {
            Type = type;
        }
    }

    private readonly record struct ScalarConstantProcessingContext : IScalarConstantProcessingContext
    {
        public DefinedType Type { get; }

        public HashSet<string> ReservedConstants { get; } = new();
        public HashSet<string> ReservedConstantMultiples { get; } = new();

        public ScalarConstantProcessingContext(DefinedType type)
        {
            Type = type;
        }
    }

    private readonly record struct UnitListProcessingContext : IUniqueItemListProcessingContext<string?>
    {
        public DefinedType Type { get; }

        public HashSet<string?> ListedItems { get; } = new();

        public UnitListProcessingContext(DefinedType type)
        {
            Type = type;
        }
    }

    private readonly record struct ConvertibleQuantityProcessingContext : IConvertibleQuantityProcessingContext
    {
        public DefinedType Type { get; }

        public HashSet<NamedType> ListedQuantities { get; } = new();

        public ConvertibleQuantityProcessingContext(DefinedType type)
        {
            Type = type;
        }
    }

    protected static class Processers
    {
        public static DerivedQuantityProcesser DerivedQuantityProcesser { get; } = new(DerivedQuantityDiagnostics.Instance);

        public static IncludeBasesProcesser IncludeBasesProcesser { get; } = new(UnitListProcessingDiagnostics<RawIncludeBasesDefinition, IncludeBasesLocations>.Instance);
        public static ExcludeBasesProcesser ExcludeBasesProcesser { get; } = new(UnitListProcessingDiagnostics<RawExcludeBasesDefinition, ExcludeBasesLocations>.Instance);

        public static IncludeUnitsProcesser IncludeUnitsProcesser { get; } = new(UnitListProcessingDiagnostics<RawIncludeUnitsDefinition, IncludeUnitsLocations>.Instance);
        public static ExcludeUnitsProcesser ExcludeUnitsProcesser { get; } = new(UnitListProcessingDiagnostics<RawExcludeUnitsDefinition, ExcludeUnitsLocations>.Instance);

        public static ConvertibleQuantityProcesser ConvertibleQuantityProcesser { get; } = new(ConvertibleQuantityDiagnostics.Instance);
    }
}
