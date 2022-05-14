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

internal static class DefinitionsStage
{
    public static IncrementalValuesProvider<DataModel> ExtractDefinitions(IncrementalGeneratorInitializationContext context,
        IncrementalValuesProvider<DocumentationStage.Result> inputProvider)
    {
        var resultsWithDiagnostics = inputProvider.Select(ExtractDefinitionsAndDiagnostics);

        context.ReportDiagnostics(resultsWithDiagnostics);
        return resultsWithDiagnostics.ExtractResult();
    }

    private static ResultWithDiagnostics<DataModel> ExtractDefinitionsAndDiagnostics(DocumentationStage.Result input, CancellationToken token)
    {
        AExtractor<UnitAliasDefinition> unitAliases = UnitAliasExtractor.Extract(input.TypeSymbol);
        AExtractor<DerivedUnitDefinition> derivedUnits = DerivedUnitExtractor.Extract(input.TypeSymbol);
        AExtractor<FixedUnitDefinition> fixedUnits = FixedUnitExtractor.Extract(input.TypeSymbol);
        AExtractor<OffsetUnitDefinition> offsetUnits = OffsetUnitExtractor.Extract(input.TypeSymbol);
        AExtractor<PrefixedUnitDefinition> prefixedUnits = PrefixedUnitExtractor.Extract(input.TypeSymbol);
        AExtractor<ScaledUnitDefinition> scaledUnits = ScaledUnitExtractor.Extract(input.TypeSymbol);
        
        DataModel result = new(DefinedType.FromSymbol(input.TypeSymbol), NamedType.FromSymbol(input.Definition.Quantity!), input.Definition.AllowBias,
            input.Documentation, unitAliases.ValidDefinitions, derivedUnits.ValidDefinitions, fixedUnits.ValidDefinitions, offsetUnits.ValidDefinitions,
            prefixedUnits.ValidDefinitions, scaledUnits.ValidDefinitions);

        var filteredResultAndDiagnostics = Filterer.Filter(result);

        IEnumerable<Diagnostic> allDiagnostics = unitAliases.Diagnostics.Concat(derivedUnits.Diagnostics).Concat(fixedUnits.Diagnostics)
            .Concat(offsetUnits.Diagnostics).Concat(prefixedUnits.Diagnostics).Concat(scaledUnits.Diagnostics).Concat(filteredResultAndDiagnostics.Diagnostics);

        return new ResultWithDiagnostics<DataModel>(filteredResultAndDiagnostics.Result, allDiagnostics);
    }

    private class Filterer
    {
        public static ResultWithDiagnostics<DataModel> Filter(DataModel input)
        {
            Filterer filterer = new(input);
            filterer.Filter();

            DataModel filtered = input with
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

        private IReadOnlyList<DerivedUnitDefinition> DerivedUnits { get; }
        private IReadOnlyList<FixedUnitDefinition> FixedUnits { get; }
        private IReadOnlyList<UnitAliasDefinition> UnitAliases { get; }
        private IReadOnlyList<OffsetUnitDefinition> OffsetUnits { get; }
        private IReadOnlyList<PrefixedUnitDefinition> PrefixedUnits { get; }
        private IReadOnlyList<ScaledUnitDefinition> ScaledUnits { get; }

        private List<DerivedUnitDefinition> FilteredDerivedUnits { get; } = new();
        private List<FixedUnitDefinition> FilteredFixedUnits { get; } = new();
        private List<UnitAliasDefinition> FilteredUnitAliases { get; } = new();
        private List<OffsetUnitDefinition> FilteredOffsetUnits { get; } = new();
        private List<PrefixedUnitDefinition> FilteredPrefixedUnits { get; } = new();
        private List<ScaledUnitDefinition> FilteredScaledUnits { get; } = new();

        private HashSet<string> DefinedNames { get; } = new();
        private List<Diagnostic> Diagnostics { get; } = new();

        private Filterer(DataModel input)
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
