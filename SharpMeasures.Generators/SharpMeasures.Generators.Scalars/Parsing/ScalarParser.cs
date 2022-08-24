namespace SharpMeasures.Generators.Scalars.Parsing;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Providers;
using SharpMeasures.Generators.Providers.DeclarationFilter;
using SharpMeasures.Generators.Scalars.Parsing.Abstraction;
using SharpMeasures.Generators.Scalars.Parsing.Diagnostics;

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;

public static partial class ScalarParser
{
    public static (IncrementalValueProvider<IScalarPopulation>, IScalarValidator) Attach(IncrementalGeneratorInitializationContext context)
    {
        var scalarBaseSymbols = AttachSymbolProvider<SharpMeasuresScalarAttribute>(context);
        var scalarSpecializationSymbols = AttachSymbolProvider<SpecializedSharpMeasuresScalarAttribute>(context);

        ScalarBaseProcesser scalarBaseProcesser = new();
        ScalarSpecializationProcesser scalarSpecializationProcesser = new();

        var scalarBases = scalarBaseSymbols.Select(scalarBaseProcesser.ParseAndProcess).ReportDiagnostics(context);
        var scalarSpecializations = scalarSpecializationSymbols.Select(scalarSpecializationProcesser.ParseAndProcess).ReportDiagnostics(context);

        var scalarBaseInterfaces = scalarBases.Select(ExtractInterface).Collect();
        var scalarSpecializationInterfaces = scalarSpecializations.Select(ExtractInterface).Collect();

        var populationWithData = scalarBaseInterfaces.Combine(scalarSpecializationInterfaces).Select(CreatePopulation);

        return (populationWithData.Select(ReducePopulation), new ScalarValidator(populationWithData, scalarBases, scalarSpecializations));
    }

    private static IncrementalValuesProvider<(TypeDeclarationSyntax Declaration, INamedTypeSymbol TypeSymbol)> AttachSymbolProvider<TAttribute>(IncrementalGeneratorInitializationContext context)
    {
        var declarations = MarkedTypeDeclarationCandidateProvider.Construct().Attach<TAttribute>(context.SyntaxProvider);
        var filteredDeclarations = FilteredDeclarationProvider.Construct<TypeDeclarationSyntax>(DeclarationFilters).AttachAndReport(context, declarations);

        return DeclarationSymbolProvider.Construct<TypeDeclarationSyntax>().Attach(filteredDeclarations, context.CompilationProvider);
    }

    private static IScalarBaseType ExtractInterface(IScalarBaseType scalarType, CancellationToken _) => scalarType;
    private static IScalarSpecializationType ExtractInterface(IScalarSpecializationType scalarType, CancellationToken _) => scalarType;

    private static IScalarPopulation ReducePopulation(IScalarPopulationWithData scalarPopulation, CancellationToken _) => scalarPopulation;

    private static IScalarPopulationWithData CreatePopulation((ImmutableArray<IScalarBaseType> Bases, ImmutableArray<IScalarSpecializationType> Specializations) scalars, CancellationToken _)
    {
        return ScalarPopulation.Build(scalars.Bases, scalars.Specializations);
    }

    private static IEnumerable<IDeclarationFilter> DeclarationFilters { get; } = new IDeclarationFilter[]
    {
        new PartialDeclarationFilter(ScalarBaseTypeDiagnostics.TypeNotPartial),
        new NonStaticDeclarationFilter(ScalarBaseTypeDiagnostics.TypeStatic)
    };
}
