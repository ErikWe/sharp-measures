﻿namespace SharpMeasures.Generators.Vectors.Parsing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Quantities.Parsing.ConvertibleQuantity;
using SharpMeasures.Generators.Quantities.Parsing.QuantityOperation;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;
using SharpMeasures.Generators.Vectors.Parsing.Abstraction;
using SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVectorGroup;

using System;
using System.Collections.Generic;
using SharpMeasures.Generators.Vectors.Parsing.VectorOperation;

internal sealed class GroupBaseParser : AGroupParser<RawSharpMeasuresVectorGroupDefinition, RawGroupBaseType>
{
    public GroupBaseParser(bool alreadyInForeignAssembly) : base(alreadyInForeignAssembly) { }

    protected override RawGroupBaseType ProduceResult(DefinedType type, RawSharpMeasuresVectorGroupDefinition definition, IEnumerable<RawQuantityOperationDefinition> operations, IEnumerable<RawVectorOperationDefinition> vectorOperations, IEnumerable<RawConvertibleQuantityDefinition> conversions, IEnumerable<RawIncludeUnitsDefinition> unitInstanceInclusions, IEnumerable<RawExcludeUnitsDefinition> unitInstanceExclusions)
    {
        return new(type, definition, operations, vectorOperations, conversions, unitInstanceInclusions, unitInstanceExclusions);
    }

    protected override (Optional<RawSharpMeasuresVectorGroupDefinition>, IEnumerable<INamedTypeSymbol>) ParseGroup(INamedTypeSymbol typeSymbol, IEnumerable<AttributeData> attributes)
    {
        if (SharpMeasuresVectorGroupParser.Parser.ParseFirstOccurrence(attributes) is not SymbolicSharpMeasuresVectorGroupDefinition symbolicGroup)
        {
            return (new Optional<RawSharpMeasuresVectorGroupDefinition>(), Array.Empty<INamedTypeSymbol>());
        }

        var rawGroup = RawSharpMeasuresVectorGroupDefinition.FromSymbolic(symbolicGroup);
        var foreignSymbols = symbolicGroup.ForeignSymbols(typeSymbol.ContainingAssembly.Name, AlreadyInForeignAssembly);

        return (rawGroup, foreignSymbols);
    }
}
