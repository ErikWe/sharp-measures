namespace SharpMeasures.Generators.Parsing.Vectors;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Attributes.Parsing.ItemLists;
using SharpMeasures.Generators.Attributes.Parsing.Quantities;
using SharpMeasures.Generators.Attributes.Parsing.Vectors;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Providers;
using SharpMeasures.Generators.Parsing.Vectors.Diagnostics;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Vectors;

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading;
using System.Linq;

internal static class VectorParsingStage
{
    public readonly record struct Output(IncrementalValuesProvider<ParsedVector> ParsedVectorProvider,
        IncrementalValueProvider<NamedTypePopulation<VectorInterface>> VectorPopulationProvider);

    public static Output Attach(IncrementalGeneratorInitializationContext context)
    {
        var declarations = MarkedTypeDeclarationCandidateProvider.Construct().Attach<GeneratedVectorAttribute>(context.SyntaxProvider);
        var partialDeclarations = PartialDeclarationProvider.Construct<TypeDeclarationSyntax>().AttachAndReport<GeneratedVectorAttribute>(context, declarations);
        var symbols = DeclarationSymbolProvider.ConstructForValueType(ConstructIntermediateResult).Attach(partialDeclarations, context.CompilationProvider);

        var parsed = symbols.Select(ExtractVectorInformation).ReportDiagnostics(context).Select(FitVectorInformation).ReportDiagnostics(context);
        var population = parsed.Select(ConstructInterface).Collect().Select(CreatePopulation);

        return new(parsed, population);
    }

    private static IOptionalWithDiagnostics<ParsedVector> ExtractVectorInformation(IntermediateResult input, CancellationToken _)
    {
        if (GeneratedUnitParser.Parser.ParseFirstOccurrence(input.TypeSymbol) is not GeneratedVectorDefinition generatedVector)
        {
            return OptionalWithDiagnostics.EmptyWithoutDiagnostics<ParsedVector>();
        }

        ValidatorContext context = new(input.TypeSymbol.AsDefinedType());
        var vectorValidity = Validators.GeneratedVectorValidator.CheckValidity(context, generatedVector);

        if (vectorValidity.IsInvalid)
        {
            return OptionalWithDiagnostics.Empty<ParsedVector>(vectorValidity);
        }

        var definedType = input.TypeSymbol.AsDefinedType();
        var typeLocation = input.Declaration.GetLocation().Minimize();

        var includeUnitsDefinitions = IncludeUnitsParser.Parser.ParseAllOccurrences(input.TypeSymbol);
        var excludeUnitsDefinitions = ExcludeUnitsParser.Parser.ParseAllOccurrences(input.TypeSymbol);

        var dimensionalEquivalenceDefinitions = DimensionalEquivalenceParser.Parser.ParseAllOccurrences(input.TypeSymbol);

        ParsedVector result = new(definedType, typeLocation, generatedVector, includeUnitsDefinitions, excludeUnitsDefinitions, dimensionalEquivalenceDefinitions);

        return OptionalWithDiagnostics.Result(result, vectorValidity.Diagnostics);
    }

    private static IResultWithDiagnostics<ParsedVector> FitVectorInformation(ParsedVector input, CancellationToken _)
    {
        IEnumerable<Diagnostic> allDiagnostics = Array.Empty<Diagnostic>();

        if (input.IncludeUnitsDefinitions.Any() && input.ExcludeUnitsDefinitions.Any())
        {
            allDiagnostics = allDiagnostics.Concat(CreateExcessiveExclusionDiagnostics<IncludeUnitsAttribute, ExcludeUnitsAttribute>(input.ExcludeUnitsDefinitions));
        }

        ParsedVector filteredResult = new(input.VectorType, input.VectorLocation, input.VectorDefinition, input.IncludeUnitsDefinitions, input.ExcludeUnitsDefinitions,
            input.DimensionalEquivalenceDefinitions);

        return ResultWithDiagnostics.Construct(filteredResult, allDiagnostics);
    }

    private static IEnumerable<Diagnostic> CreateExcessiveExclusionDiagnostics<TInclusionAttribute, TExclusionAttribute>(IEnumerable<IItemListDefinition> excessiveAttributes)
    {
        foreach (IItemListDefinition item in excessiveAttributes)
        {
            yield return DiagnosticConstruction.ExcessiveExclusion<TInclusionAttribute, TExclusionAttribute>(item.ParsingData.Locations.Attribute.AsRoslynLocation());
        }
    }

    private static VectorInterface ConstructInterface(ParsedVector vector, CancellationToken _)
    {
        return new(vector.VectorType.AsNamedType(), vector.VectorDefinition.Unit);
    }

    private static NamedTypePopulation<VectorInterface> CreatePopulation(ImmutableArray<VectorInterface> vectors, CancellationToken _)
    {
        return new(vectors, static (vector) => vector.VectorType);
    }

    private readonly record struct ValidatorContext : IValidatorContext
    {
        public DefinedType Type { get; }

        public ValidatorContext(DefinedType type)
        {
            Type = type;
        }
    }

    private static class Validators
    {
        public static GeneratedVectorValidator GeneratedVectorValidator { get; } = new(GeneratedVectorDiagnostics.Instance);
    }

    private readonly record struct IntermediateResult(TypeDeclarationSyntax Declaration, INamedTypeSymbol TypeSymbol);

    private static DeclarationSymbolProvider.DOutputTransform<TypeDeclarationSyntax, IntermediateResult> ConstructIntermediateResult
        => (declaration, symbol) => new IntermediateResult(declaration, symbol);
}
