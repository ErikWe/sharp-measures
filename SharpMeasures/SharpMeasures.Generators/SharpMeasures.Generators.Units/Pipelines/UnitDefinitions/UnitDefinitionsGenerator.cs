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
        var context = new DerivedUnitValidatorContext(input.UnitData.UnitType, input.UnitPopulation);
        DerivedUnitValidator validator = new(DerivedUnitDiagnostics.Instance);

        var filteredDerivedUnits = ValidityFilter.Create(validator).Filter(context, input.UnitData.DerivedUnits);

        DataModel model = new(input.UnitData.UnitType, input.UnitDefinition.Quantity.ScalarType.AsNamedType(), input.UnitDefinition.BiasTerm,
            input.Documentation, input.UnitData.UnitAliases, filteredDerivedUnits.Result, input.UnitData.FixedUnits, input.UnitData.OffsetUnits,
            input.UnitData.PrefixedUnits, input.UnitData.ScaledUnits);

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
