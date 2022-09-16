namespace SharpMeasures.Generators.Vectors.ForeignVectorParsing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Vectors.ForeignVectorParsing.Diagnostics;
using SharpMeasures.Generators.Vectors.Parsing;
using SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVectorGroupMember;

using System;
using System.Collections.Generic;

internal static class ForeignGroupMemberProcesser
{
    public static Optional<GroupMemberType> Process(RawGroupMemberType rawMember)
    {
        var member = ProcessMember(rawMember.Type, rawMember.Definition);

        if (member.LacksResult)
        {
            return new Optional<GroupMemberType>();
        }

        var derivations = CommonProcessing.ProcessDerivations(rawMember.Type, rawMember.Derivations);
        var constants = CommonProcessing.ProcessConstants(rawMember.Type, rawMember.Constants);
        var conversions = CommonProcessing.ProcessConversions(rawMember.Type, rawMember.Conversions);

        var includeUnitInstances = CommonProcessing.ProcessIncludeUnits(rawMember.Type, rawMember.UnitInstanceInclusions);
        var excludeUnitInstances = CommonProcessing.ProcessExcludeUnits(rawMember.Type, rawMember.UnitInstanceExclusions);

        if (includeUnitInstances.HasResult && includeUnitInstances.Result.Count > 0 && excludeUnitInstances.HasResult && excludeUnitInstances.Result.Count > 0)
        {
            excludeUnitInstances = ResultWithDiagnostics.Construct(Array.Empty<ExcludeUnitsDefinition>() as IReadOnlyList<ExcludeUnitsDefinition>);
        }

        return new GroupMemberType(rawMember.Type, rawMember.TypeLocation, member.Result, derivations.Result, constants.Result, conversions.Result, includeUnitInstances.Result, excludeUnitInstances.Result);
    }

    private static IOptionalWithDiagnostics<SharpMeasuresVectorGroupMemberDefinition> ProcessMember(DefinedType type, RawSharpMeasuresVectorGroupMemberDefinition rawDefinition)
    {
        var processingContext = new SimpleProcessingContext(type);

        return ProcessingFilter.Create(SharpMeasuresVectorGroupMemberProcesser).Filter(processingContext, rawDefinition);
    }

    private static SharpMeasuresVectorGroupMemberProcesser SharpMeasuresVectorGroupMemberProcesser { get; } = new(EmptySharpMeasuresVectorGroupMemberProcessingDiagnostics.Instance);
}
