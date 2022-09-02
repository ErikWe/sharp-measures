namespace SharpMeasures.Generators.Vectors.Parsing;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

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
    public static IOptionalWithDiagnostics<GroupMemberType> ParseAndProcess((TypeDeclarationSyntax Declaration, INamedTypeSymbol TypeSymbol) input, CancellationToken _) => ParseAndProcess(input.Declaration, input.TypeSymbol);
    private static IOptionalWithDiagnostics<GroupMemberType> ParseAndProcess(TypeDeclarationSyntax declaration, INamedTypeSymbol typeSymbol)
    {
        var vector = ParseAndProcessVector(typeSymbol);

        if (vector.LacksResult)
        {
            return vector.AsEmptyOptional<GroupMemberType>();
        }

        var derivations = CommonProcessing.ParseAndProcessDerivations(typeSymbol);
        var constants = CommonProcessing.ParseAndProcessConstants(typeSymbol, null);
        var conversions = ParseAndProcessConversions(typeSymbol, vector.Result.VectorGroup);

        var includeUnitInstances = CommonProcessing.ParseAndProcessIncludeUnitInstances(typeSymbol);
        var excludeUnitInstances = CommonProcessing.ParseAndProcessExcludeUnitInstances(typeSymbol);

        var allDiagnostics = vector.Diagnostics.Concat(derivations).Concat(constants).Concat(conversions).Concat(includeUnitInstances).Concat(excludeUnitInstances);

        if (includeUnitInstances.HasResult && includeUnitInstances.Result.Count > 0 && excludeUnitInstances.HasResult && excludeUnitInstances.Result.Count > 0)
        {
            allDiagnostics = allDiagnostics.Concat(new[] { VectorTypeDiagnostics.ContradictoryAttributes<IncludeUnitsAttribute, ExcludeUnitsAttribute>(declaration.Identifier.GetLocation().Minimize()) });
            excludeUnitInstances = ResultWithDiagnostics.Construct(Array.Empty<ExcludeUnitsDefinition>() as IReadOnlyList<ExcludeUnitsDefinition>);
        }

        GroupMemberType product = new(typeSymbol.AsDefinedType(), declaration.GetLocation().Minimize(), vector.Result, derivations.Result, constants.Result, conversions.Result, includeUnitInstances.Result, excludeUnitInstances.Result);

        return OptionalWithDiagnostics.Result(product, allDiagnostics);
    }

    private static IOptionalWithDiagnostics<SharpMeasuresVectorGroupMemberDefinition> ParseAndProcessVector(INamedTypeSymbol typeSymbol)
    {
        if (SharpMeasuresVectorGroupMemberParser.Parser.ParseFirstOccurrence(typeSymbol) is not RawSharpMeasuresVectorGroupMemberDefinition rawVector)
        {
            return OptionalWithDiagnostics.EmptyWithoutDiagnostics<SharpMeasuresVectorGroupMemberDefinition>();
        }

        var processingContext = new SimpleProcessingContext(typeSymbol.AsDefinedType());

        return ProcessingFilter.Create(SharpMeasuresVectorGroupMemberProcesser).Filter(processingContext, rawVector);
    }

    private static IResultWithDiagnostics<IReadOnlyList<ConvertibleVectorDefinition>> ParseAndProcessConversions(INamedTypeSymbol typeSymbol, NamedType group)
    {
        var rawConvertibles = ConvertibleQuantityParser.Parser.ParseAllOccurrences(typeSymbol);

        ConvertibleVectorGroupMemberProcessingContext processingContext = new(typeSymbol.AsDefinedType(), group);

        return ProcessingFilter.Create(ConvertibleVectorProcesser).Filter(processingContext, rawConvertibles);
    }

    private static SharpMeasuresVectorGroupMemberProcesser SharpMeasuresVectorGroupMemberProcesser { get; } = new(SharpMeasuresVectorGroupMemberProcessingDiagnostics.Instance);
    private static ConvertibleVectorGroupMemberrProcesser ConvertibleVectorProcesser { get; } = new(ConvertibleVectorProcessingDiagnostics.Instance);
}
