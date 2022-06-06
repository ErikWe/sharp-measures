namespace SharpMeasures.Generators.Units.Processing;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Attributes.Parsing.Units;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units.Processing.Diagnostics;

internal class GeneratedUnitProcessingContext : IProcessingContext
{
    public DefinedType Type { get; }

    public ScalarPopulation ScalarPopulation { get; }

    public GeneratedUnitProcessingContext(DefinedType type, ScalarPopulation scalarPopulation)
    {
        Type = type;
        ScalarPopulation = scalarPopulation;
    }
}

internal class GeneratedUnitProcesser : IProcesser<GeneratedUnitProcessingContext, GeneratedUnit, ProcessedGeneratedUnit>
{
    public static GeneratedUnitProcesser Instance { get; } = new();

    private GeneratedUnitProcesser() { }

    public IOptionalWithDiagnostics<ProcessedGeneratedUnit> Process(GeneratedUnitProcessingContext context, GeneratedUnit input)
    {
        if (context.ScalarPopulation.TryGetValue(input.Quantity, out ScalarInterface quantity) is false)
        {
            return OptionalWithDiagnostics.Empty<ProcessedGeneratedUnit>(GeneratedScalarDiagnostics.QuantityNotScalar(input));
        }

        if (quantity.Biased)
        {
            return OptionalWithDiagnostics.Empty<ProcessedGeneratedUnit>(GeneratedScalarDiagnostics.QuantityNotUnbiased(input));
        }

        ProcessedGeneratedUnit product = new(quantity, input.SupportsBiasedQuantities);
        return OptionalWithDiagnostics.Result(product);
    }
}
