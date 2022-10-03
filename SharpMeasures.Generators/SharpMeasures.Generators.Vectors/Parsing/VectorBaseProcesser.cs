namespace SharpMeasures.Generators.Vectors.Parsing;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.QuantityOperation;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.QuantityProcess;
using SharpMeasures.Generators.Vectors.Parsing.Abstraction;
using SharpMeasures.Generators.Vectors.Parsing.ConvertibleVector;
using SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVector;
using SharpMeasures.Generators.Vectors.Parsing.VectorConstant;

using System.Collections.Generic;
using SharpMeasures.Generators.Vectors.Parsing.VectorOperation;

internal sealed class VectorBaseProcesser : AVectorProcesser<RawVectorBaseType, RawSharpMeasuresVectorDefinition, VectorBaseType, SharpMeasuresVectorDefinition>
{
    public VectorBaseProcesser(IVectorProcessingDiagnosticsStrategy diagnosticsStrategy) : base(diagnosticsStrategy) { }

    protected override VectorBaseType ProduceResult(DefinedType type, SharpMeasuresVectorDefinition definition, IReadOnlyList<QuantityOperationDefinition> operations, IReadOnlyList<VectorOperationDefinition> vectorOperations, IReadOnlyList<QuantityProcessDefinition> processes,
        IReadOnlyList<VectorConstantDefinition> constants, IReadOnlyList<ConvertibleVectorDefinition> conversions, IReadOnlyList<IncludeUnitsDefinition> unitInstanceInclusions, IReadOnlyList<ExcludeUnitsDefinition> unitInstanceExclusions)
    {
        return new(type, definition, operations, vectorOperations, processes, constants, conversions, unitInstanceInclusions, unitInstanceExclusions);
    }

    protected override NamedType? GetUnit(SharpMeasuresVectorDefinition vector) => vector.Unit;
    protected override NamedType? GetOriginalQuantity(SharpMeasuresVectorDefinition vector) => null;
    protected override bool ConversionFromOriginalQuantitySpecified(SharpMeasuresVectorDefinition vector) => false;
    protected override bool ConversionToOriginalQuantitySpecified(SharpMeasuresVectorDefinition vector) => false;

    protected override IOptionalWithDiagnostics<SharpMeasuresVectorDefinition> ProcessVector(DefinedType type, RawSharpMeasuresVectorDefinition rawDefinition)
    {
        var processingContext = new SimpleProcessingContext(type);

        return ProcessingFilter.Create(SharpMeasuresVectorProcesser).Filter(processingContext, rawDefinition);
    }

    private SharpMeasuresVectorProcesser SharpMeasuresVectorProcesser => new(DiagnosticsStrategy.SharpMeasuresVectorDiagnostics);
}
