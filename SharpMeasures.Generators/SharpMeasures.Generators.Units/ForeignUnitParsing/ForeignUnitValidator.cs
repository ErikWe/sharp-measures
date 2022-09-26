namespace SharpMeasures.Generators.Units.ForeignUnitParsing;

using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units.Parsing;

using System.Collections.Generic;
using System.Threading;

public sealed class ForeignUnitValidator
{
    public static IUnitPopulation ValidateAndExtend(ForeignUnitProcessingResult processingResult, IUnitPopulation unitPopulation, IScalarPopulation scalarPopulation, IUnitPopulation unextendedUnitPopulation, CancellationToken token)
    {
        ForeignUnitValidator validator = new();

        if (token.IsCancellationRequested is false)
        {
            validator.Validate(processingResult, unitPopulation, scalarPopulation);
        }

        return validator.Extend(unextendedUnitPopulation);
    }

    private static UnitValidator Validator { get; } = new(UnitValidationDiagnosticsStrategies.EmptyDiagnostics);

    private List<UnitType> Units { get; } = new();

    private ForeignUnitValidator() { }

    private void Validate(ForeignUnitProcessingResult processingResult, IUnitPopulation unitPopulation, IScalarPopulation scalarPopulation)
    {
        foreach (var processedUnit in processingResult.Units)
        {
            var unit = Validator.Validate(processedUnit, UnitProcessingData.Empty, unitPopulation, scalarPopulation);

            if (unit.HasResult)
            {
                Units.Add(unit.Result);
            }
        }
    }

    private ForeignUnitProcessingResult Finalize() => new(Units);
    private IUnitPopulation Extend(IUnitPopulation unitPopulation) => ExtendedUnitPopulation.Build(unitPopulation, Finalize());
}
