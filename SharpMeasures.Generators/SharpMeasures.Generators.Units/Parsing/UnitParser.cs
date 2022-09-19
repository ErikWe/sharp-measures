namespace SharpMeasures.Generators.Units.Parsing;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

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
    public static (IUnitProcesser Processer, IncrementalValueProvider<ImmutableArray<INamedTypeSymbol>> ForeignSymbols) Attach(IncrementalGeneratorInitializationContext context)
    {
        var declarations = MarkedTypeDeclarationCandidateProvider.Construct().Attach<SharpMeasuresUnitAttribute>(context.SyntaxProvider);
        var filteredDeclarations = FilteredDeclarationProvider.Construct<TypeDeclarationSyntax>(DeclarationFilters).AttachAndReport(context, declarations);
        var symbols = DeclarationSymbolProvider.Construct(IntermediateResult.Construct).Attach(filteredDeclarations, context.CompilationProvider);

        var unitsAndForeignSymbols = symbols.Select(Parse);

        var units = unitsAndForeignSymbols.Select(ExtractUnit);
        var foreignSymbols = unitsAndForeignSymbols.Select(ExtractForeignSymbols).Collect().Expand();

        UnitProcesser processer = new(units);

        return (processer, foreignSymbols);
    }

    private static (Optional<RawUnitType>, IEnumerable<INamedTypeSymbol>) Parse(Optional<IntermediateResult> input, CancellationToken token)
    {
        if (token.IsCancellationRequested || input.HasValue is false)
        {
            return (new Optional<RawUnitType>(), Array.Empty<INamedTypeSymbol>());
        }

        return Parse(input.Value);
    }

    private static (Optional<RawUnitType>, IEnumerable<INamedTypeSymbol>) Parse(IntermediateResult input)
    {
        (var unit, var unitForeignSymbols) = ParseUnit(input.TypeSymbol);

        if (unit.HasValue is false)
        {
            return (new Optional<RawUnitType>(), Array.Empty<INamedTypeSymbol>());
        }

        (var derivations, var derivationsForeignSymbols) = ParseDerivations(input.TypeSymbol);

        var fixedUnitInstance = ParseFixedUnitInstance(input.TypeSymbol);
        var unitInstanceAliases = ParseUnitInstanceAliases(input.TypeSymbol);
        var derivedUnitInstances = ParseDerivedUnitInstances(input.TypeSymbol);
        var biasedUnitInstances = ParseBiasedUnitInstances(input.TypeSymbol);
        var prefixedUnitInstances = ParsePrefixedUnitInstances(input.TypeSymbol);
        var scaledUnitInstances = ParseScaledUnitInstances(input.TypeSymbol);

        RawUnitType unitType = new(input.TypeSymbol.AsDefinedType(), input.Declaration.Identifier.GetLocation().Minimize(), unit.Value, derivations, fixedUnitInstance.HasValue ? fixedUnitInstance.Value : null, unitInstanceAliases, derivedUnitInstances, biasedUnitInstances, prefixedUnitInstances, scaledUnitInstances);
        var foreignSymbols = unitForeignSymbols.Concat(derivationsForeignSymbols);

        return (unitType, foreignSymbols);
    }

    private static (Optional<RawSharpMeasuresUnitDefinition>, IEnumerable<INamedTypeSymbol>) ParseUnit(INamedTypeSymbol typeSymbol)
    {
        if (SharpMeasuresUnitParser.Parser.ParseFirstOccurrence(typeSymbol) is not SymbolicSharpMeasuresUnitDefinition symbolicUnit)
        {
            return (new Optional<RawSharpMeasuresUnitDefinition>(), Array.Empty<INamedTypeSymbol>());
        }

        var rawUnit = RawSharpMeasuresUnitDefinition.FromSymbolic(symbolicUnit);
        var foreignSymbols = symbolicUnit.ForeignSymbols(typeSymbol.ContainingAssembly.Name, alreadyInForeignAssembly: false);

        return (rawUnit, foreignSymbols);
    }

    private static (IEnumerable<RawDerivableUnitDefinition>, IEnumerable<INamedTypeSymbol>) ParseDerivations(INamedTypeSymbol typeSymbol)
    {
        var symbolicDerivations = DerivableUnitParser.Parser.ParseAllOccurrences(typeSymbol);

        var rawDerivations = symbolicDerivations.Select(static (symbolicDerivation) => RawDerivableUnitDefinition.FromSymbolic(symbolicDerivation));
        var foreignSymbols = symbolicDerivations.SelectMany((symbolicDerivation) => symbolicDerivation.ForeignSymbols(typeSymbol.ContainingAssembly.Name, alreadyInForeignAssembly: false));

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

    private static IEnumerable<IDeclarationFilter> DeclarationFilters { get; } = new IDeclarationFilter[]
    {
        new PartialDeclarationFilter(UnitTypeDiagnostics.TypeNotPartial),
        new NonStaticDeclarationFilter(UnitTypeDiagnostics.TypeStatic)
    };

    private static Optional<RawUnitType> ExtractUnit((Optional<RawUnitType> Definition, IEnumerable<INamedTypeSymbol>) input, CancellationToken _) => input.Definition;
    private static IEnumerable<INamedTypeSymbol> ExtractForeignSymbols((Optional<RawUnitType>, IEnumerable<INamedTypeSymbol> ForeignSymbols) input, CancellationToken _) => input.ForeignSymbols;

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
