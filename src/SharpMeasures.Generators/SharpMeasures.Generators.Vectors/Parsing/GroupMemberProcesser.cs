namespace SharpMeasures.Generators.Vectors.Parsing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.ConvertibleQuantity;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Vectors.Parsing.Abstraction;
using SharpMeasures.Generators.Vectors.Parsing.Contexts.Processing;
using SharpMeasures.Generators.Vectors.Parsing.ConvertibleVector;
using SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVectorGroupMember;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

internal interface IGroupMemberTypeProcessingDiagnostics
{
    public abstract Diagnostic? ContradictoryUnitInstanceInclusionAndExclusion(IVectorGroupMember member);
}

internal sealed class GroupMemberProcesser
{
    private IVectorProcessingDiagnosticsStrategy DiagnosticsStrategy { get; }

    public GroupMemberProcesser(IVectorProcessingDiagnosticsStrategy diagnosticsStrategy)
    {
        DiagnosticsStrategy = diagnosticsStrategy;
    }

    public IOptionalWithDiagnostics<GroupMemberType> Process(Optional<RawGroupMemberType> rawMember, CancellationToken token)
    {
        if (token.IsCancellationRequested || rawMember.HasValue is false)
        {
            return OptionalWithDiagnostics.Empty<GroupMemberType>();
        }

        return Process(rawMember.Value);
    }

    public IOptionalWithDiagnostics<GroupMemberType> Process(RawGroupMemberType rawMember)
    {
        var member = ProcessMember(rawMember.Type, rawMember.Definition);

        if (member.LacksResult)
        {
            return member.AsEmptyOptional<GroupMemberType>();
        }

        HashSet<(string, NamedType)> reservedOperatorSignatures = new();

        var operations = CommonProcessing.ProcessOperations(rawMember.Type, rawMember.Operations, reservedOperatorSignatures, DiagnosticsStrategy);
        var vectorOperations = CommonProcessing.ProcessVectorOperations(rawMember.Type, rawMember.VectorOperations, reservedOperatorSignatures, DiagnosticsStrategy);
        var processes = CommonProcessing.ProcessProcesses(rawMember.Type, rawMember.Processes, DiagnosticsStrategy);
        var constants = CommonProcessing.ProcessConstants(rawMember.Type, rawMember.Constants, null, DiagnosticsStrategy);
        var conversions = ProcessConversions(rawMember.Type, rawMember.Conversions, member.Result.VectorGroup);

        var includeUnitInstances = CommonProcessing.ProcessIncludeUnitInstances(rawMember.Type, rawMember.UnitInstanceInclusions, DiagnosticsStrategy);
        var excludeUnitInstances = CommonProcessing.ProcessExcludeUnitInstances(rawMember.Type, rawMember.UnitInstanceExclusions, DiagnosticsStrategy);

        var allDiagnostics = member.Diagnostics.Concat(operations).Concat(vectorOperations).Concat(processes).Concat(constants).Concat(conversions).Concat(includeUnitInstances).Concat(excludeUnitInstances);

        if (includeUnitInstances.HasResult && includeUnitInstances.Result.Count > 0 && excludeUnitInstances.HasResult && excludeUnitInstances.Result.Count > 0)
        {
            if (DiagnosticsStrategy.GroupMemberTypeDiagnostics.ContradictoryUnitInstanceInclusionAndExclusion(member.Result) is Diagnostic contradictoryDiagnostics)
            {
                allDiagnostics = allDiagnostics.Concat(new[] { contradictoryDiagnostics });
            }

            excludeUnitInstances = ResultWithDiagnostics.Construct(Array.Empty<ExcludeUnitsDefinition>() as IReadOnlyList<ExcludeUnitsDefinition>);
        }

        GroupMemberType product = new(rawMember.Type, member.Result, operations.Result, vectorOperations.Result, processes.Result, constants.Result, conversions.Result, includeUnitInstances.Result, excludeUnitInstances.Result);

        return OptionalWithDiagnostics.Result(product, allDiagnostics);
    }

    private IOptionalWithDiagnostics<SharpMeasuresVectorGroupMemberDefinition> ProcessMember(DefinedType type, RawSharpMeasuresVectorGroupMemberDefinition rawDefinition)
    {
        var processingContext = new SimpleProcessingContext(type);

        return ProcessingFilter.Create(SharpMeasuresVectorGroupMemberProcesser).Filter(processingContext, rawDefinition);
    }

    private IResultWithDiagnostics<IReadOnlyList<ConvertibleVectorDefinition>> ProcessConversions(DefinedType type, IEnumerable<RawConvertibleQuantityDefinition> rawDefinitions, NamedType group)
    {
        ConvertibleVectorGroupMemberProcessingContext processingContext = new(type, group);

        return ProcessingFilter.Create(ConvertibleVectorProcesser).Filter(processingContext, rawDefinitions);
    }

    private SharpMeasuresVectorGroupMemberProcesser SharpMeasuresVectorGroupMemberProcesser => new(DiagnosticsStrategy.SharpMeasuresVectorGroupMemberDiagnostics);
    private ConvertibleVectorGroupMemberrProcesser ConvertibleVectorProcesser => new(DiagnosticsStrategy.ConvertibleVectorDiagnostics);
}
