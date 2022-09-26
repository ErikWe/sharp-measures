namespace SharpMeasures.Generators.Scalars.Parsing.ScalarConstant;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.QuantityConstant;

using System.Linq;

internal interface IScalarConstantProcessingDiagnostics : IQuantityConstantProcessingDiagnostics<RawScalarConstantDefinition, ScalarConstantLocations> { }

internal sealed class ScalarConstantProcesser : AQuantityConstantProcesser<IQuantityConstantProcessingContext, RawScalarConstantDefinition, ScalarConstantLocations, ScalarConstantDefinition>
{
    public ScalarConstantProcesser(IScalarConstantProcessingDiagnostics diagnostics) : base(diagnostics) { }

    public override IOptionalWithDiagnostics<ScalarConstantDefinition> Process(IQuantityConstantProcessingContext context, RawScalarConstantDefinition definition)
    {
        var validity = VerifyRequiredPropertiesSet(definition)
            .Validate(() => Validate(context, definition));

        if (validity.IsInvalid)
        {
            return validity.AsEmptyOptional<ScalarConstantDefinition>();
        }

        var processedMultiplesPropertyData = ProcessMultiplesPropertyData(context, definition);

        var product = ProduceResult(definition, processedMultiplesPropertyData.Result.Generate, processedMultiplesPropertyData.Result.Name);

        var allDiagnostics = validity.Concat(processedMultiplesPropertyData);

        return OptionalWithDiagnostics.Result(product, allDiagnostics);
    }

    private static ScalarConstantDefinition ProduceResult(RawScalarConstantDefinition definition, bool generateMultiples, string? multiplesName) => new(definition.Name!, definition.UnitInstanceName!, definition.Value, generateMultiples, multiplesName, definition.Locations);
}
