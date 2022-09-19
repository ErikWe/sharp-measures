namespace SharpMeasures.Generators.Units.ForeignUnitParsing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Units.Parsing;
using SharpMeasures.Generators.Units.Parsing.BiasedUnitInstance;
using SharpMeasures.Generators.Units.Parsing.DerivableUnit;
using SharpMeasures.Generators.Units.Parsing.DerivedUnitInstance;
using SharpMeasures.Generators.Units.Parsing.FixedUnitInstance;
using SharpMeasures.Generators.Units.Parsing.PrefixedUnitInstance;
using SharpMeasures.Generators.Units.Parsing.ScaledUnitInstance;
using SharpMeasures.Generators.Units.Parsing.SharpMeasuresUnit;
using SharpMeasures.Generators.Units.Parsing.UnitInstanceAlias;

using System;
using System.Collections.Generic;
using System.Linq;

public sealed class ForeignUnitParser
{
    private List<RawUnitType> Units { get; } = new();

    public Optional<IEnumerable<INamedTypeSymbol>> TryParse(INamedTypeSymbol typeSymbol)
    {
        (var unit, var unitReferencedSymbols) = Parse(typeSymbol);

        if (unit.HasValue)
        {
            Units.Add(unit.Value);

            return new Optional<IEnumerable<INamedTypeSymbol>>(unitReferencedSymbols);
        }

        return new Optional<IEnumerable<INamedTypeSymbol>>();
    }

    public IForeignUnitProcesser Finalize() => new ForeignUnitProcesser(new ForeignUnitParsingResult(Units));

    private static (Optional<RawUnitType> Definition, IEnumerable<INamedTypeSymbol> ReferencedSymbols) Parse(INamedTypeSymbol typeSymbol)
    {
        (var unit, var unitReferencedSymbols) = ParseUnit(typeSymbol);

        if (unit.HasValue is false)
        {
            return (new Optional<RawUnitType>(), Array.Empty<INamedTypeSymbol>());
        }

        (var derivations, var derivationsReferencedSymbols) = ParseDerivations(typeSymbol);

        var fixedUnitInstance = ParseFixedUnitInstance(typeSymbol);
        var unitInstanceAliases = ParseUnitInstanceAliases(typeSymbol);
        var derivedUnitInstances = ParseDerivedUnitInstances(typeSymbol);
        var biasedUnitInstances = ParseBiasedUnitInstances(typeSymbol);
        var prefixedUnitInstances = ParsePrefixedUnitInstances(typeSymbol);
        var scaledUnitInstances = ParseScaledUnitInstances(typeSymbol);

        RawUnitType rawUnit = new(typeSymbol.AsDefinedType(), MinimalLocation.None, unit.Value, derivations, fixedUnitInstance.HasValue ? fixedUnitInstance.Value : null, unitInstanceAliases, derivedUnitInstances, biasedUnitInstances, prefixedUnitInstances, scaledUnitInstances);
        var referencedSymbols = unitReferencedSymbols.Concat(derivationsReferencedSymbols);

        return (rawUnit, referencedSymbols);
    }

    private static (Optional<RawSharpMeasuresUnitDefinition>, IEnumerable<INamedTypeSymbol>) ParseUnit(INamedTypeSymbol typeSymbol)
    {
        if (SharpMeasuresUnitParser.Parser.ParseFirstOccurrence(typeSymbol) is not SymbolicSharpMeasuresUnitDefinition symbolicUnit)
        {
            return (new Optional<RawSharpMeasuresUnitDefinition>(), Array.Empty<INamedTypeSymbol>());
        }

        var rawUnit = RawSharpMeasuresUnitDefinition.FromSymbolic(symbolicUnit);
        var foreignSymbols = symbolicUnit.ForeignSymbols(typeSymbol.ContainingAssembly.Name, alreadyInForeignAssembly: true);

        return (rawUnit, foreignSymbols);
    }

    private static (IEnumerable<RawDerivableUnitDefinition>, IEnumerable<INamedTypeSymbol>) ParseDerivations(INamedTypeSymbol typeSymbol)
    {
        var symbolicDerivations = DerivableUnitParser.Parser.ParseAllOccurrences(typeSymbol);

        var rawDerivations = symbolicDerivations.Select(static (symbolicDerivation) => RawDerivableUnitDefinition.FromSymbolic(symbolicDerivation));
        var foreignSymbols = symbolicDerivations.SelectMany((symbolicDerivation) => symbolicDerivation.ForeignSymbols(typeSymbol.ContainingAssembly.Name, alreadyInForeignAssembly: true));

        return (rawDerivations, foreignSymbols);
    }

    private static Optional<RawFixedUnitInstanceDefinition> ParseFixedUnitInstance(INamedTypeSymbol typeSymbol)
    {
        if (FixedUnitInstanceParser.Parser.ParseFirstOccurrence(typeSymbol) is not RawFixedUnitInstanceDefinition rawFixedUnitInstance)
        {
            return new Optional<RawFixedUnitInstanceDefinition>();
        }

        return rawFixedUnitInstance;
    }

    private static IEnumerable<RawUnitInstanceAliasDefinition> ParseUnitInstanceAliases(INamedTypeSymbol typeSymbol) => UnitInstanceAliasParser.Parser.ParseAllOccurrences(typeSymbol);
    private static IEnumerable<RawDerivedUnitInstanceDefinition> ParseDerivedUnitInstances(INamedTypeSymbol typeSymbol) => DerivedUnitInstanceParser.Parser.ParseAllOccurrences(typeSymbol);
    private static IEnumerable<RawBiasedUnitInstanceDefinition> ParseBiasedUnitInstances(INamedTypeSymbol typeSymbol) => BiasedUnitInstanceParser.Parser.ParseAllOccurrences(typeSymbol);
    private static IEnumerable<RawPrefixedUnitInstanceDefinition> ParsePrefixedUnitInstances(INamedTypeSymbol typeSymbol) => PrefixedUnitInstanceParser.Parser.ParseAllOccurrences(typeSymbol);
    private static IEnumerable<RawScaledUnitInstanceDefinition> ParseScaledUnitInstances(INamedTypeSymbol typeSymbol) => ScaledUnitInstanceParser.Parser.ParseAllOccurrences(typeSymbol);
}
