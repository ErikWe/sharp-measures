namespace SharpMeasures.Generators.Vectors.ForeignVectorParsing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Vectors.ForeignVectorParsing.Diagnostics.Processing;
using SharpMeasures.Generators.Vectors.Parsing;
using SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVectorGroupMember;

using System;

internal static class ForeignGroupMemberProcesser
{
    public static Optional<GroupMemberType> Process(RawGroupMemberType rawMember)
    {
        var member = ProcessMember(rawMember.Type, rawMember.Definition);

        if (member.HasValue is false)
        {
            return new Optional<GroupMemberType>();
        }

        var derivations = CommonProcessing.ProcessDerivations(rawMember.Type, rawMember.Derivations);
        var constants = CommonProcessing.ProcessConstants(rawMember.Type, rawMember.Constants);
        var conversions = CommonProcessing.ProcessConversions(rawMember.Type, originalQuantity: null, conversionFromOriginalQuantitySpecified: false, conversionToOriginalQuantitySpecified: false, rawMember.Conversions);

        var includeUnitInstances = CommonProcessing.ProcessIncludeUnits(rawMember.Type, rawMember.UnitInstanceInclusions);
        var excludeUnitInstances = CommonProcessing.ProcessExcludeUnits(rawMember.Type, rawMember.UnitInstanceExclusions);

        if (includeUnitInstances.Count > 0 && excludeUnitInstances.Count > 0)
        {
            excludeUnitInstances = Array.Empty<ExcludeUnitsDefinition>();
        }

        return new GroupMemberType(rawMember.Type, rawMember.TypeLocation, member.Value, derivations, constants, conversions, includeUnitInstances, excludeUnitInstances);
    }

    private static Optional<SharpMeasuresVectorGroupMemberDefinition> ProcessMember(DefinedType type, RawSharpMeasuresVectorGroupMemberDefinition rawDefinition)
    {
        var processingContext = new SimpleProcessingContext(type);

        var member = ProcessingFilter.Create(SharpMeasuresVectorGroupMemberProcesser).Filter(processingContext, rawDefinition);

        if (member.LacksResult)
        {
            return new Optional<SharpMeasuresVectorGroupMemberDefinition>();
        }

        return member.Result;
    }

    private static SharpMeasuresVectorGroupMemberProcesser SharpMeasuresVectorGroupMemberProcesser { get; } = new(EmptySharpMeasuresVectorGroupMemberProcessingDiagnostics.Instance);
}
