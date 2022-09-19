namespace SharpMeasures.Generators.Units.Parsing.Contexts.Validation;

using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units.Parsing.SharpMeasuresUnit;

internal sealed record class SharpMeasuresUnitValidationContext : ISharpMeasuresUnitValidationContext
{
    public DefinedType Type { get; }

    public UnitProcessingData ProcessingData { get; }
    public IScalarPopulation ScalarPopulation { get; }

    public SharpMeasuresUnitValidationContext(DefinedType type, UnitProcessingData processingData, IScalarPopulation scalarPopulation)
    {
        Type = type;

        ProcessingData = processingData;
        ScalarPopulation = scalarPopulation;
    }
}
