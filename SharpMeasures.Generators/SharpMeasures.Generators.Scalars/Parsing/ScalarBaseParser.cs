namespace SharpMeasures.Generators.Scalars.Parsing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Quantities.Parsing.ConvertibleQuantity;
using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;
using SharpMeasures.Generators.Scalars.Parsing.Abstraction;
using SharpMeasures.Generators.Scalars.Parsing.ExcludeUnitBases;
using SharpMeasures.Generators.Scalars.Parsing.IncludeUnitBases;
using SharpMeasures.Generators.Scalars.Parsing.ScalarConstant;
using SharpMeasures.Generators.Scalars.Parsing.SharpMeasuresScalar;

using System;
using System.Collections.Generic;

internal class ScalarBaseParser : AScalarParser<RawSharpMeasuresScalarDefinition, RawScalarBaseType>
{
    protected override RawScalarBaseType ProduceResult(DefinedType type, MinimalLocation typeLocation, RawSharpMeasuresScalarDefinition definition, IEnumerable<RawDerivedQuantityDefinition> derivations,
        IEnumerable<RawScalarConstantDefinition> constants, IEnumerable<RawConvertibleQuantityDefinition> conversions, IEnumerable<RawIncludeUnitBasesDefinition> baseInclusions,
        IEnumerable<RawExcludeUnitBasesDefinition> baseExclusions, IEnumerable<RawIncludeUnitsDefinition> unitInstanceInclusions, IEnumerable<RawExcludeUnitsDefinition> unitInstanceExclusions)
    {
        return new(type, typeLocation, definition, derivations, constants, conversions, baseInclusions, baseExclusions, unitInstanceInclusions, unitInstanceExclusions);
    }

    protected override (Optional<RawSharpMeasuresScalarDefinition>, IEnumerable<INamedTypeSymbol>) ParseScalar(INamedTypeSymbol typeSymbol)
    {
        if (SharpMeasuresScalarParser.Parser.ParseFirstOccurrence(typeSymbol) is not SymbolicSharpMeasuresScalarDefinition symbolicScalar)
        {
            return (new Optional<RawSharpMeasuresScalarDefinition>(), Array.Empty<INamedTypeSymbol>());
        }

        var rawScalar = RawSharpMeasuresScalarDefinition.FromSymbolic(symbolicScalar);
        var foreignSymbols = symbolicScalar.ForeignSymbols(typeSymbol.ContainingAssembly.Name);

        return (rawScalar, foreignSymbols);
    }
}
