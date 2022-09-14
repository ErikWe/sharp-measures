namespace SharpMeasures.Generators.Vectors.Parsing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Quantities.Parsing.ConvertibleQuantity;
using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;
using SharpMeasures.Generators.Vectors.Parsing.Abstraction;
using SharpMeasures.Generators.Vectors.Parsing.SpecializedSharpMeasuresVector;
using SharpMeasures.Generators.Vectors.Parsing.VectorConstant;

using System;
using System.Collections.Generic;

internal class VectorSpecializationParser : AVectorParser<RawSpecializedSharpMeasuresVectorDefinition, RawVectorSpecializationType>
{
    protected override RawVectorSpecializationType ProduceResult(DefinedType type, MinimalLocation typeLocation, RawSpecializedSharpMeasuresVectorDefinition definition, IEnumerable<RawDerivedQuantityDefinition> derivations,
        IEnumerable<RawVectorConstantDefinition> constants, IEnumerable<RawConvertibleQuantityDefinition> conversions, IEnumerable<RawIncludeUnitsDefinition> unitInstanceInclusions, IEnumerable<RawExcludeUnitsDefinition> unitInstanceExclusions)
    {
        return new(type, typeLocation, definition, derivations, constants, conversions, unitInstanceInclusions, unitInstanceExclusions);
    }

    protected override (Optional<RawSpecializedSharpMeasuresVectorDefinition>, IEnumerable<INamedTypeSymbol>) ParseVector(INamedTypeSymbol typeSymbol)
    {
        if (SpecializedSharpMeasuresVectorParser.Parser.ParseFirstOccurrence(typeSymbol) is not SymbolicSpecializedSharpMeasuresVectorDefinition symbolicVector)
        {
            return (new Optional<RawSpecializedSharpMeasuresVectorDefinition>(), Array.Empty<INamedTypeSymbol>());
        }

        var rawVector = RawSpecializedSharpMeasuresVectorDefinition.FromSymbolic(symbolicVector);
        var foreignSymbols = symbolicVector.ForeignSymbols(typeSymbol.ContainingAssembly.Name);

        return (rawVector, foreignSymbols);
    }
}
