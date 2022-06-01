namespace SharpMeasures.Generators.Units.Processing;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Attributes.Parsing.Units;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units.Processing.Diagnostics;

internal class GeneratedUnitProcessingContext : IProcessingContext
{
    public DefinedType Type { get; }

    public NamedTypePopulation<ScalarInterface> ScalarPopulation { get; }

    public GeneratedUnitProcessingContext(DefinedType type, NamedTypePopulation<ScalarInterface> scalarPopulation)
    {
        Type = type;
        ScalarPopulation = scalarPopulation;
    }
}

internal class GeneratedUnitProcesser : IProcesser<GeneratedUnitProcessingContext, GeneratedUnitDefinition, ProcessedGeneratedUnit>
{
    public static GeneratedUnitProcesser Instance { get; } = new();

    private GeneratedUnitProcesser() { }

    public IOptionalWithDiagnostics<ProcessedGeneratedUnit> Process(GeneratedUnitProcessingContext context, GeneratedUnitDefinition input)
    {
        if (context.ScalarPopulation.Population.TryGetValue(input.Quantity, out ScalarInterface quantity) is false)
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
