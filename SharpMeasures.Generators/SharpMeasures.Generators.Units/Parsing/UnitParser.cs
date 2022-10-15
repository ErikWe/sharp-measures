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
    public static (UnitParsingResult ParsingResult, IncrementalValueProvider<ImmutableArray<INamedTypeSymbol>> ForeignSymbols) Attach(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<Optional<(TypeDeclarationSyntax Declaration, string Attribute)>> declarations)
    {
        var unitDeclarations = declarations.Select(ExtractUnitDeclarations);
        var filteredDeclarations = FilteredDeclarationProvider.Construct<TypeDeclarationSyntax>(DeclarationFilters).AttachAndReport(context, unitDeclarations);
        var symbols = DeclarationSymbolProvider.Construct<TypeDeclarationSyntax, INamedTypeSymbol>(ExtractSymbol).Attach(filteredDeclarations, context.CompilationProvider);

        var unitsAndForeignSymbols = symbols.Select(Parse);

        var units = unitsAndForeignSymbols.Select(ExtractDefinition);
        var foreignSymbols = unitsAndForeignSymbols.Select(ExtractForeignSymbols).Collect().Expand();

        unitsAndForeignSymbols.Select(ExtractSymbol).Select(CreateTypeAlreadyDefinedDiagnostics).ReportDiagnostics(context);

        return (new UnitParsingResult(units), foreignSymbols);
    }

    private static Optional<(INamedTypeSymbol, RawUnitType, IEnumerable<INamedTypeSymbol>)> Parse(Optional<INamedTypeSymbol> typeSymbol, CancellationToken token)
    {
        if (token.IsCancellationRequested || typeSymbol.HasValue is false)
        {
            return new Optional<(INamedTypeSymbol, RawUnitType, IEnumerable<INamedTypeSymbol>)>();
        }

        return Parse(typeSymbol.Value);
    }

    internal static Optional<(INamedTypeSymbol Symbol, RawUnitType Definition, IEnumerable<INamedTypeSymbol> ForeignSymbols)> Parse(INamedTypeSymbol typeSymbol)
    {
        (var unit, var unitForeignSymbols) = ParseUnit(typeSymbol);

        if (unit.HasValue is false)
        {
            return new Optional<(INamedTypeSymbol, RawUnitType, IEnumerable<INamedTypeSymbol>)>();
        }

        (var derivations, var derivationsForeignSymbols) = ParseDerivations(typeSymbol);

        var fixedUnitInstance = ParseFixedUnitInstance(typeSymbol);
        var unitInstanceAliases = ParseUnitInstanceAliases(typeSymbol);
        var derivedUnitInstances = ParseDerivedUnitInstances(typeSymbol);
        var biasedUnitInstances = ParseBiasedUnitInstances(typeSymbol);
        var prefixedUnitInstances = ParsePrefixedUnitInstances(typeSymbol);
        var scaledUnitInstances = ParseScaledUnitInstances(typeSymbol);

        RawUnitType unitType = new(typeSymbol.AsDefinedType(), unit.Value, derivations, fixedUnitInstance.HasValue ? fixedUnitInstance.Value : null, unitInstanceAliases, derivedUnitInstances, biasedUnitInstances, prefixedUnitInstances, scaledUnitInstances);
        var foreignSymbols = unitForeignSymbols.Concat(derivationsForeignSymbols);

        return (typeSymbol, unitType, foreignSymbols);
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

    private static Optional<TypeDeclarationSyntax> ExtractUnitDeclarations(Optional<(TypeDeclarationSyntax Declaration, string Attribute)> data, CancellationToken _)
    {
        if (data.HasValue is false || data.Value.Attribute != typeof(UnitAttribute).FullName)
        {
            return new Optional<TypeDeclarationSyntax>();
        }

        return data.Value.Declaration;
    }

    private static Optional<Diagnostic> CreateTypeAlreadyDefinedDiagnostics(Optional<INamedTypeSymbol> typeSymbol, CancellationToken token)
    {
        if (token.IsCancellationRequested || typeSymbol.HasValue is false)
        {
            return new Optional<Diagnostic>();
        }

        if (typeSymbol.Value.GetAttributeOfType<ScalarQuantityAttribute>() is AttributeData scalarAttribute)
        {
            return UnitTypeDiagnostics.ScalarTypeAlreadyUnit(scalarAttribute, typeSymbol.Value);
        }

        if (typeSymbol.Value.GetAttributeOfType<SpecializedScalarQuantityAttribute>() is AttributeData specializedScalarAttribute)
        {
            return UnitTypeDiagnostics.SpecializedScalarTypeAlreadyUnit(specializedScalarAttribute, typeSymbol.Value);
        }

        if (typeSymbol.Value.GetAttributeOfType<VectorQuantityAttribute>() is AttributeData vectorAttribute)
        {
            return UnitTypeDiagnostics.VectorTypeAlreadyUnit(vectorAttribute, typeSymbol.Value);
        }

        if (typeSymbol.Value.GetAttributeOfType<SpecializedVectorQuantityAttribute>() is AttributeData specializedVectorAttribute)
        {
            return UnitTypeDiagnostics.SpecializedVectorTypeAlreadyUnit(specializedVectorAttribute, typeSymbol.Value);
        }

        if (typeSymbol.Value.GetAttributeOfType<VectorGroupMemberAttribute>() is AttributeData vectorGroupMemberAttribute)
        {
            return UnitTypeDiagnostics.VectorGroupMemberTypeAlreadyUnit(vectorGroupMemberAttribute, typeSymbol.Value);
        }

        return new Optional<Diagnostic>();
    }

    private static Optional<INamedTypeSymbol> ExtractSymbol(Optional<TypeDeclarationSyntax> declaration, INamedTypeSymbol typeSymbol) => new(typeSymbol);

    private static Optional<INamedTypeSymbol> ExtractSymbol<T1, T2>(Optional<(INamedTypeSymbol Symbol, T1, T2)> input, CancellationToken _) => input.HasValue ? new Optional<INamedTypeSymbol>(input.Value.Symbol) : new Optional<INamedTypeSymbol>();
    private static Optional<RawUnitType> ExtractDefinition<T1, T2>(Optional<(T1, RawUnitType Definition, T2)> input, CancellationToken _) => input.HasValue ? input.Value.Definition : new Optional<RawUnitType>();
    private static IEnumerable<INamedTypeSymbol> ExtractForeignSymbols<T1, T2>(Optional<(T1, T2, IEnumerable<INamedTypeSymbol> ForeignSymbols)> input, CancellationToken _) => input.HasValue ? input.Value.ForeignSymbols : Array.Empty<INamedTypeSymbol>();

    private static IEnumerable<IDeclarationFilter> DeclarationFilters { get; } = new IDeclarationFilter[]
    {
        new PartialDeclarationFilter(UnitTypeDiagnostics.TypeNotPartial),
        new NonStaticDeclarationFilter(UnitTypeDiagnostics.TypeStatic)
    };
}
