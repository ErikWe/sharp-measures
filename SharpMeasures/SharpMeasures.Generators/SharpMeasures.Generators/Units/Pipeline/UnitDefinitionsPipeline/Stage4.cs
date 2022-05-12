namespace SharpMeasures.Generators.Units.Pipeline.UnitDefinitionsPipeline;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Attributes.Parsing.Extraction;
using SharpMeasures.Generators.Attributes.Parsing.Units;
using SharpMeasures.Generators.Documentation;
using SharpMeasures.Generators.Units.Extraction;
using SharpMeasures.Generators.Utility;

using System.Collections.Generic;
using System.Linq;
using System.Threading;

internal static class Stage4
{
    public readonly record struct Result(SharpMeasuresGenerator.DeclarationData Declaration, DefinedType TypeDefinition, NamedType Quantity, bool Biased,
        DocumentationFile Documentation, IReadOnlyList<UnitAliasParameters> UnitAliases, IReadOnlyList<DerivedUnitParameters> DerivedUnits,
        IReadOnlyList<FixedUnitParameters> FixedUnits, IReadOnlyList<OffsetUnitParameters> OffsetUnits,
        IReadOnlyList<PrefixedUnitParameters> PrefixedUnits, IReadOnlyList<ScaledUnitParameters> ScaledUnits);

    public static IncrementalValuesProvider<Result> ExtractDefinitions(IncrementalGeneratorInitializationContext context,
        IncrementalValuesProvider<Stage3.Result> inputProvider)
    {
        var resultsWithDiagnostics = inputProvider.Select(ExtractDefinitionsAndDiagnostics);

        context.ReportDiagnostics(resultsWithDiagnostics);
        return resultsWithDiagnostics.ExtractResult();
    }

    private static ResultWithDiagnostics<Result> ExtractDefinitionsAndDiagnostics(Stage3.Result input, CancellationToken token)
    {
        AExtractor<UnitAliasParameters> unitAliases = UnitAliasExtractor.Extract(input.TypeSymbol);
        AExtractor<DerivedUnitParameters> derivedUnits = DerivedUnitExtractor.Extract(input.TypeSymbol);
        AExtractor<FixedUnitParameters> fixedUnits = FixedUnitExtractor.Extract(input.TypeSymbol);
        AExtractor<OffsetUnitParameters> offsetUnits = OffsetUnitExtractor.Extract(input.TypeSymbol);
        AExtractor<PrefixedUnitParameters> prefixedUnits = PrefixedUnitExtractor.Extract(input.TypeSymbol);
        AExtractor<ScaledUnitParameters> scaledUnits = ScaledUnitExtractor.Extract(input.TypeSymbol);
        
        Result result = new(input.Declaration, DefinedType.FromSymbol(input.TypeSymbol), NamedType.FromSymbol(input.QuantitySymbol), input.Biased,
            input.Documentation, unitAliases.ValidDefinitions, derivedUnits.ValidDefinitions, fixedUnits.ValidDefinitions, offsetUnits.ValidDefinitions,
            prefixedUnits.ValidDefinitions, scaledUnits.ValidDefinitions);

        var filteredResultAndDiagnostics = Filterer.Filter(result);

        IEnumerable<Diagnostic> allDiagnostics = unitAliases.Diagnostics.Concat(derivedUnits.Diagnostics).Concat(fixedUnits.Diagnostics)
            .Concat(offsetUnits.Diagnostics).Concat(prefixedUnits.Diagnostics).Concat(scaledUnits.Diagnostics).Concat(filteredResultAndDiagnostics.Diagnostics);

        return new ResultWithDiagnostics<Result>(filteredResultAndDiagnostics.Result, allDiagnostics);
    }

    private class Filterer
    {
        public static ResultWithDiagnostics<Result> Filter(Result input)
        {
            Filterer filterer = new(input);
            filterer.Filter();

            Result filtered = input with
            {
                DerivedUnits = filterer.DerivedUnits,
                FixedUnits = filterer.FixedUnits,
                UnitAliases = filterer.UnitAliases,
                OffsetUnits = filterer.OffsetUnits,
                PrefixedUnits = filterer.PrefixedUnits,
                ScaledUnits = filterer.ScaledUnits
            };

            return new(filtered, filterer.Diagnostics);
        }

        private TypeDeclarationSyntax Declaration { get; }

        private IReadOnlyList<DerivedUnitParameters> DerivedUnits { get; }
        private IReadOnlyList<FixedUnitParameters> FixedUnits { get; }
        private IReadOnlyList<UnitAliasParameters> UnitAliases { get; }
        private IReadOnlyList<OffsetUnitParameters> OffsetUnits { get; }
        private IReadOnlyList<PrefixedUnitParameters> PrefixedUnits { get; }
        private IReadOnlyList<ScaledUnitParameters> ScaledUnits { get; }

        private List<DerivedUnitParameters> FilteredDerivedUnits { get; } = new();
        private List<FixedUnitParameters> FilteredFixedUnits { get; } = new();
        private List<UnitAliasParameters> FilteredUnitAliases { get; } = new();
        private List<OffsetUnitParameters> FilteredOffsetUnits { get; } = new();
        private List<PrefixedUnitParameters> FilteredPrefixedUnits { get; } = new();
        private List<ScaledUnitParameters> FilteredScaledUnits { get; } = new();

        private HashSet<string> DefinedNames { get; } = new();
        private List<Diagnostic> Diagnostics { get; } = new();

        private Filterer(Result input)
        {
            Declaration = input.Declaration.TypeDeclaration;

            DerivedUnits = input.DerivedUnits;
            FixedUnits = input.FixedUnits;
            UnitAliases = input.UnitAliases;
            OffsetUnits = input.OffsetUnits;
            PrefixedUnits = input.PrefixedUnits;
            ScaledUnits = input.ScaledUnits;
        }

        private void Filter()
        {
            // TODO: Filter, and produce diagnostics 
        }
    }
}
