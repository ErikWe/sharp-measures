﻿namespace SharpMeasures.Generators.Vectors.Parsing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Quantities.Parsing.ConvertibleQuantity;
using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.ProcessedQuantity;
using SharpMeasures.Generators.Vectors.Parsing.Abstraction;
using SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVector;
using SharpMeasures.Generators.Vectors.Parsing.VectorConstant;

using System;
using System.Collections.Generic;

internal sealed class VectorBaseParser : AVectorParser<RawSharpMeasuresVectorDefinition, RawVectorBaseType>
{
    protected override RawVectorBaseType ProduceResult(DefinedType type, RawSharpMeasuresVectorDefinition definition, IEnumerable<RawDerivedQuantityDefinition> derivations, IEnumerable<RawProcessedQuantityDefinition> processes, IEnumerable<RawVectorConstantDefinition> constants, IEnumerable<RawConvertibleQuantityDefinition> conversions, IEnumerable<RawIncludeUnitsDefinition> unitInstanceInclusions, IEnumerable<RawExcludeUnitsDefinition> unitInstanceExclusions)
    {
        return new(type, definition, derivations, processes, constants, conversions, unitInstanceInclusions, unitInstanceExclusions);
    }

    protected override (Optional<RawSharpMeasuresVectorDefinition>, IEnumerable<INamedTypeSymbol>) ParseVector(INamedTypeSymbol typeSymbol)
    {
        if (SharpMeasuresVectorParser.Parser.ParseFirstOccurrence(typeSymbol) is not SymbolicSharpMeasuresVectorDefinition symbolicVector)
        {
            return (new Optional<RawSharpMeasuresVectorDefinition>(), Array.Empty<INamedTypeSymbol>());
        }

        var rawVector = RawSharpMeasuresVectorDefinition.FromSymbolic(symbolicVector);
        var foreignSymbols = symbolicVector.ForeignSymbols(typeSymbol.ContainingAssembly.Name, alreadyInForeignAssembly: false);

        return (rawVector, foreignSymbols);
    }
}
