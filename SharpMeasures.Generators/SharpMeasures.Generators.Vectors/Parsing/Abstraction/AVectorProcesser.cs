﻿namespace SharpMeasures.Generators.Vectors.Parsing.Abstraction;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;
using SharpMeasures.Generators.Vectors.Parsing.ConvertibleVector;
using SharpMeasures.Generators.Vectors.Parsing.Diagnostics;
using SharpMeasures.Generators.Vectors.Parsing.VectorConstant;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

internal abstract class AVectorProcesser<TDefinition, TProduct>
    where TDefinition : IVector
{
    public IOptionalWithDiagnostics<TProduct> ParseAndProcess((TypeDeclarationSyntax Declaration, INamedTypeSymbol TypeSymbol) input, CancellationToken _) => ParseAndProcess(input.Declaration, input.TypeSymbol);
    private IOptionalWithDiagnostics<TProduct> ParseAndProcess(TypeDeclarationSyntax declaration, INamedTypeSymbol typeSymbol)
    {
        var vector = ParseAndProcessVector(typeSymbol);

        if (vector.LacksResult)
        {
            return vector.AsEmptyOptional<TProduct>();
        }

        var unit = GetUnit(vector.Result);

        var derivations = CommonProcessing.ParseAndProcessDerivations(typeSymbol);
        var constants = CommonProcessing.ParseAndProcessConstants(typeSymbol, unit);
        var conversions = CommonProcessing.ParseAndProcessConversions(typeSymbol);

        var includeUnitInstances = CommonProcessing.ParseAndProcessIncludeUnitInstances(typeSymbol);
        var excludeUnitInstances = CommonProcessing.ParseAndProcessExcludeUnitInstances(typeSymbol);

        var allDiagnostics = vector.Diagnostics.Concat(derivations).Concat(constants).Concat(conversions).Concat(includeUnitInstances).Concat(excludeUnitInstances);

        if (includeUnitInstances.HasResult && includeUnitInstances.Result.Count > 0 && excludeUnitInstances.HasResult && excludeUnitInstances.Result.Count > 0)
        {
            allDiagnostics = allDiagnostics.Concat(new[] { VectorTypeDiagnostics.ContradictoryAttributes<IncludeUnitsAttribute, ExcludeUnitsAttribute>(declaration.Identifier.GetLocation().Minimize()) });
            excludeUnitInstances = ResultWithDiagnostics.Construct(Array.Empty<ExcludeUnitsDefinition>() as IReadOnlyList<ExcludeUnitsDefinition>);
        }

        TProduct product = FinalizeProcess(typeSymbol.AsDefinedType(), declaration.GetLocation().Minimize(), vector.Result, derivations.Result, constants.Result,
            conversions.Result, includeUnitInstances.Result, excludeUnitInstances.Result);

        return OptionalWithDiagnostics.Result(product, allDiagnostics);
    }

    protected abstract TProduct FinalizeProcess(DefinedType type, MinimalLocation typeLocation, TDefinition definition, IReadOnlyList<DerivedQuantityDefinition> derivations,
        IReadOnlyList<VectorConstantDefinition> constants, IReadOnlyList<ConvertibleVectorDefinition> conversions, IReadOnlyList<IncludeUnitsDefinition> unitInclusions,
        IReadOnlyList<ExcludeUnitsDefinition> unitExclusions);

    protected abstract IOptionalWithDiagnostics<TDefinition> ParseAndProcessVector(INamedTypeSymbol typeSymbol);

    protected abstract NamedType? GetUnit(TDefinition scalar);
}
