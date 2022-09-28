namespace SharpMeasures.Generators.Vectors.Parsing;

using SharpMeasures.Generators;
using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.ProcessedQuantity;
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Vectors.Parsing.Abstraction;
using SharpMeasures.Generators.Vectors.Parsing.Contexts.Validation;
using SharpMeasures.Generators.Vectors.Parsing.ConvertibleVector;
using SharpMeasures.Generators.Vectors.Parsing.SpecializedSharpMeasuresVector;
using SharpMeasures.Generators.Vectors.Parsing.VectorConstant;

using System.Collections.Generic;

internal sealed class VectorSpecializationValidator : AVectorValidatorr<VectorSpecializationType, SpecializedSharpMeasuresVectorDefinition>
{
    public VectorSpecializationValidator(IVectorValidationDiagnosticsStrategy diagnosticsStrategy) : base(diagnosticsStrategy) { }

    protected override VectorSpecializationType ProduceResult(DefinedType type, SpecializedSharpMeasuresVectorDefinition definition, IReadOnlyList<DerivedQuantityDefinition> derivations, IReadOnlyList<ProcessedQuantityDefinition> processes, IReadOnlyList<VectorConstantDefinition> constants, IReadOnlyList<ConvertibleVectorDefinition> conversion, IReadOnlyList<IncludeUnitsDefinition> unitinstanceInclusions, IReadOnlyList<ExcludeUnitsDefinition> unitInstanceExclusions)
    {
        return new(type, definition, derivations, processes, constants, conversion, unitinstanceInclusions, unitInstanceExclusions);
    }

    protected override IOptionalWithDiagnostics<SpecializedSharpMeasuresVectorDefinition> ValidateVector(VectorSpecializationType vectorType, VectorProcessingData processingData, IUnitPopulation unitPopulation, IScalarPopulation scalarPopulation, IVectorPopulation vectorPopulation)
    {
        var validationContext = new SpecializedSharpMeasuresVectorValidationContext(vectorType.Type, processingData, unitPopulation, scalarPopulation, vectorPopulation);

        return ProcessingFilter.Create(SpecializedSharpMeasuresVectorValidator).Filter(validationContext, vectorType.Definition);
    }

    private SpecializedSharpMeasuresVectorValidator SpecializedSharpMeasuresVectorValidator => new(DiagnosticsStrategy.SpecializedSharpMeasuresVectorDiagnostics);
}
