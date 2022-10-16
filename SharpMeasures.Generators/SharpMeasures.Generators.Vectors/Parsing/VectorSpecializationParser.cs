namespace SharpMeasures.Generators.Vectors.Parsing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Quantities.Parsing.ConvertibleQuantity;
using SharpMeasures.Generators.Quantities.Parsing.QuantityOperation;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.QuantityProcess;
using SharpMeasures.Generators.Vectors.Parsing.Abstraction;
using SharpMeasures.Generators.Vectors.Parsing.SpecializedSharpMeasuresVector;
using SharpMeasures.Generators.Vectors.Parsing.VectorConstant;

using System;
using System.Collections.Generic;
using SharpMeasures.Generators.Vectors.Parsing.VectorOperation;

internal sealed class VectorSpecializationParser : AVectorParser<RawSpecializedSharpMeasuresVectorDefinition, RawVectorSpecializationType>
{
    protected override RawVectorSpecializationType ProduceResult(DefinedType type, RawSpecializedSharpMeasuresVectorDefinition definition, IEnumerable<RawQuantityOperationDefinition> operations, IEnumerable<RawVectorOperationDefinition> vectorOperations, IEnumerable<RawQuantityProcessDefinition> processes,
        IEnumerable<RawVectorConstantDefinition> constants, IEnumerable<RawConvertibleQuantityDefinition> conversions, IEnumerable<RawIncludeUnitsDefinition> unitInstanceInclusions, IEnumerable<RawExcludeUnitsDefinition> unitInstanceExclusions)
    {
        return new(type, definition, operations, vectorOperations, processes, constants, conversions, unitInstanceInclusions, unitInstanceExclusions);
    }

    protected override (Optional<RawSpecializedSharpMeasuresVectorDefinition>, IEnumerable<INamedTypeSymbol>) ParseVector(INamedTypeSymbol typeSymbol, IEnumerable<AttributeData> attributes)
    {
        if (SpecializedSharpMeasuresVectorParser.Parser.ParseFirstOccurrence(attributes) is not SymbolicSpecializedSharpMeasuresVectorDefinition symbolicVector)
        {
            return (new Optional<RawSpecializedSharpMeasuresVectorDefinition>(), Array.Empty<INamedTypeSymbol>());
        }

        var rawVector = RawSpecializedSharpMeasuresVectorDefinition.FromSymbolic(symbolicVector);
        var foreignSymbols = symbolicVector.ForeignSymbols(typeSymbol.ContainingAssembly.Name, alreadyInForeignAssembly: false);

        return (rawVector, foreignSymbols);
    }
}
