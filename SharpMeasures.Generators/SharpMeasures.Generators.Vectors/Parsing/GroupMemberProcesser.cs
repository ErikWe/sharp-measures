namespace SharpMeasures.Generators.Vectors.Parsing;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.UnitList;
using SharpMeasures.Generators.Vectors.Parsing.Abstraction;
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
        var conversions = CommonProcessing.ParseAndProcessConversions(typeSymbol);

        var includeUnits = CommonProcessing.ParseAndProcessUnitList(typeSymbol, IncludeUnitsParser.Parser);
        var excludeUnits = CommonProcessing.ParseAndProcessUnitList(typeSymbol, ExcludeUnitsParser.Parser);

        var allDiagnostics = vector.Diagnostics.Concat(derivations).Concat(constants).Concat(conversions).Concat(includeUnits).Concat(excludeUnits);

        if (includeUnits.HasResult && includeUnits.Result.Count > 0 && excludeUnits.HasResult && excludeUnits.Result.Count > 0)
        {
            allDiagnostics = allDiagnostics.Concat(new[] { VectorTypeDiagnostics.ContradictoryAttributes<IncludeUnitsAttribute, ExcludeUnitsAttribute>(declaration.GetLocation().Minimize()) });
            excludeUnits = ResultWithDiagnostics.Construct(Array.Empty<UnitListDefinition>() as IReadOnlyList<UnitListDefinition>);
        }

        GroupMemberType product = new(typeSymbol.AsDefinedType(), declaration.GetLocation().Minimize(), vector.Result, derivations.Result, constants.Result, conversions.Result, includeUnits.Result, excludeUnits.Result);

        return OptionalWithDiagnostics.Result(product, allDiagnostics);
    }

    private static IOptionalWithDiagnostics<SharpMeasuresVectorGroupMemberDefinition> ParseAndProcessVector(INamedTypeSymbol typeSymbol)
    {
        if (SharpMeasuresVectorGroupMemberParser.Parser.ParseFirstOccurrence(typeSymbol) is not RawSharpMeasuresVectorGroupMemberDefinition rawVector)
        {
            return OptionalWithDiagnostics.EmptyWithoutDiagnostics<SharpMeasuresVectorGroupMemberDefinition>();
        }

        var processingContext = new SimpleProcessingContext(typeSymbol.AsDefinedType());

        return SharpMeasuresVectorGroupMemberProcesser.Process(processingContext, rawVector);
    }

    private static SharpMeasuresVectorGroupMemberProcesser SharpMeasuresVectorGroupMemberProcesser { get; } = new(SharpMeasuresVectorGroupMemberProcessingDiagnostics.Instance);
}
