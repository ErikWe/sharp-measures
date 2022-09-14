namespace SharpMeasures.Generators.Units.Parsing;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Providers;
using SharpMeasures.Generators.Providers.DeclarationFilter;
using SharpMeasures.Generators.Units.Parsing.BiasedUnitInstance;
using SharpMeasures.Generators.Units.Parsing.DerivableUnit;
using SharpMeasures.Generators.Units.Parsing.DerivedUnitInstance;
using SharpMeasures.Generators.Units.Parsing.Diagnostics;
using SharpMeasures.Generators.Units.Parsing.FixedUnitInstance;
using SharpMeasures.Generators.Units.Parsing.PrefixedUnitInstance;
using SharpMeasures.Generators.Units.Parsing.ScaledUnitInstance;
using SharpMeasures.Generators.Units.Parsing.SharpMeasuresUnit;
using SharpMeasures.Generators.Units.Parsing.UnitInstanceAlias;

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;

public static class UnitParser
{
    public static (IUnitProcesser Processer, IncrementalValueProvider<ImmutableArray<ForeignSymbolCollection>> ForeignSymbols) Attach(IncrementalGeneratorInitializationContext context)
    {
        var declarations = MarkedTypeDeclarationCandidateProvider.Construct().Attach<SharpMeasuresUnitAttribute>(context.SyntaxProvider);
        var filteredDeclarations = FilteredDeclarationProvider.Construct<TypeDeclarationSyntax>(DeclarationFilters).AttachAndReport(context, declarations);
        var symbols = DeclarationSymbolProvider.Construct(IntermediateResult.Construct).Attach(filteredDeclarations, context.CompilationProvider);

        var unitsAndForeignSymbols = symbols.Select(Parse);

        var units = unitsAndForeignSymbols.Select(ExtractUnit);
        var foreignSymbols = unitsAndForeignSymbols.Select(ExtractForeignSymbols).Collect();

        UnitProcesser processer = new(units);

        return (processer, foreignSymbols);
    }

    private static (Optional<RawUnitType> Definition, ForeignSymbolCollection ForeignSymbolcs) Parse(Optional<IntermediateResult> input, CancellationToken token)
    {
        if (token.IsCancellationRequested || input.HasValue is false)
        {
            return (new Optional<RawUnitType>(), ForeignSymbolCollection.Empty);
        }

        return Parse(input.Value);
    }

    private static (Optional<RawUnitType> Definition, ForeignSymbolCollection ForeignSymbolcs) Parse(IntermediateResult input)
    {
        (var unit, var unitForeignSymbols) = ParseUnit(input.TypeSymbol);

        if (unit.HasValue is false)
        {
            return (new Optional<RawUnitType>(), ForeignSymbolCollection.Empty);
        }

        (var derivations, var derivationsForeignSymbols) = ParseDerivations(input.TypeSymbol);

        var fixedUnitInstance = ParseFixedUnitInstance(input.TypeSymbol);
        var unitInstanceAliases = ParseUnitInstanceAliases(input.TypeSymbol);
        var derivedUnitInstances = ParseDerivedUnitInstances(input.TypeSymbol);
        var biasedUnitInstances = ParseBiasedUnitInstances(input.TypeSymbol);
        var prefixedUnitInstances = ParsePrefixedUnitInstances(input.TypeSymbol);
        var scaledUnitInstances = ParseScaledUnitInstances(input.TypeSymbol);

        RawUnitType unitType = new(input.TypeSymbol.AsDefinedType(), input.Declaration.Identifier.GetLocation().Minimize(), unit.Value, derivations, fixedUnitInstance.HasValue ? fixedUnitInstance.Value : null, unitInstanceAliases, derivedUnitInstances, biasedUnitInstances, prefixedUnitInstances, scaledUnitInstances);
        ForeignSymbolCollection foreignSymbols = new(unitForeignSymbols.Concat(derivationsForeignSymbols).ToList());

        return (unitType, foreignSymbols);
    }

    private static (Optional<RawSharpMeasuresUnitDefinition> Definition, IEnumerable<INamedTypeSymbol> ForeignSymbols) ParseUnit(INamedTypeSymbol typeSymbol)
    {
        if (SharpMeasuresUnitParser.Parser.ParseFirstOccurrence(typeSymbol) is not SymbolicSharpMeasuresUnitDefinition symbolicUnit)
        {
            return (new Optional<RawSharpMeasuresUnitDefinition>(), Array.Empty<INamedTypeSymbol>());
        }

        var rawUnit = RawSharpMeasuresUnitDefinition.FromSymbolic(symbolicUnit);
        var foreignSymbols = symbolicUnit.ForeignSymbols(typeSymbol.ContainingAssembly.Name);

        return (rawUnit, foreignSymbols);
    }

    private static (IEnumerable<RawDerivableUnitDefinition> Definitions, IEnumerable<INamedTypeSymbol> ForeignSymbols) ParseDerivations(INamedTypeSymbol typeSymbol)
    {
        var symbolicDerivations = DerivableUnitParser.Parser.ParseAllOccurrences(typeSymbol);

        var rawDerivations = symbolicDerivations.Select(static (symbolicDerivation) => RawDerivableUnitDefinition.FromSymbolic(symbolicDerivation));
        var foreignSymbols = symbolicDerivations.SelectMany((symbolicDerivation) => symbolicDerivation.ForeignSymbols(typeSymbol.ContainingAssembly.Name));

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

    private static IEnumerable<RawUnitInstanceAliasDefinition> ParseUnitInstanceAliases(INamedTypeSymbol typeSymbol)
    {
        return UnitInstanceAliasParser.Parser.ParseAllOccurrences(typeSymbol);
    }

    private static IEnumerable<RawDerivedUnitInstanceDefinition> ParseDerivedUnitInstances(INamedTypeSymbol typeSymbol)
    {
        return DerivedUnitInstanceParser.Parser.ParseAllOccurrences(typeSymbol);
    }

    private static IEnumerable<RawBiasedUnitInstanceDefinition> ParseBiasedUnitInstances(INamedTypeSymbol typeSymbol)
    {
        return BiasedUnitInstanceParser.Parser.ParseAllOccurrences(typeSymbol);
    }

    private static IEnumerable<RawPrefixedUnitInstanceDefinition> ParsePrefixedUnitInstances(INamedTypeSymbol typeSymbol)
    {
        return PrefixedUnitInstanceParser.Parser.ParseAllOccurrences(typeSymbol);
    }

    private static IEnumerable<RawScaledUnitInstanceDefinition> ParseScaledUnitInstances(INamedTypeSymbol typeSymbol)
    {
        return ScaledUnitInstanceParser.Parser.ParseAllOccurrences(typeSymbol);
    }

    private static IEnumerable<IDeclarationFilter> DeclarationFilters { get; } = new IDeclarationFilter[]
    {
        new PartialDeclarationFilter(UnitTypeDiagnostics.TypeNotPartial),
        new NonStaticDeclarationFilter(UnitTypeDiagnostics.TypeStatic)
    };

    private static Optional<RawUnitType> ExtractUnit((Optional<RawUnitType> Definition, ForeignSymbolCollection ForeignSymbols) input, CancellationToken _) => input.Definition;
    private static ForeignSymbolCollection ExtractForeignSymbols((Optional<RawUnitType> Definition, ForeignSymbolCollection ForeignSymbols) input, CancellationToken _) => input.ForeignSymbols;

    private readonly record struct IntermediateResult(TypeDeclarationSyntax Declaration, INamedTypeSymbol TypeSymbol)
    {
        public static DeclarationSymbolProvider.DOutputTransform<TypeDeclarationSyntax, IntermediateResult> Construct => (declaration, symbol) =>
        {
            if (declaration.HasValue is false)
            {
                return new Optional<IntermediateResult>();
            }

            return new IntermediateResult(declaration.Value, symbol);
        };
    }
}
