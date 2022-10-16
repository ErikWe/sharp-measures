namespace SharpMeasures.Generators.Scalars.Parsing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Quantities.Parsing.ConvertibleQuantity;
using SharpMeasures.Generators.Quantities.Parsing.QuantityOperation;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;
using SharpMeasures.Generators.Scalars.Parsing.Abstraction;
using SharpMeasures.Generators.Scalars.Parsing.ExcludeUnitBases;
using SharpMeasures.Generators.Scalars.Parsing.IncludeUnitBases;
using SharpMeasures.Generators.Scalars.Parsing.ScalarConstant;
using SharpMeasures.Generators.Scalars.Parsing.SpecializedSharpMeasuresScalar;

using System;
using System.Collections.Generic;
using SharpMeasures.Generators.Quantities.Parsing.QuantityProcess;

internal sealed class ScalarSpecializationParser : AScalarParser<RawSpecializedSharpMeasuresScalarDefinition, RawScalarSpecializationType>
{
    protected override RawScalarSpecializationType ProduceResult(DefinedType type, RawSpecializedSharpMeasuresScalarDefinition definition, IEnumerable<RawQuantityOperationDefinition> operations, IEnumerable<RawQuantityProcessDefinition> processes, IEnumerable<RawScalarConstantDefinition> constants,
        IEnumerable<RawConvertibleQuantityDefinition> conversions,IEnumerable<RawIncludeUnitBasesDefinition> baseInclusions, IEnumerable<RawExcludeUnitBasesDefinition> baseExclusions, IEnumerable<RawIncludeUnitsDefinition> unitInstanceInclusions, IEnumerable<RawExcludeUnitsDefinition> unitInstanceExclusions)
    {
        return new(type, definition, operations, processes, constants, conversions, baseInclusions, baseExclusions, unitInstanceInclusions, unitInstanceExclusions);
    }

    protected override (Optional<RawSpecializedSharpMeasuresScalarDefinition>, IEnumerable<INamedTypeSymbol>) ParseScalar(INamedTypeSymbol typeSymbol, IEnumerable<AttributeData> attributes)
    {
        if (SpecializedSharpMeasuresScalarParser.Parser.ParseFirstOccurrence(attributes) is not SymbolicSpecializedSharpMeasuresScalarDefinition symbolicScalar)
        {
            return (new Optional<RawSpecializedSharpMeasuresScalarDefinition>(), Array.Empty<INamedTypeSymbol>());
        }

        var rawScalar = RawSpecializedSharpMeasuresScalarDefinition.FromSymbolic(symbolicScalar);
        var foreignSymbols = symbolicScalar.ForeignSymbols(typeSymbol.ContainingAssembly.Name, alreadyInForeignAssembly: false);

        return (rawScalar, foreignSymbols);
    }
}
