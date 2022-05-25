namespace SharpMeasures.Generators.Units.Pipeline.Derivable;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Units.Validation;

using System.Threading;
using System.Linq;

internal static class DerivableUnitGenerator
{
    public static void Initialize(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<Units.DataModel> inputProvider)
    {
        var filteredAndReduced = inputProvider.Select(FilterAndReduceToDataModel).ReportDiagnostics(context);

        context.RegisterSourceOutput(filteredAndReduced, Execution.Execute);
    }

    private static IResultWithDiagnostics<DataModel> FilterAndReduceToDataModel(Units.DataModel input, CancellationToken _)
    {
        DerivableUnitValidatorContext context = new(input.Unit.UnitType, input.UnitPopulation);

        var validity = ValidityFilter.Create(ExternalDerivableUnitValidator.Instance).Filter(context, input.Unit.DerivableUnitDefinitions);

        ExtendedDerivableUnitDefinition[] extendedDefinitions = new ExtendedDerivableUnitDefinition[validity.Result.Count];

        for (int i = 0; i < extendedDefinitions.Length; i++)
        {
            NamedType[] quantities = new NamedType[validity.Result[i].Signature.Count];

            for (int j = 0; j < quantities.Length; j++)
            {
                quantities[j] = input.UnitPopulation.Population[validity.Result[i].Signature[j]].QuantityType;
            }

            extendedDefinitions[i] = new(validity.Result[i], quantities);
        }

        DataModel model = new(input.Unit.UnitType, input.Unit.UnitDefinition.Quantity, input.Documentation, extendedDefinitions);

        return ResultWithDiagnostics.Construct(model, validity.Diagnostics);
    }
}
