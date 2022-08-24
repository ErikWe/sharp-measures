﻿namespace SharpMeasures.Generators.Vectors.Parsing.VectorConstant;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.QuantityConstant;

using System.Linq;

internal class VectorConstantProcesser : AQuantityConstantProcesser<IQuantityConstantProcessingContext, RawVectorConstantDefinition, VectorConstantLocations, VectorConstantDefinition>
{
    public VectorConstantProcesser(IQuantityConstantProcessingDiagnostics<RawVectorConstantDefinition, VectorConstantLocations> diagnostics) : base(diagnostics) { }

    public override IOptionalWithDiagnostics<VectorConstantDefinition> Process(IQuantityConstantProcessingContext context, RawVectorConstantDefinition definition)
    {
        var validity = VerifyRequiredPropertiesSet(definition)
            .Validate(() => Validate(context, definition));

        if (validity.IsInvalid)
        {
            return validity.AsEmptyOptional<VectorConstantDefinition>();
        }

        var processedMultiplesPropertyData = ProcessMultiplesPropertyData(context, definition);

        var product = ProduceResult(definition, processedMultiplesPropertyData.Result.Generate, processedMultiplesPropertyData.Result.Name);

        var allDiagnostics = validity.Concat(processedMultiplesPropertyData);

        return OptionalWithDiagnostics.Result(product, allDiagnostics);
    }

    private static VectorConstantDefinition ProduceResult(RawVectorConstantDefinition definition, bool generateMultiples, string? multiplesName)
    {
        return new(definition.Name!, definition.Unit!, definition.Value, generateMultiples, multiplesName, definition.Locations);
    }
}