namespace SharpMeasures.Generators.Units.Parsing.Contexts.Validation;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units.Parsing.SharpMeasuresUnit;

internal record class SharpMeasuresUnitValidationContext : SimpleProcessingContext, ISharpMeasuresUnitValidationContext
{
    public UnitProcessingData ProcessingData { get; }
    public IScalarPopulation ScalarPopulation { get; }

    public SharpMeasuresUnitValidationContext(DefinedType type, UnitProcessingData processingData, IScalarPopulation scalarPopulation) : base(type)
    {
        ProcessingData = processingData;
        ScalarPopulation = scalarPopulation;
    }
}
