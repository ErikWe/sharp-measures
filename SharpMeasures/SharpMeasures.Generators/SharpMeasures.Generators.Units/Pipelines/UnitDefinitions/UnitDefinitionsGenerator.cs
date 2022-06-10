namespace SharpMeasures.Generators.Units.Pipelines.UnitDefinitions;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Units.Diagnostics;
using SharpMeasures.Generators.Units.Refinement.DerivedUnit;

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
        DerivedUnitValidator validator = new(DerivedUnitDiagnostics.Instance);

        var filteredDerivedUnits = ValidityFilter.Create(validator).Filter(context, input.Unit.DerivedUnits);

        DataModel model = new(input.Unit.UnitType, input.Quantity.ScalarType.AsNamedType(), input.Unit.UnitDefinition.SupportsBiasedQuantities, input.Documentation,
            input.Unit.UnitAliases, filteredDerivedUnits.Result, input.Unit.FixedUnits, input.Unit.OffsetUnits,
            input.Unit.PrefixedUnits, input.Unit.ScaledUnits);

        return ResultWithDiagnostics.Construct(model, filteredDerivedUnits.Diagnostics);
    }

    private readonly record struct DerivedUnitValidatorContext : IDerivedUnitValidatorContext
    {
        public DefinedType Type { get; }

        public UnitPopulation UnitPopulation { get; }

        public DerivedUnitValidatorContext(DefinedType type, UnitPopulation unitPopulation)
        {
            Type = type;

            UnitPopulation = unitPopulation;
        }
    }
}
