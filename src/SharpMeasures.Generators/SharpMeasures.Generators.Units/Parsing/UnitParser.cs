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

public sealed class UnitParser
{
    public static (UnitParsingResult ParsingResult, IncrementalValueProvider<ImmutableArray<INamedTypeSymbol>> ForeignSymbols) Attach(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<Optional<(TypeDeclarationSyntax Declaration, string Attribute)>> declarations)
    {
        UnitParser parser = new(alreadyInForeignAssembly: false);

        var unitDeclarations = declarations.Select(ExtractUnitDeclarations);
        var filteredDeclarations = FilteredDeclarationProvider.Construct<TypeDeclarationSyntax>(DeclarationFilters).AttachAndReport(context, unitDeclarations);
        var symbols = DeclarationSymbolProvider.Construct<TypeDeclarationSyntax, INamedTypeSymbol>(ExtractSymbol).Attach(filteredDeclarations, context.CompilationProvider);

        var unitsAndForeignSymbols = symbols.Select(parser.Parse);

        var units = unitsAndForeignSymbols.Select(ExtractDefinition);
        var foreignSymbols = unitsAndForeignSymbols.Select(ExtractForeignSymbols).Collect().Expand();

        unitsAndForeignSymbols.Select(CreateTypeAlreadyDefinedDiagnostics).ReportDiagnostics(context);

        return (new UnitParsingResult(units), foreignSymbols);
    }

    private bool AlreadyInForeignAssembly { get; }

    internal UnitParser(bool alreadyInForeignAssembly)
    {
        AlreadyInForeignAssembly = alreadyInForeignAssembly;
    }

    private Optional<(IEnumerable<AttributeData>, RawUnitType, IEnumerable<INamedTypeSymbol>)> Parse(Optional<INamedTypeSymbol> typeSymbol, CancellationToken token)
    {
        if (token.IsCancellationRequested || typeSymbol.HasValue is false)
        {
            return new Optional<(IEnumerable<AttributeData>, RawUnitType, IEnumerable<INamedTypeSymbol>)>();
        }

        return Parse(typeSymbol.Value);
    }

    internal Optional<(IEnumerable<AttributeData> Attributes, RawUnitType Definition, IEnumerable<INamedTypeSymbol> ForeignSymbols)> Parse(INamedTypeSymbol typeSymbol)
    {
        var attributes = typeSymbol.GetAttributes();

        (var unit, var unitForeignSymbols) = ParseUnit(typeSymbol, attributes);

        if (unit.HasValue is false)
        {
            return new Optional<(IEnumerable<AttributeData>, RawUnitType, IEnumerable<INamedTypeSymbol>)>();
        }

        (var derivations, var derivationsForeignSymbols) = ParseDerivations(typeSymbol, attributes);

        var fixedUnitInstance = ParseFixedUnitInstance(attributes);
        var unitInstanceAliases = ParseUnitInstanceAliases(attributes);
        var derivedUnitInstances = ParseDerivedUnitInstances(attributes);
        var biasedUnitInstances = ParseBiasedUnitInstances(attributes);
        var prefixedUnitInstances = ParsePrefixedUnitInstances(attributes);
        var scaledUnitInstances = ParseScaledUnitInstances(attributes);

        RawUnitType unitType = new(typeSymbol.AsDefinedType(), unit.Value, derivations, fixedUnitInstance.HasValue ? fixedUnitInstance.Value : null, unitInstanceAliases, derivedUnitInstances, biasedUnitInstances, prefixedUnitInstances, scaledUnitInstances);
        var foreignSymbols = unitForeignSymbols.Concat(derivationsForeignSymbols);

        return (attributes, unitType, foreignSymbols);
    }

    private (Optional<RawSharpMeasuresUnitDefinition>, IEnumerable<INamedTypeSymbol>) ParseUnit(INamedTypeSymbol typeSymbol, IEnumerable<AttributeData> attributes)
    {
        if (SharpMeasuresUnitParser.Parser.ParseFirstOccurrence(attributes) is not SymbolicSharpMeasuresUnitDefinition symbolicUnit)
        {
            return (new Optional<RawSharpMeasuresUnitDefinition>(), Array.Empty<INamedTypeSymbol>());
        }

        var rawUnit = RawSharpMeasuresUnitDefinition.FromSymbolic(symbolicUnit);
        var foreignSymbols = symbolicUnit.ForeignSymbols(typeSymbol.ContainingAssembly.Name, AlreadyInForeignAssembly);

        return (rawUnit, foreignSymbols);
    }

    private (IEnumerable<RawDerivableUnitDefinition>, IEnumerable<INamedTypeSymbol>) ParseDerivations(INamedTypeSymbol typeSymbol, IEnumerable<AttributeData> attributes)
    {
        var symbolicDerivations = DerivableUnitParser.Parser.ParseAllOccurrences(attributes);

        var rawDerivations = symbolicDerivations.Select(static (symbolicDerivation) => RawDerivableUnitDefinition.FromSymbolic(symbolicDerivation));
        var foreignSymbols = symbolicDerivations.SelectMany((symbolicDerivation) => symbolicDerivation.ForeignSymbols(typeSymbol.ContainingAssembly.Name, AlreadyInForeignAssembly));

        return (rawDerivations, foreignSymbols);
    }

    private static Optional<RawFixedUnitInstanceDefinition> ParseFixedUnitInstance(IEnumerable<AttributeData> attributes)
    {
        if (FixedUnitInstanceParser.Parser.ParseFirstOccurrence(attributes) is not RawFixedUnitInstanceDefinition rawFixedUnitInstance)
        {
            return new Optional<RawFixedUnitInstanceDefinition>();
        }

        return rawFixedUnitInstance;
    }

    private static IEnumerable<RawUnitInstanceAliasDefinition> ParseUnitInstanceAliases(IEnumerable<AttributeData> attributes) => UnitInstanceAliasParser.Parser.ParseAllOccurrences(attributes);
    private static IEnumerable<RawDerivedUnitInstanceDefinition> ParseDerivedUnitInstances(IEnumerable<AttributeData> attributes) => DerivedUnitInstanceParser.Parser.ParseAllOccurrences(attributes);
    private static IEnumerable<RawBiasedUnitInstanceDefinition> ParseBiasedUnitInstances(IEnumerable<AttributeData> attributes) => BiasedUnitInstanceParser.Parser.ParseAllOccurrences(attributes);
    private static IEnumerable<RawPrefixedUnitInstanceDefinition> ParsePrefixedUnitInstances(IEnumerable<AttributeData> attributes) => PrefixedUnitInstanceParser.Parser.ParseAllOccurrences(attributes);
    private static IEnumerable<RawScaledUnitInstanceDefinition> ParseScaledUnitInstances(IEnumerable<AttributeData> attributes) => ScaledUnitInstanceParser.Parser.ParseAllOccurrences(attributes);

    private static Optional<TypeDeclarationSyntax> ExtractUnitDeclarations(Optional<(TypeDeclarationSyntax Declaration, string Attribute)> data, CancellationToken _)
    {
        if (data.HasValue is false || data.Value.Attribute != typeof(UnitAttribute).FullName)
        {
            return new Optional<TypeDeclarationSyntax>();
        }

        return data.Value.Declaration;
    }

    private static Dictionary<string, Func<AttributeData, DefinedType, Diagnostic>> ConflictingSharpMeasuresTypes { get; } = new()
    {
        { typeof(ScalarQuantityAttribute).FullName, UnitTypeDiagnostics.ScalarTypeAlreadyUnit },
        { typeof(SpecializedScalarQuantityAttribute).FullName, UnitTypeDiagnostics.SpecializedScalarTypeAlreadyUnit },
        { typeof(VectorQuantityAttribute).FullName, UnitTypeDiagnostics.VectorTypeAlreadyUnit },
        { typeof(SpecializedVectorQuantityAttribute).FullName, UnitTypeDiagnostics.SpecializedVectorTypeAlreadyUnit },
        { typeof(VectorGroupMemberAttribute).FullName, UnitTypeDiagnostics.VectorGroupMemberTypeAlreadyUnit }
    };

    private static Optional<Diagnostic> CreateTypeAlreadyDefinedDiagnostics<T>(Optional<(IEnumerable<AttributeData> AttributeData, RawUnitType Definition, T)> input, CancellationToken token)
    {
        if (token.IsCancellationRequested || input.HasValue is false)
        {
            return new Optional<Diagnostic>();
        }

        foreach (var attributeData in input.Value.AttributeData)
        {
            if (attributeData.AttributeClass?.ToDisplayString() is string attributeType && ConflictingSharpMeasuresTypes.TryGetValue(attributeType, out var diagnosticsDelegate))
            {
                return diagnosticsDelegate(attributeData, input.Value.Definition.Type);
            }
        }

        return new Optional<Diagnostic>();
    }

    private static Optional<INamedTypeSymbol> ExtractSymbol<T>(T _, INamedTypeSymbol typeSymbol) => new(typeSymbol);

    private static Optional<RawUnitType> ExtractDefinition<T1, T2>(Optional<(T1, RawUnitType Definition, T2)> input, CancellationToken _) => input.HasValue ? input.Value.Definition : new Optional<RawUnitType>();
    private static IEnumerable<INamedTypeSymbol> ExtractForeignSymbols<T1, T2>(Optional<(T1, T2, IEnumerable<INamedTypeSymbol> ForeignSymbols)> input, CancellationToken _) => input.HasValue ? input.Value.ForeignSymbols : Array.Empty<INamedTypeSymbol>();

    private static IEnumerable<IDeclarationFilter> DeclarationFilters { get; } = new IDeclarationFilter[]
    {
        new PartialDeclarationFilter(UnitTypeDiagnostics.TypeNotPartial),
        new NonStaticDeclarationFilter(UnitTypeDiagnostics.TypeStatic)
    };
}
