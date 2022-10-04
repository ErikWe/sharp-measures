namespace SharpMeasures.Generators.Vectors.Parsing;

using SharpMeasures.Generators;
using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.QuantityOperation;
using SharpMeasures.Generators.Quantities.Parsing.QuantityProcess;
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Vectors.Parsing.Abstraction;
using SharpMeasures.Generators.Vectors.Parsing.Contexts.Validation;
using SharpMeasures.Generators.Vectors.Parsing.ConvertibleVector;
using SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVector;
using SharpMeasures.Generators.Vectors.Parsing.VectorConstant;
using SharpMeasures.Generators.Vectors.Parsing.VectorOperation;

using System.Collections.Generic;

internal sealed class VectorBaseValidator : AVectorValidatorr<VectorBaseType, SharpMeasuresVectorDefinition>
{
    public VectorBaseValidator(IVectorValidationDiagnosticsStrategy diagnosticsStrategy) : base(diagnosticsStrategy) { }

    protected override VectorBaseType ProduceResult(DefinedType type, SharpMeasuresVectorDefinition definition, IReadOnlyList<QuantityOperationDefinition> operations, IReadOnlyList<VectorOperationDefinition> vectorOperations, IReadOnlyList<QuantityProcessDefinition> processes, IReadOnlyList<VectorConstantDefinition> constants, IReadOnlyList<ConvertibleVectorDefinition> conversion, IReadOnlyList<IncludeUnitsDefinition> unitinstanceInclusions, IReadOnlyList<ExcludeUnitsDefinition> unitInstanceExclusions)
    {
        return new(type, definition, operations, vectorOperations, processes, constants, conversion, unitinstanceInclusions, unitInstanceExclusions);
    }

    protected override IOptionalWithDiagnostics<SharpMeasuresVectorDefinition> ValidateVector(VectorBaseType vectorType, VectorProcessingData processingData, IUnitPopulation unitPopulation, IScalarPopulation scalarPopulation, IVectorPopulation vectorPopulation)
    {
        var validationContext = new SharpMeasuresVectorValidationContext(vectorType.Type, processingData, unitPopulation, scalarPopulation, vectorPopulation);

        return ProcessingFilter.Create(SharpMeasuresVectorValidator).Filter(validationContext, vectorType.Definition);
    }

    private SharpMeasuresVectorValidator SharpMeasuresVectorValidator => new(DiagnosticsStrategy.SharpMeasuresVectorDiagnostics);
}
