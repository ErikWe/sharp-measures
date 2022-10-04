namespace SharpMeasures.Generators.Vectors.Parsing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Quantities.Parsing.ConvertibleQuantity;
using SharpMeasures.Generators.Quantities.Parsing.QuantityOperation;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;
using SharpMeasures.Generators.Vectors.Parsing.Abstraction;
using SharpMeasures.Generators.Vectors.Parsing.SpecializedSharpMeasuresVectorGroup;

using System;
using System.Collections.Generic;
using SharpMeasures.Generators.Vectors.Parsing.VectorOperation;

internal sealed class GroupSpecializationParser : AGroupParser<RawSpecializedSharpMeasuresVectorGroupDefinition, RawGroupSpecializationType>
{
    protected override RawGroupSpecializationType ProduceResult(DefinedType type, RawSpecializedSharpMeasuresVectorGroupDefinition definition, IEnumerable<RawQuantityOperationDefinition> operations, IEnumerable<RawVectorOperationDefinition> vectorOperations, IEnumerable<RawConvertibleQuantityDefinition> conversions, IEnumerable<RawIncludeUnitsDefinition> unitInstanceInclusions, IEnumerable<RawExcludeUnitsDefinition> unitInstanceExclusions)
    {
        return new(type, definition, operations, vectorOperations, conversions, unitInstanceInclusions, unitInstanceExclusions);
    }

    protected override (Optional<RawSpecializedSharpMeasuresVectorGroupDefinition>, IEnumerable<INamedTypeSymbol>) ParseGroup(INamedTypeSymbol typeSymbol)
    {
        if (SpecializedSharpMeasuresVectorGroupParser.Parser.ParseFirstOccurrence(typeSymbol) is not SymbolicSpecializedSharpMeasuresVectorGroupDefinition symbolicGroup)
        {
            return (new Optional<RawSpecializedSharpMeasuresVectorGroupDefinition>(), Array.Empty<INamedTypeSymbol>());
        }

        var rawGroup = RawSpecializedSharpMeasuresVectorGroupDefinition.FromSymbolic(symbolicGroup);
        var foreignSymbols = symbolicGroup.ForeignSymbols(typeSymbol.ContainingAssembly.Name, alreadyInForeignAssembly: false);

        return (rawGroup, foreignSymbols);
    }
}
