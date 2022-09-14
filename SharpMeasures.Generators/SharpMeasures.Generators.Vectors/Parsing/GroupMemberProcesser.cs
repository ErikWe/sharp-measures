namespace SharpMeasures.Generators.Vectors.Parsing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Quantities.Parsing.ConvertibleQuantity;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Vectors.Parsing.Abstraction;
using SharpMeasures.Generators.Vectors.Parsing.Contexts.Processing;
using SharpMeasures.Generators.Vectors.Parsing.ConvertibleVector;
using SharpMeasures.Generators.Vectors.Parsing.Diagnostics;
using SharpMeasures.Generators.Vectors.Parsing.Diagnostics.Processing;
using SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVectorGroupMember;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

internal static class GroupMemberProcesser
{
    public static IOptionalWithDiagnostics<GroupMemberType> Process(Optional<RawGroupMemberType> rawMember, CancellationToken token)
    {
        if (token.IsCancellationRequested || rawMember.HasValue is false)
        {
            return OptionalWithDiagnostics.Empty<GroupMemberType>();
        }

        return Process(rawMember.Value);
    }

    private static IOptionalWithDiagnostics<GroupMemberType> Process(RawGroupMemberType rawMember)
    {
        var member = ProcessMember(rawMember.Type, rawMember.Definition);

        if (member.LacksResult)
        {
            return member.AsEmptyOptional<GroupMemberType>();
        }

        var derivations = CommonProcessing.ProcessDerivations(rawMember.Type, rawMember.Derivations);
        var constants = CommonProcessing.ProcessConstants(rawMember.Type, rawMember.Constants, null);
        var conversions = ProcessConversions(rawMember.Type, rawMember.Conversions, member.Result.VectorGroup);

        var includeUnitInstances = CommonProcessing.ProcessIncludeUnitInstances(rawMember.Type, rawMember.UnitInstanceInclusions);
        var excludeUnitInstances = CommonProcessing.ProcessExcludeUnitInstances(rawMember.Type, rawMember.UnitInstanceExclusions);

        var allDiagnostics = member.Diagnostics.Concat(derivations).Concat(constants).Concat(conversions).Concat(includeUnitInstances).Concat(excludeUnitInstances);

        if (includeUnitInstances.HasResult && includeUnitInstances.Result.Count > 0 && excludeUnitInstances.HasResult && excludeUnitInstances.Result.Count > 0)
        {
            allDiagnostics = allDiagnostics.Concat(new[] { VectorTypeDiagnostics.ContradictoryAttributes<IncludeUnitsAttribute, ExcludeUnitsAttribute>(rawMember.TypeLocation) });
            excludeUnitInstances = ResultWithDiagnostics.Construct(Array.Empty<ExcludeUnitsDefinition>() as IReadOnlyList<ExcludeUnitsDefinition>);
        }

        GroupMemberType product = new(rawMember.Type, rawMember.TypeLocation, member.Result, derivations.Result, constants.Result, conversions.Result, includeUnitInstances.Result, excludeUnitInstances.Result);

        return OptionalWithDiagnostics.Result(product, allDiagnostics);
    }

    private static IOptionalWithDiagnostics<SharpMeasuresVectorGroupMemberDefinition> ProcessMember(DefinedType type, RawSharpMeasuresVectorGroupMemberDefinition rawDefinition)
    {
        var processingContext = new SimpleProcessingContext(type);

        return ProcessingFilter.Create(SharpMeasuresVectorGroupMemberProcesser).Filter(processingContext, rawDefinition);
    }

    private static IResultWithDiagnostics<IReadOnlyList<ConvertibleVectorDefinition>> ProcessConversions(DefinedType type, IEnumerable<RawConvertibleQuantityDefinition> rawDefinitions, NamedType group)
    {
        ConvertibleVectorGroupMemberProcessingContext processingContext = new(type, group);

        return ProcessingFilter.Create(ConvertibleVectorProcesser).Filter(processingContext, rawDefinitions);
    }

    private static SharpMeasuresVectorGroupMemberProcesser SharpMeasuresVectorGroupMemberProcesser { get; } = new(SharpMeasuresVectorGroupMemberProcessingDiagnostics.Instance);
    private static ConvertibleVectorGroupMemberrProcesser ConvertibleVectorProcesser { get; } = new(ConvertibleVectorProcessingDiagnostics.Instance);
}
