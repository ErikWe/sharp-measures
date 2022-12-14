namespace SharpMeasures.Generators.Units.ForeignUnitParsing;

using SharpMeasures.Generators.Units.Parsing;

using System.Collections.Generic;
using System.Threading;

public sealed class ForeignUnitProcesser
{
    public static (ForeignUnitProcessingResult ProcessingResult, IUnitPopulation Population) ProcessAndExtend(ForeignUnitParsingResult parsingResult, IUnitPopulation unextendedUnitPopulation, CancellationToken token)
    {
        var processer = new ForeignUnitProcesser();

        if (token.IsCancellationRequested is false)
        {
            processer.Process(parsingResult);
        }

        return (processer.Finalize(), processer.Extend(unextendedUnitPopulation));
    }

    private static UnitProcesser Processer { get; } = new(UnitProcessingDiagnosticsStrategies.EmptyDiagnostics);

    private List<UnitType> Units { get; } = new();

    private ForeignUnitProcesser() { }

    private void Process(ForeignUnitParsingResult parsingResult)
    {
        foreach (var rawUnit in parsingResult.Units)
        {
            var unit = Processer.Process(rawUnit);

            if (unit.HasResult)
            {
                Units.Add(unit.Result);
            }
        }
    }

    private ForeignUnitProcessingResult Finalize() => new(Units);
    private IUnitPopulation Extend(IUnitPopulation unitPopulation) => ExtendedUnitPopulation.Build(unitPopulation, Finalize());
}
