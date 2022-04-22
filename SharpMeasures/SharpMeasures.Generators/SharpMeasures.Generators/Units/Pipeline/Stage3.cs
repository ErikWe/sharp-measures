namespace SharpMeasures.Generators.Units.Pipeline;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Attributes.Parsing.Scalars;
using SharpMeasures.Generators.Attributes.Parsing.Units;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Documentation;
using SharpMeasures.Generators.Providers;
using SharpMeasures.Generators.Utility;

using System.Collections.Generic;
using System.Threading;

internal static class Stage3
{
    public readonly record struct Result(IEnumerable<DocumentationFile> Documentation, DefinedType TypeDefinition, INamedTypeSymbol TypeSymbol,
        NamedType Quantity, bool Biased);

    private readonly record struct IntermediateResult(IEnumerable<DocumentationFile> Documentation, GeneratedUnitAttributeParameters UnitParameters,
        DefinedType TypeDefinition, INamedTypeSymbol TypeSymbol, NamedType Quantity, bool Biased, Diagnostic? QuantityNotScalarQuantityDiagnostics = null,
        Diagnostic? QuantityNotUnbiasedScalarQuantityDiagnostics = null);

    public static IncrementalValuesProvider<Result> Perform(IncrementalGeneratorInitializationContext context,
        IncrementalValuesProvider<Stage2.Result> provider)
    {
        IncrementalValuesProvider<IntermediateResult> intermediateResults
            = TypeSymbolProvider.Attach(provider, context.CompilationProvider, ExtractDeclaration, ParseUnitParameters).WhereNotNull();

        IncrementalValuesProvider<Result> validResults = intermediateResults.Select(ValidResultsElseNull).WhereNotNull();
        IncrementalValuesProvider<Diagnostic> invalidResults = intermediateResults.Select(DiagnosticsElseNull).WhereNotNull();

        context.RegisterSourceOutput(invalidResults, ReportDiagnostics);
        return validResults;
    }

    private static void ReportDiagnostics(SourceProductionContext context, Diagnostic diagnostics)
        => context.ReportDiagnostic(diagnostics);

    private static Result? ValidResultsElseNull(IntermediateResult result, CancellationToken _)
        => result.QuantityNotScalarQuantityDiagnostics is null && result.QuantityNotUnbiasedScalarQuantityDiagnostics is null
            ? new Result(result.Documentation, result.TypeDefinition, result.TypeSymbol, result.Quantity, result.Biased)
            : null;

    private static Diagnostic? DiagnosticsElseNull(IntermediateResult result, CancellationToken _)
    {
        if (result.QuantityNotScalarQuantityDiagnostics is not null)
        {
            return result.QuantityNotScalarQuantityDiagnostics;
        }
        else if (result.QuantityNotUnbiasedScalarQuantityDiagnostics is not null)
        {
            return result.QuantityNotUnbiasedScalarQuantityDiagnostics;
        }
        else
        {
            return null;
        }
    }

    private static TypeDeclarationSyntax ExtractDeclaration(Stage2.Result input) => input.Declaration;
    private static IntermediateResult? ParseUnitParameters(Stage2.Result input, INamedTypeSymbol? symbol)
    {
        if (symbol is null)
        {
            return null;
        }

        if (symbol.GetAttributeOfType<GeneratedUnitAttribute>() is not AttributeData unitAttributeData)
        {
            return null;
        }

        if (GeneratedUnitAttributeParameters.Parser.Parse(unitAttributeData) is not
            GeneratedUnitAttributeParameters { Quantity: INamedTypeSymbol quantity } parameters)
        {
            return null;
        }

        if (parameters.Quantity.GetAttributeOfType<GeneratedScalarQuantityAttribute>() is not AttributeData scalarAttribute)
        {
            if (CreateTypeIsNotScalarQuantityDiagnostics(unitAttributeData) is Diagnostic diagnostic)
            {
                return new IntermediateResult(input.Documentation, parameters, DefinedType.FromSymbol(symbol), symbol, NamedType.FromSymbol(quantity), parameters.Biased,
                    QuantityNotScalarQuantityDiagnostics: diagnostic);
            }
            else
            {
                return null;
            }
        }

        if (GeneratedScalarQuantityAttributeParameters.Parser.Parse(scalarAttribute).Biased == true)
        {
            if (CreateTypeIsNotUnbiased(unitAttributeData) is Diagnostic diagnostic)
            {
                return new IntermediateResult(input.Documentation, parameters, DefinedType.FromSymbol(symbol), symbol, NamedType.FromSymbol(quantity), parameters.Biased,
                    QuantityNotUnbiasedScalarQuantityDiagnostics: diagnostic);
            }
            else
            {
                return null;
            }
        }

        return new IntermediateResult(input.Documentation, parameters, DefinedType.FromSymbol(symbol), symbol, NamedType.FromSymbol(quantity), parameters.Biased);
    }

    private static Diagnostic? CreateTypeIsNotScalarQuantityDiagnostics(AttributeData unitAttributeData)
    {
        if (GetQuantityTypeofSyntax(unitAttributeData) is TypeOfExpressionSyntax typeofSyntax)
        {
            return TypeNotScalarQuantityDiagnostics.Create(typeofSyntax);
        }
        else
        {
            return null;
        }
    }

    private static Diagnostic? CreateTypeIsNotUnbiased(AttributeData unitAttributeData)
    {
        if (GetQuantityTypeofSyntax(unitAttributeData) is TypeOfExpressionSyntax typeofSyntax)
        {
            return TypeNotUnbiasedScalarQuantityDiagnostics.Create(typeofSyntax);
        }
        else
        {
            return null;
        }
    }

    private static TypeOfExpressionSyntax? GetQuantityTypeofSyntax(AttributeData unitAttributeData)
    {
        if (unitAttributeData.ApplicationSyntaxReference?.GetSyntax() is not AttributeSyntax unitAttributeSyntax)
        {
            return null;
        }

        int attributeArgumentIndex = GeneratedUnitAttributeParameters.Parser.ParseIndices(unitAttributeData)[nameof(GeneratedUnitAttributeParameters.Quantity)];

        if (unitAttributeSyntax.ArgumentList?.Arguments[attributeArgumentIndex] is not AttributeArgumentSyntax argumentSyntax)
        {
            return null;
        }

        foreach (SyntaxNode childSyntaxNode in argumentSyntax.ChildNodes())
        {
            if (childSyntaxNode is TypeOfExpressionSyntax typeofSyntax)
            {
                return typeofSyntax;
            }
        }

        return null;
    }
}
