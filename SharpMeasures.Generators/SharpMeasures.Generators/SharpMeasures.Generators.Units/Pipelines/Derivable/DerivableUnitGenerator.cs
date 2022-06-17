namespace SharpMeasures.Generators.Units.Pipelines.Derivable;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Units.Diagnostics;
using SharpMeasures.Generators.Units.Refinement.DerivableUnit;

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
        DerivableUnitRefinementContext context = new(input.UnitData.UnitType, input.UnitPopulation);
        DerivableUnitRefiner refiner = new(DerivableUnitDiagnostics.Instance);

        var processed = ProcessingFilter.Create(refiner).Filter(context, input.UnitData.UnitDerivations);

        DataModel model = new(input.UnitData.UnitType, input.UnitDefinition.Quantity.ScalarType.AsNamedType(), input.Documentation, processed.Result);
        return ResultWithDiagnostics.Construct(model, processed.Diagnostics);
    }

    private readonly record struct DerivableUnitRefinementContext : IDerivableUnitRefinementContext
    {
        public DefinedType Type { get; }

        public UnitPopulation UnitPopulation { get; }

        public DerivableUnitRefinementContext(DefinedType type, UnitPopulation unitPopulation)
        {
            Type = type;

            UnitPopulation = unitPopulation;
        }
    }
}
