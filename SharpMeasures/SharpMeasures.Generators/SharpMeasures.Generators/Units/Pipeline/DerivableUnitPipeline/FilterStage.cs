namespace SharpMeasures.Generators.Units.Pipeline.DerivableUnitPipeline;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.Units;
using SharpMeasures.Generators.Diagnostics.DerivableUnits;
using SharpMeasures.Generators.SourceBuilding;
using SharpMeasures.Generators.Utility;

using System.Collections.Generic;
using System.Text;
using System.Threading;

internal static class FilterStage
{
    public static IncrementalValuesProvider<DataModel> FilterDuplicates(IncrementalGeneratorInitializationContext context,
        IncrementalValuesProvider<DataModel> inputProvider)
    {
        var withoutDuplicates = inputProvider.Select(FilterDuplicateDefinitions);

        context.ReportDiagnostics(withoutDuplicates);
        return withoutDuplicates.ExtractResult();
    }

    private static ResultWithDiagnostics<DataModel> FilterDuplicateDefinitions(DataModel input, CancellationToken _)
    {
        HashSet<string> tags = new();
        List<CacheableDerivableUnitDefinition> filtered = new(input.DefinedDerivations.Count);
        List<Diagnostic> diagnostics = new();

        foreach (CacheableDerivableUnitDefinition definition in input.DefinedDerivations)
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

    private static string GetSignatureString(CacheableDerivableUnitDefinition parameters)
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

    private static Diagnostic CreateDuplicateDerivationSignatureDiagnostics(DefinedType unit, CacheableDerivableUnitDefinition definition, string signature)
    {
        return DuplicateUnitDerivationSignatureDiagnostics.Create(definition.Locations.Signature, unit.Name, signature);
    }
}
