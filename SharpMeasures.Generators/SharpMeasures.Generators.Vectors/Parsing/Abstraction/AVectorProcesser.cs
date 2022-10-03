namespace SharpMeasures.Generators.Vectors.Parsing.Abstraction;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.QuantityOperation;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.QuantityProcess;
using SharpMeasures.Generators.Vectors.Parsing.ConvertibleVector;
using SharpMeasures.Generators.Vectors.Parsing.VectorConstant;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using SharpMeasures.Generators.Vectors.Parsing.VectorOperation;

internal interface IVectorTypeProcessingDiagnostics
{
    public abstract Diagnostic? ContradictoryUnitInstanceInclusionAndExclusion(IVector vector);
}

internal abstract class AVectorProcesser<TRawType, TRawDefinition, TProductType, TProductDefinition>
    where TRawType : ARawVectorType<TRawDefinition>
    where TProductDefinition : IVector
{
    protected IVectorProcessingDiagnosticsStrategy DiagnosticsStrategy { get; }

    protected AVectorProcesser(IVectorProcessingDiagnosticsStrategy diagnosticsStrategy)
    {
        DiagnosticsStrategy = diagnosticsStrategy;
    }

    public IOptionalWithDiagnostics<TProductType> Process(Optional<TRawType> rawVector, CancellationToken token)
    {
        if (token.IsCancellationRequested || rawVector.HasValue is false)
        {
            return OptionalWithDiagnostics.Empty<TProductType>();
        }

        return Process(rawVector.Value);
    }

    public IOptionalWithDiagnostics<TProductType> Process(TRawType rawVector)
    {
        var vector = ProcessVector(rawVector.Type, rawVector.Definition);

        if (vector.LacksResult)
        {
            return vector.AsEmptyOptional<TProductType>();
        }

        HashSet<(string, NamedType)> reservedOperatorSignatures = new();

        var unit = GetUnit(vector.Result);

        var operations = CommonProcessing.ProcessOperations(rawVector.Type, rawVector.Operations, reservedOperatorSignatures, DiagnosticsStrategy);
        var vectorOperations = CommonProcessing.ProcessVectorOperations(rawVector.Type, rawVector.VectorOperations, reservedOperatorSignatures, DiagnosticsStrategy);
        var processes = CommonProcessing.ProcessProcesses(rawVector.Type, rawVector.Processes, DiagnosticsStrategy);
        var constants = CommonProcessing.ProcessConstants(rawVector.Type, rawVector.Constants, unit, DiagnosticsStrategy);
        var conversions = CommonProcessing.ProcessConversions(rawVector.Type, GetOriginalQuantity(vector.Result), ConversionFromOriginalQuantitySpecified(vector.Result), ConversionToOriginalQuantitySpecified(vector.Result), rawVector.Conversions, DiagnosticsStrategy);

        var includeUnitInstances = CommonProcessing.ProcessIncludeUnitInstances(rawVector.Type, rawVector.UnitInstanceInclusions, DiagnosticsStrategy);
        var excludeUnitInstances = CommonProcessing.ProcessExcludeUnitInstances(rawVector.Type, rawVector.UnitInstanceExclusions, DiagnosticsStrategy);

        var allDiagnostics = vector.Diagnostics.Concat(operations).Concat(vectorOperations).Concat(processes).Concat(constants).Concat(conversions).Concat(includeUnitInstances).Concat(excludeUnitInstances);

        if (includeUnitInstances.HasResult && includeUnitInstances.Result.Count > 0 && excludeUnitInstances.HasResult && excludeUnitInstances.Result.Count > 0)
        {
            if (DiagnosticsStrategy.VectorTypeDiagnostics.ContradictoryUnitInstanceInclusionAndExclusion(vector.Result) is Diagnostic contradictoryDiagnostics)
            {
                allDiagnostics = allDiagnostics.Concat(new[] { contradictoryDiagnostics });
            }

            excludeUnitInstances = ResultWithDiagnostics.Construct(Array.Empty<ExcludeUnitsDefinition>() as IReadOnlyList<ExcludeUnitsDefinition>);
        }

        TProductType product = ProduceResult(rawVector.Type, vector.Result, operations.Result, vectorOperations.Result, processes.Result, constants.Result, conversions.Result, includeUnitInstances.Result, excludeUnitInstances.Result);

        return OptionalWithDiagnostics.Result(product, allDiagnostics);
    }

    protected abstract TProductType ProduceResult(DefinedType type, TProductDefinition definition, IReadOnlyList<QuantityOperationDefinition> operations, IReadOnlyList<VectorOperationDefinition> vectorOperations, IReadOnlyList<QuantityProcessDefinition> processes, IReadOnlyList<VectorConstantDefinition> constants, IReadOnlyList<ConvertibleVectorDefinition> conversions, IReadOnlyList<IncludeUnitsDefinition> unitInstanceInclusions, IReadOnlyList<ExcludeUnitsDefinition> unitInstanceExclusions);

    protected abstract IOptionalWithDiagnostics<TProductDefinition> ProcessVector(DefinedType type, TRawDefinition rawDefinition);

    protected abstract NamedType? GetUnit(TProductDefinition vector);
    protected abstract NamedType? GetOriginalQuantity(TProductDefinition vector);
    protected abstract bool ConversionFromOriginalQuantitySpecified(TProductDefinition vector);
    protected abstract bool ConversionToOriginalQuantitySpecified(TProductDefinition vector);
}
