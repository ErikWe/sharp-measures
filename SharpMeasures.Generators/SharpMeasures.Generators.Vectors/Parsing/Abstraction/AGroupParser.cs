namespace SharpMeasures.Generators.Vectors.Parsing.Abstraction;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Quantities.Parsing.ConvertibleQuantity;
using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;
using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;

using System.Collections.Generic;
using System.Linq;
using System.Threading;

internal abstract class AGroupParser<TDefinition, TProduct>
{
    public (Optional<TProduct> Definition, ForeignSymbolCollection ForeignSymbols) Parse(Optional<(TypeDeclarationSyntax Declaration, INamedTypeSymbol TypeSymbol)> input, CancellationToken token)
    {
        if (token.IsCancellationRequested || input.HasValue is false)
        {
            return (new Optional<TProduct>(), ForeignSymbolCollection.Empty);
        }

        return Parse(input.Value.Declaration, input.Value.TypeSymbol);
    }

    private (Optional<TProduct>, ForeignSymbolCollection) Parse(TypeDeclarationSyntax declaration, INamedTypeSymbol typeSymbol)
    {
        (var group, var groupForeignSymbols) = ParseGroup(typeSymbol);

        if (group.HasValue is false)
        {
            return (new Optional<TProduct>(), ForeignSymbolCollection.Empty);
        }

        (var derivations, var derivationForeignSymbols) = CommonParsing.ParseDerivations(typeSymbol);
        (var conversions, var conversionForeignSymbols) = CommonParsing.ParseConversions(typeSymbol);

        var includeUnitInstances = CommonParsing.ParseIncludeUnitInstances(typeSymbol);
        var excludeUnitInstances = CommonParsing.ParseExcludeUnitInstances(typeSymbol);

        TProduct product = ProduceResult(typeSymbol.AsDefinedType(), declaration.Identifier.GetLocation().Minimize(), group.Value, derivations, conversions, includeUnitInstances, excludeUnitInstances);
        ForeignSymbolCollection foreignSymbols = new(groupForeignSymbols.Concat(derivationForeignSymbols).Concat(conversionForeignSymbols).ToList());

        return (product, foreignSymbols);
    }

    protected abstract TProduct ProduceResult(DefinedType type, MinimalLocation typeLocation, TDefinition definition, IEnumerable<RawDerivedQuantityDefinition> derivations,
        IEnumerable<RawConvertibleQuantityDefinition> conversions, IEnumerable<RawIncludeUnitsDefinition> unitInstanceInclusions, IEnumerable<RawExcludeUnitsDefinition> unitInstanceExclusions);

    protected abstract (Optional<TDefinition> Definition, IEnumerable<INamedTypeSymbol> ForeignSymbols) ParseGroup(INamedTypeSymbol typeSymbol);
}
