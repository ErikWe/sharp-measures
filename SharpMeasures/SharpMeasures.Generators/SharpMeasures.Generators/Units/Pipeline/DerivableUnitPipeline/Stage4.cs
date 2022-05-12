namespace SharpMeasures.Generators.Units.Pipeline.DerivableUnitPipeline;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.Extraction;
using SharpMeasures.Generators.Attributes.Parsing.Units;
using SharpMeasures.Generators.Diagnostics.DerivableUnits;
using SharpMeasures.Generators.SourceBuilding;
using SharpMeasures.Generators.Units.Extraction;
using SharpMeasures.Generators.Utility;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

internal static class Stage4
{
    public static IncrementalValuesProvider<DataModel> ExtractDerivableDefinitions(IncrementalGeneratorInitializationContext context,
        IncrementalValuesProvider<Stage3.Result> inputProvider)
    {
        var resultsWithDiagnostics = inputProvider.Select(ExtractDefinitionsAndDiagnostics);

        context.ReportDiagnostics(resultsWithDiagnostics);
        return resultsWithDiagnostics.ExtractResult().Where(HasAnyDefinitions);
    }

    private static ResultWithDiagnostics<DataModel> ExtractDefinitionsAndDiagnostics(Stage3.Result input, CancellationToken token)
    {
        AExtractor<DerivableUnitDefinition> derivable = DerivableUnitExtractor.Extract(input.TypeSymbol);

        DataModel result = new(DefinedType.FromSymbol(input.TypeSymbol), NamedType.FromSymbol(input.QuantitySymbol),
            MinimalLocation.FromLocation(input.Declaration.TypeDeclaration.GetLocation()), derivable.ValidDefinitions, input.Documentation);

        var filteredResultAndDiagnostics = FilterDuplicateDefinitions(result);

        var allDiagnostics = derivable.Diagnostics.Concat(filteredResultAndDiagnostics.Diagnostics);
        return new ResultWithDiagnostics<DataModel>(filteredResultAndDiagnostics.Result, allDiagnostics);
    }

    private static bool HasAnyDefinitions(DataModel result) => result.DefinedDerivations.GetEnumerator().MoveNext();

    private static ResultWithDiagnostics<DataModel> FilterDuplicateDefinitions(DataModel input)
    {
        HashSet<string> tags = new();
        List<DerivableUnitDefinition> filtered = new(input.DefinedDerivations.Count);
        List<Diagnostic> diagnostics = new();

        foreach (DerivableUnitDefinition definition in input.DefinedDerivations)
        {
            string signature = GetSignatureString(definition);

            if (tags.Contains(signature))
            {
                diagnostics.Add(CreateDuplicateDerivationSignatureDiagnostics(input.Unit, definition, signature));
                continue;
            }

            tags.Add(signature);
            filtered.Add(definition);
        }

        return new(input with { DefinedDerivations = filtered }, diagnostics);
    }

    private static string GetSignatureString(DerivableUnitDefinition parameters)
    {
        StringBuilder tag = new();
        IterativeBuilding.AppendEnumerable(tag, signatureComponents(), ", ");
        return tag.ToString();

        IEnumerable<string> signatureComponents()
        {
            foreach (NamedType component in parameters.Signature)
            {
                yield return component.Name;
            }
        }
    }

    private static Diagnostic CreateDuplicateDerivationSignatureDiagnostics(DefinedType unit, DerivableUnitDefinition definition, string signature)
    {
        return DuplicateUnitDerivationSignatureDiagnostics.Create(definition.Locations.Signature, unit.Name, signature);
    }
}
