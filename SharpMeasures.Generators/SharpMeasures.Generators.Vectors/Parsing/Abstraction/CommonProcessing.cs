﻿namespace SharpMeasures.Generators.Vectors.Parsing.Abstraction;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Quantities.Parsing.Contexts.Processing;
using SharpMeasures.Generators.Quantities.Parsing.ConvertibleQuantity;
using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;
using SharpMeasures.Generators.Quantities.Parsing.Diagnostics.Processing;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;
using SharpMeasures.Generators.Vectors.Parsing.ConvertibleVector;
using SharpMeasures.Generators.Vectors.Parsing.Diagnostics.Processing;
using SharpMeasures.Generators.Vectors.Parsing.VectorConstant;

using System.Collections.Generic;

internal static class CommonProcessing
{
    public static IResultWithDiagnostics<IReadOnlyList<DerivedQuantityDefinition>> ParseAndProcessDerivations(INamedTypeSymbol typeSymbol)
    {
        var rawDerivations = DerivedQuantityParser.Parser.ParseAllOccurrences(typeSymbol);

        var processingContext = new SimpleProcessingContext(typeSymbol.AsDefinedType());

        return ProcessingFilter.Create(DerivedQuantityProcesser).Filter(processingContext, rawDerivations);
    }

    public static IResultWithDiagnostics<IReadOnlyList<VectorConstantDefinition>> ParseAndProcessConstants(INamedTypeSymbol typeSymbol, NamedType? unit)
    {
        var rawConstants = VectorConstantParser.Parser.ParseAllOccurrences(typeSymbol);

        var scalarConstantProcesser = unit is null
            ? VectorConstantProcesserForUnknownUnit
            : VectorConstantProcesser(unit.Value);

        QuantityConstantProcessingContext processingContext = new(typeSymbol.AsDefinedType());

        return ProcessingFilter.Create(scalarConstantProcesser).Filter(processingContext, rawConstants);
    }

    public static IResultWithDiagnostics<IReadOnlyList<ConvertibleVectorDefinition>> ParseAndProcessConversions(INamedTypeSymbol typeSymbol)
    {
        var rawConvertibles = ConvertibleQuantityParser.Parser.ParseAllOccurrences(typeSymbol);

        ConvertibleQuantityProcessingContext processingContext = new(typeSymbol.AsDefinedType());

        return ProcessingFilter.Create(ConvertibleVectorProcesser).Filter(processingContext, rawConvertibles);
    }

    public static IResultWithDiagnostics<IReadOnlyList<IncludeUnitsDefinition>> ParseAndProcessIncludeUnitInstances(INamedTypeSymbol typeSymbol)
    {
        var rawIncludeUnits = IncludeUnitsParser.Parser.ParseAllOccurrences(typeSymbol);

        IncludeUnitsProcessingContext processingContext = new(typeSymbol.AsDefinedType());

        return ProcessingFilter.Create(IncludeUnitsProcesser).Filter(processingContext, rawIncludeUnits);
    }

    public static IResultWithDiagnostics<IReadOnlyList<ExcludeUnitsDefinition>> ParseAndProcessExcludeUnitInstances(INamedTypeSymbol typeSymbol)
    {
        var rawExcludeUnits = ExcludeUnitsParser.Parser.ParseAllOccurrences(typeSymbol);

        ExcludeUnitsProcessingContext processingContext = new(typeSymbol.AsDefinedType());

        return ProcessingFilter.Create(ExcludeUnitsProcesser).Filter(processingContext, rawExcludeUnits);
    }

    private static DerivedQuantityProcesser DerivedQuantityProcesser { get; } = new(DerivedQuantityProcessingDiagnostics.Instance);
    private static VectorConstantProcesser VectorConstantProcesser(NamedType unit) => new(new VectorConstantProcessingDiagnostics(unit));
    private static VectorConstantProcesser VectorConstantProcesserForUnknownUnit { get; } = new(new VectorConstantProcessingDiagnostics());
    private static ConvertibleVectorProcesser ConvertibleVectorProcesser { get; } = new(ConvertibleVectorProcessingDiagnostics.Instance);

    private static IncludeUnitsProcesser IncludeUnitsProcesser { get; } = new(IncludeUnitsProcessingDiagnostics.Instance);
    private static ExcludeUnitsProcesser ExcludeUnitsProcesser { get; } = new(ExcludeUnitsProcessingDiagnostics.Instance);
}