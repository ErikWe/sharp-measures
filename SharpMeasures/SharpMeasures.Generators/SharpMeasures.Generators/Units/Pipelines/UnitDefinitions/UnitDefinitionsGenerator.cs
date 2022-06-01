﻿namespace SharpMeasures.Generators.Units.Pipelines.UnitDefinitions;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Units.Validation;

using System.Threading;

internal static class UnitDefinitionsGenerator
{
    public static void Initialize(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<Units.DataModel> inputProvider)
    {
        var filteredAndReduced = inputProvider.Select(FilterAndReduceToDataModel).ReportDiagnostics(context);

        context.RegisterSourceOutput(filteredAndReduced, Execution.Execute);
    }

    private static IResultWithDiagnostics<DataModel> FilterAndReduceToDataModel(Units.DataModel input, CancellationToken _)
    {
        var context = new DerivedUnitValidatorContext(input.Unit.UnitType, input.UnitPopulation);

        var filteredDerivedUnits = ValidityFilter.Create(DerivedUnitValidator.Instance).Filter(context, input.Unit.DerivedUnitDefinitions);

        DataModel model = new(input.Unit.UnitType, input.Quantity.ScalarType.AsNamedType(), input.Unit.UnitDefinition.SupportsBiasedQuantities, input.Documentation,
            input.Unit.UnitAliasDefinitions, filteredDerivedUnits.Result, input.Unit.FixedUnitDefinitions, input.Unit.OffsetUnitDefinitions,
            input.Unit.PrefixedUnitDefinitions, input.Unit.ScaledUnitDefinitions);

        return ResultWithDiagnostics.Construct(model, filteredDerivedUnits.Diagnostics);
    }
}
